using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Query;
using VgFc.Test.Dto;

namespace VgFc.Test.Services
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var qryAttributes = new EnableQueryAttribute()
            {
                AllowedArithmeticOperators = AllowedArithmeticOperators.None
            };
            config.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
            //
            GlobalConfiguration.Configuration.MessageHandlers.Add(new BasicAuthenticationHandler(new AuthenticationService()));
            //
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Product>("Products");

            // Web API routes
            config.MapODataServiceRoute
            (
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel()
            );
            /*
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute
            (
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            */
        }
    }
}