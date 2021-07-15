using Cw.Payslip.Repos.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cw.Payslip.Repos.Context
{
    public interface IPayslipContext: System.IDisposable
    {
        Task<int> SaveChangesAsync();
        DbSet<EmployeeTable> EmployeeDbset { get; set; }
        DbSet<PayslipTable> PayslipDbset { get; set; }
        List<PayslipTable> GetAllPayslips();
    }
}
