using Cw.Payslip.Shared.DTO;
using Cw.Payslip.Shared;

namespace Cw.Payslip.Repos
{
    public interface IPayslipRepository
    {
        PayslipValidatorOperationResult<PayslipDto> GetPayslipFor(string payslipId);
        PayslipValidatorOperationResult UploadEmployeeDetail(EmployeeDto employeeDto);
    }
}
