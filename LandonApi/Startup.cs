using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandonApi.Filters;
using LandonApi.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LandonApi
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
            services.AddMvc(opt =>
            {

                opt.Filters.Add(typeof(JsonExceptionFilter)); //add our exception filter we wrote to process exceptoins based on dev/prod environment

            var jsonFormatter = opt.OutputFormatters.OfType<JsonOutputFormatter>().Single(); // find the first instance in list where jsonoutputformatter is
            opt.OutputFormatters.Remove(jsonFormatter); // remove json formatter from list of output formatters.., we want our ion formatter in
            opt.OutputFormatters.Add(new IonOutputFormatter(jsonFormatter)); // so we have to add it
            }  // we are doing all this, creating infrastructure folder, adding ionoutputformatter class and wiring up to startup just to add
               // ION to the resopnse for JSON LOL application type: application/json to let people know we are 'ION compliant. i hope it does more
               // than change this, and actually does ION compliant responses.
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // we want to use lowercase instead of pascal case. whatever

            services.AddRouting(opt => opt.LowercaseUrls = true);

            // now install nuget package for versioning and then wire it up here
            services.AddApiVersioning(opt =>
            {
                // use media type versioning, although others are supported with this nuget version tool
                opt.ApiVersionReader = new MediaTypeApiVersionReader();
                opt.AssumeDefaultVersionWhenUnspecified = true; // specify default version if no version is requested from GET
                opt.ReportApiVersions = true; // show version in the headers. this is optional
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionSelector = new CurrentImplementationApiVersionSelector(opt); // select highest version automatically and use it 

            });

            // now go to the a controller like root controller and add your version in a selector
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
