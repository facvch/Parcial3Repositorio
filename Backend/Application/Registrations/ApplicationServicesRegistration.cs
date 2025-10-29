//using Application.ApplicationServices; // Descomentar si se usan servicios de aplicacion
using Application.UseCases.Game.Commands.GuessNumber;
using Application.UseCases.Game.Commands.StartGame;
using Application.UseCases.Player.Commands.RegisterPlayer;
using Core.Application;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Registrations
{
    /// <summary>
    /// Aqui se deben registrar todas las dependencias de la capa de aplicacion
    /// </summary>
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));

            services.AddPublishers();
            services.AddSubscribers();

            services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<ICommandQueryBus, MediatrCommandQueryBus>();

            services.AddTransient<IRequestHandler<RegisterPlayerCommand, Application.DataTransferObjects.RegisterPlayerResponse>, RegisterPlayerHandler>();
            services.AddTransient<IRequestHandler<StartGameCommand, Application.DataTransferObjects.StartGameResponse>, StartGameHandler>();
            services.AddTransient<IRequestHandler<GuessNumberCommand, Application.DataTransferObjects.GuessNumberResponse>, GuessNumberHandler>();

            return services;
        }

        private static IServiceCollection AddPublishers(this IServiceCollection services)
        {
            //Aca irian registrados los handlers que publican en el bus de eventos
            return services;
        }

        private static IServiceCollection AddSubscribers(this IServiceCollection services)
        {
            //Aca irian registrados los handlers que se suscriben al bus de eventos
            return services;
        }
    }
}
