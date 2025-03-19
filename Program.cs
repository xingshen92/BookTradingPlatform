using BookTradingPlatform.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//=========================phpadmin sql=========================//
builder.Services.AddDbContext<WebDatabase>(options => 
        options.UseMySQL(builder.Configuration.GetConnectionString("WebDatabase"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//var connectionString = builder.Configuration.GetConnectionString("WebDatabase");
//builder.Services.AddDbContext<WebDatabase>(options =>
//	options.UseMySql(
//		connectionString,
//		ServerVersion.AutoDetect(connectionString) // 这里会自动检测 MySQL 版本
//	)
//);
//builder.Services.AddDbContext<WebDatabase>(options => options.UseMySQL(builder.Configuration.GetConnectionString("WebDatabase")));
//=========================JWT Token身份驗證=========================//
var key = Encoding.UTF8.GetBytes("Your_Secret_Key_Replace_With_Actual_Secret");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

app.UseAuthentication();  // 啟用身份驗證
app.UseAuthorization();   // 啟用授權
//=========================axios=========================//
//var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
//
//builder.Services.AddCors(options =>
//{
//	options.AddPolicy(name: MyAllowSpecificOrigins,
//					  policy =>
//					  {
//						  policy.WithOrigins("http://localhost:5134") // Vue3 开发服务器地址
//								.AllowAnyHeader()
//								.AllowAnyMethod();
//					  });
//});
//
//var app = builder.Build();
//app.Urls.Add("http://localhost:5000"); // 你的 API 端口
//
//app.UseCors(MyAllowSpecificOrigins);

//==================================================//
app.UseHttpsRedirection(); //強制執行https
app.UseAuthorization();
app.MapControllers();

app.Run();

//=========================IP Adders=========================//
//builder.Services.AddHttpsRedirection(options =>
//{
//    options.HttpsPort = null;
//});

// 明确监听 HTTP 和 HTTPS 端口
//builder.WebHost.ConfigureKestrel(serverOptions =>
//{
//    serverOptions.ListenAnyIP(5134); // HTTP 端口
//    serverOptions.ListenAnyIP(7134, listenOptions => listenOptions.UseHttps()); // HTTPS 端口
//});
//==================================================//