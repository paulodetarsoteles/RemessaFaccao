using Microsoft.AspNetCore.Identity;
using RemessaFaccao.DAL.Repositories.Interfaces;
using RemessaFaccao.DAL.Setting;

var builder = WebApplication.CreateBuilder(args);
var connectDb = builder.Configuration.GetSection("ConnectionString");

builder.Services.AddControllersWithViews();
builder.Services.Configure<ConnectionSetting>(connectDb);
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ConnectionDbContext>().AddDefaultTokenProviders();
builder.Services.AddScoped<IFaccaoRepository, FaccaoRepository>();
builder.Services.AddScoped<IPerfilRepository, PerfilRepository>();
builder.Services.AddScoped<IRemessaRepository, RemessaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
