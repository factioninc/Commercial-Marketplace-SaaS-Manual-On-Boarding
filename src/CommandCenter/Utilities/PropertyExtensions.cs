namespace CommandCenter.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    public static class PropertyExtensions
    {
        public static string GetDisplayName<T>(this string property)
        {
            MemberInfo propertyInfo = typeof(T).GetProperty(property);
            var displayAttribute = propertyInfo?.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

            return displayAttribute != null ? displayAttribute.Name : string.Empty;
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName();
        }
    }
}
