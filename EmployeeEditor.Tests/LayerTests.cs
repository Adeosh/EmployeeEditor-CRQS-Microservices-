namespace EmployeeEditor.Tests
{
    public class LayerTests : BaseTest
    {
        [Fact]
        public void Domain_Should_NotHaveDependencyOnApplication()
        {
            var result = Types.InAssembly(DomainAssambly)
                .Should()
                .NotHaveDependencyOn("Application")
                .GetResult();

            result.IsSuccessful.Should().BeTrue();
        }
    }
}
