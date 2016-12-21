using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;
using WhereWereWe.Api.Configuration;
using WhereWereWe.Repositories.Configuration;
using WhereWereWe.Services.Configuration;

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
            {
                config.AddProfile<SeriesMappingProfile>();
                config.AddProfile<UserMappingProfile>();
                config.AddProfile<SeriesProgressMappingProfile>();
                config.AddProfile<ViewModelsMappingProfile>();
            }).CreateMapper();

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

            services.AddCors();

            services.AddOptions();

            var jwtOptions = Configuration.GetSection("Authentication:Jwt");

            services.Configure<TokenIssuerOptions>(options =>
            {
                options.Issuer = "WhereWereWeApi";
                options.Audience = jwtOptions["Audience"];
                options.ValidFor = new TimeSpan(1, 0, 0, 0);
                options.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions["Key"])), SecurityAlgorithms.HmacSha256);
            });
            
            services.AddRepositoryDbContexts(Configuration.GetConnectionString("WhereWereWeConnectionString"));
            services.AddRepositories();

            services.AddWhereWereWeServices();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<TokenIssuerOptions> jwtOptions)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.WithOrigins("*"));

            //var facebookOptions = Configuration.GetSection("Authentication:Facebook");
            //app.UseFacebookAuthentication(new FacebookOptions
            //{
            //    AppId = facebookOptions["AppId"],
            //    AppSecret = facebookOptions["AppSecret"],
            //    Scope = { },
            //    Fields = { "name" }
            //});

            app.UseJwtBearerAuthentication(GetBearerOptions(jwtOptions.Value));

            app.UseMvc();
        }

        private JwtBearerOptions GetBearerOptions(TokenIssuerOptions jwtOptions)
        {
            return new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = jwtOptions.SigningCredentials.Key,

                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,

                    ValidateLifetime = true
                }
            };
        }
    }
}
