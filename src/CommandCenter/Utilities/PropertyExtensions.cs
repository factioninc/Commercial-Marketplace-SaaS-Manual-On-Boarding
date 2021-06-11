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
        public static string GetDisplayName(this object instance, string property)
        {
            Type t = instance.GetType();
            MemberInfo propertyInfo = t.GetProperty(property);
            var displayAttribute = propertyInfo?.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

            return displayAttribute != null ? displayAttribute.Name : propertyInfo.Name;
        }

        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.GetName() ?? enumValue.ToString();
        }
    }
}
