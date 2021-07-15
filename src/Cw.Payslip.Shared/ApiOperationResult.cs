using System;

namespace Cw.Payslip.Shared
{
    public class ApiOperationResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorText { get; set; }
        public string ErrorDetailsText { get; set; }

        public static T CreateSuccess<T>() where T : ApiOperationResult, new()
        {
            return new T() { IsSuccess = true };
        }

        public static T CreateError<T>(string errorText) where T : ApiOperationResult, new()
        {
            return new T { IsSuccess = false, ErrorText = errorText };
        }

        public static T CreateError<T>(string errorText, string errorDetailsText) where T : ApiOperationResult, new()
        {
            return new T { IsSuccess = false, ErrorText = errorText, ErrorDetailsText = errorDetailsText };
        }

        public static T CreateError<T>(string errorText, Exception e) where T : ApiOperationResult, new()
        {
            return new T { IsSuccess = false, ErrorText = errorText, ErrorDetailsText = e.ToString() };
        }
    }

    public class ApiOperationResult<T>
    {
        public T Data { get; set; }
    }
}
