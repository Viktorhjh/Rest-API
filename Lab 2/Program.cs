using Lab_2;
using Lab_2.Authentication;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DataService>();
builder.Services.AddControllers();
builder.Services.AddAuthentication()
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>(
     "Basic", null);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
