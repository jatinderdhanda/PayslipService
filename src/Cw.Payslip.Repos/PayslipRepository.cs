using Cw.Payslip.Repos.Context;
using Cw.Payslip.Shared.DTO;
using Cw.Payslip.Repos.Entity;
using Cw.Payslip.Shared;
using System.Linq;

namespace Cw.Payslip.Repos
{
    public class PayslipRepository: IPayslipRepository
    {
        private IPayslipContext _payslipContext;
        public PayslipRepository(IPayslipContext payslipContext)
        {
            _payslipContext = payslipContext;
        }
        private PayslipDto FindMatchingPayslip(string payslipId)
        {
            return _payslipContext.GetAllPayslips().Where(x=> x.PayslipId == payslipId).Select(x => PayslipTable.CreateDto(x)).FirstOrDefault();
        }

        public PayslipValidatorOperationResult<PayslipDto> GetPayslipFor(string payslipId)
        {
            var matchingPayslip = FindMatchingPayslip(payslipId);
            if (matchingPayslip != null)return PayslipValidatorOperationResult.CreateSuccess(matchingPayslip);
            return PayslipValidatorOperationResult.CreateError(new PayslipDto());

        }
        public PayslipValidatorOperationResult UploadEmployeeDetail(EmployeeDto employeeDto) {
            var newEmployee = new EmployeeTable
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Status = DataStatus.Valid,
                EmployeeId = employeeDto.EmployeeId
            };
            _payslipContext.EmployeeDbset.Add(newEmployee);
            _payslipContext.SaveChangesAsync();

            return PayslipValidatorOperationResult.CreateSuccess();


        }
    }
}
