﻿using Duende.IdentityServer;
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
                    ClientId = "angular_client",
                    ClientName = "Angular Blog Client",
                    ClientUri = "http://localhost:4200",
                    ClientSecrets = {new Secret("angularLupin".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true, //protect CSRF
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    AccessTokenLifetime = 60*60*2,

                    RedirectUris =
                    {
                        "http://localhost:4200",
                        "http://localhost:4200/signin-callback",
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
                        "userAPI",
                        "postAPI",
                        "notificationAPI",
                        "chatAPI"
                    }
                },
                new Client
                {
                    ClientId = "post_client_api",
                    ClientName = "Post API Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("postapi".Sha256()) },

                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "userAPI",
                        "postAPI",
                        "notificationAPI",
                        "chatAPI"
                    },

                    // Redirect URIs and Post Logout Redirect URIs specific to the client host
                    RedirectUris =
                    {
                        "http://localhost:7002/signin-oidc",
                        "http://localhost:7002/signout-callback-oidc",
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:7002/signout-callback-oidc"
                    }
                }
        };
}