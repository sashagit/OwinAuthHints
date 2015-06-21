// Copyright (c) Sasha Cikusa, Inc. All rights reserved. Apache License v2.0

using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OwinAuthWeb.AppHost.Middleware.OnlyAuthenticated
{
    public class OnlyAuthenticatedMiddleware : OwinMiddleware
    {
        public OnlyAuthenticatedMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        private bool IsAuthenticated(IOwinContext context)
        {
            IPrincipal user = context.Request.User;
            if (user != null)
                if (user.Identity != null)
                    if (user.Identity.IsAuthenticated)
                        return true;
            return false;
        }

        public override Task Invoke(IOwinContext context)
        {
            if (IsAuthenticated(context))
                return Next.Invoke(context);
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                var auth = context.Response.Context.Authentication;
                return Helpers.GetCompletedTask();
            }
        }

        
    }
}
