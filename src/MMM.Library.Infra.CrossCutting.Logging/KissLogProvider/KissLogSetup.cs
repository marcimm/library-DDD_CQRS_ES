using KissLog;
using KissLog.Apis.v1.Listeners;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Text;

namespace MMM.Library.Infra.CrossCutting.Logging.KissLogProvider
{
    public static class KissLogSetup
    {
        public static IServiceCollection AddKissLogSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Log provider: https://kisslog.net/
            services.AddScoped<ILogger>((context) =>
            {
                return Logger.Factory.Get();
            });

            return services;
        }


        public static IApplicationBuilder UseKissLogSetup(this IApplicationBuilder app, IConfiguration configuration)
        {
            // app.UseKissLogMiddleware() must to be referenced after app.UseAuthentication(), app.UseSession()
            app.UseKissLogMiddleware(options =>
            {
                ConfigureKissLog(options, configuration);
            });

            return app;
        }

        private static void ConfigureKissLog(IOptionsBuilder options, IConfiguration configuration)
        {
            // register KissLog.net cloud listener
            options.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
                configuration["KissLog.OrganizationId"],    //  "f6eb0676-a5b1-4e7a-b72f-15e484cbdb61"
                configuration["KissLog.ApplicationId"])     //  "9befd7ba-3ab2-4625-bac5-0b63bd8a8128"
            )
            {
                ApiUrl = configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
            });

            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };
        }

    }
}
