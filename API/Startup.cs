using API.Context;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
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
            // Ini untuk CORS AllowAllOrigins
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            //});

            //services.AddControllers();
            //=============================================================================

            // untuk menambahkan Controller menggunakan Json
            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );

            // untuk menambahkan DbContext Baru
            //services.AddDbContext<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("APIContext")));
            // ini adalah menambahkan DbContext dengan LazyLoadingProxies
            services.AddDbContext<MyContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("APIContext")));

            // ini untuk JWT atau untuk Token dan Authorize
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => 
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidAudience = "Test",
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfsdfsjdbf78sdyfssdfsdfbuidfs98gdfsdbf"))
                };
            });

            services.AddScoped<EmployeeRepository>();
            services.AddScoped<AccountRepository>();
            services.AddScoped<RoleRepository>();
            services.AddScoped<AccountRoleRepository>();
            services.AddScoped<EducationRepository>();
            services.AddScoped<ProfilingRepository>();
            services.AddScoped<UniversityRepository>();

            //Ini syntax untuk menambahkan CORS WithOrigins dan harus paling bawah gini
            services.AddCors(c =>
            {
                //wajib tuh ditambahin .AllowAnyHeader().AllowAnyMethod().AllowCredentials() biar bisa dipakai semua header, method, dan credentialsnya
                c.AddPolicy("AllowOrigin", options => options.WithOrigins("https://localhost:44383").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCors(options => options.AllowAnyOrigin()); // ini ditambahin bila ingin menggunakan CORS AllowAllOrigins

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // ini ditambahin bila ingin menggunakan Autentifikasi di controller

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("AllowOrigin"); // ini harus ditaruh sebelum end Point dan ini berfungsi untuk CORS withOrigins

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
