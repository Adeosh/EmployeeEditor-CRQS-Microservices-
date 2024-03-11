using EmployeeEditor.Infrastructure.Data;
using EmployeeEditor.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Transactions;

namespace EmployeeEditor.Tests.Infrastructure
{
    public class EmployeeRepositoryTests
    {
        private async Task<ApplicationDbContext> GetDataBaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql("Host=localhost;Port=5433;User id=postgres;Password=zaq1234;Database=EmployEditor3")
                .Options;

            var context = new ApplicationDbContext(options);

            return context;
        }

        //[Fact]
        //public async Task EmployeeRepository_AddEmployee_AddsEmployeeToContext()
        //{
        //    // Arrange
        //    var context = await GetDataBaseContext();
        //    var employeeRepository = new EmployeeRepository(context);
        //    var employeeToAdd = new Employee("Johnny", null, "Doe", 30, Email.Create("jof44hn.doe@mail.com"), "IT", 5000, true, DateTime.UtcNow, DateTime.UtcNow);

        //    // Act
        //    employeeRepository.AddEmployee(employeeToAdd);
        //    await context.SaveChangesAsync();

        //    // Assert
        //    var addedEmployee = await context.Employees.FirstOrDefaultAsync(e => e.Email == employeeToAdd.Email);
        //    Assert.NotNull(addedEmployee);
        //    Assert.Equal(employeeToAdd.FirstName, addedEmployee.FirstName);
        //    Assert.Equal(employeeToAdd.MiddleName, addedEmployee.MiddleName);
        //    Assert.Equal(employeeToAdd.LastName, addedEmployee.LastName);
        //    Assert.Equal(employeeToAdd.Age, addedEmployee.Age);
        //    Assert.Equal(employeeToAdd.Email, addedEmployee.Email);
        //    Assert.Equal(employeeToAdd.Department, addedEmployee.Department);
        //    Assert.Equal(employeeToAdd.Salary, addedEmployee.Salary);
        //    Assert.Equal(employeeToAdd.EmploymentDate, addedEmployee.EmploymentDate);
        //    Assert.Equal(employeeToAdd.CreatedAt, addedEmployee.CreatedAt);
        //}

        [Fact]
        public async Task EmployeeRepository_UpdateEmployee_UpdatesEmployeeInContext()
        {
            // Arrange
            var context = await GetDataBaseContext();
            var employeeRepository = new EmployeeRepository(context);
            var employeeToUpdate = new Employee("Jane", "Terry", "Smithtt", 28, Email.Create("jane.smith@mail.com"), 
                "Marketing", 3500, true, DateTime.UtcNow, DateTime.UtcNow);
            await context.Employees.AddAsync(employeeToUpdate);

            employeeToUpdate.Department = "Sales";
            employeeToUpdate.Salary = 4200;

            // Act
            employeeRepository.UpdateEmployee(employeeToUpdate);

            // Assert
            var updatedEmployee = await context.Employees.FindAsync(employeeToUpdate.Id);
            Assert.NotNull(updatedEmployee);
            Assert.Equal("Sales", updatedEmployee.Department);
            Assert.Equal(4200, updatedEmployee.Salary);
        }

        [Fact]
        public async Task EmployeeRepository_RemoveEmployee_RemovesEmployeeFromContext()
        {
            // Arrange
            var context = await GetDataBaseContext();
            var employeeRepository = new EmployeeRepository(context);
            var employeeToRemove = new Employee("Mike", "Fury", "Johnson", 40, Email.Create("mike.johnson@mail.com"), 
                "Finance", 5000, true, DateTime.UtcNow, DateTime.UtcNow);
            await context.Employees.AddAsync(employeeToRemove);

            // Act
            employeeRepository.RemoveEmployee(employeeToRemove);

            // Assert
            var removedEmployee = await context.Employees.FindAsync(employeeToRemove.Id);
            Assert.Null(removedEmployee);
        }

        [Fact]
        public async Task EmployeeRepository_GetEmployeeByEmailAsync_ReturnsNullForNonexistentEmail()
        {
            // Arrange
            var context = await GetDataBaseContext();
            var employeeRepository = new EmployeeRepository(context);
            var nonExistentEmail = "nonexistent@mail.com";

            // Act
            var result = await employeeRepository.GetEmployeeByEmailAsync(nonExistentEmail);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_ReturnsNull_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var context = await GetDataBaseContext(); // Replace with your logic to get a mock context without the employee
            var employeeRepository = new EmployeeRepository(context);
            var nonExistingEmployeeId = Guid.NewGuid(); // Generate a non-existing ID

            // Act
            var result = await employeeRepository.GetEmployeeByIdAsync(nonExistingEmployeeId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ReturnsListOfEmployees()
        {
            // Arrange
            var context = await GetDataBaseContext(); // Replace with your logic to get a mock context with multiple employees
            var employeeRepository = new EmployeeRepository(context);

            // Act
            var results = await employeeRepository.GetAllEmployeesAsync();

            // Assert
            Assert.NotNull(results);
            Assert.NotEmpty(results); // Assert there are at least some employees
                                      // Assert additional properties on each employee if needed
        }

        [Fact]
        public async Task GetEmployeesByDepartmentAsync_ReturnsEmptyList_ForNonexistentDepartment()
        {
            // Arrange
            var nonExistingDepartment = "Nonexistent";
            var context = await GetDataBaseContext(); // Replace with your logic to get a mock context with employees (may not have the specific department)
            var employeeRepository = new EmployeeRepository(context);

            // Act
            var results = await employeeRepository.GetEmployeesByDepartmentAsync(nonExistingDepartment);

            // Assert
            Assert.NotNull(results);
            Assert.Empty(results); // Assert there are no employees in the non-existent department
        }

        [Fact]
        public async Task IsEmailUniqueAsync_ReturnsTrue_ForNonexistentEmail()
        {
            // Arrange
            var nonExistingEmail = "unique@mail.com";
            var context = await GetDataBaseContext(); // Replace with your logic to get a mock context with employees (may not have the specific email)
            var employeeRepository = new EmployeeRepository(context);

            // Act
            var isUnique = await employeeRepository.IsEmailUniqueAsync(nonExistingEmail);

            // Assert
            Assert.True(isUnique);
        }
        //unfinished!!!
    }
}
