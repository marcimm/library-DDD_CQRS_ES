using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMM.Library.Application.Interfaces;
using MMM.Library.Application.Services;
using MMM.Library.Domain.Core.EvetSourcing;
using MMM.Library.Domain.Core.Interfaces;
using MMM.Library.Domain.Core.Mediator;
using MMM.Library.Domain.Core.Notifications;
using MMM.Library.Domain.CQRS.Commands;
using MMM.Library.Domain.CQRS.Events;
using MMM.Library.Domain.CQRS.Handlers;
using MMM.Library.Domain.CQRS.Interfaces;
using MMM.Library.Domain.CQRS.Queries;
using MMM.Library.Domain.Interfaces;
using MMM.Library.Infra.CrossCutting.Email.SendGridProvider;
using MMM.Library.Infra.CrossCutting.Email.SmtpClientEmail;
using MMM.Library.Infra.CrossCutting.Identity.Authorization.CustomPolices;
using MMM.Library.Infra.CrossCutting.Identity.DbContext;
using MMM.Library.Infra.CrossCutting.Identity.Models;
using MMM.Library.Infra.CrossCutting.Identity.Services;
using MMM.Library.Infra.CrossCutting.Logging;
using MMM.Library.Infra.CrossCutting.Logging.AspNetFilter;
using MMM.Library.Infra.CrossCutting.Logging.AspNetFilter.CustomExeception;
using MMM.Library.Infra.CrossCutting.Logging.KissLogProvider;
using MMM.Library.Infra.Data.Context;
using MMM.Library.Infra.Data.EventSourcing;
using MMM.Library.Infra.Data.Repository;
using MMM.Library.Infra.Data.UoW;

namespace MMM.Library.Infra.CrossCutting.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {           
            // Mediator         
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Email Service : Select Provider
            // SendGrid Provider -> https://sendgrid.com/
            services.AddScoped<IEmailService, SendGridEmailService>();
            services.Configure<SendGripApiOptions>(configuration);
            // Smtp Client
            //services.AddScoped<IEmailService, SmtpEmailService>();
            //services.Configure<SmtpConfiguration>(configuration.GetSection("SmtpConfiguration"));

            // Application Layer
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<IBookAppService, BookAppService>();

            // Domain - Notifications
            services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<BookingItemCommandAdd, bool>, BookingCommandHandler>();
            
            // Domain - Events
            services.AddScoped<INotificationHandler<BookingItemEventAdded>, BookingEventHandler>();
            
            // Domain - Queries
            services.AddScoped<IBookQueries, BookQueries>();

            // Infra - Data  
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventSourcingRepository, EventSourcingRepository>();

            // Infra - Unit Of Work  
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Infra - CrossCutting - Identity  
            services.AddScoped<IUser, AspNetUser>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<IAuthorizationHandler, ClaimsAuthPoliceHandler>();

            // EF Contexts ---
            services.AddScoped<LibraryDbContext>();
            services.AddScoped<EventSourcingDbContext>();
            services.AddScoped<ApplicationDbContext>();

            // Infra - Filters
            services.AddScoped<AuditFilter>();
            services.AddScoped<GlobalActionLogger>();
            services.Configure<SystemInfo>(configuration.GetSection("SystemInfo"));
            //services.AddScoped<HttpResponseExceptionFilter>();            

            return services;
        }
    }
}
