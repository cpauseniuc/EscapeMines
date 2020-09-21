using Microsoft.Extensions.DependencyInjection;
using Escape.Mines.Domain.Interfaces.Repositories;
using Escape.Mines.Infrastructure.Repositories;

namespace Escape.Mines.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGameBoardRepository), typeof(GameBoardRepository));
            services.AddTransient(typeof(ITurtleRepository), typeof(TurtleRepository));
            return services;
        }

    }
}
