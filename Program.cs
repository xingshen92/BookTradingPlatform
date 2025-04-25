using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BookTradingPlatform.Data;
using System.Text;
using Microsoft.OpenApi.Models;
using BookTradingPlatform.Services;

var builder = WebApplication.CreateBuilder(args);


// 註冊服務
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//=========================phpadmin sql=========================//
builder.Services.AddDbContext<WebDatabase>(options => 
        options.UseMySql(builder.Configuration.GetConnectionString("WebDatabase"),new MySqlServerVersion(new Version(8, 0, 29))));
//var connectionString = builder.Configuration.GetConnectionString("WebDatabase");
//builder.Services.AddDbContext<WebDatabase>(options =>
//	options.UseMySql(
//		connectionString,
//		ServerVersion.AutoDetect(connectionString) // 这里会自动检测 MySQL 版本
//	)
//);
//builder.Services.AddDbContext<WebDatabase>(options => options.UseMySQL(builder.Configuration.GetConnectionString("WebDatabase")));
//=========================JWT Token身份驗證=========================//
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
		var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = builder.Configuration["Jwt:Issuer"],
			ValidAudience = builder.Configuration["Jwt:Audience"],
			IssuerSigningKey = new SymmetricSecurityKey(key)
		};
    });
builder.Services.AddScoped<JwtToken>();
//=========================axios=========================//
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
					  policy =>
					  {
						  policy.AllowAnyOrigin() // Vue3 IP位置 WithOrigins("http://15.235.167.223")
								.AllowAnyHeader()
								.AllowAnyMethod();
					  });
});
//=========================IP 監聽訪問=========================//
builder.WebHost.UseUrls("http://0.0.0.0:2025");
//==================================================//
// 構建應用程式
var app = builder.Build();
// 配置中介軟體
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseRouting();
app.UseAuthentication();  // 啟用身份驗證
app.UseAuthorization();   // 啟用授權

app.UseHttpsRedirection(); //強制執行https
app.MapControllers();

app.Urls.Add("http://0.0.0.0:2025"); // 你的 API 端口
app.UseCors(MyAllowSpecificOrigins); // 啟用 CORS 策略

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
