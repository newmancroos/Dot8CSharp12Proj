using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataContext.Sqlite
{
    public static class NorthwindContextExtensions
    {
        public static IServiceCollection AddNorthwindContext(this IServiceCollection services, string relativePath = "..", string databaseName = "Northwind.db")
        {
            string path = Path.Combine(relativePath, databaseName);
            path = Path.GetFullPath(path);

            NorthwindContextLogger.WriteLine($"Database path : {path}");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(message: $"{path} not found", fileName: path);
            }

            services.AddDbContext<NorthwindContext>(optionsAction: options =>
            {
                options.UseSqlite($"Data Source ={path}");
                options.LogTo(NorthwindContextLogger.WriteLine, new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);

            return services;
        }
    }
}
