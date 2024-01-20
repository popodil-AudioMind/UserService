using Microsoft.AspNetCore.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Steeltoe.Discovery.Client;
using Steeltoe.Discovery.Eureka;
using System.Text;
using UserService;

/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();*/

namespace UserService
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                //.AddServiceDiscovery(options => options.UseEureka())
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .AddDiscoveryClient();
    }
}