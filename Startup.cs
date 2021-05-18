using DummyProjectApi.DataContext;
using DummyProjectApi.Repositories.RegistrationRepository;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;

namespace DummyProjectApi
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
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Dot Net Core With React Dummy Project",
                        Description = "Basics Of Dot Net Core",
                        Version = "v1"
                    });
                // To Display Model Description On Swagger
                var filename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filepath = Path.Combine(AppContext.BaseDirectory, filename);
                options.IncludeXmlComments(filepath);
            });
            services.AddHealthChecks()
                    .AddSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            services.AddHealthChecks();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(x => x.WithOrigins("https://localhost:44337"));
                options.AddPolicy("ReactPolicy", x => x.WithOrigins("http://localhost:3000"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseHealthChecksUI();

            app.UseEndpoints(endpoints =>
            { 
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
               
            });

            app.UseHealthChecksUI();
        }
    }
}
