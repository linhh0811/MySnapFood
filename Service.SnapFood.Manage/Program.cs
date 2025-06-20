using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.FileProviders;
using Microsoft.FluentUI.AspNetCore.Components;
using Service.SnapFood.Manage.Components;
using Service.SnapFood.Manage.Infrastructure.Services;
using Service.SnapFood.Share.Interface.Extentions;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAuthentication();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpClient<ICallServiceRegistry, CallServiceRegistry>();
builder.Services.AddFluentUIComponents();
builder.Services.AddHttpClient<ICallServiceRegistry, CallServiceRegistry>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7213");
});
builder.Services.AddFluentUIComponents(options => options.ValidateClassNames = false);
builder.Services.AddScoped<ImageService>(); 
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7213/")
});

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

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "..", "Service.SnapFood.Share", "Images")),
    RequestPath = "/Images"
});
app.Run();
