using Energi.DataAccess;
using Energi.DataAccess.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Energi.Service;
using Energi.Service.DeviceService;
using Energi.Extentions.Database;
using Energi.Service.MQTTService;
using Energi.Web.HostedService;
using Energi.Service.MessageService;

namespace Energi.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ServiceSettings serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            // Add database to IOC, from extentions.
            services.PrepareMongo()
            .AddMongoDB<Device>("Devices");

            // Add services.
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddSingleton<IMqttService, MqttService>();
            services.AddSingleton<IMessageService, MessageService>();

            // RabbitMQ.
            //services.AddMassTransit(x =>
            //{
            //    x.UsingRabbitMq((context, configurater) =>
            //    {
            //        RabbitMQSettings rabbitMQSettings = Configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
            //        configurater.Host(rabbitMQSettings.Host);
            //        configurater.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(serviceSettings.ServiceName, false));

            //    });
            //});

            //services.AddMassTransitHostedService();

            // Hosted services.
            //services.AddHostedService<MessageHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
