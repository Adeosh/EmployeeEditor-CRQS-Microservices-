using EmployeeEditor.Application.Abstractions;
using FluentValidation;

namespace EmployeeEditor.Application.Employees.Create
{
    public sealed class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator(IEmployeeRepository employeeRepository)
        {
            RuleFor(e => e.Email).MustAsync(async (email, _) =>
            {
                return await employeeRepository.IsEmailUniqueAsync(email);
            }).WithMessage("This email must be unique");
        }
    }
}
