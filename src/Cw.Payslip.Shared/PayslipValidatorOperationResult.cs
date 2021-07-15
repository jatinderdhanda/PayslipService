namespace Cw.Payslip.Shared
{
    public  class PayslipValidatorOperationResult: ApiOperationResult
    {
        public PayslipException ExceptionType { get; set; }

        public static PayslipValidatorOperationResult <T> CreateError<T>(T obj)
        {
            return new PayslipValidatorOperationResult<T> { IsSuccess = false, Result = obj, ExceptionType = PayslipException.DataNotFound };
        }

        public static PayslipValidatorOperationResult<T> CreateSuccess<T>(T obj)
        {
            return new PayslipValidatorOperationResult<T> { IsSuccess = true, Result = obj, ExceptionType = PayslipException.None };
        }

        public static PayslipValidatorOperationResult CreateSuccess()
        {
            return new PayslipValidatorOperationResult { IsSuccess = true,ExceptionType = PayslipException.None };
        }

    }


    public class PayslipValidatorOperationResult<T> : PayslipValidatorOperationResult
    {
        public T Result { get; set; }
    }

    public enum PayslipException {
        None,
        UnexpectedExecption,
        IllegalOperation,
        DataNotFound,
        UnexpectedFormatReceived
    }
}
