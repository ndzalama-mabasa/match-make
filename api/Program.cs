using galaxy_match_make.Data;
using galaxy_match_make.Models;
using galaxy_match_make.Repositories;
using galaxy_match_make.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure JWT Bearer Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://accounts.google.com";
    options.Audience = builder.Configuration["Google:ClientId"];
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<DapperContext>();

builder.Services.AddScoped<IPlanetRepository, PlanetRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IReactionRepository, ReactionRepository>();

builder.Services.AddScoped<IGenericRepository<CharacteristicsDto>, GenericRepository<CharacteristicsDto>>();
builder.Services.AddScoped<IGenericRepository<ProfileAttributesDto>, GenericRepository<ProfileAttributesDto>>();
builder.Services.AddScoped<IGenericRepository<ProfilePreferencesDto>, GenericRepository<ProfilePreferencesDto>>();

builder.Services.AddScoped<IGenericRepository<ProfileDto>, GenericRepository<ProfileDto>>();
builder.Services.AddScoped<IGenericService<ProfileDto>, GenericService<ProfileDto>>();
builder.Services.AddScoped<IProfileService, ProfileService>();

builder.Services.AddScoped<GoogleAuthService>();
builder.Services.AddHttpClient();

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

app.UseAuthentication();
app.UseMiddleware<MiddlewareService>();
app.UseAuthorization();

app.MapControllers();

app.Run();
