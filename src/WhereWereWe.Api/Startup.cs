using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using WhereWereWe.Domain.Interfaces;
using WhereWereWe.Repositories;

namespace WhereWereWe.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var mapper = new MapperConfiguration(config =>
                    config.AddProfile<SeriesMappingProfile>())
                .CreateMapper();

            // Do not allow application to start with broken configuration. Fail fast.
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            services.AddSingleton(mapper);

            services.AddMvc().AddJsonOptions(o =>
            {
                var serializerSettings = o.SerializerSettings;

                // Pretty-print
                serializerSettings.NullValueHandling = NullValueHandling.Ignore;
                serializerSettings.Formatting = Formatting.Indented;
                serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                serializerSettings.Converters.Add(new StringEnumConverter());

                JsonConvert.DefaultSettings = () => serializerSettings;
            });

            services.AddDbContext<SeriesContext>(options =>
                options.UseSqlServer(Configuration.GetSection("ConnectionStrings")["WhereWereWeConnectionString"]));

            services.AddTransient<ISeriesRepository, SeriesRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
