using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Serilog;
using Meli.Quasar.Common.Configurations;
using Meli.Quasar.Domain.Exceptions;

namespace Meli.Quasar.Api.Swagger
{
    /// <summary>
    /// SwaggerDefaultValues
    /// </summary>
    public static class SwaggerGenSettings
    {
        /// <summary>
        /// AddSwaggerGen
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerGenForService(this IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    if (XmlCommentsFilePath() != null)
                    {
                        options.IncludeXmlComments(XmlCommentsFilePath());
                    }

                    options.OperationFilter<SwaggerOperationFilter>();
                    options.EnableAnnotations();
                    options.CustomSchemaIds(type => type.FullName);
                });
        }

  
        private static string XmlCommentsFilePath()
        {
            string path = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

            return path;
        }
    }
}
