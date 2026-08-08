using Inventory_Management.Components;
using Inventory_Management.Handler;
using Inventory_Management.Interface;
using Inventory_Management.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBlazorBootstrap();

// Register AuthTokenHolder scoped per Blazor circuit
builder.Services.AddScoped<AuthTokenHolder>();

var apiBaseUri = new Uri("https://localhost:7004/");

// AuthService does not need auth header (performs login)
builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = apiBaseUri;
});

// Helper function to create authenticated HttpClient within circuit's DI scope
HttpClient CreateAuthenticatedClient(IServiceProvider sp)
{
    var tokenHolder = sp.GetRequiredService<AuthTokenHolder>();
    var handler = new AuthHeaderHandler(tokenHolder, new HttpClientHandler());
    return new HttpClient(handler) { BaseAddress = apiBaseUri };
}

builder.Services.AddScoped<IUserService>(sp => new UserService(CreateAuthenticatedClient(sp)));
builder.Services.AddScoped<ICompanyService>(sp => new CompanyService(CreateAuthenticatedClient(sp)));
builder.Services.AddScoped<IRoleService>(sp => new RoleService(CreateAuthenticatedClient(sp)));
builder.Services.AddScoped<IFormService>(sp => new FormService(CreateAuthenticatedClient(sp)));
builder.Services.AddScoped<IFormBusinessRulesService>(sp => new FormBusinessRulesService(CreateAuthenticatedClient(sp)));
builder.Services.AddScoped<IFormRulesPermissionService>(sp => new FormRulesPermissionService(CreateAuthenticatedClient(sp)));

builder.Services.AddScoped<SessionContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
