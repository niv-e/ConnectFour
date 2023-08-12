using Azure;
using ConnectFourWebApplication.Pages.Shared;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace ConnectFourWebApplication.Pages.Queries
{
    [IgnoreAntiforgeryToken]
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConnectFourDbContext _context;
        [BindProperty]
        public bool OrderByName { get; set; }
        [BindProperty]
        public List<SelectListItem> Players { get; set; } = new List<SelectListItem>();

        [BindProperty]
        public string SelectedPlayerId { get; set; } = "Nan";
        public DAL.Entities.Player SelectedPlayer { get; set; }
        public List<DAL.Entities.Player> PlayersDetails { get; set; } = new();

        public List<string> Columns { get; set; } = new();


        public IndexModel(IHttpClientFactory httpClientFactory, ConnectFourDbContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }

        public IEnumerable<_ButtonInfo> Buttons = new List<_ButtonInfo>
        {
            new _ButtonInfo 
            { 
                Name = "Show Players",
                Endpoint = "api/queries/players",
                //Argument from check box 
            },
            new _ButtonInfo 
            {
                Name = "Get All Players Last Game Date Descending",
                Endpoint = "api/queries/playerslastgame"
            },
            new _ButtonInfo 
            {
                Name = "Get All Games Details",
                Endpoint = "api/queries/gamedetails"
            },
            new _ButtonInfo 
            {
                Name = "Get All Games Details Distinct",
                Endpoint = "api/queries/gamedetailsdistinct"
            },
            new _ButtonInfo 
            {
                Name = "Get All Player Games",
                Endpoint = "api/queries/getallplayergames"
                //Argument from combo box
            },
            new _ButtonInfo 
            {
                Name = "Get All Players Games Count",
                Endpoint = "api/queries/getallplayersgamescount"
            },
            new _ButtonInfo 
            {
                Name = "Get Players Per Game Count",
                Endpoint = "api/queries/getplayerspergamecount"
            },
            new _ButtonInfo 
            {
                Name = "Get Players Per Country",
                Endpoint = "api/queries/getplayerspercountry"
            },
        };

        public IActionResult OnPostSelectedPlayer(string selectedPlayer)
        {
            PopulateDropBox();

            SelectedPlayer = _context
                .Players
                .Single(p => p.PlayerId == int.Parse(selectedPlayer));

            Columns = SelectedPlayer.GetType()
                .GetProperties()
                .Select(p => p.Name)
                .ToList();
            return Page();

        }
        public async Task<IActionResult> OnPostShowAllPlayers()
        {
            PopulateDropBox();

            HttpResponseMessage responseMessage = null;
            if (OrderByName)
            {
                responseMessage = await CallButtonEndPoint("Show Players", new Dictionary<string, string>{ { "orderbyname", OrderByName.ToString() } });
            }
            else
            {
                responseMessage = await CallButtonEndPoint("Show Players", new Dictionary<string, string>());
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                PlayersDetails = JsonSerializer.Deserialize<List<DAL.Entities.Player>>(content, options) ??
                    throw new InvalidCastException();
            }

            Columns = PlayersDetails.First().GetType()
                        .GetProperties()
                        .Select(p => p.Name)
                        .ToList();

            return Page();

        }


        public async Task<JsonResult> OnPostUpdateTable(string buttonName, Dictionary<string, string> arguments)
        {
            HttpResponseMessage response = await CallButtonEndPoint(buttonName, arguments);

            JsonArray results = null;

            var updatedData = new List<TableRow>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jobject = JsonObject.Parse(content);

                if (jobject is JsonArray jobjectArray)
                {
                    var tableColumns = new TableRow();

                    foreach (KeyValuePair<string, JsonNode> property in jobjectArray.FirstOrDefault().AsObject())
                    {
                        tableColumns.Values.Add(property.Key);
                    }
                    updatedData.Add(tableColumns);

                    foreach (var item in jobjectArray)
                    {
                        var tableRow = new TableRow();

                        foreach (KeyValuePair<string, JsonNode> property in item.AsObject())
                        {
                            tableRow.Values.Add(property.Value.ToString());
                        }
                        updatedData.Add(tableRow);

                    }
                }

            }
            return new JsonResult(updatedData);

        }

        private async Task<HttpResponseMessage> CallButtonEndPoint(string buttonName, Dictionary<string, string> arguments)
        {
            _ButtonInfo button = Buttons.Single(b => b.Name == buttonName);

            var httpClient = _httpClientFactory.CreateClient("DefaultClient");

            if (arguments?.Count() != 0)
            {
                button.Endpoint = button.Endpoint + "?" + string.Join("&", arguments.Select(kv => $"{kv.Key}={kv.Value}"));
            }

            var response = await httpClient.GetAsync(button.Endpoint);
            return response;
        }

        public async Task OnGetAsync()
        {
            PopulateDropBox();

        }

        private void PopulateDropBox()
        {
            foreach (var player in _context.Players)
                Players.Add(new SelectListItem { Text = player.FirstName, Value = player.PlayerId.ToString() });
            Players.Insert(0, new SelectListItem { Value = "0", Text = "Select Player" });
            }
    }
    public class TableRow
    {
        public List<string> Values { get; set; } = new List<string>();
    }

}
