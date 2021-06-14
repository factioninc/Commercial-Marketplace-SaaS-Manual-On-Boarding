// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace CommandCenter.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension classes used to get the Display Name attribute of model properties, and also enum values.
    /// </summary>
    public static class PropertyExtensions
    {
        /// <summary>
        /// Extension method to return the Name component of a Display attribute, if defined.
        /// </summary>
        /// <param name="instance">The instance of the object to be interrogated to gather the Display attribute.</param>
        /// <param name="property">The name of the instance property to interrogate.</param>
        /// <returns>The Name property of the Display attribute, if defined.  Otherwise, the name of the property.</returns>
        public static string GetDisplayName(this object instance, string property)
        {
            Type t = instance.GetType();
            MemberInfo propertyInfo = t.GetProperty(property);
            var displayAttribute = propertyInfo?.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;

            return displayAttribute != null ? displayAttribute.Name : propertyInfo.Name;
        }

        /// <summary>
        /// Gets the string to use as a Display Name for a given enum value.
        /// </summary>
        /// <param name="enumValue">Teh value to be interrogated for a Display Name.</param>
        /// <returns>The Name property of a Display attribute for the presented enum, if defined.  Otherwise, the value itself as a string.</returns>
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
