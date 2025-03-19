using BookTradingPlatform.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
//var connectionString = builder.Configuration.GetConnectionString("WebDatabase");
//builder.Services.AddDbContext<WebDatabase>(options =>
//	options.UseMySql(
//		connectionString,
//		ServerVersion.AutoDetect(connectionString) // 这里会自动检测 MySQL 版本
//	)
//);
//builder.Services.AddDbContext<WebDatabase>(options => options.UseMySQL(builder.Configuration.GetConnectionString("WebDatabase")));
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
