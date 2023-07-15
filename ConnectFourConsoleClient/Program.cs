// See https://aka.ms/new-console-template for more information
using Model.bounderies;
using Model.Dtos;
using System.Text;
using System.Text.Json;

Console.WriteLine("Hello, World!");

HttpClient client = new HttpClient();
client.BaseAddress = new Uri(@"http://localhost:5022"); 

bool startGame = false;
while (!startGame)
{
    Console.WriteLine("Press P to player or E to exit");

    switch (Console.ReadKey().Key)
    {
        case ConsoleKey.E:
            Environment.Exit(0);
            break;
        case ConsoleKey.P:
            startGame = true;
            break;
        default:
            Console.WriteLine("Invalid input");
            break;
    };
    Console.ReadLine();
}

string userInput = string.Empty;



PlayerDto player = await RegisterPlayer(client);

GameSessionDto session = await CreateGameSession(player.Id, client);


while (userInput != ConsoleKey.E.ToString())
{
    Console.WriteLine("Enter a column to insert pawn");
    userInput = Console.ReadLine();
    int col = int.Parse(userInput);

    var results = await PlacePawn(client, session, col);

    if(results)
    {
        //await GetGameSessionById(session)
    }
}

async Task<bool> PlacePawn(HttpClient client, GameSessionDto session, int col)
{
    var startGameUrl = "api/game/" + session.SessionId;

    var json = JsonSerializer.Serialize(1);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    HttpResponseMessage response = await client?.PostAsync(startGameUrl, content);

    string responseContent = await response.Content.ReadAsStringAsync();

    return response.IsSuccessStatusCode;
}

async Task<PlayerDto> RegisterPlayer(HttpClient client)
{
    var requestBody = new PlayerBoundary
    {
        Id = 1,
        FirstName = "Niv",
        Country = "Israel",
        PhoneNumber = "+972-1234567"
    };
    var json = JsonSerializer.Serialize(requestBody);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var registerUrl = client?.BaseAddress?.ToString() + "api/profile/register";

    HttpResponseMessage response = await client?.PostAsync(registerUrl, content);

    response.EnsureSuccessStatusCode();

    string responseContent = await response.Content.ReadAsStringAsync();

    return JsonSerializer.Deserialize<PlayerDto>(responseContent) ?? throw new InvalidCastException();
}
async Task<GameSessionDto> CreateGameSession(int id, HttpClient client)
{

    var startGameUrl = "api/game/" + "start/" + id;

    HttpResponseMessage response = await client?.GetAsync(startGameUrl);

    response.EnsureSuccessStatusCode();

    string responseContent = await response.Content.ReadAsStringAsync();

    return JsonSerializer.Deserialize<GameSessionDto>(responseContent) ?? throw new InvalidCastException();
}
