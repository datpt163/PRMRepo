using Capstone.Api.Common.ConfigureService;
using Capstone.Api.Module.Auth.Validator;
using Capstone.Application;
using Capstone.Application.Common.Email;
using Capstone.Application.Common.Jwt;
using Capstone.Domain.Entities;
using Capstone.Infrastructure.DbContexts;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation(fv =>
        fv.RegisterValidatorsFromAssemblyContaining<RegisterCommandValidator>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
builder.Services.AddSwaggerService();
builder.Services.AddDataService(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly));
builder.Services.AddIdentity<User, Capstone.Domain.Entities.Role>()
    .AddEntityFrameworkStores<SeCapstoneContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthSerivce(builder.Configuration);

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
        options.TokenLifespan = TimeSpan.FromDays(1));



builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = ConfigurationOptions.Parse(
    builder.Configuration.GetConnectionString("Redis") ?? "", true);
    configuration.ResolveDns = true;
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddGreetingService(builder.Configuration);
var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(); 
//}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

#region MigrateDbContext
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<ManageEmployeeDbContext>();
//    dbContext.Database.Migrate();
//}
#endregion
app.UseCors("AllowAll");
app.Run();
