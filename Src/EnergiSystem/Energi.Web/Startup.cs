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
using Energi.Web.Hubs;
using Microsoft.AspNetCore.ResponseCompression;
using Energi.Service.HeatingService;
using Energi.Service.DatabaseService;

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
            //ServiceSettings serviceSettings = Configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            // Add database to IOC, from extentions.
            services.PrepareMongo()
            .AddMongoDB<Device>("Devices");

            // Add hubs.
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            // Add services.
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IDatabaseService, DatabaseService>();
            services.AddSingleton<IMqttService, MqttService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddHostedService<MessageHostedService>();
            services.AddTransient<IHeatingService, HeatingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

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
                endpoints.MapHub<DeviceHub>("/hub/devicehub");
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
