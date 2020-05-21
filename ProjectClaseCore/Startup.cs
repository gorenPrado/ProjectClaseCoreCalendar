using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProjectClaseCore.Data;
using ProjectClaseCore.Repositorio;
using ProjectClaseCore.Token;

namespace ProjectClaseCore
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
            String cadena = Configuration.GetConnectionString("cadena");
            services.AddControllers();
            services.AddDbContext<ContextoProject>(options => options.UseSqlServer(cadena));
            services.AddSwaggerGen(s => s.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "Api CRUD Departamentos", Version = "v1", Description = "Seguimos" }));
            HelperToken helper = new HelperToken(Configuration);
            services.AddAuthentication(helper.GetAuthOptions()).AddJwtBearer(helper.GetJwtOptions());
            services.AddTransient<RepositorioProject>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(
                c =>
                {
                    c.SwaggerEndpoint(
                        url: "/swagger/v1/swagger.json",
                        name: "Api v1"
                        );
                    c.RoutePrefix = "";
                }
                );

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
