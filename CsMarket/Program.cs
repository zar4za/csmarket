using CsMarket.Auth;
using CsMarket.Data;
using CsMarket.Infrastructure;
using CsMarket.Steam;
using CsMarket.Steam.Inventory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<UsersContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IUserRepository, UserEFRepository>();
builder.Services.AddTransient<IUserSummaryProvider, SteamWebApiClient>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<IInventoryFactory, SteamSupplyInventoryFactory>();

builder.Services.AddJwt(builder.Configuration);
builder.Services.AddSteam(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var corsDevScheme = "devScheme";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsDevScheme,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "http://localhost:8080");
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(corsDevScheme);
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
