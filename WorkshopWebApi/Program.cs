using Microsoft.EntityFrameworkCore;
using WorkshopWebAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WorkshopWebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Dodanie us³ug do kontenera DI
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WorkshopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konfiguracja JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidIssuer = builder.Configuration["Jwt:Issuer"]
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// **Inicjalizacja bazy danych i dodanie domyœlnego u¿ytkownika**
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<WorkshopContext>();

    // **Automatyczna migracja bazy**
    context.Database.Migrate();

    // **Dodanie u¿ytkownika, jeœli nie istnieje**
    if (!context.Users.Any())
    {
        var plainPassword = "admin123";
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainPassword);

        Console.WriteLine($" Has³o admina przed hash: {plainPassword}");
        Console.WriteLine($" Has³o admina po hash: {hashedPassword}");

        var user = new User
        {
            Username = "admin",
            PasswordHash = hashedPassword
        };

        context.Users.Add(user);
        context.SaveChanges();
    }
}


// Konfiguracja HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// **Uruchomienie aplikacji (musi byæ na koñcu!)**
app.Run();
