namespace EmployeeEditor.Tests.Domain
{
    public class EmployeeTests
    {
        [Fact]
        public void UpdateEmployeeInfo()
        {
            // Arrange
            var employee = new Employee("Jack", "Arnold", "Black", 30, 
                new Email("arni333@example.com"), "IT", 50000, true,
                new DateTime(2017, 11, 11), new DateTime(2021, 10, 1));

            // Act
            employee.Update("Jo", "Alexander", "Rambo", 35, 
                new Email("jo7711@example.com"), "HR", 60000, false, 
                new DateTime(2021, 10, 15));

            // Assert
            Assert.Equal("Jo", employee.FirstName);
            Assert.Equal("Alexander", employee.MiddleName);
            Assert.Equal("Rambo", employee.LastName);
            Assert.Equal(35, employee.Age);
            Assert.Equal("jo7711@example.com", employee.Email?.Value);
            Assert.Equal("HR", employee.Department);
            Assert.Equal(60000, employee.Salary);
            Assert.False(employee.IsActive);
            Assert.Equal(new DateTime(2021, 10, 15), employee.UpdatedAt);
        }
    }
}
