using AspNet.Security.OAuth.HHru;
using HHApiLib.Configurations;
using HHApiLib.Services;
using HHRU_Console.Api.Services;
using HHRU_Console.Core.Configuration;
using HHRU_Console.Core.Services;
using HHRU_Console.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

Setup.Init(builder.Configuration.GetSection("ApiConfig").Get<Config>());

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddHHruConsoleCoreServices();
builder.Services.AddMongoDBDataContext(x =>
{
    x.ConnectionString = builder.Configuration.GetSection("MongoDB:ConnectionString").Get<string>();
    x.DatabaseName = builder.Configuration.GetSection("MongoDB:DatabaseName").Get<string>();
});

builder.Services.AddTransient<IAuthorizationHandlerProvider, DefaultAuthorizationHandlerProvider>();
builder.Services.AddSingleton<IAccessService, AccessService>();

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
        options.Events.OnCreatingTicket = (x) =>
        {
            var service = x.HttpContext.RequestServices.GetService<IAccountService>();
            var email = x.Identity.Claims.FirstOrDefault(x => x.Type == ClaimsIdentity.DefaultNameClaimType);
            if (email != null)
            {
                service.SaveUserData(new HHRU_Console.Core.Models.UserAccessData()
                {
                    AccessToken = x.AccessToken,
                    RefreshToken = x.RefreshToken,
                    ExpiresIn = x.ExpiresIn.Value,
                    Email = email.Value,
                });
            }
            return Task.CompletedTask;
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
