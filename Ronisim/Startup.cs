using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ronisim.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using System;
using Microsoft.AspNetCore.DataProtection;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ronisim
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie();

            var environment = services.BuildServiceProvider().GetRequiredService<IHostingEnvironment>();

            services.AddDataProtection()
                    .SetApplicationName($"my-app-{environment.EnvironmentName}")
                    .PersistKeysToFileSystem(new DirectoryInfo($@"{environment.ContentRootPath}\keys"));
            services.AddDbContext<ronisimContext>(options => options.UseSqlServer(@"Server=localhost;Database=temp2;Trusted_Connection=True;"));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Login");
                });

            services.AddDistributedMemoryCache();



            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromHours(10);
                options.Cookie.HttpOnly = true;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseDeveloperExceptionPage();

            app.UseResponseCompression();

            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();

                    headers.CacheControl = new CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(30)
                    };
                }
            });

            app.UseSession();

            app.UseAuthentication();

            //app.UseCookieAuthentication(new CookieAuthenticationOptions()
            //{
            //    AuthenticationScheme = "Auth0.Ronisim",
            //    LoginPath = new PathString("/Login"),
            //    AutomaticAuthenticate = true,.
            //    AutomaticChallenge = true,
            //    CookieName = "Auth0.Ronisim",
            //    ExpireTimeSpan = TimeSpan.FromHours(4),
            //    SlidingExpiration = true
            //});

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
