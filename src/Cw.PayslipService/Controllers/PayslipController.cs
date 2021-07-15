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
        public ActionResult UploadEmployee([FromBody] Employee createEmployeeCommand)
        {
            var validationResults = new EmployeeValidator().Validate(createEmployeeCommand);
            if (!validationResults.IsValid)
            {
                return BadRequest(validationResults.Errors);
            }

            var user = _authenticateService.Authenticate(createEmployeeCommand.Username, createEmployeeCommand.Password);
            if (user == null) return BadRequest();
            var employeeDto = new EmployeeDto
            {
                Email = createEmployeeCommand.Email,
                EmployeeId = createEmployeeCommand.EmployeeId,
                FirstName = createEmployeeCommand.FirstName,
                LastName = createEmployeeCommand.LastName
            };
            var response =_payslipRepository.UploadEmployeeDetail(employeeDto);
            return Ok(response);
        }

        [HttpGet]
        public ActionResult PayslipLookup(string employeeId)
        {
            if (string.IsNullOrWhiteSpace(employeeId)) return null;
            var payslipModel = _payslipRepository.GetPayslipFor(employeeId);

            if (payslipModel.IsSuccess)
            {
                return Ok( new Models.Payslip
                {
                    BaseSalary = payslipModel.Result.BaseSalary,
                    PayslipId = payslipModel.Result.PayslipId,
                    PaymentStartDate = payslipModel.Result.PaymentStartDate.ToShortDateString(),
                    PaymentEndDate = payslipModel.Result.PaymentEndDate.Date.ToShortDateString()
                });
            }

            return BadRequest();
        }
        
    }
}
