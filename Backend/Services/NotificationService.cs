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
        // Notify only the sender CTEForm -> Exchange Coordinator, PreApprovalForm -> Student, EquivalanceRequest -> Student
        public async Task<bool> CreateNewApprovalNotification(object formObject, FormType formType)
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
            else if (formType == FormType.EquivalanceRequest)
            {
                formId = (formObject as EquivalanceRequest).Id.ToString();
            }

            if (formType == FormType.CTEForm)
            {
                var form = formObject as CTEForm;
                Student student = await _userService.GetStudent(form.IDNumber);
                Department departmentOfStudent = student.Major.DepartmentName;
                var exchangeCoordinators = await _userService.GetExchangeCoordinatorsByDepartmentAsync(departmentOfStudent);

                foreach (var exchangeCoordinator in exchangeCoordinators)
                {

                    var notification = new Notification
                    {
                        content = "Approval status of a "
                                    + EnumStringify.FormTypeStringify(formType)
                                    + " with id " + formId + " has been changed",
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
                    content = "Approval status of a "
                                + EnumStringify.FormTypeStringify(formType)
                                + " with id " + formId + " has been changed",
                    read = false
                };
                notification.userId = student.Id;
                await _notificationRepository.AddNotification(notification);
            }
            else if (formType == FormType.EquivalanceRequest)
            {
                var form = formObject as EquivalanceRequest;
                Student student = await _userService.GetStudent(form.StudentId);

                var notification = new Notification
                {
                    content = "Approval status of a "
                                + EnumStringify.FormTypeStringify(formType)
                                + " with id " + formId + " has been changed",
                    read = false
                };
                notification.userId = student.Id;
                await _notificationRepository.AddNotification(notification);
            }

            return true;
        }

        public async Task<bool> CreateNewFormNotification(object formObject, FormType formType)
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
            else if (formType == FormType.EquivalanceRequest)
            {
                var form = formObject as EquivalanceRequest;
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

            // Create Notification for Exchange Coordinators
            var exchangeCoordinators = await _userService.GetExchangeCoordinatorsByDepartmentAsync(departmentOfStudent);
            foreach (var exchangeCoordinator in exchangeCoordinators)
            {
                var notification = new Notification
                {
                    content = "New " + EnumStringify.FormTypeStringify(formType) + " has been submitted by "
                            + firstName + " "
                            + lastName + " ("
                            + studentIdNumber + ")",
                    read = false
                };

                notification.userId = exchangeCoordinator.Id;
                await _notificationRepository.AddNotification(notification);
            }

            // Create Notification for other aux. users
            if (formType == FormType.CTEForm)
            {
                var deanDepartmentChairs = await _userService.GetDeanDepartmentChairsByDepartmentAsync(departmentOfStudent);

                foreach (var deanDepartmentChair in deanDepartmentChairs)
                {
                    var notification = new Notification
                    {
                        content = "New " + EnumStringify.FormTypeStringify(formType) + " has been submitted by "
                            + firstName + " "
                            + lastName + " ("
                            + studentIdNumber + ")",
                        read = false
                    };

                    notification.userId = deanDepartmentChair.Id;
                    await _notificationRepository.AddNotification(notification);
                }

            }
            else if (formType == FormType.EquivalanceRequest)
            {
                var form = formObject as EquivalanceRequest;
                var instructors = await _userService.GetCourseCoordinatorsInstructorsByCourseCodeAsync(form.ExemptedCourse.CourseCode);
                foreach (var instructor in instructors)
                {
                    var notification = new Notification
                    {
                        content = "New " + EnumStringify.FormTypeStringify(formType) + " has been submitted by "
                            + firstName + " "
                            + lastName + " ("
                            + studentIdNumber + ")",
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
    }
}
