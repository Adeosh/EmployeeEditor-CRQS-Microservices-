namespace EmployeeEditor.Tests.Domain
{
    public class EmployeeNotFoundExceptionTests
    {
        [Fact]
        public void Constructor_SetsMessage()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var exception = new EmployeeNotFoundException(id);

            // Assert
            exception.Message.Should().Be($"The employee with the Id = {id} was not found");
        }
    }
}
