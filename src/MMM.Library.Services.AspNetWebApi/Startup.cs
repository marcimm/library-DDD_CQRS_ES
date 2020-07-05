using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MMM.Library.Application.AutoMapper;
using MMM.Library.Infra.CrossCutting.Identity.ApiConfiguration;
using MMM.Library.Infra.CrossCutting.Identity.UsersSedeer;
using MMM.Library.Infra.CrossCutting.IoC;
using MMM.Library.Infra.CrossCutting.Logging.AspNetFilter;
using MMM.Library.Infra.CrossCutting.Logging.AspNetFilter.CustomExeception;
using MMM.Library.Infra.CrossCutting.Logging.KissLogProvider;
using MMM.Library.Services.AspNetWebApi.Configurations;
using System;
using System.Linq;

namespace MMM.Library.Services.AspNetWebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true);

            if (env.IsDevelopment())
            {
                // Adicionar UserSecrets 
                builder.AddUserSecrets<Startup>();
            }

            //builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddKissLogSetup(Configuration);

            services.AddSession();
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add(typeof(GlobalActionLogger));
                //options.Filters.Add(typeof(HttpResponseExceptionFilter));
            }
            ).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddDatabaseSetup(Configuration);
            services.AddIdentitySetup(Configuration);
            services.AddAuthSetup(Configuration);
            services.AddSwaggerSetup();
                        
            services.AddAutoMapper(typeof(AutoMapperSetup));
            services.AddMediatR(typeof(Startup));

            // Dependencias 
            services.ResolveDependencies(Configuration);

            // Api Versioning
            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            // Gzip Compression
            //services.AddResponseCompression(options =>
            //{
            //    options.Providers.Add<GzipCompressionProvider>();
            //    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/json" });
            //});

            // Caching all Api
            // services.AddResponseCaching();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicyDevelopment",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
                              IServiceProvider serviceProvider)

        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("CorsPolicyDevelopment");
            }
            else
            {
                app.UseExceptionHandler("/error");
                // app.UseCors("CorsPolicyDevelopment");
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseGlobalizationSetup();

            app.UseAuthentication(); 
            app.UseAuthorization();            
            app.UseSession();

            // -> app.UseKissLogMiddleware() must to be referenced after app.UseAuthentication(), app.UseSession()              
            app.UseKissLogSetup(Configuration);
            // -<

            app.UseSwaggerSetup();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Add new Dummy Users: 
            // user1 name: Admin, user1 password: Admin@123
            // user2 name: User, user2 password: User@123
            //DummyUsersSedeer.AddUsersWithRoles(serviceProvider).Wait();
        }
    }
}
