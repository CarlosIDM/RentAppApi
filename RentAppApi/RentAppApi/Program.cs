using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using RentAppApi.DataBases;
using RentAppApi.Helpers;
using RentAppApi.Service.Categories;
using RentAppApi.Service.Codes;
using RentAppApi.Service.Products;
using RentAppApi.Service.Users;


//dotnet ef migrations add InitialCreate
//dotnet ef database update


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RentAppDbContext>();

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    x.JsonSerializerOptions.DictionaryKeyPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    x.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategory, Category>();
builder.Services.AddScoped<ICode, Code>();
builder.Services.AddScoped<IProductsService, ProductService>();
builder.Services.AddScoped<ISubCategories, SubCategories>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<RentAppDbContext>();
    dataContext.Database.Migrate();
}

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();