using AutoMapper;
using BusinessLogic.Contracts;
using Microsoft.AspNetCore.Mvc;
using Model.bounderies;
using Model.Dtos;
using Model.Entities;

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
        public async Task<ActionResult<PlayerDto>> RegisterPlayer(
            PlayerBoundary playerBoundary)
        {
            try
            {
                Player player = await _playerService.RegisterPlayer(playerBoundary);

                return Ok(_mapper.Map<Player,PlayerDto>(player));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
