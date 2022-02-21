

using GeekShopping.Web.Services;
using GeekShopping.Web.Services.IServices;

namespace GeekShopping.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IProductService, ProductService>(c =>
             c.BaseAddress = new Uri(Configuration["ServicesUrls:ProductAPI"]));

            // Add services to the container.
            services.AddControllersWithViews();

        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

        }
    }
}