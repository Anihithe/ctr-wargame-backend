using CtrWargame.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace CtrWargame.Infrastructure.Persistence;

public class CtrWargameDbContext(DbContextOptions<CtrWargameDbContext> options) : DbContext(options), ICtrWargameDbContext
{
    public async Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
    {
        return await Database.CanConnectAsync(cancellationToken);
    }
}