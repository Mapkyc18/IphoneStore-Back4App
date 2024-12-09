using testing_final.Logic.Data;
using testing_final.Logic.Interfaces;
using testing_final.Logic.Services;
using UI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
    
var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "orders.db");
Console.WriteLine($"Database path: {dbPath}");
builder.Services.AddSingleton<IDatabase>(provider => new Database(dbPath));
builder.Services.AddSingleton<OrderManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
