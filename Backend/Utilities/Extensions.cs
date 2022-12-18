using System.Reflection;
using System.Runtime.Serialization;

namespace Backend.Utilities
{
    /// <summary>An utility class to add static helper methods</summary>
    public static class Extensions
    {
        /// <summary>Gets the value of an enum member.</summary>
        /// <typeparam name="T">The type of the enum.</typeparam>
        /// <param name="value">The enum member.</param>
        /// <returns>The value of the enum member.</returns>
        [Obsolete("Use Utilities.EnumStringify instead")]
        public static String GetEnumMemberValue<T>(T value)
            where T : struct, IConvertible
        {
            return typeof(T)
                .GetTypeInfo()
                .DeclaredMembers
                .SingleOrDefault(x => x.Name == value.ToString())
                ?.GetCustomAttribute<EnumMemberAttribute>(false)
                ?.Value;
        }
    }
}
