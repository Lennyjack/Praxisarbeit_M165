using MongoDB.Driver;  // For MongoDB client and collections
using MongoDB.Bson;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;
using JetstreamAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MongoDB settings from appsettings.json for configuration
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

// Register MongoDB client with the connection string from the configuration
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = builder.Configuration.GetSection("MongoDbSettings").Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);  // Create MongoClient using the connection string
});

// Register MongoDbService to handle MongoDB operations as a singleton
builder.Services.AddSingleton<MongoDbService>();

// Register UserService to handle user authentication and management as a singleton
builder.Services.AddSingleton<UserService>();

// Add Authentication services and configure JWT Bearer for token-based authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,  // Validate token issuer
            ValidateAudience = true,  // Validate token audience
            ValidateLifetime = true,  // Validate token expiration
            ValidateIssuerSigningKey = true,  // Validate the signing key of the token
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"])),  // Secret key for token signing
            ClockSkew = TimeSpan.Zero, // Set to zero to avoid delay in token expiration
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],  // Issuer value from appsettings
            ValidAudience = builder.Configuration["JwtSettings:Audience"]  // Audience value from appsettings
        };
    });

// Add MVC controllers to the services
builder.Services.AddControllers();

// Enable Swagger for API documentation and exploration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger UI only in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();  // Enable Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "JetstreamAPI v1");  // Set Swagger endpoint
        c.RoutePrefix = string.Empty;  // Make Swagger UI available at the root URL
    });
}

// Use authentication middleware to handle JWT token authentication
app.UseAuthentication();

// Use authorization middleware to enforce authorization rules
app.UseAuthorization();

app.UseHttpsRedirection();  // Enforce HTTPS

// Map controllers to the HTTP request pipeline
app.MapControllers();

// Run the application
app.Run();
