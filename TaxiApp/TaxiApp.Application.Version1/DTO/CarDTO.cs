﻿namespace TaxiApp.Application.Version1_0.DTO
{
    public sealed record CarDTO(
        int Id,
        string Brand,
        string Number,
        string Color,
        string DriverFullName,
        string AdditionalInfo
    );
}
