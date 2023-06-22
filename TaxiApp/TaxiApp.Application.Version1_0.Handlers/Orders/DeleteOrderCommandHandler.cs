using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Orders
{
    internal sealed class DeleteOrderCommandHandler : RequestHandler<DeleteOrderCommand, bool>
    {
        private readonly OrdersService _ordersService;

        public DeleteOrderCommandHandler(
            OrdersService ordersService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _ordersService = ordersService;
        }

        protected override async Task<Response<bool>> ExecuteOverride(DeleteOrderCommand request)
        {
            await _ordersService.Delete(request.Id);

            return Success(true);
        }

        protected override async Task VerifyOverride(DeleteOrderCommand request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);
        }
    }
}
