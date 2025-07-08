using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "HW1", Version = "v1" });

    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();


app.UseStaticFiles(); 

if (true) 
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        var serverPrefix = app.Environment.IsDevelopment() ? "" : "/cgroup16/test2/tar1";
        c.SwaggerEndpoint($"{serverPrefix}/swagger/v1/swagger.json", "HW1 v1");

        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();
