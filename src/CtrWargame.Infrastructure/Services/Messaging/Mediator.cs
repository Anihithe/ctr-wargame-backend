using System.Collections.Concurrent;
using CtrWargame.Application.Common.Messaging;

namespace CtrWargame.Infrastructure.Services.Messaging;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    private static readonly ConcurrentDictionary<Type, object> RequestWrapper = new();
    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requestType = request.GetType();

        var wrapper = RequestWrapper.GetOrAdd(requestType, type =>
        {
            var wrapperType = typeof(RequestHandlerWrapperImpl<,>).MakeGenericType(type, typeof(TResponse));
            return Activator.CreateInstance(wrapperType)!;
        });
        
        var requestHandlerWrapper = (RequestHandlerWrapper<TResponse>)wrapper;
        
        return requestHandlerWrapper.HandleAsync(request, serviceProvider, cancellationToken);
    }
}