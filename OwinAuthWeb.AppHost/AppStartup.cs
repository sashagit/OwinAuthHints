// Copyright (c) Sasha Cikusa, Inc. All rights reserved. Apache License v2.0

using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OwinAuthWeb.AppHost.Middleware;
using System.Web.Http;
using Microsoft.Owin.Diagnostics;
using Microsoft.Owin.Extensions;

namespace OwinAuthWeb.AppHost
{
    class AppStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNullAuthenticator();
            app.UseOnlyAuthenticated();

            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            app.UseWebApi(config);
            app.UseStageMarker(PipelineStage.MapHandler);

            app.Properties["host.AppMode"] = "diagnostics";
            app.UseWelcomePage("/");
        }

        public static void EnableErrorPage(IAppBuilder app)
        {
            var options = new ErrorPageOptions
            {
                ShowCookies = true,
                ShowHeaders = true,
                ShowQuery = true,
                ShowExceptionDetails = true,
                ShowSourceCode = true,
                SourceCodeLineCount = 5,
                ShowEnvironment = true
            };

            app.UseErrorPage(options);
        }
    }
}
