using CtrWargame.Application.Common.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CtrWargame.Infrastructure.Services.Messaging;

internal abstract class RequestHandlerWrapper<TResponse>
{
    public abstract Task<TResponse> HandleAsync(
        IRequest<TResponse> request,
        IServiceProvider serviceProvider,
        CancellationToken cancellationToken);
}

internal class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse>
    where TRequest : IRequest<TResponse>
{
    public override Task<TResponse> HandleAsync(
        IRequest<TResponse> request,
        IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
    {
        var handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        
        var behaviors = serviceProvider.GetServices<IPipelineBehavior<TRequest, TResponse>>();
        
        RequestHandlerDelegate<TResponse> handlerDelegate = () => handler.HandleAsync((TRequest)request, cancellationToken);

        var pipeline = behaviors.Reverse().Aggregate(
            handlerDelegate,
            (next, behavior) =>
                () => behavior.HandleAsync((TRequest)request, next, cancellationToken));
        return pipeline();
    }
}