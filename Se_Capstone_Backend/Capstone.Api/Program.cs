using Capstone.Api.Common.ConfigureService;
using Capstone.Api.Middleware;
using Capstone.Api.Module.Auth.Validator;
using Capstone.Api.Module.Statuses.SignalR;
using Capstone.Application;
using Capstone.Application.Common.Email;
using Capstone.Application.Common.Jwt;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContexts;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<RegisterCommandValidator>());

builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
var corsUrls = builder.Configuration.GetSection("Cors:Urls").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
           
            policy.WithOrigins(corsUrls)
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                .AllowCredentials(); 
        });
});
builder.Services.AddSignalR();
builder.Services.AddSwaggerService();
builder.Services.AddDataService(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly));
builder.Services.AddIdentity<User, Capstone.Domain.Entities.Role>()
    .AddEntityFrameworkStores<SeCapstoneContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthSerivce(builder.Configuration);
builder.Services.AddMassTransitService(builder.Configuration);


builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
        options.TokenLifespan = TimeSpan.FromDays(1));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(
    builder.Configuration.GetConnectionString("Redis") ?? "", true);
    configuration.ResolveDns = true;
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddGreetingService(builder.Configuration);
var app = builder.Build();

#region Cultures
var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("vi")
    };

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});
#endregion

app.UseMiddleware<BlacklistedTokenMiddleware>();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();
//}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

#region MigrateDbContext
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SeCapstoneContext>();
    dbContext.Database.Migrate();
}
#endregion



app.MapHub<StatusHub>("/statusHub");
app.Run();
