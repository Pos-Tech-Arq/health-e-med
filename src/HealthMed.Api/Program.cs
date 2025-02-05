using HealthMed.Infra.Configurations;
using HealthMed.Infra.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);


//var cultureInfo = new CultureInfo("pt-BR");
//CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
//CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.ConfigureIdentity(builder.Configuration);
builder.Services.ConfigureJWTAuthentication(builder.Configuration);
//services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();