using Cw.Payslip.Repos;
using Cw.Payslip.Repos.Context;
using Cw.Payslip.Repos.Entity;
using Cw.Payslip.Shared;
using Cw.Payslip.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace Cw.PayslipService.Tests
{
    [TestClass]
    public class PayslipServiceTests
    {
        private readonly IPayslipContext _payslipContext;
        private PayslipRepository _payslipRepository;
        //private DbSet<PayslipTable> PayslipDbset { get; set; }
        //public List<PayslipTable> getProducts() => PayslipDbset.Local.ToList<PayslipTable>();

        public PayslipServiceTests()
        {
            _payslipContext = Substitute.For<IPayslipContext>();
            _payslipRepository = new PayslipRepository(_payslipContext);
            LoadDefaultData();
        }
        private void LoadDefaultData()
        {
            var PaySlipEntity = new List<PayslipTable>
            {
                new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-16), PaymentEndDate = System.DateTime.Now, BaseSalary = "120000", PayslipId = "567098", Status = Payslip.Shared.DTO.DataStatus.Valid },
                new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-30), PaymentEndDate = System.DateTime.Now.AddDays(-15), BaseSalary = "120000", PayslipId = "567090", Status = Payslip.Shared.DTO.DataStatus.Valid },
                new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-45), PaymentEndDate = System.DateTime.Now.AddDays(-29), BaseSalary = "151000", PayslipId = "567998", Status = Payslip.Shared.DTO.DataStatus.Valid },
                new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-60), PaymentEndDate = System.DateTime.Now.AddDays(-44), BaseSalary = "100000", PayslipId = "567089", Status = Payslip.Shared.DTO.DataStatus.Valid }
            };
            _payslipContext.GetAllPayslips().Returns(PaySlipEntity);
            //var mockDbForPayslip = Substitute.For<DbSet<PayslipTable>, IQueryable<PayslipTable>>();
            //var mockLocalDbPayslip = Substitute.For<Microsoft.EntityFrameworkCore.ChangeTracking.LocalView<PayslipTable>>();
            //mockLocalDbPayslip.Add(new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-16), PaymentEndDate = System.DateTime.Now, BaseSalary = "120000", PayslipId = "567098", Status = DataStatus.Valid });
            //_payslipContext.PayslipDbset.Returns(mockDbForPayslip);
            //_payslipContext.PayslipDbset.Local.Returns(mockLocalDbPayslip);

            var mockDbForEmployee = Substitute.For<DbSet<EmployeeTable>>();
            _payslipContext.EmployeeDbset.Returns(mockDbForEmployee);
        }
        [TestMethod]    
        public void SearchingForPayslipShouldReturnValidRecord()
        {
            var results = _payslipRepository.GetPayslipFor("567098");
            Assert.IsTrue(results.IsSuccess);
            Assert.AreEqual("567098", results.Result.PayslipId);
        }

        [TestMethod]
        public void SearchingForPayslipShouldFailIfInValidRecord()
        {
            var results = _payslipRepository.GetPayslipFor("567000");
            Assert.IsFalse(results.IsSuccess);
            Assert.AreEqual(results.ExceptionType, PayslipException.DataNotFound);
        }

        [TestMethod]
        public void UploadingEmployeeDetailShouldReturnSuccess()
        {
            var employeeDto = new EmployeeDto
            {
                Email = "jats@gmail.com",
                EmployeeId = "0076564",
                FirstName = "Jats",
                LastName = "Dhanda"
            };
            var results = _payslipRepository.UploadEmployeeDetail(employeeDto);
            Assert.IsTrue(results.IsSuccess);
        }
    }
}
