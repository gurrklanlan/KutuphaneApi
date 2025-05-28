using System.Text;
using KutuphaneCore.Entities;
using KutuphaneDataAcces;
using KutuphaneDataAcces.Repository;
using KutuphaneService.Interfaces;
using KutuphaneService.Mapper;
using KutuphaneService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Serilog;



Log.Logger = new LoggerConfiguration()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// RateLimiter Yapýlandýrmasý
builder.Services.AddRateLimiter(x =>
{
    x.AddFixedWindowLimiter("RateLimiter", x =>
    {
        x.PermitLimit = 5;
        x.Window = TimeSpan.FromSeconds(8);
        x.QueueProcessingOrder =System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
        x.QueueLimit = 2; //kuyrukta beklenecek request

    });

});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer= builder.Configuration["Jwt:Issuer"],
      ValidAudience= builder.Configuration["Jwt:Audience"],
      IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))

    };

});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "Kutuphane API", Version = "v1"});

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        
     {
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Reference = new Microsoft.OpenApi.Models.OpenApiReference
            {
                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                Id ="Bearer"
            }
        },
      
            
            new string[] {}
     }

    });

});






builder.Services.AddDbContext<KutuphaneDbContext>(options =>
{
    var connectionStrings= builder.Configuration.GetSection
    (ConnectionStringOption.Key).Get<ConnectionStringOption>();

    options.UseSqlServer(connectionStrings!.SqlServer);
});
builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddScoped<IGenericRepository<Author>, GenericRepository<Author>>();
builder.Services.AddScoped<IAuthorService, AuthorService>();

builder.Services.AddScoped<IGenericRepository<Book>, GenericRepository<Book>>();
builder.Services.AddScoped<IBooksService, KutuphaneService.Services.BookService>();

builder.Services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();
