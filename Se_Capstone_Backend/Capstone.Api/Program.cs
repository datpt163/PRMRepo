using Capstone.Api.Common.ConfigureService;
using Capstone.Api.Module.Profile.Validator;
using Capstone.Application;
using Capstone.Application.Common.Email;
using Capstone.Application.Common.Jwt;
using Capstone.Infrastructure.DbContext;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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
builder.Services.AddAuthSerivce(builder.Configuration);
builder.Services.AddDataService();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly));
builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UpdateUserValidator>());
builder.Services.AddGreetingService(builder.Configuration);
var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(); 
//}

app.UseHttpsRedirection();

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
