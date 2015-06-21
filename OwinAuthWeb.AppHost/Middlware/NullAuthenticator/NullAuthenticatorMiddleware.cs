// Copyright (c) Sasha Cikusa, Inc. All rights reserved. Apache License v2.0

using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OwinAuthWeb.AppHost.Middleware.NullAuthenticator
{
    public class NullAuthenticatorMiddleware : OwinMiddleware
    {
        public NullAuthenticatorMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            if (TryAuthenticate(context))
                return Next.Invoke(context);
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Helpers.GetCompletedTask();
            }
        }

        private bool TryAuthenticate(IOwinContext context)
        {
            //string clientId, clientSecret;
            //if (!TryGetBasicCredentials(out clientId, out clientSecret))
            //{

            //}

            // detokenize client-secret etc...

            // dummy stuff
            var identity = new GenericIdentity("user", "<auth-type>");
            var userRole = new Claim(ClaimTypes.Role, "user", ClaimValueTypes.String);
            identity.AddClaim(userRole);

            var claims = new ClaimsIdentity(identity);
            Helpers.AddUserIdentity(context, claims);


            //AuthenticationProperties properties = CreateProperties(user.UserName);
            //AuthenticationTicket ticket = new AuthenticationTicket(genId, properties);
            //context.Validated(ticket);
            //context.Request.Context.Authentication.SignIn(genId);

            //AddUserIdentity(context, claims);

            //await Next.Invoke(context);

            //context.Request.User = null;
            return true;
        }
    }
}
