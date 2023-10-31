using project_airlines.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
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
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllerRoute(
//    name: "Edit",
//    pattern: "Supplier/Edit/{id}",
//    defaults: new { controller = "Supplier", action = "_Edit" }
//);


// Start the application
app.Run();
