using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecretSanta_Core;
using SecretSanta_Core.BusinessLogic;
using SecretSanta_Core.Data;
using SecretSanta_Core.Repositories;
using SecretSanta_Core.Services;
using Stripe;
using Quartz;
using SecretSanta_Core.Jobs;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(
	options => options.UseSqlServer(connectionString), optionsLifetime: ServiceLifetime.Singleton
);


builder.Services.AddDbContextFactory<ApplicationDbContext>(
	options => options.UseSqlServer(connectionString)
);

builder.Services.AddDefaultIdentity<ApplicationUser>()
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<ConnectionString>(
	options => options.DefaultConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
);

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(10);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddHttpClient();
builder.Services.AddSingleton<EmailService>();
builder.Services.AddScoped<ExceptionUtilityService>();
builder.Services.AddSingleton<RankSubNumberService>();
builder.Services.AddSingleton<USPSAddressService>();
builder.Services.AddScoped<StorageService>();
builder.Services.AddScoped<AgencyUserRepository>();
builder.Services.AddScoped<DashboardCountRepository>();
builder.Services.AddScoped<RecipientViewRepository>();
builder.Services.AddScoped<GiftViewRepository>();
builder.Services.AddScoped<AdoptHeartRepository>();
builder.Services.AddScoped<LetterSantaRepository>();
builder.Services.AddScoped<ThankYouRepository>();
builder.Services.AddScoped<ApproveThankYouRepository>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
StripeConfiguration.ApiKey = builder.Configuration.GetSection("AppSettings")["StripeSecretKey"];

builder.Services.AddHttpsRedirection(options =>
{
	options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
	options.HttpsPort = 443;
});

builder.Services.AddQuartz(q =>
{
	// Just use the name of your job that you created in the Jobs folder.
	var jobKey = new JobKey("DailyDigestJob");
	q.AddJob<DailyDigestJob>(opts => opts.WithIdentity(jobKey));

	q.AddTrigger(opts => opts
		.ForJob(jobKey)
		.WithIdentity("DailyDigestJob-trigger")
		.WithCronSchedule("0 0 0/12 * * ?")
	);
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	//app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
