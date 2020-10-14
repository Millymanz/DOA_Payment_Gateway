using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using System.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Payments.Storage.Repositories;
using Payments.Storage;
using Payments.Storage.Repositories.Interfaces;
using AutoMapper;
using Payments.Banking;
using Payments.Banking.Interfaces;

namespace PaymentGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionString"];
            services.AddTransient(x => new DataContext(connectionString));

            services.AddAutoMapper(typeof(Startup));

            services.AddApplicationInsightsTelemetry();

            services.AddScoped<ICurrencyCodeRepository, CurrencyCodeRepository>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IMerchantRepository, MerchantRepository>();

            services.AddSingleton<IBankService, BankServiceMock>();

            services.AddControllers().AddJsonOptions(jsonOptions =>
            {
                jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            // Registering Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DOA PaymentGateway API", Version = "v1.0" });
                c.EnableAnnotations();
            });

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DOA PaymentGateway API V1.0");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });


            TriggerDataMigrations(app);
        }

        private void TriggerDataMigrations(IApplicationBuilder app)
        {
            int maxAttempts = 2;
            int attempt = 0;

            while (attempt <= maxAttempts)
            {
                attempt++;

                try
                {
                    var dataContext = app.ApplicationServices.GetService<DataContext>();
                    dataContext.Database.Migrate();
                }
                catch (SqlException ex)
                {
                    var waitTimeInSeconds = 5;
                    var logger = app.ApplicationServices.GetService<ILogger<Startup>>();

                    logger.LogWarning($"Retrying in {waitTimeInSeconds} seconds :: " + ex);

                    Thread.Sleep(TimeSpan.FromSeconds(waitTimeInSeconds));
                }
            }
        }
    }
}
