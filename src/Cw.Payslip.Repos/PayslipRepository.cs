using Cw.Payslip.Repos.Context;
using Cw.Payslip.Shared.DTO;
using Cw.Payslip.Repos.Entity;
using Cw.Payslip.Shared;
using System.Collections.Generic;
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
        private List<PayslipDto> LoadedExistingPayslips()
        {
            return _payslipContext.GetAllPayslips().Select(x => PayslipTable.CreateDto(x)).ToList();
        }

        public PayslipValidatorOperationResult<PayslipDto> GetPayslipFor(string payslipId)
        {
            var payslipArray = LoadedExistingPayslips();
            foreach (var payslip in payslipArray)
            {
                if (payslip.PayslipId == payslipId && payslip.Status == DataStatus.Valid)
                    return PayslipValidatorOperationResult.CreateSuccess(payslip);
            }
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
