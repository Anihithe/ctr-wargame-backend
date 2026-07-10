using Microsoft.EntityFrameworkCore;

namespace CtrWargame.Infrastructure.Persistence;

public class CtrWargameDbContext(DbContextOptions<CtrWargameDbContext> options) : DbContext(options);