using Newtonsoft.Json.Serialization;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------
// FORÇAR TLS 1.2 PARA SQL SERVER
// ------------------------------
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

// ------------------------------
// ADD SERVICES
// ------------------------------
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JSON Serializer com Newtonsoft.Json
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ContractResolver = new DefaultContractResolver());

// ------------------------------
// BUILD APP
// ------------------------------
var app = builder.Build();

// ------------------------------
// ENABLE CORS (para teste/development)
// ------------------------------
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

// ------------------------------
// MIDDLEWARES
// ------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

// ------------------------------
// MAP CONTROLLERS
// ------------------------------
app.MapControllers();

// ------------------------------
// RUN APP
// ------------------------------
app.Run();