using FirstApiProject;
using FirstApiProject.Data.DbContexts;
using FirstApiProject.Models;
using FirstApiProject.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Serilog;

#region Log
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/CityInfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CityContext>( options =>
{
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:CityConnectionString"]
        );
});

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//injoori mitoonim logg endakhtanamoon ro tanzim konim ya gheyre faal konim ya masalan faqat console kar kone o ... !

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(option =>
{
    //ba 2khat zir mitoonim default formate input outputemoon ro avaz konim
    //option.OutputFormatters.Add()
    //option.InputFormatters.Add()


    //age client formate dgi bekhad k ma nemidim (masalan yaru xml mikhad ma json midim) status code 406 mide behesh 
    option.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();// ino k bnvisim api ma alave bar xml, json ham mide 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IValidator<PointOfInterestForCreationDto>, PointOfInterestValidator>();
builder.Services.AddScoped<IValidator<PointOfInterestForUpdateDto>, PointOfInterestUpdateValidator>();
builder.Services.AddScoped<IMailService,LocalMailService>();
builder.Services.AddSingleton<CitiesDataStore>();
DependencyInjections.AddRepository(builder.Services);

var app = builder.Build();

#region Pipelines
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
#endregion


app.Run();
