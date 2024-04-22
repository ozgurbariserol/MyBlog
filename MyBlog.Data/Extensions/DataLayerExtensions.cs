using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyBlog.Data.Context;
using MyBlog.Data.Repositories.Abstractions;
using MyBlog.Data.Repositories.Concretes;
using MyBlog.Data.UnitOfWorks;

namespace MyBlog.Data.Extensions
{
    public static class DataLayerExtensions
    {
        public static IServiceCollection LoadDataLayerExtension(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IUnitOfWork,UnitOfWork>();

            return services;
        }
    }
}
