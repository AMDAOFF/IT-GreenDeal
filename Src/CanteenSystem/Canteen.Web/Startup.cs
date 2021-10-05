using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Canteen.Service.UserService;
using Canteen.Service.LoginService;
using Canteen.Service.EncryptionService;
using Canteen.Service.RegisterService;
using Canteen.Service.DishService;
using Canteen.Service.IngridentsService;
using Canteen.Service.AllergyService;
using Microsoft.AspNetCore.Http;
using Canteen.Service.UserAllergyService;

namespace Canteen.Web
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
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<IDishService, DishService>();
            services.AddScoped<IIngredientsService, IngredientsService>();
            services.AddScoped<IAllergyService, AllergyService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUserAllergyService, UserAllergyService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
