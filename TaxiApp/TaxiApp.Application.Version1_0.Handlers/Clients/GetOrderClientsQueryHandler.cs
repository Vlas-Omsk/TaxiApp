using Microsoft.EntityFrameworkCore;
using TaxiApp.Application.Abstractions;
using TaxiApp.Application.Version1_0.DTO;
using TaxiApp.Application.Version1_0.Queries;
using TaxiApp.DataTypes;
using TaxiApp.Domain.Services;

namespace TaxiApp.Application.Version1_0.Handlers.Clients
{
    internal sealed class GetOrderClientsQueryHandler : RequestHandler<GetOrderClientsQuery, OrderClientDTO[]>
    {
        private readonly ClientsService _clientsService;

        public GetOrderClientsQueryHandler(
            ClientsService clientsService,
            AccessVerifierFactory accessVerifierFactory
        ) : base(accessVerifierFactory)
        {
            _clientsService = clientsService;
        }

        protected override async Task<Response<OrderClientDTO[]>> ExecuteOverride(GetOrderClientsQuery request)
        {
            var result = await _clientsService.GetAll()
                .Select(x => new OrderClientDTO(
                    x.Id,
                    x.Name
                ))
                .ToArrayAsync();

            return Success(result);
        }

        protected override async Task VerifyOverride(GetOrderClientsQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator, UserRole.Dispatcher);
        }
    }
}
