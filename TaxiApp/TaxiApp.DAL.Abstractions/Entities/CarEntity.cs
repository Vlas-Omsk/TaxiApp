﻿namespace TaxiApp.DAL.Entities
{
    public sealed class CarEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Number { get; set; }
        public string Color { get; set; }
        public int YearOfManufacture { get; set; }
        public string AdditionalInfo { get; set; }
        public ICollection<DriverEntity> Drivers { get; } = new List<DriverEntity>();
        public ICollection<CarTariffEntity> CarTariffs { get; } = new List<CarTariffEntity>();
        //public ICollection<OrderEntity> Orders { get; set; } = new List<OrderEntity>();
    }
}
