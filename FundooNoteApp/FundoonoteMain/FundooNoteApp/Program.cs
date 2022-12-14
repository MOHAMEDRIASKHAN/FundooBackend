using BusinessLayer.Interfaces;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using RespositryLayer.Context;
using RespositryLayer.Interface;
using RespositryLayer.Services;
using System.Text;


var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();  ///
logger.Debug("init main function");
var builder = WebApplication.CreateBuilder(args);
//var logger = NLogBuilder.ConfigureNLog("").GetCurrentClassLogger();
//logger.Debug("init main function");
//CreateHostBuilder(args).Build().Run();
//NLog.LogManager.Shutdown();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
    name: "AllowOrigin",
  builder => {
      builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
  });
});
builder.Services.AddMemoryCache();
builder.Services.AddStackExchangeRedisCache(option =>
{
    option.Configuration = "localhost: 6379";
});
builder.Services.AddDbContext<FundooDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FundooDB")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddTransient<IUserBN, UserBN>();
builder.Services.AddTransient<INoteRepo1, NoteRepo>();
builder.Services.AddTransient<INoteBN, NoteBN>();
builder.Services.AddTransient<ILabelBN, LabelBN>();
builder.Services.AddTransient<ILabelRepo, LabelRepo>();
builder.Services.AddTransient<ICollabRepo, CollabRepo>();
builder.Services.AddTransient<ICollabBN, CollabBN>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Welcome to FundooNotes" });
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "enter JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                 { jwtSecurityScheme, Array.Empty<string>() }
                });
});
var tokenKey = builder.Configuration.GetValue<string>("Jwt:key");
var key = Encoding.ASCII.GetBytes(tokenKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Logging.ClearProviders();
builder.Host.UseNLog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowOrigin");

app.MapControllers();
app.MapControllerRoute(
   name: "Admin",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
NLog.LogManager.Shutdown();
