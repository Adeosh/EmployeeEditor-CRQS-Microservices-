using MediatR;

namespace EmployeeEditor.Application.Employees.Create
{
    public record CreateEmployeeCommand(
        string FirstName,
        string MiddleName,
        string LastName,
        int Age,
        string Email,
        string Department,
        decimal Salary,
        bool IsActive,
        DateTime? EmploymentDate,
        DateTime? CreatedAt,
        DateTime? UpdatedAt = null) : IRequest;

    public record CreateEmployeeRequest(
        string FirstName,
        string MiddleName,
        string LastName,
        int Age,
        string Email,
        string Department,
        decimal Salary,
        bool IsActive,
        DateTime? EmploymentDate,
        DateTime? CreatedAt,
        DateTime? UpdatedAt = null);
}
