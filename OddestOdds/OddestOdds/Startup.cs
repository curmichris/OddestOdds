using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OddestOdds.Core.Interfaces;
using OddestOdds.Infrastructure.Data;
using OddestOdds.Infrastructure.Extensions;
using OddestOdds.Service.Implementation;
using OddestOdds.Service.Interfaces;
using OddestOdds.Web.Hubs;

namespace OddestOdds
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
            services.AddMvc();
            services.AddSignalR();

            services.AddTransient<IOddsRepository, OddsRepository>();
            services.AddTransient<IOddsService, OddsService>();
            services.AddTransient<IMessageHub, MessageHub>();
            var connectionString = Configuration["connectionStrings:oddestOddsDbConnectionString"];
            services.AddDbContext<OddestOddsContext>(o => o.UseSqlServer(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, OddestOddsContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            context.Database.Migrate();
            context.EnsureSeedDataForContext();;

            app.UseStaticFiles();
            app.UseSignalR(config => config.MapHub<MessageHub>("/messages"));
            app.UseMvc();
        }
    }
}
