using galaxy_match_make.Data;
using galaxy_match_make.Repositories;
using galaxy_match_make.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IPlanetRepository, PlanetRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<GoogleAuthService>();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddSingleton(sp =>
{
    var context = sp.GetRequiredService<DapperContext>();
    return context.CreateConnection();
});
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<MiddlewareService>();

app.UseAuthorization();

app.MapControllers();

app.Run();
