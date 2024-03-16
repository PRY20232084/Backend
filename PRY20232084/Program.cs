using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PRY20232084.Data;
using PRY20232084.Data.DataAcess;
using PRY20232084.Data.Interfaces;
using PRY20232084.Models;
using PRY20232084.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;

    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
})
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ADD DATA ACCESS
builder.Services.AddTransient<ISizeDA, SizeDA>();

// ADD SERVICES
builder.Services.AddTransient<SizeService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("InventoryManagement",
        builder => builder.WithOrigins("*").WithHeaders("*").WithMethods("*"));
    options.AddPolicy(name: "AllowSpecificOrigin",
                  policy =>
                  {
                      policy.WithOrigins("http://localhost:4200") // Angular CLI default port
                             .AllowAnyHeader()
                             .AllowAnyMethod();
                  });
});

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
