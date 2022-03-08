using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RunningMan.Data;
using RunningMan.Models;
using RunningMan.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunningMan
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
            services.AddScoped<IAccountRepository, AccountRepository>();

            services.Configure<AppSetting>(Configuration.GetSection("AppSettings"));

            services.AddDbContext<MyDbContext>(option => {
                option.UseSqlServer(Configuration.GetConnectionString("MyDB"));
            });

            var secretKey = Configuration["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);

            services.AddAuthentication
                (JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Auto get token
                        ValidateIssuer = false,
                        ValidateAudience = false,

                        //Sign into token
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                        ClockSkew = TimeSpan.Zero

                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RunningMan", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RunningMan v1"));
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