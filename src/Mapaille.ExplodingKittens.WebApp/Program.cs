using Mapaille.ExplodingKittens.WebApp;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddHealthChecks();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<StaticFilesVersion>();
builder.Services.AddSingleton<GameModel>();
builder.Services.AddTransient<PlayerModel>();

var app = builder.Build();

if (!builder.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();
app.UseHealthChecks("/live");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();
