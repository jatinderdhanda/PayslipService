using Cw.Payslip.Repos;
using Cw.Payslip.Repos.Context;
using Cw.Payslip.Repos.Entity;
using Cw.Payslip.Shared;
using Cw.Payslip.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Collections.Generic;

namespace Cw.PayslipService.Tests
{
    [TestClass]
    public class PayslipRepositoryTests
    {
        private readonly IPayslipContext _payslipContext;
        private PayslipRepository _payslipRepository;

        public PayslipRepositoryTests()
        {
            _payslipContext = Substitute.For<IPayslipContext>();
            _payslipRepository = new PayslipRepository(_payslipContext);
            LoadDefaultData();
        }
        private void LoadDefaultData()
        {
            var PaySlipEntity = new List<PayslipTable>
            {
                new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-16), PaymentEndDate = System.DateTime.Now, BaseSalary = "120000", PayslipId = "567098",  EmployeeId = "0098765", Status = DataStatus.Valid },
                new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-30), PaymentEndDate = System.DateTime.Now.AddDays(-15), BaseSalary = "120000", PayslipId = "567090",  EmployeeId = "0098766", Status = DataStatus.Valid },
                new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-45), PaymentEndDate = System.DateTime.Now.AddDays(-29), BaseSalary = "151000", PayslipId = "567998", EmployeeId = "0098767", Status = DataStatus.Valid },
                new PayslipTable { PaymentStartDate = System.DateTime.Now.AddDays(-60), PaymentEndDate = System.DateTime.Now.AddDays(-44), BaseSalary = "100000", PayslipId = "567089", EmployeeId = "0098768", Status = DataStatus.Valid }
            };
            _payslipContext.GetAllPayslips().Returns(PaySlipEntity);
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
