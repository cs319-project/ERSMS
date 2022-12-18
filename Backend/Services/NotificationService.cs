using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.DTOs;
using Backend.Entities;
using Backend.Interfaces;
using Backend.Utilities;
using Backend.Utilities.Enum;

namespace Backend.Services
{
    public class NotificationService : INotificationService
    {

        private readonly INotificationRepository _notificationRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        // Constructor
        public NotificationService(INotificationRepository notificationRepository,
                                        IMapper mapper, IUserService userService)
        {
            _notificationRepository = notificationRepository;
            _userService = userService;
            _mapper = mapper;
        }

        // Methods
        // Notify only the sender CTEForm -> Exchange Coordinator, PreApprovalForm -> Student, EquivalenceRequest -> Student
        public async Task<bool> CreateNewApprovalNotification(Form formObject, FormType formType,
                                                                 bool isApproved, string approverName)
        {
            string formId = "";
            if (formType == FormType.CTEForm)
            {
                formId = (formObject as CTEForm).Id.ToString();
            }
            else if (formType == FormType.PreApprovalForm)
            {
                formId = (formObject as PreApprovalForm).Id.ToString();
            }
            else if (formType == FormType.EquivalenceRequest)
            {
                formId = (formObject as EquivalenceRequest).Id.ToString();
            }
            else
            {
                return false;
            }

            string finalContent = ContentCreationApproval(formId, formType, approverName, isApproved);

            if (formType == FormType.CTEForm)
            {
                var form = formObject as CTEForm;
                Student student = await _userService.GetStudent(form.IDNumber);
                Department departmentOfStudent = student.Major.DepartmentName;
                var exchangeCoordinators = await _userService.GetExchangeCoordinatorsByDepartmentAsync(departmentOfStudent);

                foreach (var exchangeCoordinator in exchangeCoordinators)
                {

                    // new object must be created because of primary key uniqueness
                    var notification = new Notification
                    {
                        content = finalContent,
                        read = false
                    };
                    notification.userId = exchangeCoordinator.Id;
                    await _notificationRepository.AddNotification(notification);
                }
            }
            else if (formType == FormType.PreApprovalForm)
            {
                var form = formObject as PreApprovalForm;
                Student student = await _userService.GetStudent(form.IDNumber);

                var notification = new Notification
                {
                    content = finalContent,
                    read = false
                };
                notification.userId = student.Id;
                await _notificationRepository.AddNotification(notification);
            }
            else if (formType == FormType.EquivalenceRequest)
            {
                var form = formObject as EquivalenceRequest;
                Student student = await _userService.GetStudent(form.StudentId);

                var notification = new Notification
                {
                    content = finalContent,
                    read = false
                };
                notification.userId = student.Id;
                await _notificationRepository.AddNotification(notification);
            }

            return true;
        }

        public async Task<bool> CreateNewFormNotification(Form formObject, FormType formType)
        {
            string firstName = "";
            string lastName = "";
            string studentIdNumber = "";

            if (formType == FormType.CTEForm)
            {
                var form = formObject as CTEForm;
                firstName = form.FirstName;
                lastName = form.LastName;
                studentIdNumber = form.IDNumber;
            }
            else if (formType == FormType.PreApprovalForm)
            {
                var form = formObject as PreApprovalForm;
                firstName = form.FirstName;
                lastName = form.LastName;
                studentIdNumber = form.IDNumber;
            }
            else if (formType == FormType.EquivalenceRequest)
            {
                var form = formObject as EquivalenceRequest;
                var temp = await _userService.GetStudent(form.StudentId);
                firstName = temp.FirstName;
                lastName = temp.LastName;
                studentIdNumber = form.StudentId;
            }
            else
                return false;

            var student = await _userService.GetStudent(studentIdNumber);
            if (student == null)
                return false;
            Department departmentOfStudent = student.Major.DepartmentName;
            string finalContent = ContentCreationNewForm(formType, firstName, lastName, studentIdNumber);
            // Create Notification for Exchange Coordinators
            if (formType != FormType.CTEForm)
            {
                var exchangeCoordinators = await _userService.GetExchangeCoordinatorsByDepartmentAsync(departmentOfStudent);
                foreach (var exchangeCoordinator in exchangeCoordinators)
                {
                    var notification = new Notification
                    {
                        content = finalContent,
                        read = false
                    };

                    notification.userId = exchangeCoordinator.Id;
                    await _notificationRepository.AddNotification(notification);
                }
            }

            // Create Notification for other aux. users
            if (formType == FormType.CTEForm)
            {
                var deanDepartmentChairs = await _userService.GetDeanDepartmentChairsByDepartmentAsync(departmentOfStudent);
                var studentTemp = await _userService.GetStudent(studentIdNumber);
                Notification notificationTemp = new Notification
                {
                    content = finalContent,
                    read = false,
                    userId = studentTemp.Id
                };
                await _notificationRepository.AddNotification(notificationTemp);
                foreach (var deanDepartmentChair in deanDepartmentChairs)
                {
                    var notification = new Notification
                    {
                        content = finalContent,
                        read = false
                    };

                    notification.userId = deanDepartmentChair.Id;
                    await _notificationRepository.AddNotification(notification);
                }

            }
            else if (formType == FormType.EquivalenceRequest)
            {
                var form = formObject as EquivalenceRequest;
                var instructors = await _userService.GetCourseCoordinatorsInstructorsByCourseCodeAsync(form.ExemptedCourse.CourseCode);
                foreach (var instructor in instructors)
                {
                    var notification = new Notification
                    {
                        content = finalContent,
                        read = false
                    };

                    notification.userId = instructor.Id;
                    await _notificationRepository.AddNotification(notification);
                }
            }

            return true;
        }

        public async Task<List<NotificationDto>> GetNotifications(string userId, bool unread = true)
        {
            var notifications = await _notificationRepository.GetNotifications();
            var user = (DomainUserDto)await _userService.GetUser(userId);
            List<NotificationDto> listToReturn = new List<NotificationDto>();

            foreach (Notification notification in notifications)
            {
                if (user != null && notification.userId == user.Id && !notification.read == unread)
                    listToReturn.Add(_mapper.Map<NotificationDto>(notification));
            }

            return listToReturn;
        }

        public async Task<bool> MarkAllAsRead(string userId)
        {
            var notifications = await _notificationRepository.GetNotifications();
            var user = (DomainUserDto)await _userService.GetUser(userId);
            bool flag = false;

            foreach (Notification notification in notifications)
            {
                if (user != null && notification.userId == user.Id)
                {
                    notification.read = true;
                    await _notificationRepository.UpdateNotification(notification);
                    flag = true;
                }
            }

            return flag;
        }

        public async Task<bool> MarkAsRead(Guid id)
        {
            var notification = await _notificationRepository.GetNotification(id);
            if (notification == null)
                return false;
            notification.read = true;
            return await _notificationRepository.UpdateNotification(notification);
        }

        private string ContentCreationApproval(string formId, FormType formType,
                                        string approverName, bool isApproved)
        {
            return EnumStringify.FormTypeStringify(formType) + " with id " + formId + " has been "
                    + (isApproved ? "approved" : "rejected") + " by " + approverName;
        }

        private string ContentCreationNewForm(FormType formType, string firstName, string lastName, string studentIdNumber)
        {
            return "New " + EnumStringify.FormTypeStringify(formType) + " has been submitted "
                            + (formType == FormType.CTEForm ? "for " : "by ")
                            + firstName + " "
                            + lastName + " ("
                            + studentIdNumber + ")";
        }
    }
}
