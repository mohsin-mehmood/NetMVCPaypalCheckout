using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PaypalDemo.Paypal;

namespace PaypalDemo
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
            services.AddOptions<PaypalConfig>()
                .BindConfiguration("PayPal")
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<PaypalConfig>>().Value);
            services.AddScoped<IAuthTokenProvider, AuthTokenProvider>();
            services.AddScoped<PaypalAuthHandler>();
            services.AddScoped<PaypalService>();

            services.AddHttpClient("Paypal", (srv, cfg) =>
            {
                var payPalConfig = srv.GetRequiredService<IOptions<PaypalConfig>>().Value;


                cfg.BaseAddress = new System.Uri(payPalConfig.ApiUrl);
            })
            .AddHttpMessageHandler<PaypalAuthHandler>();

            services.AddRazorPages();
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

        }
    }
}
