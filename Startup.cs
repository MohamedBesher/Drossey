using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Drossey.Data.Core;
using Drossey.Admin.Services;
using Drossey.Data;
using Drossey.Data.Core.Models;
using Drossey.Data.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MotleyFlash;
using MotleyFlash.AspNetCore.MessageProviders;
using Sakura.AspNetCore.Mvc;
using Drossey.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Drossey.Services.WizIQ;
using System.Net.Http.Headers;
using Microsoft.Net.Http.Headers;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;
using Drossey.Admin.Filters;
using Drossey.Data.Core.ErrorDescriber;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Linq;

namespace Drossey.Admin
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
            services.AddDbContext<ApiContext>();
            //options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<ApiContextSeed>();
            //a confirmed email.
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequiredLength = 6;
                config.User.AllowedUserNameCharacters = null;
                config.User.RequireUniqueEmail = true;
                

            })
                .AddEntityFrameworkStores<ApiContext>()
                .AddErrorDescriber<CustomIdentityErrorDescriber>()
                .AddDefaultTokenProviders();
              



           

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddResponseCaching();
            services.AddAutoMapper();
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IWizIQSender, WizIQSender>();
            services.AddSingleton<IWizIQClass, WizIQClass>();
            services.AddSingleton<ITimeZone, TimeZone>();
            services.AddSingleton<IPinCodeGenerator, PinCodeGenerator>();
            services.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddBootstrapPagerGenerator(options =>
            {
                // Use default pager options.
                options.ConfigureDefault();
            });         
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 
            //      .AddCookie("UserAuth", options =>
            //{
               
            //    options.LoginPath = string.Empty;



            //});      
            services.AddDistributedMemoryCache();         
            #region FlashMessage

            services.AddSession();
            // Needed so we can access the user's session.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(x => x.GetRequiredService<IHttpContextAccessor>().HttpContext.Session);

            services.AddScoped<IMessageProvider, SessionMessageProvider>();

            // Customize the message types (i.e. we are using Bootstrap v3 and need to provide a custom-value for the error message-type).
            services.AddScoped<IMessageTypes>(x =>
            {
                return new MessageTypes(error: "danger");
            });

            services.AddScoped<IMessengerOptions, MessengerOptions>();

            // We are using a stack to hold messages (i.e. LIFO).
            services.AddScoped<IMessenger, StackMessenger>();

            #endregion
          
            //using JWT
            services.AddAuthentication()
                  .AddCookie("UserPanel", cfg => cfg.SlidingExpiration = true)
                  .AddJwtBearer(cfg =>
                  {
                      cfg.RequireHttpsMetadata = false;
                      cfg.SaveToken = true;
                      cfg.TokenValidationParameters = new TokenValidationParameters()
                      {
                          ValidIssuer = Configuration["Tokens:Issuer"],
                          ValidAudience = Configuration["Tokens:Issuer"],  
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                      };

                  });

            services.AddCors(cfg =>
            {
                cfg.AddPolicy("UserPanel", bldr =>
                {
                    bldr.AllowAnyHeader()
                        .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });
            services.AddLocalization(options => options.ResourcesPath = "Resources");
          
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddSingleton<IEmailSender, EmailSender>();
            //services.AddUrlHelper();
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Drossey API", Version = "v1" });
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApiContextSeed seeding)
       {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //    app.UseDatabaseErrorPage();
            //}
            //else
            //{
             // app.UseExceptionHandler("/error");
            // app.UseStatusCodePagesWithReExecute("/error/{0}");
            //app.UseStatusCodePagesWithRedirects("/error/{0}");

            //}
            app.UseStaticFiles();       
            app.UseSession();
            app.UseAuthentication();
            app.UseCors("UserPanel");
            app.UseSwagger();
            //var supportedCultures = new[]
            //{
            //    new CultureInfo("ar"),
            //    new CultureInfo("en"),
            //};
            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("ar"),
            //    // Formatting numbers, dates, etc.
            //    SupportedCultures = supportedCultures,
            //    // UI strings that we have localized.
            //    SupportedUICultures = supportedCultures
            //});

            ConfigureApplicationLocalization(app);

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Drossey Api");
            });
            app.UseMvc(routes =>
            {            
                routes.MapRoute(
                name: "areaRoute",
                template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
          
                routes.MapRoute(
                  name: "default",
                  template: "{controller=Home}/{action=Index}/{id?}");
            });           
            seeding.EnsureSeeding().Wait();
        }

        private void ConfigureApplicationLocalization(IApplicationBuilder app)
        {
            var arabic = "ar";
            var englishRequestCulture = new RequestCulture(culture: arabic, uiCulture: arabic);
            var supportedCultures = new List<CultureInfo>
                     {
                         new CultureInfo("en"),
                         new CultureInfo(arabic)
                     };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = englishRequestCulture,
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,

            };

            options.RequestCultureProviders = new List<IRequestCultureProvider>();

            //options.RequestCultureProviders = new List<IRequestCultureProvider>
            // {
            //new QueryStringRequestCultureProvider(),
            //new CookieRequestCultureProvider()
            //};
            //RequestCultureProvider requestProvider = options.RequestCultureProviders.OfType<CookieRequestCultureProvider>().First();
            //options.RequestCultureProviders.Remove(requestProvider);

            app.UseRequestLocalization(options);
        }
    }
}
