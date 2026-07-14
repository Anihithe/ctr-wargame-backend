using CtrWargame.Application.Common.Messaging;
using CtrWargame.Application.Features;
using CtrWargame.WebApi.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddWebApi(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapPost("/ping", async (PingQuery query, IMediator mediator, CancellationToken cancellationToken) =>
{
    var response = await mediator.SendAsync(query, cancellationToken);
    return Results.Ok(response);
});

app.UseHttpsRedirection();

app.Run();