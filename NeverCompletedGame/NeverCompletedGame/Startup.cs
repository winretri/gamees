using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.EventEmitter;
using Infrastructure.Riddles;
using Infrastructure.Views;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeverCompletedGame.Infrastructure;

namespace NeverCompletedGame
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.IsEssential = true;
                options.Cookie.Name = "ncg_session";
            });
            string tempFilePath = "C:\\Temp\\ncg\\";
            string connectionStringView = $"DataSource={tempFilePath}views.db";
            string connectionStringEvent = $"DataSource={tempFilePath}events.db";
            services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddScoped<ICommandFactory, CommandFactory>();
            services.AddDbContext<GameDbContext>((builder => builder.UseSqlite(connectionStringEvent)));
            services.AddDbContext<GameViewDbContext>((builder => builder.UseSqlite(connectionStringView)));
            services.AddScoped<IRiddleRepository, FileRiddleRepository>();
            services.AddScoped<GameCommandHandler>();
            services.AddScoped<IRepository, EventSourcedGamesRepository>();
            services.AddScoped<IViewStore, GameViewStore>();
            services.AddScoped<IEventBus, EventEmitter>();
            services.AddSignalR();
            services.AddMvc();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ng/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseSession();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ng";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            UpdateDatabase(app);
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<GameViewDbContext>())
                {
                    context.Database.EnsureCreated();
                }
                using (var context = serviceScope.ServiceProvider.GetService<GameDbContext>())
                {
                    context.Database.EnsureCreated();
                }
            }
        }
    }
}
