using System;
using System.Linq;
using System.Runtime.Serialization;

namespace BloodSearch.Core.Extensions {

    public static class EnumExtensions {

        public static string ToEnumString<T>(this T type) {
            var enumType = typeof(T);
            var name = Enum.GetName(enumType, type);
            var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
            return enumMemberAttribute.Value;
        }

        public static T ToEnum<T>(this string value) {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}