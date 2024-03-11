using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Application.Abstractions.Data;
using EmployeeEditor.Domain.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace EmployeeEditor.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IApplicationDbContext _context;

        public EmployeeService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetAllEmployeeAsync()
        {
            return await _context.Employees
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Guid> CreateEmployeeAsync(Employee employee)
        {
            employee.UpdatedAt = null;

            var result = await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return result.Entity.Id;
        }

        public async Task<Employee?> UpdateEmployeeAsync(Employee employee)
        {
            var result = await _context.Employees.FindAsync(employee.Id);

            if (result == null)
            {
                throw new Exception($"Employee with Id {employee.Id} not found");
            }

            result.FirstName = employee.FirstName;
            result.MiddleName = employee.MiddleName;
            result.LastName = employee.LastName;
            result.Age = employee.Age;
            result.Email = employee.Email;
            result.Department = employee.Department;
            result.Salary = employee.Salary;
            result.UpdatedAt = employee.UpdatedAt;

            _context.Employees.Update(result);
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return false;
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
