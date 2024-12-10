using EggsAndToast.Data;
using EggsAndToast.Data.Auth;
using EggsAndToast.Data.Communication;
using EggsAndToast.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EggsAndToast.Web;

public static class ProgramAuthConfiguration
{
    public static void ConfigureAuthentication(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        builder.Services
            .AddIdentity<User, Role>(c =>
            {
                c.ClaimsIdentity.RoleClaimType = AppClaimTypes.Role;
                c.ClaimsIdentity.EmailClaimType = AppClaimTypes.Email;
                c.ClaimsIdentity.UserIdClaimType = AppClaimTypes.UserId;
                c.ClaimsIdentity.UserNameClaimType = AppClaimTypes.UserName;

                c.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<ClaimsPrincipalFactory>();


        builder.Services
            .AddAuthentication()
            ;

        builder.Services.Configure<SecurityStampValidatorOptions>(o =>
        {
            // Configure how often to refresh user claims and validate
            // that the user is still allowed to sign in.
            o.ValidationInterval = TimeSpan.FromMinutes(5);
        });

        builder.Services.ConfigureApplicationCookie(c =>
        {
            c.LoginPath = "/SignIn"; // Razor page "Pages/SignIn.cshtml"

        });
    }

}
