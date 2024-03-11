using EmployeeEditor.Domain.Models.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeEditor.Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName)
            .HasMaxLength(40);

            builder.Property(e => e.MiddleName)
                .HasMaxLength(40);

            builder.Property(e => e.LastName)
                .HasMaxLength(40);

            builder.Property(e => e.Age);

            builder.Property(e => e.Email)
                        .HasMaxLength(255)
                        .HasConversion(
                            v => v.Value,
                            v => Email.Create(v)!
                        );

            builder.HasIndex(e => e.Email).IsUnique();

            builder.Property(e => e.Department)
                .HasMaxLength(40);

            builder.Property(e => e.Salary)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.IsActive);

            builder.Property(e => e.EmploymentDate);

            builder.Property(e => e.CreatedAt);

            builder.Property(e => e.UpdatedAt);
        }
    }
}
