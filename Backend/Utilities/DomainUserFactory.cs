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
            Actors _enum = (Actors)System.Enum.Parse(typeof(Actors), actorType);
            var enumVal = Extensions.GetEnumMemberValue(_enum);

            DomainUser domainUser = null;

            switch (enumVal)
            {
                case "Student":
                    domainUser = new Student();
                    break;
                case "Exchange Coordinator":
                    domainUser = new ExchangeCoordinator();
                    break;
                default:
                    throw new System.ArgumentException("Invalid actor type");
            }

            initializer(domainUser);
            return domainUser;
        }
    }
}
