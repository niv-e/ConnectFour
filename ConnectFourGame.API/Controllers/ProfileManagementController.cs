using AutoMapper;
using BusinessLogic.Contracts;
using BusinessLogic.Model.Boundaries;
using BusinessLogic.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ConnectFourGame.API.Controllers
{
    [Route("api/profile")]
    [ApiController]
    public class ProfileManagementController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ILogger<ProfileManagementController> _logger;
        private readonly IMapper _mapper;


        public ProfileManagementController(ILogger<ProfileManagementController> logger, IPlayerService playerService, IMapper mapper)
        {
            _logger = logger;
            _playerService = playerService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<PlayerBoundary>> RegisterPlayer(
            PlayerBoundary playerBoundary)
        {
            try
            {
                 return await _playerService.RegisterPlayer(playerBoundary);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{playerid}", Name = "GetPlayerById")]
        public async Task<ActionResult<PlayerBoundary>> GetPlayerById(int playerid)
        {
            try
            {
                PlayerBoundary? foundedPlayer = await _playerService.GetPlayerById(playerid);

                return foundedPlayer is null ?
                    BadRequest($"Could not found player with id {playerid}") : 
                    foundedPlayer;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
