using AspNet.Security.OAuth.HHru;
using HHApiLib.Configurations;
using HHApiLib.Services;
using HHRU_Console.Api.Services;
using HHRU_Console.Core.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

Setup.Init(builder.Configuration.GetSection("ApiConfig").Get<Config>());

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddHHruConsoleCoreServices();

builder.Services.AddTransient<IAuthorizationHandlerProvider, DefaultAuthorizationHandlerProvider>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalHost", p => p
    .WithOrigins(builder.Configuration.GetSection("AllowOrigin").Get<string>())
    .AllowCredentials()
    .AllowAnyHeader()
    .AllowAnyMethod());
});

builder.Services.AddAuthentication(HHruAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.Cookie.SameSite = SameSiteMode.None;
    })
    .AddHHru(options =>
    {
        options.ClientId = builder.Configuration.GetSection("ApiConfig:ClientId").Get<string>();
        options.ClientSecret = builder.Configuration.GetSection("ApiConfig:ClientSecret").Get<string>();
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.SignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.SaveTokens = true;
        options.ClaimsIssuer = CookieAuthenticationDefaults.AuthenticationScheme;
        options.UserInformationEndpoint = "https://api.hh.ru/me";
        options.BackchannelHttpHandler = new HttpClientHandler()
        {
            UseDefaultCredentials = true,
            UseCookies = true,
            AllowAutoRedirect = true,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalHost");


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
