
using Microsoft.EntityFrameworkCore;
using SostavSD.Data;
using SostavSD;
using TanvirArjel.Blazor.DependencyInjection;
using MudBlazor.Services;
using SostavSD.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using SostavSD.Interfaces;
using SostavSD.Services;
using SostavSD.Classes.Email;
using SostavSD.Entities;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using NLog.Web;
using MudBlazor;

var logger = NLogBuilder
    .ConfigureNLog("nlog.config")
    .GetCurrentClassLogger();
try
{
    var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

//register the SostavSDContext
builder.Services.AddDbContext<SostavSDContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<UserSostav>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<SostavSDContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
});


builder.Services.AddLocalization(opt => opt.ResourcesPath = "ResourceFiles");
builder.Services.AddControllersWithViews()
    .AddViewLocalization();

builder.Services.AddSingleton<IEmailConfiguration>(builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

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
        context.Database.Migrate();

        await DBInitializerWithUsers.InitializeUsers(services);
    }
    catch (Exception ex)
    {
        var log = services.GetRequiredService<ILogger<Program>>();
        log.LogError(ex, "An error occurred while seeding the database.");
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


    logger.Debug("Initialize app");
    app.Run();




static void AddBusinessLogicServices(IServiceCollection collection)
{
    collection.AddScoped<IAuthorizedUserService, AuthorizedUserService>();

    collection.AddScoped<IContractService, ContractService>();
    collection.AddScoped<ICompanyService, CompanyService>();
    collection.AddScoped<ISourceOfFinancingService, SourceOfFinancingService>();
    collection.AddTransient<IBuildingZoneService, BuildingZoneService>();
    collection.AddTransient<IEmailService, EmailService>();
    collection.AddTransient<IWordExport,WordExportService>();
    collection.AddTransient<IPdfExport, PdfExportService>();
    collection.AddTransient<IExcelExport, ExcelExportService>();
    collection.AddMudServices();
    collection.AddMudServices(config =>
	{
		config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

		config.SnackbarConfiguration.PreventDuplicates = false;
		config.SnackbarConfiguration.NewestOnTop = false;
		config.SnackbarConfiguration.ShowCloseIcon = true;
		config.SnackbarConfiguration.VisibleStateDuration = 10000;
		config.SnackbarConfiguration.HideTransitionDuration = 500;
		config.SnackbarConfiguration.ShowTransitionDuration = 500;
		config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
	});
	}

}
catch (Exception ex)
{
    logger.Error(ex, "Close app");

}
finally
{
    NLog.LogManager.Shutdown();
}