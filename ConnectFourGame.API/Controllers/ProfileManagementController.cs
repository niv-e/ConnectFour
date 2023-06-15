using AutoMapper;
using BusinessLogic;
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
        private readonly ILogger<ProfileManagementController> _logger;
        private readonly IRepository<Player> _playerRepository;
        private readonly IMapper _mapper;

        public ProfileManagementController(ILogger<ProfileManagementController> logger, IRepository<Player> playerRepository, IMapper mapper)
        {
            _logger = logger;
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<PlayerDto>> RegisterPlayer(
            PlayerBoundary playerBoundary)
        {
            try
            {
                Player player = _mapper.Map<PlayerBoundary, Player>(playerBoundary);

                await _playerRepository.Insert(player);

                return Ok(_mapper.Map<Player, PlayerDto>(player));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
