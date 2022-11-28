using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer.Services;

public class ProfileService : IProfileService
{
    private UserManager<ApplicationUser> userManager { get; set; }

    public ProfileService(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);

        var claims = new List<Claim>
        {
            new Claim("Email", user.Email)
        };

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);

        context.IsActive = user is not null;
    }
}