namespace TaxiApp.Domain.Abstractions.Models
{
    public sealed record FullName
    {
        public FullName(string lastName, string firstName, string patronymic)
        {
            LastName = lastName;
            FirstName = firstName;
            Patronymic = patronymic;
        }

        public string LastName { get; }
        public string FirstName { get; }
        public string Patronymic { get; }
    }
}
