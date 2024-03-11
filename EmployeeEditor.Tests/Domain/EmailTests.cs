namespace EmployeeEditor.Tests.Domain
{
    public class EmailTests
    {
        [Fact]
        public void Create_Should_ReturnNull_WhenValueIsNull()
        {
            // Arrange
            string? value = null;

            // Act
            var email = Email.Create(value);

            // Assert
            Assert.Null(email);
        }

        [Fact]
        public void Create_Should_ReturnNull_WhenValueIsEmpty()
        {
            // Arrange
            var value = string.Empty;

            // Act
            var email = Email.Create(value);

            // Assert
            Assert.Null(email);
        }

        [Fact]
        public void Create_ValidEmail_ReturnsEmailObject()
        {
            // Arrange
            string email = "test@example.com";

            // Act
            var result = Email.Create(email);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(email, result.Value);
        }

        [Fact]
        public void EmailsShouldBeEqualIfValuesAreEqual()
        {
            // Arrange
            Email email1 = Email.Create("test@email.test");
            Email email2 = Email.Create("test@email.test");

            // Act
            email1.ShouldBe(email2);

            // Assert
            email1.Value.ShouldBe(email2.Value);
        }
    }
}
