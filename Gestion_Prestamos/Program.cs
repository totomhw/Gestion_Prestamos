using Gestion_Prestamos.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar la cadena de conexión desde el archivo appsettings.json
builder.Services.AddDbContext<GestionPrestamosContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregar servicios para controladores y vistas
builder.Services.AddControllersWithViews();

// Configurar la autenticación mediante cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login"; // Ruta al login
        options.LogoutPath = "/Auth/Logout"; // Ruta al logout
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // La sesión expira después de 20 minutos
        options.SlidingExpiration = true; // Renovación automática de la cookie si hay actividad
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

// Configurar las políticas de autorización según roles
builder.Services.AddAuthorization(options =>
{
    // Política para los usuarios con rol Admin
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));

    // Política para los usuarios con rol Personal
    options.AddPolicy("RequirePersonalRole", policy => policy.RequireRole("Personal"));
});

var app = builder.Build();

// Configurar el pipeline de solicitud HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Activar autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Configurar las rutas de la aplicación
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

// Ejecutar la aplicación
app.Run();
