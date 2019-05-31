using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebServis.Models;

namespace WebServis
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var konekcioniString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mlade\Documents\Visual Studio 2017\Projects\MojaWebAplikacija\Studije.mdf;Integrated Security=True;Connect Timeout=30";
            services.AddDbContext<CUSERSMLADEDOCUMENTSVISUALSTUDIO2017PROJECTSMOJAWEBAPLIKACIJASTUDIJEMDFContext>(options => options.UseSqlServer(konekcioniString));
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
