
namespace TransBillRepository.Applib
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    public static class EnumExtenstions
    {
        public static string Description(this Enum value)
        {
            return value.GetType()
                .GetRuntimeField(value.ToString())
                .GetCustomAttributes<DescriptionAttribute>()
                .FirstOrDefault()?.Description ?? string.Empty;
        }
    }
}
