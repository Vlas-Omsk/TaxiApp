namespace TaxiApp.DataTypes
{
    public sealed record FullName(
        string LastName,
        string FirstName,
        string Patronymic
    )
    {
        public override string ToString()
        {
            return $"{LastName} {FirstName} {Patronymic}";
        }
    }
}
