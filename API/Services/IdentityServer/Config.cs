using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
              new IdentityResources.OpenId(),
              new IdentityResources.Profile(),
              new IdentityResources.Address(),
              new IdentityResources.Email(),
              new IdentityResource(
                    "roles",
                    "Your role(s)",
                    new List<string>() { "role" })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
       new ApiScope[]
       {
               new ApiScope("userAPI", "User API"),
               new ApiScope("postAPI", "Post API"),
               new ApiScope("notificationAPI", "Notification API"),
               new ApiScope("chatAPI", "Chat API")
       };

    public static IEnumerable<ApiResource> ApiResources =>
      new ApiResource[]
      {
               new ApiResource("userAPI", "User API"),
               new ApiResource("postAPI", "Post API"),
               new ApiResource("notificationAPI", "Notification API"),
               new ApiResource("chatAPI", "Chat API")
      };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
                new Client
                {
                    ClientId = "angular_user_client",
                    ClientName = "Angular User Client",
                    ClientUri = "http://localhost:4200",
                    ClientSecrets = {new Secret("angularUserClient".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true, //protect CSRF
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    RedirectUris =
                    {
                        "http://localhost:4200/login-completed",
                        "http://localhost:4200/dash-board",
                        "http://localhost:4200/assets/silent-callback.html"
                    },

                    PostLogoutRedirectUris = new List<string>() { "http://localhost:4200/signout-callback" },
                    AllowedCorsOrigins = { "http://localhost:4200" },

                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "userAPI"
                    }
                },
                new Client
                {
                    ClientId = "angular_post_client",
                    ClientName = "Angular Post Client",
                    ClientUri = "http://localhost:4100",
                    ClientSecrets = {new Secret("angularPostClient".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true, //protect CSRF
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    RedirectUris =
                    {
                        "http://localhost:4100/login-completed",
                        "http://localhost:4100/news",
                        "http://localhost:4100/assets/silent-callback.html"
                    },

                    PostLogoutRedirectUris = new List<string>() { "http://localhost:4100/signout-callback" },
                    AllowedCorsOrigins = { "http://localhost:4100" },

                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "postAPI"
                    }
                },
                new Client
                {
                    ClientId = "angular_notification_client",
                    ClientName = "Angular Notification Client",
                    ClientUri = "http://localhost:4000",
                    ClientSecrets = {new Secret("angularNotificationClient".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true, //protect CSRF
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    RedirectUris =
                    {
                        "http://localhost:4000/login-completed",
                        "http://localhost:4000/assets/silent-callback.html"
                    },

                    PostLogoutRedirectUris = new List<string>() { "http://localhost:4000/signout-callback" },
                    AllowedCorsOrigins = { "http://localhost:4000" },

                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "notificationAPI"
                    }
                },
        };
}