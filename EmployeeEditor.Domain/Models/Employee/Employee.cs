using System.ComponentModel.DataAnnotations;

namespace EmployeeEditor.Domain.Models.Employee
{
    public sealed class Employee
    {
        public Employee(
            string firstName,
            string middleName, 
            string lastName, 
            int age, 
            Email email, 
            string department, 
            decimal salary, 
            bool isActive, 
            DateTime? employmentDate,
            DateTime? createdAt) 
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Age = age;
            Email = email;
            Department = department;
            Salary = salary;
            IsActive = isActive;
            EmploymentDate = employmentDate;
            CreatedAt = createdAt;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        public int Age { get; set; }
        public Email? Email { get; set; }
        public string? Department { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EmploymentDate { get; private set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedAt { get; private set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? UpdatedAt { get; set; }

        public void Update(
            string firstName,
            string middleName,
            string lastName,
            int age, 
            Email email,
            string department, 
            decimal salary,
            bool isActive,
            DateTime? updatedAt)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Age = age;
            Email = email;
            Department = department;
            Salary = salary;
            IsActive = isActive;
            UpdatedAt = updatedAt;
        }
    }
}
