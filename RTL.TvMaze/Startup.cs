using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RTL.TvMaze.DbContexts;
using RTL.TvMaze.Repository;
using RTL.TvMaze.Services;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace RTL.TvMaze
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
            var config = new Config();
            Configuration.GetSection("TvMaze").Bind(config);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(Configuration.GetConnectionString("TvMaze")));

            services.AddScoped<DbContext, DatabaseContext>();
            services.AddScoped<IShowRepository, ShowRepository>();
            services.AddScoped<ICastRepository, CastRepository>();
            services.AddScoped<IShowService, ShowService>();
            services.AddScoped<IScraper, Scraper>();

            // todo
            //   services.AddHttpClient<IShowService, ShowService>(client => client.BaseAddress = new Uri(config.BaseUrl))
            //      .SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "TvMaze API",
                    Description = "RTL TvMaze API",
                    TermsOfService = "None"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TvMaze API V1");
            });
        }
    }
}
