using EmployeeEditor.Domain.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace EmployeeEditor.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<Employee> Employees { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
