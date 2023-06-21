using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL;
using TaxiApp.DAL.Entities;
using TaxiApp.DataTypes;

namespace TaxiApp.Domain.Services
{
    public sealed class DriversService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DriversService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<DriverEntity> GetAll()
        {
            return _applicationDbContext.Drivers;
        }

        public async Task Update(
            int id,
            FullName fullName,
            string inn,
            string passport,
            string address,
            string additionalInfo,
            int? carId
        )
        {
            var driver = await _applicationDbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id);

            if (fullName != null)
            {
                driver.LastName = fullName.LastName;
                driver.FirstName = fullName.FirstName;
                driver.Patronymic = fullName.Patronymic;
            }
            if (inn != null)
                driver.Inn = inn;
            if (passport != null)
                driver.Passport = passport;
            if (address != null)
                driver.Address = address;
            if (additionalInfo != null)
                driver.AdditionalInfo = additionalInfo;
            if (carId.HasValue)
                driver.CarId = carId.Value;

            _applicationDbContext.Drivers.Update(driver);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _applicationDbContext.Drivers.Remove(
                await _applicationDbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id)
            );
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
