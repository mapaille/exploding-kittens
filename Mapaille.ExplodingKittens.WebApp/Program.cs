using Mapaille.ExplodingKittens.WebApp.Components;
using Mapaille.ExplodingKittens.WebApp.Services;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorComponents();

builder.Services.AddSingleton<VersionService>();

var app = builder.Build();

if (!builder.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
