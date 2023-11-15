using project_airlines.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using project_airlines.Models.User;


var builder = WebApplication.CreateBuilder(args);
    // Configure services
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
 

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Other configurations
builder.Services.AddScoped<UserManager<User>>();
builder.Services.AddScoped<SignInManager<User>>();




// Configure authentication with JWT Bearer


builder.Services.Configure<IdentityOptions>(options =>
{
    // Configure password options
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;

    // Other configuration options...
});


// hot reload 
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure middleware pipeline
if (!app.Environment.IsDevelopment())
{
    // Error handling and security features for production
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Development error page
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); // If you have authentication configured
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Register}/{id?}"
    );
app.MapControllerRoute(
    name: "Register",
    pattern: "User/Register",
    defaults: new { controller = "User", action = "Register" }
);

app.MapControllerRoute(
    name: "GroupementZoneGeo",
    pattern: "Home/GroupedCountries",
    defaults: new { controller = "Home", action = "_GroupedCountries" }
);

// Start the application
app.Run();
