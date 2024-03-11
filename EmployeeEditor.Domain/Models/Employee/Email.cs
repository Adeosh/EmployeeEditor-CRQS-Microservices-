namespace EmployeeEditor.Domain.Models.Employee
{
    public record Email
    {
        public Email(string value) => Value = value;

        public string? Value { get; }

        public static explicit operator string(Email value) => value.Value!;

        public static Email? Create(string? email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return default;
            }

            if (email.Split('@').Length != 2)
            {
                return default;
            }

            return new Email(email);
        }
    }
}
