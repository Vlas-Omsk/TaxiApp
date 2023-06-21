namespace TaxiApp.DataTypes
{
    public sealed record FullName(
        string LastName,
        string FirstName,
        string Patronymic
    );
}
