using MadPay724.Data.DatabaseContext;
using MadPay724.Repository;
using MadPay724.Repository.Infrastructure;
using MadPay724.Services.Interface;
using MadPay724.Services.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using MadPay724.Common.Helpers;
using System.Linq;
using NSwag;
using System.Collections.Generic;
using NSwag.Generation.Processors.Security;
using AutoMapper;

namespace MadPay724
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
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddCors();

            services.AddScoped<IUnitOfWork<MadPayDbContext>, UnitOfWork<MadPayDbContext>>();
            services.AddScoped<IAuthService, AuthService>();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddOpenApiDocument(d =>
            {
            d.DocumentName = "Site";
            d.ApiGroupNames = new[] { "Site" };
            d.PostProcess = doc =>
            {
                doc.Info.Title = "api";
                doc.Info.Contact = new NSwag.OpenApiContact
                {
                    Name = "hamed",
                    Email = "hamed",
                    Url = ""
                };
                doc.Info.License = new NSwag.OpenApiLicense
                {
                    Name = "hamed",
                    Url = ""
                };
            };

            d.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authentication",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "My Authentication",

            });
            d.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor(name: "bearer"));

                
            });
            services.AddOpenApiDocument(d =>
            {
                d.DocumentName = "Api";
                d.ApiGroupNames = new[] { "Api" };
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if(error != null)
                        {
                            context.Response.AddAppError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    });
                });
            }

            app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseOpenApi(); // serve OpenAPI/Swagger documents
            app.UseSwaggerUi3(); // serve Swagger UI

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
