using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebApplication.Application;
using WebApplication.Application.Authorization;
using WebApplication.Application.Users.Commands;
using WebApplication.Middleware;
using WebApplication.Mongo;
using WebApplication.Mongo.Repositories;

namespace WebApplication
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
            var assembly = typeof(CreateUserCommand).GetTypeInfo().Assembly;
            services.AddMediatR(assembly);
            services.AddControllers();
            services.AddSwaggerGen();
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new MappingProfiles());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<MongoConfiguration>(Configuration.GetSection("MongoConfiguration"));
            services.AddSingleton<MongoDBContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IMeetingRepository, MeetingRepository>();
            services.AddScoped<IInvitationRepository, InvitationRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            var jwtTokenConfig = Configuration.GetSection("jwtTokenConfig").Get<JWTConfiguration>();
            services.AddSingleton(jwtTokenConfig);
            services.AddSingleton<IJWTService, JWTService>();
            services.AddSingleton<IHashService, HashService>();
            services.AddScoped<Application.Authorization.IAuthorizationService, AuthorizationService>();
            services.AddHostedService<JWTRefreshTokenCache>();
            services.AddHttpContextAccessor();
            services.AddCors(options =>
            {
                options.AddPolicy("VueCorsPolicy", builder =>
                {
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    // .AllowCredentials()
                    .AllowAnyOrigin();
                    //.WithOrigins("https://localhost:8080");
                });
            });
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    JwtBearerDefaults.AuthenticationScheme);

                defaultAuthorizationPolicyBuilder =
                    defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();

            
            app.UseRouting();
            app.UseCors("VueCorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
            });

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseMiddleware<MeetUpMiddleware>();
            app.UseMiddleware<JWTMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}