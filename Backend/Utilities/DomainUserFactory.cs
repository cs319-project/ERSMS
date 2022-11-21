using Backend.Entities;
using Backend.DTOs;
using Backend.Utilities.Enum;

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
            Actors _enum = (Actors)System.Enum.Parse(typeof(Actors), actorType);
            DomainUser domainUser = null;

            switch (_enum)
            {
                case Actors.Student:
                    domainUser = new Student();
                    break;
                default:
                    throw new System.ArgumentException("Invalid actor type");
            }

            initializer(domainUser);
            return domainUser;
        }
    }
}
