using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DontGetSpicy;
using DontGetSpicy.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web_API", Version = "v1" });
            });services.AddDistributedMemoryCache();
            services.AddSession(opts => 
            {
                opts.Cookie.HttpOnly=true;
            });
            
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

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
