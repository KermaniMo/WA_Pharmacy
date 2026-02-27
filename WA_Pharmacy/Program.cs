using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WA_Pharmacy.App_Const;
using WA_Pharmacy.AppCode.Contracts.IService;
using WA_Pharmacy.AppCode.Profiles;
using WA_Pharmacy.AppCode.Repository;
using WA_Pharmacy.AppCode.Services;
using WA_Pharmacy.EFCore.DbContextFolder;
using WA_Pharmacy.EFCore.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// 1. Get the connection string from the built-in Configuration
var connectionString = builder.Configuration.GetConnectionString(AppSettingsConst.ConnectionString);

// 2. Register the DbContext
builder.Services.AddDbContext<PharmacyContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<PharmacyContext>()
    .AddDefaultTokenProviders();

//-------------------------------------------------
builder.Services.AddScoped<IUnitOfWork, PharmacyContext>();
builder.Services.AddRazorPages();

//builder.Services.AddAutoMapper(cfg =>
//{
//    cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODAxNTI2NDAwIiwiaWF0IjoiMTc3MDAzODQ3NyIsImFjY291bnRfaWQiOiIwMTljMWU4MTAxM2U3NTA1OTcxNTUwZmMwNDk1ZmY5NSIsImN1c3RvbWVyX2lkIjoiY3RtXzAxa2dmODdneTR6ZWNraHM3bTA5NmJuemcxIiwic3ViX2lkIjoiLSIsImVkaXRpb24iOiIwIiwidHlwZSI6IjIifQ.rs8RIVyqIkpYVR_3BZ9k7LmRTdhnDIRwXyGbRrGZk0V-6W5ol2F-WqmW51PIItbaXOC8UG2ue6iomxsHlPa82nqlRtRxity3R2gyMIfpbFOVjOyprC7oU1rkpAkdDBKwP-0AMfJ6aphEFqxkMGeZJrH3dKi0BwRUnsp1G89OnYf5SfjvKWtcam9ETRCh0xjjhErzTqLR4jbcrVIBUOKNRIzBC_4-RQkc8Vt169ciyza_J-7NUHNEtED0J7r4M7AAEdGnHQZuMJ2BalEbMoL4vi1s6eTsmyk4_1Zt666ZNrz5cAZKSNvJ-eIbsCeZ_4CyD9kJERi3Dzy6BNWCIhiO5Q";
//}, typeof(Program));
//-------------------------------------------------

builder.Services.AddAutoMapper(cfg => cfg.AddProfile<CustomerProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MedicineProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<InsuranceProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<DoctorProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<PrescriptionProfile>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<InsuredProfile>());

//-------------------------------------------------


//-------------------------------------------------

builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
//-------------------------------------------------

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IMedicineService, MedicineService>();
builder.Services.AddScoped<IInsuranceService, InsuranceService>();
builder.Services.AddScoped<IInsuredService, InsuredService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
builder.Services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, WA_Pharmacy.Services.EmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
