using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Absence.DataAccess.EFCore;
using Absence.DataAccess.Interfaces;
using Absence.DataAccess.Repositories;
using Absence.Service.AbsenceReportService;
using Absence.Service.CameraService;
using Absence.Service.ClassroomService;
using Absence.Service.SchoolService;
using Absence.Service.ScheduleService;
using Absence.Service.StudentService;
using Absence.Service.SubjectService;
using Absence.Service.TeacherService;
using Absence.Service.AutoMappingService;
using Absence.Service.StudentClassService;

namespace Absence.WebCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<AbsenceContext>(options => options.UseSqlServer(Configuration.GetConnectionString("local")));

            services.AddScoped<IAbsenceReportRepository, AbsenceReportRepository>();
            services.AddScoped<ICameraRepository, CameraRepository>();
            services.AddScoped<IClassroomRepository, ClassroomRepository>();
            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IStudentClassRepository, StudentClassRepository>();

            services.AddScoped<IAbsenceReportService, AbsenceReportService>();
            services.AddScoped<ICameraService, CameraService>();
            services.AddScoped<IClassroomService, ClassroomService>();
            services.AddScoped<IScheduleService, ScheduleService>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IStudentClassService, StudentClassService>();
            services.AddScoped<MappingService, MappingService>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
