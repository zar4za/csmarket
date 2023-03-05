using CsMarket.Auth;
using CsMarket.Data;
using CsMarket.Infrastructure;
using CsMarket.Market;
using CsMarket.Steam;
using CsMarket.Steam.Inventory;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddDbContext<MarketContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IUserRepository, UserEFRepository>();
builder.Services.AddTransient<IUserSummaryProvider, SteamWebApiClient>();
builder.Services.AddTransient<AuthService>();
builder.Services.AddTransient<IInventoryFactory, SteamInventoryFactory>();
builder.Services.AddTransient<IListingRepository, ListingEFRepository>();
builder.Services.AddTransient<IMarketService, MarketService>();

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
