using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskFlow.Api.Data;

namespace TaskFlow.ContractTests
{
    public sealed class TaskFlowApiFactory : WebApplicationFactory<Program>
    {
        private readonly string _databasePath;
        private string ConnectionString =>$"Data Source={_databasePath};Pooling=False";

        public TaskFlowApiFactory(string databasePath)
        {
            _databasePath = databasePath;


            var directory = Path.GetDirectoryName(_databasePath);

            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, configuration) =>
            {
                configuration.AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        ["ConnectionStrings:DefaultConnection"] =ConnectionString
                    });
            });

            builder.ConfigureServices(services =>
            {
                var descriptors = services
                    .Where(descriptor =>
                        descriptor.ServiceType ==
                            typeof(DbContextOptions<TaskFlowDbContext>) ||
                        descriptor.ServiceType ==
                            typeof(TaskFlowDbContext))
                    .ToList();

                foreach (var descriptor in descriptors)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<TaskFlowDbContext>(options => options.UseSqlite(ConnectionString));
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = base.CreateHost(builder);

            // Inicializa o banco utilizando o provedor real da aplicação de teste,
            // evitando a criação de um segundo contêiner de dependências.
            using var scope = host.Services.CreateScope();

            var dbContext =
                scope.ServiceProvider.GetRequiredService<TaskFlowDbContext>();

            dbContext.Database.EnsureCreated();

            return host;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!disposing)
            {
                return;
            }

            DeleteDatabaseFile(_databasePath);
            DeleteDatabaseFile($"{_databasePath}-wal");
            DeleteDatabaseFile($"{_databasePath}-shm");
        }

        private static void DeleteDatabaseFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}