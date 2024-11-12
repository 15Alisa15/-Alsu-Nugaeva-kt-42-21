using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Nugaeva_Alsu_OZKT_42_21.Database;
using Nugaeva_Alsu_OZKT_42_21.Interfaces.StudentsInterfaces;
using Nugaeva_Alsu_OZKT_42_21.Middlewares;
using Nugaeva_Alsu_OZKT_42_21.ServiceExtensions;
using System;

namespace Nugaeva_Alsu_OZKT_42_21
{
    public class Program
    {
        private const string DefaultConnectionName = "DefaultConnection";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logger = LogManager.Setup()
                .LoadConfigurationFromAppSettings()
                .GetCurrentClassLogger();

            try
            {
                builder.Host.UseNLog();

                // ��������� �������
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                // ������������ ��������
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // ��������� ������ �����������
                var connectionString = builder.Configuration.GetConnectionString(DefaultConnectionName) ??
                    throw new InvalidOperationException($"Connection string '{DefaultConnectionName}' not found.");

                // ���������� ��������� ���� ������
                builder.Services.AddDbContext<NugaevaDbContext>(options =>
                    options.UseSqlServer(connectionString));

                // ���������� ���������������� ��������
                //AddCustomServices(builder.Services);
                builder.Services.AddServices();

                var app = builder.Build();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseMiddleware<ExceptionHandlerMiddleware>();

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw; // ������������� ���������� � ����������� ��� �����
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

       /* private static void AddCustomServices(IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
        }*/
    }
}