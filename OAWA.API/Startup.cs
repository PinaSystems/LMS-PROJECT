using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using OAWA.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using OAWA.API.Services;
using Microsoft.Extensions.FileProviders;
using System.IO;
using OAWA.Data.Helpers;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Jupiter.API;
using OAWA.Data.Models;

namespace OAWA.API
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
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(x => x.UseMySql(connectionString, b => b.MigrationsAssembly("OAWA.API")));
            
            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequiredLength = 4;

            });

            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(options =>
                            {
                                options.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                                        .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                                    ValidateIssuer = false,
                                    ValidateAudience = false,
                                    ValidateLifetime = true,
                                    ClockSkew = TimeSpan.Zero
                                };

                                options.Events = new JwtBearerEvents
                                {
                                    OnAuthenticationFailed = context =>
                                    {
                                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                                        {
                                            context.Response.Headers.Add("Token-Expired", "true");
                                        }
                                        return Task.CompletedTask;
                                    }
                                };
                            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireSuperAdminRole", policy => { policy.RequireRole("SuperAdmin"); });
                options.AddPolicy("RequireAdminRole", policy => { policy.RequireRole("SuperAdmin", "CompanyAdmin"); });
                options.AddPolicy("RequireMemberRole", policy => { policy.RequireRole("SuperAdmin", "CompanyAdmin", "EndUser"); });
            });


            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    Configuration["EmailSender:Host"],
                    Configuration.GetValue<int>("EmailSender:Port"),
                    Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    Configuration["EmailSender:UserName"],
                    Configuration["EmailSender:Password"],
                    Configuration["EmailSender:EmailFrom"]
                )
            );


            services.AddSignalR();
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddApiVersioning(config =>
    {
        config.DefaultApiVersion = new ApiVersion(1, 0);
        config.AssumeDefaultVersionWhenUnspecified = true;
        config.ReportApiVersions = true;
        config.ApiVersionReader = new HeaderApiVersionReader("X-version");

    });
    services.AddSession(options => {   
                options.IdleTimeout = TimeSpan.FromMinutes(30);
    });
            services.BuildServiceProvider().GetService<DataContext>().Database.Migrate();
            services.AddCors();
            
            Mapper.Reset();
            services.AddAutoMapper();
            services.AddTransient<Seed>();
            services.AddTransient<IImageHandler, ImageHandler>();
            services.AddSingleton<IImageWriter, ImageWriter>();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<INewsletterRepository, NewsletterRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();
            services.AddScoped<INuggetRepository, NuggetRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder)
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
                        if (error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message);
                            await context.Response.WriteAsync(error.Error.Message);

                        }
                    });
                });
                // app.UseHsts();
            }
            app.UseSession();
            seeder.SeedUsers();
            //app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader()
            .AllowAnyMethod());//.WithExposedHeaders("Token-Expired"));
            app.UseAuthentication();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                 Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles")),
                RequestPath = "/StaticFiles",
                ServeUnknownFileTypes = true,
                DefaultContentType = "application/json"
            });
            app.UseMvc(routes =>
            {
                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { Controller = "Fallback", Action = "Index" }
                );
            });
            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = 1 });
        }
    }
}
