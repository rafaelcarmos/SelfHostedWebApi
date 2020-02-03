using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Application;
using Application.Default.Queries;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.OpenApi.Models;

namespace SelfHostedWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddControllers(o => o.Filters.Add<AuthorizationFilter>());
                    services.AddMediatR(typeof(DefaultGetQuery).Assembly);
                    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
                    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceTraceBehaviour<,>));
                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Title = "SelfHosted .NET Core WebApi",
                            Version = "v1"
                        });
                        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                        {
                            Description = "JWT Authorization header using the Bearer scheme.",
                            Type = SecuritySchemeType.Http,
                            Scheme = "bearer"
                        });
                        c.AddSecurityDefinition("Basic", new OpenApiSecurityScheme
                        {
                            Description = "JWT Authorization header using the Basic scheme.",
                            Type = SecuritySchemeType.Http,
                            Scheme = "basic"
                        });
                        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "Bearer",
                                        Type = ReferenceType.SecurityScheme
                                    }
                                }, new List<string>()
                            }
                        });
                        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme
                                {
                                    Reference = new OpenApiReference
                                    {
                                        Id = "Basic",
                                        Type = ReferenceType.SecurityScheme
                                    }
                                }, new List<string>()
                            }
                        });
                    });
                });

    }
}
