using Business.Context;
using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Records_Api.Models;
using Records_Api.Services;

namespace Records_Api
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
            services.AddDbContext<RecordContext>(options =>
                    options.UseSqlite(Configuration["ConnectionString_RecordDb"]));

            services.AddSingleton<Business.Interfaces.IHostedService, HostedService>();
            services.AddScoped<IRecordService, RecordService>();

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder => builder.WithOrigins(Configuration["WebClientUrl"])
                                                                              .AllowAnyMethod()
                                                                              .AllowAnyHeader()
                                                                              .AllowCredentials()
                            ));

            services.AddSignalR();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCors("CorsPolicy");

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ModifyRecordHub>(Configuration["Hub"]);
            });
        }
    }
}
