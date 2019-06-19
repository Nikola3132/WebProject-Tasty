namespace Tasty.Data
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Tasty.Data.Models;

    public class TastyUserStore : UserStore<
        TastyUser,
        TastyRole,
        TastyDbContext,
        string,
        IdentityUserClaim<string>,
        IdentityUserRole<string>,
        IdentityUserLogin<string>,
        IdentityUserToken<string>,
        IdentityRoleClaim<string>>
    {
        public TastyUserStore(TastyDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }

        protected override IdentityUserRole<string> CreateUserRole(TastyUser user, TastyRole role)
        {
            return new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id };
        }

        protected override IdentityUserClaim<string> CreateUserClaim(TastyUser user, Claim claim)
        {
            var identityUserClaim = new IdentityUserClaim<string> { UserId = user.Id };
            identityUserClaim.InitializeFromClaim(claim);
            return identityUserClaim;
        }

        protected override IdentityUserLogin<string> CreateUserLogin(TastyUser user, UserLoginInfo login) =>
            new IdentityUserLogin<string>
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName,
            };

        protected override IdentityUserToken<string> CreateUserToken(
            TastyUser user,
            string loginProvider,
            string name,
            string value)
        {
            var token = new IdentityUserToken<string>
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value,
            };
            return token;
        }
    }
}
