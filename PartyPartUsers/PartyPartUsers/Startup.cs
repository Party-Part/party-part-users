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
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using PartyPartUsers.Models;

namespace PartyPartUsers
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
            services.AddDbContext<UserContext>(opt =>
                opt.UseNpgsql("Host=rc1a-b123fvq685u6nb54.mdb.yandexcloud.net;Port=6432;Database=party-part;User Id=root;Password=rootroot;SslMode=Require;Trust Server Certificate=true"));
            services.AddControllers();
            services.AddCors(options => options.AddPolicy(
                "AllowAll", 
                builder => builder.WithOrigins("https://localhost:3000", "https://prtprt.ru")
                    .WithHeaders(HeaderNames.ContentType, "application/json")
                    .WithMethods("PUT", "DELETE", "GET", "OPTIONS", "POST")        
                    .AllowCredentials()
                    .AllowAnyHeader()
            ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}