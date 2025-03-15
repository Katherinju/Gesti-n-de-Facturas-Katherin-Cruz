using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Obtener la cadena de conexi�n
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configurar Entity Framework Core con resiliencia en la conexi�n
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions =>
        sqlServerOptions.EnableRetryOnFailure()
    )
);

// Habilitar controladores con vistas (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

Console.WriteLine("Probando conexi�n a la base de datos...");

// Verificar conexi�n a la base de datos (Solo para prueba, puedes eliminarlo despu�s)
try
{
    using (var connection = new SqlConnection(connectionString))
    {
        connection.Open();
        Console.WriteLine("Conexi�n exitosa a SQL Server");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error de conexi�n: {ex.Message}");
}
Console.WriteLine(" Prueba de conexi�n finalizada.");
// Configuraci�n del pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// Configurar rutas para controladores MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Facturas}/{action=Index}/{id?}");

app.Run();
