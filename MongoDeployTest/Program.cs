using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Repository;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<DbConfiguration>(configuration.GetSection("MongoDbConnection"));
builder.Services.AddScoped<ICustomerService, CustomerService>();
var dbConfiguration = builder.Configuration.GetSection("MongoDbConnection").Get<DbConfiguration>();
builder.Services.AddScoped<ICustomerRepository>( _ => new CustomerRepository(dbConfiguration));


BsonClassMap.RegisterClassMap<Customer>(cm =>
{
    cm.AutoMap();
    cm.MapIdMember(c => c.Id).SetIdGenerator(CombGuidGenerator.Instance); ;
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();