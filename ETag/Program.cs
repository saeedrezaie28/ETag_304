using Delta;
using ETag.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(_ => new SqlConnection(config.GetConnectionString("cn")));
builder.Services.AddScoped<UserServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapControllers();
app.UseDelta();

var group = app.MapGroup("users");
group.MapGet("/", async (string name, int top, UserServices userServices) =>
{
    var user = await userServices.GetUserByName(name, top);
    return Results.Ok(user);
});


app.Run();
