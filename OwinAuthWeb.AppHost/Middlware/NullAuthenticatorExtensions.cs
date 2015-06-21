// Copyright (c) Sasha Cikusa, Inc. All rights reserved. Apache License v2.0

using System;
using Microsoft.Owin.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using OwinAuthWeb.AppHost.Middleware.NullAuthenticator;

namespace OwinAuthWeb.AppHost.Middleware
{
    public static class NullAuthenticatorExtensions
    {
        public static IAppBuilder UseNullAuthenticator(this IAppBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            builder.Use(typeof(NullAuthenticatorMiddleware));
            builder.UseStageMarker(PipelineStage.Authenticate);
            return builder;
        }
    }
}
