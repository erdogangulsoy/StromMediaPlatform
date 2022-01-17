using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace StromMediaPlatform
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(x =>
            {
                x.MultipartBodyLengthLimit = 500000000; //428mb

            });

            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"C:\videos"));

        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseForwardedHeaders();
            app.UseSerilogRequestLogging();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
