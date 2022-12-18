using Backend.DTOs;
using Backend.Entities;
using Backend.Utilities.Enum;

namespace Backend.Utilities
{

    /// <summary> This class an example of factory design pattern which creates a domain user based on the actor type. </summary>
    public class DomainUserFactory
    {
        /// <summary>Creates a new user.</summary>
        /// <param name="register">Register information.</param>
        /// <returns>The new user.</returns>
        public static DomainUser Create(RegisterDto register) =>
            Create(register.ActorType, u =>
            {
                u.FirstName = register.FirstName;
                u.LastName = register.LastName;
            }, register.UserName);

        /// <summary>Creates a new domain user.</summary>
        /// <param name="actorType">The type of actor.</param>
        /// <param name="initializer">The initializer.</param>
        /// <param name="userName">The username.</param>
        /// <returns>The domain user.</returns>
        public static DomainUser Create(string actorType, Action<DomainUser> initializer, string userName)
        {
            DomainUser domainUser = null;
            var actor = EnumStringify.ActorEnumarator(actorType);

            switch (actor)
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
                case Actors.OISEP:
                    domainUser = new OISEP();
                    break;
                default:
                    throw new System.ArgumentException("Invalid actor type");
            }

            domainUser.ActorType = actor;
            initializer(domainUser);
            return domainUser;
        }
    }
}
