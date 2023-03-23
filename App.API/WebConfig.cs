using App.API.Authentication;
using Owin;
using System.Web.Http;

namespace App.API
{
    public class WebConfig
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            HttpConfiguration config = new HttpConfiguration();
            
            // Configure Route with Attributes
            config.MapHttpAttributeRoutes();

            config.Filters.Add(new RequireHttpsAttribute());

            appBuilder.UseWebApi(config);
        }
    }
}
