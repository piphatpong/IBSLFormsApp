using System;
using System.Collections.Generic;
using System.Text;

namespace IBSLFormsApp.Helpers
{
    public class BaseHelper
    {
        public static readonly UTF8Encoding ByteConverter = new UTF8Encoding();
        public static byte[] ConvertStringToByte(string text)
        {
            return ByteConverter.GetBytes(text);
        }
        public static string ConvertByteToString(byte[] bytes)
        {
            return ByteConverter.GetString(bytes);
        }
    }
}
