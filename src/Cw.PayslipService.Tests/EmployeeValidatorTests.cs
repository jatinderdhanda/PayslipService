using Cw.PayslipService.Models;
using Cw.PayslipService.Validator;
using FluentValidation.TestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cw.PayslipService.Tests
{
    [TestClass]
   public class EmployeeValidatorTests
    {
        private readonly EmployeeValidator _validator;
        public EmployeeValidatorTests()
        {
            _validator = new EmployeeValidator();
        }

        [TestMethod]
        public void PassingValidDataForEmployeeDoesNotGivesValidationError()
        {
            var employee = GetEmployeeData();
            var result = _validator.TestValidate(employee);
            result.ShouldNotHaveValidationErrorFor(employee.EmployeeId);
            result.ShouldNotHaveValidationErrorFor(employee.Email);
            result.ShouldNotHaveValidationErrorFor(employee.FirstName);
            result.ShouldNotHaveValidationErrorFor(employee.LastName);
        }

        [TestMethod]
        public void PassingInvalidDataForEmailGivesValidationError()
        {
            var employee = GetEmployeeData();
            employee.Email = string.Empty;
            var result = _validator.TestValidate(employee);
            result.ShouldHaveValidationErrorFor(employee => employee.Email).WithErrorMessage("Email is required.");
        }

        [TestMethod]
        public void PassingInvalidDataForEmployeeIdGivesValidationError()
        {
            var employee = GetEmployeeData();
            employee.EmployeeId = string.Empty;
            var result = _validator.TestValidate(employee);
            result.ShouldHaveValidationErrorFor(employee => employee.EmployeeId).WithErrorMessage("Employee Id is required.");
        }

        [TestMethod]
        public void PassingInvalidDataForFirstNameGivesValidationError()
        {
            var employee = GetEmployeeData();
            employee.FirstName = string.Empty;
            var result = _validator.TestValidate(employee);
            result.ShouldHaveValidationErrorFor(employee => employee.FirstName).WithErrorMessage("First name is required.");
        }

        [TestMethod]
        public void PassingInvalidDataForLastNameGivesValidationError()
        {
            var employee = GetEmployeeData();
            employee.LastName = string.Empty;
            var result = _validator.TestValidate(employee);
            result.ShouldHaveValidationErrorFor(employee => employee.LastName).WithErrorMessage("Last Name is required.");
        }

        #region Given

        public static Employee GetEmployeeData()
        {
            return new Employee
            {
                Email = "jats@gmail.com",
                EmployeeId = "456567",
                FirstName = "Jats",
                LastName = "Dhanda"
            };
        }

        #endregion
    }
}
