using BillPayments_LookUp_Validation.Data;
using BillPayments_LookUp_Validation.Services;
using BillPayments_LookUp_Validation.Services.Authentication;
using BillPayments_LookUp_Validation.ServicesImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Load app.json configuration
builder.Configuration.AddJsonFile("appsettings.json");

// Additional configuration sources (optional)
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IValidate, ValidateImplement>();
builder.Services.AddScoped<IValidate_MasvingoPolyCollege, ValidateImplement_MasvingoPolyCollege>();
builder.Services.AddScoped<IGetCOHBalance, GetCOHBalance>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IWalletAccountService, WalletAccountService>();
// SQL Server Connection
builder.Services.AddDbContext<MasvingoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("localDb")));

// Oracle Connection
//builder.Services.AddDbContext<FlexicubeContext>(options =>
//    options.UseOracle(builder.Configuration.GetConnectionString("flexcube")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
