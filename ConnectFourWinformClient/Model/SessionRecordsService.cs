using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourWinformClient.Model
{
    public class SessionRecordsService
    {
        private readonly ClientDbContext _dbContext;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public SessionRecordsService(ClientDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveStep(Guid sessionId, GameStep step)
        {
            await _semaphore.WaitAsync(); // Asynchronous semaphore acquisition

            try
            {
                var sessionRecordEntity = await _dbContext
                    .SessionRecord
                    .FirstOrDefaultAsync(record => record.SessionId == sessionId);

                if (sessionRecordEntity == null)
                {
                    sessionRecordEntity = new SessionRecordEntity();
                    sessionRecordEntity.SessionId = sessionId;
                    sessionRecordEntity.FormatedGameSteps = step.ToFormatedGameStep();
                    await _dbContext.AddAsync(sessionRecordEntity);

                }
                else
                {
                    sessionRecordEntity.FormatedGameSteps = 
                        sessionRecordEntity.FormatedGameSteps +
                        $"&{step.ToFormatedGameStep()}";
                    _dbContext.Update(sessionRecordEntity);

                }
                await _dbContext.SaveChangesAsync();
            }
            finally
            {
                _semaphore.Release(); // Release the lock asynchronously
            }


        }

        public async Task<SessionRecordDto> GetRecordBySessionId(Guid sessionId)
        {
            await _semaphore.WaitAsync(); // Asynchronous semaphore acquisition

            try
            {
                var sessionRecordEntity = await _dbContext
                    .SessionRecord
                    .FirstOrDefaultAsync(record => record.SessionId == sessionId);

                if (sessionRecordEntity == null)
                {
                    throw new KeyNotFoundException($"Could not find record for session id : {sessionId}");
                }

                var gameStep = new GameStep();
                //Player,5,0&Server,5,3&Player,4,0&Server,4,3&Player,3,0&Server,5,6&Player,2,0
                var gameSteps = sessionRecordEntity
                    .FormatedGameSteps
                    .Split('&')
                    .Select(s => gameStep.FromFormatedGameStep(s))
                    .ToList();

                return new SessionRecordDto { SessionId = sessionId, GameSteps = gameSteps };

            }
            finally
            {
                _semaphore.Release(); // Release the lock asynchronously
            }





        }


    }
}
