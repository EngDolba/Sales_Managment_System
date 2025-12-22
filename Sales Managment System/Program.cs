using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sales_Managment_System;
using Sales_Managment_System.Contracts;
using Sales_Managment_System.Contracts.ConverterContracts;
using Sales_Managment_System.Contracts.ServiceContracts;
using Sales_Managment_System.DTOs;
using Sales_Managment_System.Repositories;
using Sales_Managment_System.Services;
using Serilog;
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.File("Logs").CreateLogger();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("Oracle")));
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddScoped(typeof(ITransactionRepository), typeof(TransactionRepository));
builder.Services.AddScoped(typeof(ITransactionService), typeof(TransactionService));
builder.Services.AddScoped(typeof(IServiceRepository), typeof(ServiceRepository));
builder.Services.AddScoped(typeof(IDailyReportRepository), typeof(DailyReportRepository));
builder.Services.AddScoped(typeof(IDailyReportService), typeof(DailyReportService));
builder.Services.AddScoped(typeof(IServiceService), typeof(ServiceService));
builder.Services.AddScoped(typeof(ITransactionToTransactionDto), typeof(TransactionToTransactionDto));
builder.Services.AddScoped(typeof(IAuthenticationRepository), typeof(AuthenticationRepository));
builder.Services.AddScoped(typeof(IAuthenticationService),typeof(AuthenticationService));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
            ValidateIssuerSigningKey = true
        };
    });
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Sales Management System API", 
        Version = "v1" 
    });

    // Add JWT Authentication
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    // Apply JWT to all endpoints
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();      // 1. HTTPS redirection (optional but recommended)
app.UseStaticFiles();            // 2. Static files
app.UseRouting();                // 3. Routing MUST come before Authentication
app.UseAuthentication();         // 4. Authentication MUST come before Authorization
app.UseAuthorization();          // 5. Authorization
app.MapControllers();            // 6. Map controllers

app.Run();