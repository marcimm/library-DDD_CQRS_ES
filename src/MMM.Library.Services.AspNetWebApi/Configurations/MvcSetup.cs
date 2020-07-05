using Microsoft.Extensions.DependencyInjection;
using MMM.Library.Infra.CrossCutting.Logging.AspNetFilter;
using MMM.Library.Infra.CrossCutting.Logging.KissLogProvider;
using System;

namespace MMM.Library.Services.AspNetWebApi.Configurations
{
    public static class MvcSetup
    {
        public static IServiceCollection AddMvcSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(typeof(AuditFilter));

            //    // TODO: Custom Response
            //    //options.Filters.Add(new HttpResponseExceptionFilter());
            //}); //CompatibilityVersion: docs.microsoft.com/pt-br/aspnet/core/mvc/compatibility-version?view=aspnetcore-3.1


            return services;
        }
    }
}
