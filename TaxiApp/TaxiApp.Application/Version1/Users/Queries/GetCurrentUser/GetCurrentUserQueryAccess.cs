using TaxiApp.Application.Version1.DTO;
using TaxiApp.Application.Version1.Queries;

namespace TaxiApp.Application.Version1.Users.Queries.GetCurrentUser
{
    internal sealed class GetCurrentUserQueryAccess : RequestAccess<GetCurrentUserQuery, UserDTO>
    {
        public GetCurrentUserQueryAccess(AccessVerifierFactory accessVerifierFactory) : base(accessVerifierFactory)
        {
        }

        protected override async Task VerifyOverride(GetCurrentUserQuery request, AccessVerifier verifier)
        {
            await verifier.NotAnonymouse();
        }
    }
}
