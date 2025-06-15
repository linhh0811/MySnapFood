using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Client.Components;
using Service.SnapFood.Client.Infrastructure.Service;
using Service.SnapFood.Share.Interface.Extentions;
using Blazored.LocalStorage;
using Service.SnapFood.Client.Components.Layout;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAuthentication();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<NavMenu>(); // ??ng ký NavMenu nh? m?t d?ch v?

builder.Services.AddHttpClient<ICallServiceRegistry, CallServiceRegistry>();
builder.Services.AddFluentUIComponents();
builder.Services.AddHttpClient<ICallServiceRegistry, CallServiceRegistry>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7213");
});
builder.Services.AddFluentUIComponents(options => options.ValidateClassNames = false);
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
