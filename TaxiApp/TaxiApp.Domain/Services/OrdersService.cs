using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL;
using TaxiApp.DAL.Entities;

namespace TaxiApp.Domain.Services
{
    public sealed class OrdersService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public OrdersService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IQueryable<OrderEntity> GetAll()
        {
            return _applicationDbContext.Orders;
        }

        public async Task<OrderEntity> Create(
            int driverId,
            int clientId,
            decimal cost,
            string addressFrom,
            string addressTo,
            string comment
        )
        {
            var order = new OrderEntity()
            {
                DriverId = driverId,
                ClientId = clientId,
                Cost = cost,
                AddressFrom = addressFrom,
                AddressTo = addressTo,
                Comment = comment,
                CreatedAt = DateTime.UtcNow
            };

            await _applicationDbContext.Orders.AddAsync(order);
            await _applicationDbContext.SaveChangesAsync();

            return order;
        }

        public async Task Update(
            int id,
            int? driverId,
            int? clientId,
            decimal? cost,
            string addressFrom,
            string addressTo,
            string comment
        )
        {
            var order = await _applicationDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);

            if (driverId.HasValue)
                order.DriverId = driverId.Value;
            if (clientId.HasValue)
                order.ClientId = clientId.Value;
            if (cost.HasValue)
                order.Cost = cost.Value;
            if (addressFrom != null)
                order.AddressFrom = addressFrom;
            if (addressTo != null)
                order.AddressTo = addressTo;
            if (comment != null)
                order.Comment = comment;

            _applicationDbContext.Orders.Update(order);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            _applicationDbContext.Orders.Remove(
                await _applicationDbContext.Orders.FirstOrDefaultAsync(x => x.Id == id)
            );
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
