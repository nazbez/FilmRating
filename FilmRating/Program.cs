using FilmRating.Infrastructure.Injection;
using Microsoft.AspNetCore.SpaServices.AngularCli;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("Configs/appsettings.json");

var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddSpaStaticFiles(cfg =>
{
    cfg.RootPath = "ClientApp/dist";
});

builder.Services.RegisterDependencies(configuration);

var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseSpaStaticFiles();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); 

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";
 
    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
    }
});

app.Run();