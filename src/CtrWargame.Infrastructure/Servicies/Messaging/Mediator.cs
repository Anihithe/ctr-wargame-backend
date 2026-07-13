using CtrWargame.Application.Common.Messaging;

namespace CtrWargame.Infrastructure.Servicies.Messaging;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requestType = request.GetType();
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));
        
        var handler = serviceProvider.GetService(handlerType);

        if (handler is null) throw new InvalidOperationException($"No handler found for {handlerType}");

        var method = handlerType.GetMethod(nameof(IRequestHandler<,>.HandleAsync));
        
        return await (Task<TResponse>)method!.Invoke(handler, [request, cancellationToken])!;
    }
}