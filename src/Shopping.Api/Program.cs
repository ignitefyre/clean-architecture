using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shopping.Api.Endpoints.Extensions;
using Shopping.Api.Extensions;
using Shopping.Api.Mappings;
using Shopping.Application;
using Shopping.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Shopping Cart API", Version = "v1" });
    c.DocumentFilter<SortEndpointsFilter>();
});

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(config["Auth0:Authority"], config["Auth0:Audience"]);

builder.Services.AddAutoMapper(typeof(CartResponseProfile));

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddEndpoints(typeof(Program).Assembly);
builder.Services.AddEndpointPolicies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();

app.Run();