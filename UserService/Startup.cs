using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using UserService.Context;
using UserService.Services;
using System.Net;
using UserService.Data;
using UserService.SQL;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;

namespace UserService
{
    public class Startup
    {
        private IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.SetIsOriginAllowed((host) => true)
                        //.WithOrigins("http://localhost:19006/")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            //DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            string connectionstring;
            if (_env.IsDevelopment()) connectionstring = Configuration.GetValue<string>("ConnectionStrings:DevConnection");
            else connectionstring = Configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
            try
            {
                services.AddDbContextPool<UserDatabaseContext>(
                    options => options.UseMySql(connectionstring.ToString(), ServerVersion.AutoDetect(connectionstring))
                );
            }
            catch (Exception)
            {
                throw;
            }
            
            services.AddScoped<ISqlUser, SqlUser>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RestAPI", Version = "v1" });
            });
            services.AddSignalR();

            //services.AddDiscoveryClient(Configuration);
            //services.AddDiscoveryClient();
            //services.AddServiceDiscovery(options => options.UseEureka());

            /*services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.TemporaryRedirect;
                options.HttpsPort = 443;
            });*/
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            DatabaseManagementService.MigrationInitialisation(app);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestAPI v1"));


            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}