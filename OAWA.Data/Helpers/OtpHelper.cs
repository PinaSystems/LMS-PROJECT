using System;

namespace Jupiter.API.Helpers
{
    public class OtpHelper
    {
        public static string GenerateOtp()
        {
            return new Random().Next(111111, 999999).ToString();
        }
    }
}