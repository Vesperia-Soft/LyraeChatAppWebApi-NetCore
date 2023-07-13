using LyraeChatApp.Application.Services;
using LyraeChatApp.Domain.UnitOfWork;
using LyraeChatApp.Infrastructure.Jwt;
using LyraeChatApp.Persistance.Mapping;
using LyraeChatApp.Persistance.Service;
using LyraeChatApp.Persistance.UnitOfWorkSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration.GetSection("Jwt:Token").Value!))
    };
});
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWorkSqlServer>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<ILogService,LogService>();
builder.Services.AddScoped<IJwtService,JwtService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(
    options =>
    {
        options.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "text/html";
            var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
            if (null != exceptionObject)
            {
                var errorMessage = $"{exceptionObject.Error.Message}";
                await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
            }
        });
    }
);

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
