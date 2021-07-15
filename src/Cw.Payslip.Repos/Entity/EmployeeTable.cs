using Cw.Payslip.Shared.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cw.Payslip.Repos.Entity
{
    public class EmployeeTable
    {
        [Column("FirstName", TypeName = "NVARCHAR(255)")]
        public string FirstName { get; set; }
        [Column("LastName", TypeName = "NVARCHAR(255)")]
        public string LastName { get; set; }
        [Column("Email", TypeName = "NVARCHAR(255)")]
        public string Email { get; set; }
        [Required]
        [Key]
        [Column("EmployeeId", TypeName = "NVARCHAR(255)")]
        public string EmployeeId { get; set; }
        [Column("Status", TypeName = "NVARCHAR(10)")]
        public DataStatus Status { get; set; }
    }
}
