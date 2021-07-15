using System;

namespace Cw.Payslip.Shared.DTO
{
    public class PayslipDto
    {
        public DateTime PaymentStartDate { get; set; }
        public DateTime PaymentEndDate { get; set; }
        public string BaseSalary { get; set; }
        public string PayslipId { get; set; }
        public string EmployeeId { get; set; }
        public DataStatus Status { get; set; }
        public DateTime Processed { get; set; }
    }
}
