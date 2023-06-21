using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL;
using TaxiApp.DAL.Entities;

namespace TaxiApp.Domain.Services
{
    public sealed class TariffsService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public TariffsService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<TariffEntity> GetAll()
        {
            return _applicationDbContext.Tariffs;
        }

        public async Task Update(
            int id,
            string name,
            decimal? startingPrice,
            TimeSpan? freeWaiting,
            decimal? paidWaitingPricePerMin,
            decimal? inCityPricePerKm,
            decimal? outsideCityPricePerKm,
            decimal? waitingOnWayPricePerMin,
            string description
        )
        {
            var tariff = await _applicationDbContext.Tariffs.FirstOrDefaultAsync(x => x.Id == id);

            if (name != null)
                tariff.Name = name;
            if (startingPrice.HasValue)
                tariff.StartingPrice = startingPrice.Value;
            if (freeWaiting.HasValue)
                tariff.FreeWaiting = freeWaiting.Value;
            if (paidWaitingPricePerMin.HasValue)
                tariff.PaidWaitingPricePerMin = paidWaitingPricePerMin.Value;
            if (inCityPricePerKm.HasValue)
                tariff.InCityPricePerKm = inCityPricePerKm.Value;
            if (outsideCityPricePerKm.HasValue)
                tariff.OutsideCityPricePerKm = outsideCityPricePerKm.Value;
            if (waitingOnWayPricePerMin.HasValue)
                tariff.WaitingOnWayPricePerMin = waitingOnWayPricePerMin.Value;
            if (description != null)
                tariff.Description = description;

            _applicationDbContext.Tariffs.Update(tariff);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _applicationDbContext.Tariffs.Remove(
                await _applicationDbContext.Tariffs.FirstOrDefaultAsync(x => x.Id == id)
            );
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
