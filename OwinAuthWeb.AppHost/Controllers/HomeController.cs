// Copyright (c) Sasha Cikusa, Inc. All rights reserved. Apache License v2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinAuthWeb.AppHost.Controllers
{
    [RoutePrefix("api")]
    public class HomeController : ApiController
    {
        [HttpGet]
        [AllowAnonymous]
        [Route("info")]
        public HttpResponseMessage GetInfo()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Are you sure?", DateTime = DateTime.Now });
        }

        [HttpGet]
        [Authorize(Roles = "ohnos")]
        [Route("ohnos")]
        public HttpResponseMessage GetOhNos()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new { Message = "Oh Nos!", DateTime = DateTime.Now });
        }
    }
}
