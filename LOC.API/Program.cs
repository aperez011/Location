using LOC.DataAccess;
using LOC.Services;
using LOC.Utilities.ServiceInterfaces;

var builder = WebApplication.CreateBuilder(args);


//Context
builder.Services.AddSingleton<dbContextLocations, dbContextLocations>();

builder.Services.AddScoped<ILocationServices, LocationServices>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
