using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Core;

namespace Api
{
    public class Startup
    { //todo: Add projects to the sln file
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            // services.AddSingleton<IRepository<Request>, FakeRepo<Request>>(); //doc: Singleton to retain its memory
            services.AddSingleton(typeof(IRepository<>), typeof(FakeRepo<>)); //doc: Singleton to retain its memory
            services.AddTransient<IBank, FakeBank>();
            services.AddTransient<IRequestFactory, RequestFactory>();
            services.AddTransient<IResponseFactory, ResponseFactory>();
            services.AddTransient<IReportFactory, ReportFactory>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection(); //todo: enable https redirection for security

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
