using Cw.Payslip.Repos.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw.Payslip.Repos.Context
{
    public class PayslipContext: DbContext, IPayslipContext
    {
        public PayslipContext(DbContextOptions<PayslipContext> options) : base(options)
        {
            LoadDefaultData();
        }

        public virtual DbSet<EmployeeTable> EmployeeDbset { get; set; }
        public virtual DbSet<PayslipTable> PayslipDbset { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public List<PayslipTable> GetAllPayslips() => PayslipDbset.Local.ToList();

        private void LoadDefaultData() {
            PayslipDbset.Add(new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-16), PaymentEndDate = System.DateTime.Now, BaseSalary = "120000", PayslipId = "567098", EmployeeId = "0098765", Status = Shared.DTO.DataStatus.Valid });
            PayslipDbset.Add(new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-30), PaymentEndDate = System.DateTime.Now.AddDays(-15), BaseSalary = "120000", PayslipId = "567090", EmployeeId = "0098766", Status = Shared.DTO.DataStatus.Valid });
            PayslipDbset.Add(new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-45), PaymentEndDate = System.DateTime.Now.AddDays(-29), BaseSalary = "151000", PayslipId = "567998", EmployeeId = "0098767", Status = Shared.DTO.DataStatus.Valid });
            PayslipDbset.Add(new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-60), PaymentEndDate = System.DateTime.Now.AddDays(-44), BaseSalary = "100000", PayslipId = "567089", EmployeeId = "0098768", Status = Shared.DTO.DataStatus.Valid });
        }
    }
}
