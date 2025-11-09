var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://+:8081");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/temperature", (string? location, string? sensorId) =>
{
    if (string.IsNullOrWhiteSpace(location))
    {
        location = sensorId switch
        {
            "1" => "Living Room",
            "2" => "Bedroom",
            "3" => "Kitchen",
            _ => "Unknown"
        };
    }
    
    if (string.IsNullOrWhiteSpace(sensorId))
    {
        sensorId = location switch
        {
            "Living Room" => "1",
            "Bedroom" => "2",
            "Kitchen" => "3",
            _ => "0"
        };
    }
    
    var random = new Random();
    var temperature = random.Next(-40, 40) + random.NextDouble();

    return Results.Ok(new
    {
        location,
        sensorId,
        value = Math.Round(temperature, 1),
        unit = "°C",
        status = "OK",       
        description = "",  
        timestamp = DateTime.UtcNow
    });
});

app.MapGet("/temperature/{sensorId}", (string sensorId) =>
{
    var location = sensorId switch
    {
        "1" => "Living Room",
        "2" => "Bedroom",
        "3" => "Kitchen",
        _ => "Unknown"
    };

    var random = new Random();
    var temperature = random.Next(-40, 40) + random.NextDouble();

    return Results.Ok(new
    {
        location,
        sensorId,
        value = Math.Round(temperature, 1),
        unit = "°C",
        status = "OK",
        description = "",
        timestamp = DateTime.UtcNow
    });
});


app.Run();
