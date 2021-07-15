using Cw.Payslip.Repos;
using Cw.Payslip.Shared.DTO;
using Cw.PayslipService.Entities;
using Cw.PayslipService.Models;
using Cw.PayslipService.Services;
using Cw.PayslipService.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cw.PayslipService.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class PayslipController : ControllerBase
    {
        private IAuthenticate _authenticateService;
        private IPayslipRepository _payslipRepository;

        public PayslipController(IAuthenticate authenticateService, IPayslipRepository payslipRepository)
        {
            _authenticateService = authenticateService;
            _payslipRepository = payslipRepository;

        }

        [AllowAnonymous]
        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public ActionResult UploadEmployee([FromBody] Employee employeeDetails)
        {
            var validationResults = new EmployeeValidator().Validate(employeeDetails);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            var user = _authenticateService.Authenticate(employeeDetails.Username, employeeDetails.Password);
            if (user == null) return BadRequest();
            var employeeDto = new EmployeeDto
            {
                Email = employeeDetails.Email,
                EmployeeId = employeeDetails.EmployeeId,
                FirstName = employeeDetails.FirstName,
                LastName = employeeDetails.LastName
            };
            var response =_payslipRepository.UploadEmployeeDetail(employeeDto);
            return Ok(response);
        }

        [HttpGet]
        public ActionResult PayslipLookup(string payslipId)
        {
            if (string.IsNullOrWhiteSpace(payslipId)) return BadRequest("Payslip Id cannot be empty");
            var payslipModel = _payslipRepository.GetPayslipFor(payslipId);

            if (payslipModel.IsSuccess)
            {
                return Ok( new Models.Payslip
                {
                    BaseSalary = payslipModel.Result.BaseSalary,
                    PayslipId = payslipModel.Result.PayslipId,
                    EmployeeId = payslipModel.Result.EmployeeId,
                    PaymentStartDate = payslipModel.Result.PaymentStartDate.ToShortDateString(),
                    PaymentEndDate = payslipModel.Result.PaymentEndDate.Date.ToShortDateString()
                });
            }

            return BadRequest();
        }
        
    }
}
