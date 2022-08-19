// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Identity.Quick
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("customAPI.read"),
                new ApiScope("customAPI.write"),
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            {
                new ApiResource("customAPI")
                {
                    Scopes = new List<string>{ "customAPI.read", "customAPI.write" },
                    ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
                }
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // m2m client credentials flow client
                new Client
                {
                    ClientId = "m2m.client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "customAPI.read" }
                },

                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    //AllowedGrantTypes = GrantTypes.Code,
                     AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://localhost:44363/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:44363/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:44363/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "customAPI.read", "customAPI.write" }
                },

                new Client
                {
                    ClientId = "765a10ef-8e67-443c-8727-50bdfdbf3b1b",
                    ClientName = "MVC Core Client APP",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "role",
                        "customAPI.write"
                    },
                    RedirectUris = new List<string>{ "https://localhost:44363/signin-oidc" },
                    PostLogoutRedirectUris = new List<string>{ "https://localhost:44363/" }
                }
            };
    }
}