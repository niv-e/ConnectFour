using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.Model.Boundaries;
using BusinessLogic.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ConnectFourGame.API.Controllers
{
    [Route("api/game")]
    [Controller]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IPlayerService _playerService = null!;
        private readonly IMapper _mapper;
        private readonly ILogger<GameController> _logger;


        public GameController(
            IGameService gameService,
            IPlayerService playerService,
            IMapper mapper,
            ILogger<GameController> logger)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(IGameService));
            _playerService = playerService ?? throw new ArgumentNullException(nameof(IPlayerService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(IMapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger));
        }

        [HttpGet("start/{playerId}")]
        
        public async Task<ActionResult> StartNewGame(int playerId)
        {
            try
            {
                PlayerBoundary? player = await _playerService.GetPlayerById(playerId);

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
        public async Task<ActionResult> PlacePawn(Guid sessionId, int colIndex)
        {
            var placmentResults = await _gameService.PlacePawn(sessionId, colIndex);

            if (placmentResults != null)
            {
                return Ok(placmentResults);
            }

            return BadRequest();
        }

        [HttpGet("getopponentmove/{sessionid}")]
        public async Task<ActionResult> GetOpponentMove(Guid sessionId)
        {
            var placmentResults = await _gameService.GetOpponentMove(sessionId);

            if (placmentResults != null)
            {
                return Ok(placmentResults);
            }

            return StatusCode(500);


        }

        [HttpGet("getwinnersequence/{sessionid}")]
        public async Task<ActionResult> GetWinnerSequenceIfExist(Guid sessionId)
        {

            try
            {
                IEnumerable<Tuple<int, int>> sequence = await _gameService.TryToGetWinnerSequence(sessionId);
                return Ok(sequence);

            }
            catch (Exception e)
            {

                return StatusCode(500,e);
            }
        }

        [HttpDelete("deletesession")]
        public async Task<ActionResult<bool>> DeleteSessionByGuid(string guid)
        {
            try
            {
                Guid g = Guid.Parse(guid);
                return await _gameService.DeleteGameByGuid(g);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
