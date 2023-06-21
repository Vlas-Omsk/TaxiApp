using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL;
using TaxiApp.DAL.Entities;

namespace TaxiApp.Domain.Services
{
    public sealed class CarsService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public CarsService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<CarEntity> GetAll()
        {
            return _applicationDbContext.Cars;
        }

        public IQueryable<TariffEntity> GetForCar(int id)
        {
            return _applicationDbContext.CarTariffs
                .Where(x => x.CarId == id)
                .Select(x => x.Tariff);
        }

        public async Task Update(
            int id,
            string number = default,
            string color = default,
            int[] tariffIds = default,
            int? yearOfManufacture = default,
            string brand = default,
            string model = default,
            string additionalInfo = default
        )
        {
            var car = await _applicationDbContext.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (number != null)
                car.Number = number;
            if (color != null)
                car.Color = color;
            if (tariffIds != null)
            {
                _applicationDbContext.CarTariffs.RemoveRange(
                    _applicationDbContext.CarTariffs.Where(x => x.CarId == car.Id)
                );

                await _applicationDbContext.CarTariffs.AddRangeAsync(
                    tariffIds.Select(x => new CarTariffEntity()
                    {
                        CarId = car.Id,
                        TariffId = x
                    })
                );
            }
            if (yearOfManufacture.HasValue)
                car.YearOfManufacture = yearOfManufacture.Value;
            if (brand != null)
                car.Brand = brand;
            if (model != null)
                car.Model = model;
            if (additionalInfo != null)
                car.AdditionalInfo = additionalInfo;

            _applicationDbContext.Cars.Update(car);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _applicationDbContext.Cars.Remove(
                await _applicationDbContext.Cars.FirstOrDefaultAsync(x => x.Id == id)
            );
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
