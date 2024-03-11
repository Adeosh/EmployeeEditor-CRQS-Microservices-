using EmployeeEditor.Application.Abstractions;
using EmployeeEditor.Application.Abstractions.Data;
using EmployeeEditor.Domain.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace EmployeeEditor.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IApplicationDbContext _context;

        public EmployeeRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public void AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _context.Employees?.Update(employee);
        }

        public void RemoveEmployee(Employee employee)
        {
            _context.Employees?.Remove(employee);
        }

        public async Task<Employee?> GetEmployeeByEmailAsync(string email)
        {
            Email emailObject = new Email(email);

            return await _context.Employees
                .FirstOrDefaultAsync(e => e.Email == emailObject);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(Guid id)
        {
            return await _context.Employees
                .FindAsync(id);
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesByDepartmentAsync(string department)
        {
            return await _context.Employees
                .Where(e => e.Department == department)
                .ToListAsync();
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            Email emailObject = new Email(email);

            return !await _context.Employees.AnyAsync(e => e.Email == emailObject);
        }
    }
}
