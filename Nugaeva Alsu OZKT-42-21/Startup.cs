using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nugaeva_Alsu_OZKT_42_21.Interfaces.StudentsInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;


namespace Nugaeva_Alsu_OZKT_42_21
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Этот метод вызывается запускаемым временем. Используйте этот метод для добавления сервисов в контейнер.
        public void ConfigureServices(IServiceCollection services)
        {
            // Добавляем контроллеры
            services.AddControllers();

            // Настройка Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nugaeva_Alsu_OZKT_42_21", Version = "v1" });
            });

            // Добавляем наш метод AddServices() внутри ConfigureServices
            services.AddServices();
        }

        // Этот метод вызывается запускаемым временем. Используйте этот метод для конфигурации HTTP-запросного пайплайна.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nugaeva_Alsu_OZKT_42_21 v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    // Определяем метод расширения AddServices()
    public static class StartupExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Здесь вы можете добавить регистрацию ваших сервисов
            services.AddTransient<IStudentService, StudentService>();

            return services;
        }
    }
}