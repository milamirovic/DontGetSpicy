using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontGetSpicy.JWT;
using DontGetSpicy.Models;
using DontGetSpicy.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Web_API
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
            services.AddCors(opts=>
            {
                opts.AddPolicy("Cors", builder => builder.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web_API", Version = "v1" });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opts=>
            {
                opts.TokenValidationParameters=new TokenValidationParameters
                   {
                       ValidateIssuer=true,
                       ValidateAudience=true,
                       ValidateLifetime=true,
                       ValidateIssuerSigningKey=true,
                       ValidIssuer=Configuration["Jwt:Issuer"],
                       ValidAudience=Configuration["Jwt:Issuer"],
                       IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))

                   } ;
                   opts.Events=new JwtBearerEvents
                   {
                        OnMessageReceived=context =>{
                            var accessToken=context.Request.Query["access_token"];
                            if(string.IsNullOrEmpty(accessToken)==false){
                                context.Token=accessToken;

                            }
                            return Task.CompletedTask;
                        }
                   };
                   
            services.Configure<FormOptions>(o => {
            o.ValueLengthLimit = int.MaxValue;
            o.MultipartBodyLengthLimit = int.MaxValue;
            o.MemoryBufferThreshold = int.MaxValue;
                 });

            });
            services.AddSignalR();//services.AddScoped<HubProvider>();
            //services.AddMvc();
            services.AddDbContext<DontGetSpicyContext>(options => 
            {
                //lambda expr koji omogucava da nad options nesto radimo i njime manipulisemo
                //koji je connection string i koji je server string
                //moramo dodati referncu dotnet add package Microsoft.EntityFrameworkCore.SqlServer
                options.UseSqlServer(Configuration.GetConnectionString("DontGetSpicyCS"));
                //da bi sada mogla da se formira tabela na osnovu nasih klasa moraju se odraditi sledece komande
                //dotnet add package Microsoft.EntityFrameworkCore.Design
                //dotnet ef
                //dotnet ef migrations add V1 - kreira se migracija koja se zove V1
                //dotnet ef database update - sve sto ima u podesavanjima migracija upise automatski i u bazu podataka
            });
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            app.UseAuthentication();
            app.UseCors("Cors");
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
                    RequestPath = new PathString("/Resources")
                });


            app.UseAuthorization();
            //app.userouting()?

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/ChatHub");
                endpoints.MapHub<GameHub>("/GameHub");
            });
            JWTGenerator.Instantiate(this.Configuration);
        }
    }
}
