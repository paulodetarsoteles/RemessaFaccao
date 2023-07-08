using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RemessaFaccao.DAL.Repositories.Interfaces;
using RemessaFaccao.DAL.Setting;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        builder.Services.AddScoped<IFaccaoRepository, FaccaoRepository>();

        builder.Services.AddScoped<IRemessaRepository, RemessaRepository>();

        builder.Services.AddScoped<IAviamentoRepository, AviamentoRepository>();

        builder.Services.AddAuthorization(options => options.AddPolicy("Admin", p => p.RequireRole("Admin")));

        builder.Services.Configure<ConnectionSetting>(builder.Configuration.GetSection("ConnectionString"));

        builder.Services.AddDbContext<ConnectionDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLString")));

        builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ConnectionDbContext>().AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 4;
            options.Password.RequiredUniqueChars = 1;
        });

        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(60);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });

        app.Run();
    }
}
