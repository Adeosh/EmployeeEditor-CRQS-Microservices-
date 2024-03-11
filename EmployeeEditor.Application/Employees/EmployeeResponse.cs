namespace EmployeeEditor.Application.Employees
{
    public record EmployeeResponse(
        Guid Id,
        string FirstName,
        string MiddleName,
        string LastName,
        int Age,
        string Email,
        string Department,
        decimal Salary,
        bool IsActive,
        DateTime? EmploymentDate);
}
