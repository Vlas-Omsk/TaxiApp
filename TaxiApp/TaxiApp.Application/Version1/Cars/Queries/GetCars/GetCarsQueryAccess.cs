using TaxiApp.Application.Version1.DTO;
using TaxiApp.Application.Version1.Queries;
using TaxiApp.DataTypes;

namespace TaxiApp.Application.Version1.Cars.Queries.GetCars
{
    internal sealed class GetCarsQueryAccess : RequestAccess<GetCarsQuery, CarDTO[]>
    {
        public GetCarsQueryAccess(AccessVerifierFactory accessVerifierFactory) : base(accessVerifierFactory)
        {
        }

        protected override async Task VerifyOverride(GetCarsQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
            await verifier.HasRole(UserRole.Administrator);
        }
    }
}
