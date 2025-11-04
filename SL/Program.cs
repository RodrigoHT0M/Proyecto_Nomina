using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.WithOrigins("https://localhost:7018")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllersWithViews();
var conString = builder.Configuration.GetConnectionString("ConnectionStringSistemaNomina") ??
     throw new InvalidOperationException("Connection string 'ConnectionStringSistemaNomina'" +
    " not found.");
builder.Services.AddDbContext<DL.SistemaNominaContext>(options =>
    options.UseSqlServer(conString));
builder.Services.AddScoped<BL.Empleado>();
builder.Services.AddScoped<BL.Usuario>();
builder.Services.AddScoped<BL.Permiso>();
builder.Services.AddScoped<DL.ListGetAllHistorialPermisoDTO>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
