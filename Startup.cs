﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using recipeconfigurationservice.Data;
using recipeconfigurationservice.Services;
using recipeconfigurationservice.Services.Interfaces;
using recipeconfigurationservice.ETLClass;
using recipeconfigurationservice.ETLClass.Interface;

namespace recipeconfigurationservice
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
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("ExtractDb")));

            services.AddTransient<IExtractService,ExtractService>();
            services.AddTransient<ILoadService,LoadService>();
            services.AddTransient<ITransformService,TransformService>();
            services.AddTransient<IHttpOtherApi,HttpOtherApi>();
            services.AddTransient<IJson,Json>();

            services.AddMvc().AddJsonOptions(options => {
    options.SerializerSettings.ReferenceLoopHandling
      = ReferenceLoopHandling.Ignore;
});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
