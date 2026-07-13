namespace CtrWargame.Application.Common.Messaging;

public interface ICommand : IRequest<Unit>;

public interface ICommand<TResponse> : IRequest<TResponse>;

public record Unit
{
    public static Unit Value => new();
}