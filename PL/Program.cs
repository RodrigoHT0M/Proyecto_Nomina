using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//var conString = builder.Configuration.GetConnectionString("ConnectionStringSistemaNomina") ??
//     throw new InvalidOperationException("Connection string 'ConnectionStringSistemaNomina'" +
//    " not found.");
//builder.Services.AddDbContext<DL.SistemaNominaContext>(options =>
//    options.UseSqlServer(conString));
//builder.Services.AddScoped<BL.Empleado>();
//builder.Services.AddScoped<BL.Usuario>();


builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
