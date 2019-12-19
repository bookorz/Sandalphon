using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sandalphon.Providers;
using System;

namespace sandalphon
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
            services.AddSignalR();
            services.AddTransient<GlobalHubServer<SignalRHub>>();
            services.AddControllersWithViews();
            // Add framework services.
            services.AddMvc().AddMvcOptions(options =>
            {
                // 把預設的訊息樣板 "The value '{0}' is not valid." 改成中文的 "'{x}' 是不合法的參數"
                options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => $"'{x}' 是不合法的參數");
            });
            //    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //services.AddRazorPages();
            // Simple example with dependency injection for a data provider.
            //services.AddSingleton<IWeatherProvider, WeatherProviderFake>();
        }
        public static GlobalHubServer<SignalRHub> GlobalHub { get; private set; }
        public static ControlHub GlobalControl { get; private set; }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            GlobalHub = serviceProvider.GetService<GlobalHubServer<SignalRHub>>();
            lock (GlobalHub)
            {
                if (GlobalControl == null)
                {
                    GlobalControl = new ControlHub();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Webpack initialization with hot-reload.
                //app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                //{
                //    HotModuleReplacement = true,
                //});
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseWebSockets();
            app.UseRouting();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<SignalRHub>("/message");
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute("spa-fallback", "{ controller = Home, action = Index }");
            });
            //app.UseSignalR(config =>
            //{
            //    config.MapHub<SignalRHub>("/message");
            //});
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");

            //    routes.MapSpaFallbackRoute(
            //        name: "spa-fallback",
            //        defaults: new { controller = "Home", action = "Index" });
            //});

        }
    }
}
