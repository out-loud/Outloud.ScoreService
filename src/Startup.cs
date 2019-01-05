using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Outloud.Common.Authentication;
using Outloud.Common.Swagger;
using Outloud.ScoreService.Persistance;
using Outloud.ScoreService.Persistance.Repositories;

namespace Outloud.ScoreService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserDataRepository, UserDataRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddAuth0();
            services.AddSwagger();

            services.AddDbContext<UserDataDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                opt => opt.UseRowNumberForPaging()), ServiceLifetime.Singleton);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<UserDataDbContext>();
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuth0();
            app.UseSwagger();
            app.UseMvc();
        }
    }
}
