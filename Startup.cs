using System;
using AutoMapper;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
//using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.AspNetCore.HttpsPolicy;
using UnityArenaApi.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using UnityArenaApi.Helpers;
using UnityArenaApi.Domain.IRepositories;
using UnityArenaApi.Domain.IServices;
using UnityArenaApi.Persistence.Repositories;
using UnityArenaApi.Services;

namespace UnityArenaApi
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

            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UnityArenaApi", Version = "v1" });
            });

            services.AddDbContext<AppDbContext>(optionsAction => optionsAction.UseSqlServer(
                Configuration.GetSection("connectionString").GetSection("connectionString").Value).
                UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            var appSettingSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingSection);

            var appSettings = appSettingSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>{
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IUnityOfWork,UnitOfWork>();
            services.AddScoped<ILobbyGameRepository,LobbyGameRepository>();
            services.AddScoped<IModeRepository,ModeRepository>();
            services.AddScoped<IPlayerInfoRepository,PlayerInfoRepository>();
            services.AddScoped<IPlayerLobbyGameRepository,PlayerLobbyGameRepository>();
            services.AddScoped<IPlayerRepository,PlayerRepository>();
            services.AddScoped<IPlayerRoleRepository,PlayerRoleRepository>();
            services.AddScoped<IRoleRepository,RoleRepository>();

            services.AddScoped<IAuthService,AuthService>();
            services.AddScoped<ILobbyGameService,LobbyGameService>();
            services.AddScoped<IModeService,ModeService>();
            services.AddScoped<IPlayerInfoService,PlayerInfoService>();
            services.AddScoped<IPlayerLobbyGameService,PlayerLobbyGameService>();
            services.AddScoped<IPlayerRoleService,PlayerRoleService>();
            services.AddScoped<IPlayerService,PlayerService>();
            services.AddScoped<IRoleService,RoleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UnityArenaApi v1"));
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
