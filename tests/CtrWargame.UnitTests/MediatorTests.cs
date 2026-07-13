using CtrWargame.Application.Common.Messaging;
using CtrWargame.Infrastructure.Services.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CtrWargame.UnitTests;

public class MediatorTests
{
    private ServiceProvider _serviceProvider = null!;
    private IMediator _mediator = null!;
    private List<string> _executionLog = null!;

    [SetUp]
    public void Setup()
    {
        _executionLog = [];
        var services = new ServiceCollection();
        services.AddScoped<IMediator, Mediator>();
        services.AddSingleton(_executionLog);
        services.AddTransient<IPipelineBehavior<TestRequest, TestResponse>, FirstTestBehavior>();
        services.AddTransient<IPipelineBehavior<TestRequest, TestResponse>, SecondTestBehavior>();
        services.AddTransient<IRequestHandler<TestRequest, TestResponse>, TestRequestHandler>();

        _serviceProvider = services.BuildServiceProvider();
        _mediator = _serviceProvider.GetRequiredService<IMediator>();
    }

    [TearDown]
    public void TearDown()
    {
        _serviceProvider.Dispose();
    }

    [Test]
    public async Task SendAsync_ShouldExecutePipelineBehaviorsInOrder()
    {
        // Arrange
        var request = new TestRequest("Ping");

        // Act
        var response = await _mediator.SendAsync(request);

        // Assert
        Assert.That(response.Result, Is.EqualTo("Pong: Ping"));

        var expectedOrder = new List<string>
        {
            "First:Before",
            "Second:Before",
            "Handled",
            "Second:After",
            "First:After"
        };

        Assert.That(_executionLog, Is.EqualTo(expectedOrder));
    }
}

// Abstractions de test locales
public record TestRequest(string Message) : IRequest<TestResponse>;

public record TestResponse(string Result);

public class TestRequestHandler(List<string> log) : IRequestHandler<TestRequest, TestResponse>
{
    public Task<TestResponse> HandleAsync(TestRequest request, CancellationToken cancellationToken)
    {
        log.Add("Handled");
        return Task.FromResult(new TestResponse($"Pong: {request.Message}"));
    }
}

public class FirstTestBehavior(List<string> log) : IPipelineBehavior<TestRequest, TestResponse>
{
    public async Task<TestResponse> HandleAsync(TestRequest request, RequestHandlerDelegate<TestResponse> next,
        CancellationToken cancellationToken)
    {
        log.Add("First:Before");
        var response = await next();
        log.Add("First:After");
        return response;
    }
}

public class SecondTestBehavior(List<string> log) : IPipelineBehavior<TestRequest, TestResponse>
{
    public async Task<TestResponse> HandleAsync(TestRequest request, RequestHandlerDelegate<TestResponse> next,
        CancellationToken cancellationToken)
    {
        log.Add("Second:Before");
        var response = await next();
        log.Add("Second:After");
        return response;
    }
}