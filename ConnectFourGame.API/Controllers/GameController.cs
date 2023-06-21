using AutoMapper;
using BusinessLogic;
using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;
using Model.Boundaries;
using Model.Dtos;
using Model.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ConnectFourGame.API.Controllers
{
    [Route("api/game")]
    [Controller]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IPlayerService _playerService = null!;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;


        public GameController(IGameService gameService, IPlayerService playerService, IMapper mapper, ILogger logger)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(IGameService));
            _playerService = playerService ?? throw new ArgumentNullException(nameof(IPlayerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger)); ;
        }

        [HttpGet("start/{playerId}")]
        
        public async Task<ActionResult> StartNewGame(int playerId)
        {
            try
            {
                Player? player = await _playerService.GetPlayerById(playerId);

                if (player == null)
                {
                    return NotFound(playerId);
                }

                GameSessionDto gameSession = await _gameService.StartGame(player);

                return CreatedAtRoute("GetSessionById",
                     new
                     {
                         sessionId = gameSession.SessionId
                     },
                     _mapper.Map<GameSessionBoundary>(gameSession));
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpGet("{sessionid}", Name = "GetSessionById")]
        public async Task<ActionResult> GetSessionById(Guid sessionId)
        {
            GameSessionDto? session = await _gameService.GetSessionById(sessionId);

            if(session == null) 
            {
                return NotFound(sessionId);
            }

            return Ok(_mapper.Map<GameSessionBoundary>(session));
        }
       
        [HttpGet("{sessionid}/{colIndex}")]
        public async Task<ActionResult> PlacePawn(Guid sessionId,int colIndex)
        {
            var placmentResults = await _gameService.PlacePawn(sessionId, colIndex);

            if (placmentResults == true)
            {
                return Ok();
            }

            return BadRequest();
        }

    }
}
