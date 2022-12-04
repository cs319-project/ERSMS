using Backend.Entities;
using Backend.DTOs;
using Backend.Utilities.Enum;
using Backend.Utilities;

namespace Backend.Utilities
{
    public class DomainUserFactory
    {
        public static DomainUser Create(RegisterDto register) =>
            Create(register.ActorType, u =>
            {
                u.FirstName = register.FirstName;
                u.LastName = register.LastName;
            }, register.UserName);

        public static DomainUser Create(string actorType, Action<DomainUser> initializer, string id)
        {
            DomainUser domainUser = null;

            switch (EnumStringify.ActorEnumarator(actorType))
            {
                case Actors.Student:
                    domainUser = new Student();
                    break;
                case Actors.ExchangeCoordinator:
                    domainUser = new ExchangeCoordinator();
                    break;
                case Actors.Admin:
                    domainUser = new Admin();
                    break;
                case Actors.DeanDepartmentChair:
                    domainUser = new DeanDepartmentChair();
                    break;
                case Actors.CourseCoordinatorInstructor:
                    domainUser = new CourseCoordinatorInstructor();
                    break;
                default:
                    throw new System.ArgumentException("Invalid actor type");
            }

            initializer(domainUser);
            return domainUser;
        }
    }
}
