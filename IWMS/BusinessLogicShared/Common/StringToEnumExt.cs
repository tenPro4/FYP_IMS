using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicShared.Common
{
    public static class StringToEnumExt
    {
        public static T ToEnum<T>(this string value, T defaultValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            T result = (T)Enum.Parse(typeof(T), value, true);
            return result != null ? result: defaultValue;

            //Enum.TryParse("Active", out StatusEnum myStatus);
        }
    }
}
