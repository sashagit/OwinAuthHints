// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. Apache License v2.0
// From https://git01.codeplex.com/katanaproject

using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OwinAuthWeb.AppHost.Middleware
{
    static class Constants
    {
        internal const string RequestSchemeKey = "owin.RequestScheme";
        internal const string RequestHeadersKey = "owin.RequestHeaders";
        internal const string ResponseHeadersKey = "owin.ResponseHeaders";
        internal const string ResponseStatusCodeKey = "owin.ResponseStatusCode";
        internal const string ResponseReasonPhraseKey = "owin.ResponseReasonPhrase";

        internal const string ServerUserKey = "server.User";
        internal const string ServerOnSendingHeadersKey = "server.OnSendingHeaders";

        internal const string WwwAuthenticateHeader = "WWW-Authenticate";
        internal const string AuthorizationHeader = "Authorization";
    }

    internal static class Helpers
    {
        public static Task GetCompletedTask()
        {
            var tcs = new TaskCompletionSource<object>();
            tcs.TrySetResult(null);
            return tcs.Task;
        }

        public static void AddUserIdentity(IOwinContext context, IIdentity identity)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");

            var newClaimsPrincipal = new ClaimsPrincipal(identity);

            IPrincipal existingPrincipal = context.Request.User;
            if (existingPrincipal != null)
            {
                var existingClaimsPrincipal = existingPrincipal as ClaimsPrincipal;
                if (existingClaimsPrincipal == null)
                {
                    IIdentity existingIdentity = existingPrincipal.Identity;
                    if (existingIdentity.IsAuthenticated)
                    {
                        newClaimsPrincipal.AddIdentity(existingIdentity as ClaimsIdentity ?? new ClaimsIdentity(existingIdentity));
                    }
                } else
                {
                    foreach (var existingClaimsIdentity in existingClaimsPrincipal.Identities)
                    {
                        if (existingClaimsIdentity.IsAuthenticated)
                        {
                            newClaimsPrincipal.AddIdentity(existingClaimsIdentity);
                        }
                    }
                }
            }
            context.Request.User = newClaimsPrincipal;
        }

        public static bool TryGetBasicCredentials(IOwinContext context, out string clientId, out string clientSecret)
        {
            // Client Authentication http://tools.ietf.org/html/rfc6749#section-2.3
            // Client Authentication Password http://tools.ietf.org/html/rfc6749#section-2.3.1
            string authorization = context.Request.Headers.GetValues("Authorization").FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(authorization) && authorization.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                string ClientId = null;
                try
                {
                    byte[] data = Convert.FromBase64String(authorization.Substring("Basic ".Length).Trim());
                    string text = Encoding.UTF8.GetString(data);
                    int delimiterIndex = text.IndexOf(':');
                    if (delimiterIndex >= 0)
                    {
                        clientId = text.Substring(0, delimiterIndex);
                        clientSecret = text.Substring(delimiterIndex + 1);
                        ClientId = clientId;
                        return true;
                    }
                } catch (FormatException)
                {
                    // Bad Base64 string
                } catch (ArgumentException)
                {
                    // Bad utf-8 string
                }
            }

            clientId = null;
            clientSecret = null;
            return false;
        }

        public static T Get<T>(this IDictionary<string, object> dictionary, string key)
        {
            object value;
            return dictionary.TryGetValue(key, out value) ? (T)value : default(T);
        }

        
    }

    /*
    * https://github.com/aspnet/Identity
    * http://www.asp.net/identity/overview/extensibility/overview-of-custom-storage-providers-for-aspnet-identity
    * 
    * http://benfoster.io/blog/aspnet-identity-stripped-bare-mvc-part-2
    * http://benfoster.io/blog/how-to-write-owin-middleware-in-5-different-steps
    * 
    * https://gkulshrestha.wordpress.com/category/net/asp-net/security/owinkatana/
    * http://leastprivilege.com/2013/10/04/owin-claims-transformation-middlewaretake-2/
    * 
    * http://www.asp.net/web-api/overview/advanced/configuring-aspnet-web-api
    * 
    */
}
