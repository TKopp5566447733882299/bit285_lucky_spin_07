﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuckySpin.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LuckySpin
{
    public class Startup
    {
        private readonly IConfiguration _config;

        // TODO: Inject configuration into Startup via it's constructor (see slides)
        //  this will be used to read the database connection string from appsetting.json
        public Startup(IConfiguration config)
        {
            _config = config;
        }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<Models.TextTransformService>();
            services.AddDbContext<LuckySpinContext>(options =>options.UseSqlServer(_config.GetConnectionString("LuckySpinDb")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseDeveloperExceptionPage();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller}/{action}/{id:long?}",
                     defaults: new { controller = "Spinner", action = "Index" }
                );
            });
            app.UseStaticFiles();
        }
    }
}
