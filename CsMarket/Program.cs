using CsMarket.Data;
using CsMarket.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<UsersContext>(x => x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))); ;
builder.Services.AddTransient<IUserRepository, UserEFRepository>();
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddSteam(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
