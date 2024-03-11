using MediatR;

namespace EmployeeEditor.Application.Employees.Update
{
    public record UpdateEmployeeCommand(
        Guid Id,
        string FirstName,
        string MiddleName,
        string LastName,
        int Age,
        string Email,
        string Department,
        decimal Salary,
        bool IsActive,
        DateTime? UpdatedAt) : IRequest;

    public record UpdateEmployeeRequest(
        string FirstName,
        string MiddleName,
        string LastName,
        int Age,
        string Email,
        string Department,
        decimal Salary,
        bool IsActive,
        DateTime? UpdatedAt);
}
