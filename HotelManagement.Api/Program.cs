using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using HotelManagement.Application;
using HotelManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(
    builder.Configuration);
builder.Services.AddApplication();

// ─── JWT Authentication ────────────────────────────────────────────────────
var secretKey = builder.Configuration["JwtSettings:Secret"]
    ?? "HotelManagementERPSecretKeyForJWTAuthentication2026!";
var issuer    = builder.Configuration["JwtSettings:Issuer"]    ?? "HotelManagementApi";
var audience  = builder.Configuration["JwtSettings:Audience"]  ?? "HotelManagementClient";

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer              = issuer,
            ValidAudience            = audience,
            IssuerSigningKey         = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew                = TimeSpan.Zero
        };
    });

// ─── Global [Authorize] — every endpoint requires auth by default ──────────
builder.Services.AddControllers(options =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.Filters.Add(new AuthorizeFilter(policy));
});

// ─── OpenAPI / Swagger with Bearer security scheme ─────────────────────────
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();

        var scheme = new OpenApiSecurityScheme
        {
            Type         = SecuritySchemeType.Http,
            Scheme       = "bearer",
            BearerFormat = "JWT",
            In           = ParameterLocation.Header,
            Description  = "Please enter JWT token"
        };

        document.Components.SecuritySchemes["Bearer"] = scheme;

        var schemeReference = new OpenApiSecuritySchemeReference("Bearer", document);
        var securityRequirement = new OpenApiSecurityRequirement
        {
            [schemeReference] = new List<string>()
        };

        document.Security ??= new List<OpenApiSecurityRequirement>();
        document.Security.Add(securityRequirement);

        return Task.CompletedTask;
    });
});

var app = builder.Build();

// ─── HTTP pipeline ─────────────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "Inventory Management API v1");
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<HotelManagement.Api.Middleware.ValidationExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
