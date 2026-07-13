using CtrWargame.Application.Common;
using CtrWargame.Application.Common.Messaging;

namespace CtrWargame.Application.Features;

public record PingQuery(string Message) :IQuery<PingResponse>;

public record PingResponse(string Message, bool DatabaseConnected, DateTimeOffset Timestamp);

public class PingHandler(ICtrWargameDbContext context) : IQueryHandler<PingQuery, PingResponse>
{
    public async Task<PingResponse> HandleAsync(PingQuery query, CancellationToken cancellationToken)
    {
        var dbConnected = await context.CanConnectAsync(cancellationToken);
        return new PingResponse($"Pong: {query.Message}", dbConnected, DateTimeOffset.UtcNow);
    }
}