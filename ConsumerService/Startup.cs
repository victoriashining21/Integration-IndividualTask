using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumerService.Contracts;
using ConsumerService.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsumerService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<NewsContext>(options => options.UseSqlServer(connection));

            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = Configuration.GetSection("RabbitMq").GetSection("UserName").Value,
                Password = Configuration.GetSection("RabbitMq").GetSection("Password").Value,
                VirtualHost = Configuration.GetSection("RabbitMq").GetSection("VirtualHost").Value,
                HostName = Configuration.GetSection("RabbitMq").GetSection("HostName").Value
            };


            IConnection conn = factory.CreateConnection();
            IModel model = conn.CreateModel();
            model.QueueDeclare("news", false, false, false, null);

            services.AddSingleton(model);
            services.AddTransient(typeof(IRepository<>),typeof(Repository<>));

            services.AddHostedService<NewsConsumer>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Consumer Service is running!");
                });
            });
        }
    }
}
