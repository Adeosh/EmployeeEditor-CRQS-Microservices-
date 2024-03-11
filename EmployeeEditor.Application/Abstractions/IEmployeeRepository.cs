using EmployeeEditor.Domain.Models.Employee;

namespace EmployeeEditor.Application.Abstractions
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(Guid id);
        Task<Employee?> GetEmployeeByEmailAsync(string email);
        Task<List<Employee>> GetEmployeesByDepartmentAsync(string department);

        Task<bool> IsEmailUniqueAsync(string email);

        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void RemoveEmployee(Employee employee);
    }
}
