using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using web_api.Data.AppDb.Context;
using web_api.Data.AuthDb.Context;
using web_api.Services;

namespace web_api
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
            services.AddDbContext<AppdbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("BasedatosConnection")));

            services.AddDbContext<AuthDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("BaseAuthConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
               .AddEntityFrameworkStores<AuthDbContext>();

            services.AddAuthentication(x =>
            {
                // Leer Data Token
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                // Autenticacion
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

                // Sin esto tira 401
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.Configure<IdentityOptions>(options =>
                {
                    // Contraseñas
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 10;
                    options.Password.RequireNonAlphanumeric = false;

                    //  Tiempo fuera
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                    options.Lockout.MaxFailedAccessAttempts = 10;

                    // Usuario
                    options.User.RequireUniqueEmail = true;
                }
            );

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<ICarteraService, CarteraService>();
            services.AddSingleton<ICalculoService, CalculoService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<ITransaccionService, TransaccionService>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "web_api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "web_api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Para usar la autenticacion con el modelo de Identity
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
