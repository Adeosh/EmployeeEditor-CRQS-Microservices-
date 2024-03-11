using EmployeeEditor.Domain.Models.Employee;

namespace EmployeeEditor.Application.Abstractions
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployeeAsync();
        Task<Guid> CreateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(Guid id);
        Task<Employee?> UpdateEmployeeAsync(Employee employee);
    }
}
