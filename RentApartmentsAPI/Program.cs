using Microsoft.EntityFrameworkCore;
using RentApartmentsAPI;
using RentApartmentsAPI.Data;
using RentApartmentsAPI.Repositories;
using RentApartmentsAPI.Repositories.RepositoryInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnection"))
    );
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
builder.Services.AddControllers()
    .AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
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
