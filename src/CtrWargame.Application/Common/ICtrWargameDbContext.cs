namespace CtrWargame.Application.Common;

public interface ICtrWargameDbContext
{
    Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);
}