using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.Commands;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Orders
{
    internal class CreateOrderCommandHandler : RequestHandler<CreateOrderCommand, CreateOrderResponseDTO>
    {
        private readonly OrdersService _ordersService;

        public CreateOrderCommandHandler(
            OrdersService ordersService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _ordersService = ordersService;
        }

        protected override async Task<Response<CreateOrderResponseDTO>> ExecuteOverride(CreateOrderCommand request)
        {
            var order = await _ordersService.Create(
                request.DriverId,
                request.ClientId,
                request.Cost,
                request.AddressFrom,
                request.AddressTo,
                request.Comment
            );

            return Success(new CreateOrderResponseDTO(
                order.Id,
                order.CreatedAt
            ));
        }

        protected override async Task VerifyOverride(CreateOrderCommand request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);
        }
    }
}
