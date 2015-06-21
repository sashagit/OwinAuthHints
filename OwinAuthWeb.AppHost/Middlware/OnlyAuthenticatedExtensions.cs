// Copyright (c) Sasha Cikusa, Inc. All rights reserved. Apache License v2.0

using Owin;
using Microsoft.Owin.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OwinAuthWeb.AppHost.Middleware.OnlyAuthenticated;

namespace OwinAuthWeb.AppHost.Middleware
{
    public static class OnlyAuthenticatedExtensions
    {
        public static IAppBuilder UseOnlyAuthenticated(this IAppBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            builder.Use(typeof(OnlyAuthenticatedMiddleware));
            builder.UseStageMarker(PipelineStage.Authorize);
            return builder;
        }
    }
}
