using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//configurar la autenticacion de usuarios
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie((Options) =>
{
    Options.LoginPath = new PathString("/User/Login");
    Options.AccessDeniedPath = new PathString("/TouristPlace/Index");
    Options.ExpireTimeSpan = TimeSpan.FromHours(8);
    Options.SlidingExpiration = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/TouristPlace/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication(); // poner en uso la autenticacion


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TouristPlace}/{action=Index}/{id?}");

app.Run();
