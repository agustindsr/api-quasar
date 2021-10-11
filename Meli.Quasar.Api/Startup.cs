using Meli.Quasar.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Meli.Quasar.Api.AutoMapper;
using Meli.Quasar.Api.Exceptions;
using Meli.Quasar.Api.Filters;
using Meli.Quasar.Api.Middleware;
using Meli.Quasar.Api.Swagger;
using Meli.Quasar.Common.Configurations;
using Meli.Quasar.DataAccess.Interface;
using Meli.Quasar.Service;
using Meli.Quasar.Service.Interface;

namespace Meli.Quasar.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
           
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorDetailModel), StatusCodes.Status400BadRequest));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorDetailModel), StatusCodes.Status500InternalServerError));
                options.Filters.Add(typeof(ModelStateValidateAttribute));
                options.Filters.Add(typeof(ExceptionsAttribute));
            }).AddNewtonsoftJson();


            #region Servicio de Logs Request/Response

            services.AddTransient<RequestResponseLoggingMiddleware>();

            #endregion

            #region Configuracion ApiBehaviorOptions

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
                options.SuppressConsumesConstraintForFormFileParameters = true;
            });

            #endregion

            #region Autommaper

            services.AddAutoMapper(typeof(CommunicationProfile));

            #endregion

            #region Open Api (swagger)

            services.AddSwaggerGenForService();
            services.AddSwaggerGenNewtonsoftSupport();

            #endregion

            #region Versionado de la Api

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddApiVersioning(
                options =>
                {
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ReportApiVersions = true;
                });

            #endregion

            #region HealthChecks

            services.AddHealthChecks();

            #endregion

            #region IOption

            services.Configure<ApiConnectConfigurationOptions>(Configuration.GetSection("ApiConnect"));
            services.Configure<OpenApiInfoConfigurationOptions>(Configuration.GetSection("OpenApiInfo"));

            #endregion

            #region Configuration Injection Dependency

            services.AddTransient<ICommunicationService, CommunicationService>();
            services.AddSingleton<ICommunicationRepository, CommunicationRepository>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IInvalidResponseBuilder, InvalidResponseBuilder>();

            #endregion
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="provider"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseApiVersioning();

            app.UseRequestResponseLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger(c => { c.SerializeAsV2 = true;});

            app.UseSwaggerUI(provider.SwaggerOptionUi);

            app.UseHealthChecks("/health");
       
        }
    }
}
