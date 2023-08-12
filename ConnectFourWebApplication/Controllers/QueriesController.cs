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


        [HttpGet("players")]
        public async Task<IActionResult> GetAllPlayers(bool orderbyname)
        {
            var getResults = _queriesRepository.GetAllPlayers(orderbyname);

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
        
        [HttpGet("getallplayergames")]
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

        [HttpGet("getplayerspergamecount")]
        public IActionResult GetAllPlayersNumberOfGames()
        {
            var getResults = _queriesRepository.GetPlayersPerGameCount();

            return Ok(getResults);
        }

        [HttpGet("getplayerspercountry")]
        public IActionResult GetPlayersPerCountry()
        {
            var getResults = _queriesRepository.GetPlayersPerCountry();

            return Ok(getResults);
        }


    }
}
