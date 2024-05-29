using ppedv.CarManager.Logic;
using ppedv.CarManager.Model.Contracts;
using ppedv.CarManager.UI.Web.Components;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddScoped<IRepository>(x => new ppedv.CarManager.Data.EfCore.EfRepository());
builder.Services.AddScoped<IRepository, ppedv.CarManager.Data.EfCore.EfRepository>();
builder.Services.AddScoped<CarServices>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

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

app.Run();
