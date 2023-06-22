using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Orders
{
    internal sealed class GetOrderInfoQueryHandler : RequestHandler<GetOrderInfoQuery, OrderInfoDTO>
    {
        private readonly OrdersService _ordersService;

        public GetOrderInfoQueryHandler(
            OrdersService ordersService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _ordersService = ordersService;
        }

        protected override async Task<Response<OrderInfoDTO>> ExecuteOverride(GetOrderInfoQuery request)
        {
            var order = await _ordersService.GetAll()
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return Success(new OrderInfoDTO(
                order.Id,
                order.DriverId,
                order.ClientId,
                order.Cost,
                order.AddressFrom,
                order.AddressTo,
                order.CreatedAt,
                order.Comment
            ));
        }

        protected override async Task VerifyOverride(GetOrderInfoQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);
        }
    }
}
