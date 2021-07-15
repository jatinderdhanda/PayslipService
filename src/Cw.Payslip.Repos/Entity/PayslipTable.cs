using Cw.Payslip.Shared.DTO;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cw.Payslip.Repos.Entity
{
    public class PayslipTable
    {
        [Column("PaymentStartDate", TypeName = "DATE")]
        public DateTime PaymentStartDate { get; set; }
        [Column("PaymentEndDate", TypeName = "DATE")]
        public DateTime PaymentEndDate { get; set; }
        [Column("BaseSalary", TypeName = "NVARCHAR(255)")]
        public string BaseSalary { get; set; }
        [Required]
        [Key]
        [Column("PayslipId", TypeName = "NVARCHAR(255)")]
        public string PayslipId { get; set; }
        [Column("Status", TypeName = "NVARCHAR(10)")]
        public DataStatus Status { get; set; }
        [Column("Processed", TypeName = "BIT")]
        public DateTime Processed { get; set; }

        public static PayslipDto CreateDto(PayslipTable result)
        {
            return new PayslipDto
            {
                PaymentStartDate = result.PaymentStartDate,
                PaymentEndDate = result.PaymentEndDate,
                PayslipId = result.PayslipId,
                BaseSalary = result.BaseSalary,
                Status = result.Status,
                Processed = result.Processed
            };
        }
    }
}
