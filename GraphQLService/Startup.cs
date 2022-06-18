using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsumerService.Contracts;
using ConsumerService.DAL;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQLService.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Caching.Redis;
using StackExchange.Redis;

namespace GraphQLService
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

            services.AddScoped<IDependencyResolver>(_ => new
                FuncDependencyResolver(_.GetRequiredService));
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDocumentWriter, DocumentWriter>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<NewsQuery>();
            services.AddScoped<NewsType>();
            services.AddScoped<ISchema, GraphQLDemoSchema>();

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            // Redis Server
            var cnstring = $"{Configuration.GetSection("Redis").GetSection("Server").Value}:{Configuration.GetSection("Redis").GetSection("Port").Value}";

            var redisOptions = new RedisCacheOptions
            {
                ConfigurationOptions = new ConfigurationOptions()
            };
            redisOptions.ConfigurationOptions.EndPoints.Add(cnstring);
            var opts = Options.Create<RedisCacheOptions>(redisOptions);

            IDistributedCache cache = new RedisCache(opts);
            services.AddSingleton(cache);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseGraphiql("/graphiql", options =>
            {
                options.GraphQlEndpoint = "/graphql";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(_ => { _.SwaggerEndpoint("/swagger/v1/swagger.json", "API docs"); });
        }
    }
}
