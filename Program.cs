using BaseProjectDotnet.Services.PersonService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<BaseProjectDotnet.Helpers.Database.DatabaseContext>();
builder.Services.AddScoped<IPersonService, PersonServiceData>();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // Show detailed exception information in development
}
else
{ 
    app.UseExceptionHandler("/Home/Error"); // Custom error handling in production
    app.UseHsts(); // Default HSTS value is 30 days. Consider changing for production scenarios, see https://aka.ms/aspnetcore-hsts
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); // Ensure Authorization is added after Authentication

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");
app.Run();