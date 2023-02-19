using CsMarket.Auth;
using CsMarket.Data;
using CsMarket.Infrastructure;
using CsMarket.Steam;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<UsersContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); ;
builder.Services.AddTransient<IUserRepository, UserEFRepository>();
builder.Services.AddTransient<IUserSummaryProvider, MockUserSummaryProvider>();
builder.Services.AddTransient<AuthService>();

builder.Services.AddJwt(builder.Configuration);
builder.Services.AddSteam(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
