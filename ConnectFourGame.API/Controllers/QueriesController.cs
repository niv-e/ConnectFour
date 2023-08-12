using DAL.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ConnectFourGame.API.Controllers
{
    [Route("api/queries")]
    [ApiController]
    public class QueriesController : ControllerBase
    {
        private readonly IQueriesRepository _queriesRepository;

        public QueriesController(IQueriesRepository queriesRepository)
        {
            _queriesRepository = queriesRepository;
        }


        [HttpGet("players/{orederbyname}")]
        public async Task<IActionResult> GetAllPlayers(bool orederbyname)
        {
            var getResults = _queriesRepository.GetAllPlayers(orederbyname);

            return Ok(getResults);
        }

        [HttpGet("playerslastgame")]
        public IActionResult GetAllPlayersLastGameDateDescending()
        {
            var getResults = _queriesRepository.GetAllPlayersLastGameDateDescending();

            return Ok(getResults);
        }
        [HttpGet("gamedetails")]
        public IActionResult GetAllGameDetails()
        {
            var getResults = _queriesRepository.GetAllGamesDetails();

            return Ok(getResults);
        }

        [HttpGet("gamedetailsdistinct")]
        public IActionResult GetAllGameDetailsDistinct()
        {
            var getResults = _queriesRepository.GetAllGamesDetailsDistinct();

            return Ok(getResults);
        }        
        
        [HttpGet("getallplayergames/{playerid}")]
        public IActionResult GetAllPlayerGames(int playerid)
        {
            var getResults = _queriesRepository.GetAllPlayerGames(playerid);

            return Ok(getResults);
        }
        
        [HttpGet("getallplayersgamescount")]
        public IActionResult GetAllPlayersGamesCount()
        {
            var getResults = _queriesRepository.GetAllPlayersNumberOfGames();

            return Ok(getResults);
        }

        [HttpGet("getallplayersnumberofgames")]
        public IActionResult GetAllPlayersNumberOfGames()
        {
            var getResults = _queriesRepository.GetPlayersPerGameCount();

            return Ok(getResults);
        }


    }
}
