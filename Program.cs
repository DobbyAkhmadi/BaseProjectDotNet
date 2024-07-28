using BaseProjectDotnet.Services.AuditService;
using BaseProjectDotnet.Services.PersonService;
using BaseProjectDotnet.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<BaseProjectDotnet.Helpers.Database.DatabaseContext>();
builder.Services.AddScoped<IPersonService, PersonServiceData>();
builder.Services.AddScoped<IUserService, UserServiceData>();
builder.Services.AddScoped<IAuditTrailService, AuditTrailServiceData>();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
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
app.UseAuthorization(); // Ensure Authorization is added after Authentications

app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}");
app.Run();
