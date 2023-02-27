// Create and configure app builder
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Actually build and run application
WebApplication app = builder.Build();
app.UseAuthorization();
app.MapControllers();
app.Run();
