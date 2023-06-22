using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Orders
{
    internal sealed class UpdateOrderCommandHandler : RequestHandler<UpdateOrderCommand, bool>
    {
        private readonly OrdersService _ordersService;

        public UpdateOrderCommandHandler(
            OrdersService ordersService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _ordersService = ordersService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(UpdateOrderCommand request)
        {
            await _ordersService.Update(
                request.Id,
                request.DriverId,
                request.ClientId,
                request.TariffId,
                request.Cost,
                request.AddressFrom,
                request.AddressTo,
                request.Comment
            );

            return Success(true);
        }

        protected override async Task VerifyOverride(UpdateOrderCommand request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);
        }
    }
}
