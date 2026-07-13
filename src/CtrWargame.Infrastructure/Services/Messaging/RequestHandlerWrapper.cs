using CtrWargame.Application.Common.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace CtrWargame.Infrastructure.Services.Messaging;

internal abstract class RequestHandlerWrapper<TResponse>
{
    public abstract Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceProvider serviceProvider, CancellationToken cancellationToken);
}

internal class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse> where TRequest : IRequest<TResponse>
{
    public override Task<TResponse> HandleAsync(IRequest<TResponse> request, IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        return handler.HandleAsync((TRequest)request, cancellationToken);
    }
}