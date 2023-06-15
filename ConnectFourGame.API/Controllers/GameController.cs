using AutoMapper;
using BusinessLogic;
using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;
using Model.Dtos;
using Model.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ConnectFourGame.API.Controllers
{
    [Route("api/game")]
    [Controller]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;
        private readonly IPlayerService _playerService = null!;
        private readonly IMapper _mapper;


        public GameController(GameService gameService, IPlayerService playerService, IMapper mapper)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(GameService));
            _playerService = playerService ?? throw new ArgumentNullException(nameof(IPlayerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
        }

        [HttpGet("start/{playerId}")]
        
        public async Task<ActionResult> StartNewGame(int playerId)
        {
            Player? player = await _playerService.GetPlayerById(playerId);

            if(player == null)
            {
                return NotFound(playerId);
            }

            GameSession gameSession = await _gameService.StartGame(player);

            return CreatedAtRoute("GetSessionById",
                 new
                 {
                     sessionId = gameSession.SessionId
                 },
                 gameSession);
        }

        [HttpGet("{sessionid}", Name = "GetSessionById")]
        public async Task<ActionResult> GetSessionById(Guid sessionId)
        {
            GameSession? session = await _gameService.GetSessionById(sessionId);

            if(session == null) 
            {
                return NotFound(sessionId);
            }

            return Ok(_mapper.Map<GameSession, GameSessionDto>(session));
        }
    }
}
