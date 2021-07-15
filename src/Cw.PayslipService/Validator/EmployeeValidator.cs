using Cw.PayslipService.Models;
using FluentValidation;

namespace Cw.PayslipService.Validator {

    public class EmployeeValidator : AbstractValidator<Employee> {

        public EmployeeValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .WithMessage("First name is required.");
            RuleFor(r => r.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required.");
            RuleFor(r => r.EmployeeId)
                .NotEmpty()
                .WithMessage("Employee Id is required.");
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email is required.");
        }

    }

}