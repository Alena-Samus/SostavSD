
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD;
using TanvirArjel.Blazor.DependencyInjection;
using MudBlazor.Services;
using SostavSD.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using SostavSD.Interfaces;
using SostavSD.Services;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//register the SostavSDContext
builder.Services.AddDbContext<SostavSDContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SostavSDContext>();
builder.Services.AddLocalization(opt => opt.ResourcesPath = "ResourceFiles");

AddBusinessLogicServices(builder.Services);
//the AddDatabaseDeveloperPageExceptionFilter provides helpful error information in the development environment.
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(typeof(AppMappingProfile));
builder.Services.AddScoped<TokenProvider>();

builder.Services.AddComponents();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<SostavSDContext>();

        DBInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapRazorPages();

app.Run();


static void AddBusinessLogicServices(IServiceCollection collection)
{
    collection.AddScoped<IContractService, ContractService>();
    collection.AddScoped<ICompanyService, CompanyService>();
    collection.AddMudServices();
    
}