using DAL.Contracts;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace DAL
{
    public class GameSessionRepository : IGameSessionRepository
    {
        private readonly ConnectFourDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public GameSessionRepository(ConnectFourDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task Insert(GameSession entity)
        {

            var gameSessionState = _context.Entry(entity).State;
            var playerState = _context.Entry(entity.Player).State;

            if (playerState == EntityState.Detached)
            {
                entity.Player = _context.Find<Player>(entity.Player.PlayerId);
            }

            await _context.GameSessions.AddAsync(entity);


            _memoryCache.Set(entity.SessionId, entity, ServiceCacheOption);

            await _context.SaveChangesAsync();
        }
        public async Task<GameSession?> GetSessionById(Guid guid)
        {
            if(_memoryCache.TryGetValue(guid, out GameSession cachedSession))
            {
                var a = _context.Entry(cachedSession).Entity;
                var b = _context.Entry(cachedSession).State;

                return cachedSession;
            }    

            var session = await _context
            .GameSessions
            .Include(g => g.Player)
            .Include(g => g.GameState)
            .Where(g => g.SessionId == guid)
            .FirstOrDefaultAsync();

            _memoryCache.Set(session.SessionId, session, ServiceCacheOption);

            return session;

        }
        public async Task Update(GameSession entity)
        {
            _memoryCache.Set(entity.SessionId, entity, ServiceCacheOption);

            var gameSessionState = _context.Entry(entity).State;
            var gameStateState = _context.Entry(entity.GameState).State;

            if(gameSessionState == EntityState.Detached)
            {
                gameSessionState = EntityState.Modified;
            }
            if(gameStateState == EntityState.Detached)
            {
                gameStateState = EntityState.Modified;
            }
            _context.GameSessions.Update(entity);


            await _context.SaveChangesAsync();
        }
        public Task SaveToDatabase() => _context
                        .SaveChangesAsync(true);

        public async Task<bool> DeleteGameById(Guid guid)
        {
            await _context.GameSessions
                .Where(g => g.SessionId == guid)
                .ExecuteDeleteAsync();
            return true;
        }

        private MemoryCacheEntryOptions ServiceCacheOption => new MemoryCacheEntryOptions()
        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
        .SetAbsoluteExpiration(TimeSpan.FromSeconds(60 * 5))
        .SetPriority(CacheItemPriority.Normal);

    }
}
