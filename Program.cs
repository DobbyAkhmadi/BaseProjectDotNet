using System.Text;
using BaseProjectDotnet.Helpers.Database;
using BaseProjectDotnet.Helpers.Middleware;
using BaseProjectDotnet.Services.AuditService;
using BaseProjectDotnet.Services.AuthService;
using BaseProjectDotnet.Services.MasterService;
using BaseProjectDotnet.Services.PersonService;
using BaseProjectDotnet.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Load configuration values from appsettings.{Environment}.json
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtKey = builder.Configuration["Jwt:Key"];

// Add services to the container
builder.Services.AddScoped<DatabaseContext>();
builder.Services.AddScoped<IPersonService, PersonServiceData>();
builder.Services.AddScoped<IUserService, UserServiceData>();
builder.Services.AddScoped<IAuditTrailService, AuditTrailServiceData>();
builder.Services.AddScoped<IMasterService, MasterServiceDAL>();
builder.Services.AddScoped<IAuthService, AuthServiceDAL>();

// Add services to the container
builder.Services.AddAuthentication(options =>
  {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer(options =>
  {
    if (jwtKey != null)
      options.TokenValidationParameters = new TokenValidationParameters
      {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
      };
  });

builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
    policy =>
    {
      policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddAuthorization();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage(); // Show detailed exception information in development
}
else
{
  app.UseExceptionHandler("/internal/Home/Error?statusCode=500"); // Custom error handling in production
  app.UseHsts(); // Default HSTS value is 30 days. Consider changing for production scenarios, see https://aka.ms/aspnetcore-hsts
}

// Use status code pages for error responses
app.UseStatusCodePages(context =>
{
  var response = context.HttpContext.Response;
  switch (response.StatusCode)
  {
    case StatusCodes.Status401Unauthorized:
      response.Redirect("/internal/Home/Error?statusCode=401");
      break;
    case StatusCodes.Status403Forbidden:
      response.Redirect("/internal/Home/Error?statusCode=403");
      break;
  }

  return Task.CompletedTask;
});
// app.UseHttpsRedirection(); // Uncomment if you want to enforce HTTPS redirection
app.UseStaticFiles();
app.UseMiddleware<JwtMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();
app.MapControllers();
app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Auth}/{action=Index}");

app.Run();
