using System;

namespace Cw.PayslipService.Models
{
    public class Payslip
    {
        public string PayslipId { get; set; }
        public string PaymentStartDate { get; set; }
        public string PaymentEndDate { get; set; }
        public string BaseSalary { get; set; }
    }
}
