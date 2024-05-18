using DATN_back_end.Common;
using DATN_back_end.Common.CurrentUserService;
using DATN_back_end.Common.FileService;
using DATN_back_end.Data;
using DATN_back_end.Data.UnitOfWork;
using DATN_back_end.Entities;
using DATN_back_end.Middlewares;
using DATN_back_end.Services.AuthService;
using DATN_back_end.Services.CompanyService;
using DATN_back_end.Services.DashBoardService;
using DATN_back_end.Services.JobPostingService;
using DATN_back_end.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (!builder.Environment.IsDevelopment())
{
    builder.Configuration["Secret"] = Environment.GetEnvironmentVariable("Secret");
    builder.Configuration["DefaultConnection"] = Environment.GetEnvironmentVariable("DefaultConnection");
    builder.Configuration["Minio:EndPoint"] = Environment.GetEnvironmentVariable("Minio:EndPoint");
    builder.Configuration["Minio:AccessKey"] = Environment.GetEnvironmentVariable("Minio:AccessKey");
    builder.Configuration["Minio:SecretKey"] = Environment.GetEnvironmentVariable("Minio:SecretKey");
    builder.Configuration["Minio:BucketName"] = Environment.GetEnvironmentVariable("Minio:BucketName");
    builder.Configuration["Minio:UseSSL"] = Environment.GetEnvironmentVariable("Minio:UseSSL");
}

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        }
    );

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
        }
    );
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IEmployerDashboardService, EmployerDashboardService>();
builder.Services.AddScoped<IJobPostingService, JobPostingService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;

app.UseSwagger();
app.UseSwaggerUI();

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
