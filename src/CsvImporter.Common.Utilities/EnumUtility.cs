using System;

namespace CsvImporter.Common.Utilities
{
    public class EnumUtility
    {
        public static string GetValue<T>(T enumValue)
        {
            return Enum.GetName(typeof(T), enumValue);
        }
    }
}
