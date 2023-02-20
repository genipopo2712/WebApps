using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApps;
using WebApps.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(p => //create authentication and option for [Authorize]
{
    p.ExpireTimeSpan = TimeSpan.FromDays(32); // remember user and password
    p.LoginPath = "/auth/login"; //link to page login if didn't login yet
    p.LogoutPath= "/auth/logout";
    //p.AccessDeniedPath= "/auth/denied";
});

builder.Services.AddTransient<IDbConnection, SqlConnection>(p => new SqlConnection(builder.Configuration.GetConnectionString("StackOverflow")));
builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();
builder.Services.AddTransient<IMemberRepository, MemberRepository>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();

builder.Services.AddDistributedSqlServerCache(p =>
{
    p.ConnectionString = builder.Configuration.GetConnectionString("DistributedCache");
    p.SchemaName = "dbo";
    p.TableName = "CacheStore";
});
builder.Services.AddSignalR();
builder.Services.AddMvc();
var app = builder.Build();

app.UseAuthentication();//if use .net6 add this line to use authentication
app.UseAuthorization(); //if use .net6 add this line to use authorization
app.MapHub<ChatHub>("/chathub");

app.UseStaticFiles();
app.MapDefaultControllerRoute();
//Area
app.MapControllerRoute(name: "dashboard", pattern: "{area:exists}/{controller=home}/{action=index}/{id?}");//map index ben ngoai va index trong Areas

app.Run();
