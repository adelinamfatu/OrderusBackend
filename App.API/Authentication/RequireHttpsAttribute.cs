﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace App.API.Authentication
{
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var req = actionContext.Request;

            if (req.RequestUri.Scheme == Uri.UriSchemeHttps) return;

            var html = "<p>Https required.</p>";

            if (req.Method.Method == "GET")
            {
                actionContext.Response = req.CreateResponse(HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");

                var uriBuilder = new UriBuilder(req.RequestUri)
                {
                    Scheme = Uri.UriSchemeHttps,
                    Port = 9000
                };

                actionContext.Response.Headers.Location = uriBuilder.Uri;
            }
            else
            {
                actionContext.Response = req.CreateResponse(HttpStatusCode.NotFound);
                actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
            }
        }
    }
}
