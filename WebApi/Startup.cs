using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApi.Configurations;
using WebApi.Data.Database;
using WebApi.Data.Database.Models;
using WebApi.Services.Business;
using WebApi.Services.Repositories;

namespace WebApi
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
            // Automapper
            services.AddAutoMapper(typeof(Startup));

            // Database
            services.AddDbContext<EfCorePresentationContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(ApplicationSettings.EfCorePresentationConnectionString));
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()));
            });

            // Repositories
            services.AddScoped<IBaseRepository<EmployeeRole>, BaseRepository<EmployeeRole>>();
            services.AddScoped<IDatabaseEntityRepository<Company>, DatabaseEntityRepository<Company>>();
            services.AddScoped<IDatabaseEntityRepository<Employee>, DatabaseEntityRepository<Employee>>();
            services.AddScoped<IDatabaseEntityRepository<Role>, DatabaseEntityRepository<Role>>();

            // Business
            services.AddScoped<ICompanyBusiness, CompanyBusiness>();
            services.AddScoped<IEmployeeBusiness, EmployeeBusiness>();
            services.AddScoped<IRoleBusiness, RoleBusiness>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
