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
    p.LogoutPath = "/auth/logout";
    //p.AccessDeniedPath= "/auth/denied";
}).AddFacebook(opt =>
{
    opt.ClientId = "1369697397155340";
    opt.ClientSecret = "d012a05d1009916928c4961ed3c6d281";
}).AddGoogle(opt =>
{
    opt.ClientId = "409014410886-ac8rv0me2tpfvdouo4d9h6pd57sms7c3.apps.googleusercontent.com";
    opt.ClientSecret = "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";
});


builder.Services.AddTransient<ContactFilter>();
builder.Services.AddTransient<IDbConnection, SqlConnection>(p => new SqlConnection(builder.Configuration.GetConnectionString("StackOverflow")));
builder.Services.AddTransient<IStatisticRepository, StatisticRepository>();
builder.Services.AddTransient<IMemberRepository, MemberRepository>();
builder.Services.AddTransient<IMessageRepository, MessageRepository>();
builder.Services.AddTransient<IWorkRepository, WorkRepository>();
builder.Services.AddTransient<IContactRepository, ContactRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IMemberInRoleRepository, MemberInRoleRepository>();

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
