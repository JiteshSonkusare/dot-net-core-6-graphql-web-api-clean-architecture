using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using WebAPI.GraphQL.Mutation;
using WebAPI.GraphQL.Queries;

namespace WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static void RegisterGrapQLDependencies(this IServiceCollection services)
        {
            services.AddRouting()
                    .AddGraphQLServer()
                    .AddProjections()
                    .AddFiltering()
                    .AddSorting()
                    .AddQueryType<UserQuery>()
                    .AddMutationType<UserMutation>();
        }

        internal static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<CleanArchitectureDBContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}
