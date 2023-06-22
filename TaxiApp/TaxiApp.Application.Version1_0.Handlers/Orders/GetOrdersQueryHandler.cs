using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Orders
{
    internal sealed class GetOrdersQueryHandler : RequestHandler<GetOrdersQuery, OrderDTO[]>
    {
        private readonly OrdersService _ordersService;

        public GetOrdersQueryHandler(
            OrdersService ordersService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _ordersService = ordersService;
        }

        protected override async Task<Response<OrderDTO[]>> ExecuteOverride(GetOrdersQuery request)
        {
            var result = await _ordersService.GetAll()
                .Select(x => new OrderDTO(
                    x.Id,
                    x.CreatedAt,
                    new FullName(
                        x.Driver.LastName,
                        x.Driver.FirstName,
                        x.Driver.Patronymic
                    )
                ))
                .ToArrayAsync();

            return Success(result);
        }

        protected override async Task VerifyOverride(GetOrdersQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);
        }
    }
}
