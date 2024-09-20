using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using Nugaeva_Alsu_OZKT_42_21.Database;


using System;

namespace Nugaeva_Alsu_OZKT_42_21
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var logger = LogManager.Setup()
				.LoadConfigurationFromAppSettings()
				.GetCurrentClassLogger();

			try
			{
				builder.Logging.ClearProviders();
				builder.Host.UseNLog();

				builder.Services.AddControllers();
				builder.Services.AddEndpointsApiExplorer();
				builder.Services.AddSwaggerGen();

				// Контекст БД
				var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
				builder.Services.AddDbContext<NugaevaDbContext>(options =>
					options.UseSqlServer(connectionString));

				// Добавляем наши сервисы
				

                builder.Services.AddServices();

                var app = builder.Build();

				if (app.Environment.IsDevelopment())
				{
					app.UseSwagger();
					app.UseSwaggerUI();
				}

				app.UseAuthorization();

				app.MapControllers();

				app.Run();
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Stopped program because of exception");
			}
			finally
			{
				LogManager.Shutdown();
			}
		}

		private static void AddCustomServices(IServiceCollection services)
		{
			// Здесь добавляются наши пользовательские сервисы
			// Например:
			// services.AddSingleton<IYourService, YourServiceImpl>();
			// services.AddScoped<IScopeService, ScopeServiceImpl>();
		}
	}
}