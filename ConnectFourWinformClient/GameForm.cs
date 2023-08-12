using BusinessLogic.Responses;
using ConnectFourWinformClient.Model;
using Microsoft.Extensions.Configuration;
using Model.Boundaries;
using Model.bounderies;
using System.Drawing.Drawing2D;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using Timer = System.Windows.Forms.Timer;

namespace ConnectFourWinformClient
{
    public partial class GameForm : Form
    {

        private Timer computerTurnTimer;
        private readonly PlayerBoundary _player;
        private HttpClient _httpClient;
        private IConfiguration _configuration;
        private Guid _gameSessionId;
        private readonly Color playerColor = Color.Yellow;
        private readonly Color computerColor = Color.Red;
        private Timer flickerTimer;
        private List<Tuple<int, int>> winerSequence;
        private Color winnerColor;
        private Color flickerColor = Color.White;
        private SessionRecordsService sessionRecordsService;
        private SessionRecordDto sessionRecord;
        private List<GameStep>.Enumerator gameSteps;
        private bool restoreEnded;

        public GameForm(PlayerBoundary playerBounder)
        {
            InitializeComponent();
            InitializeServices();
            _player = playerBounder;

            flickerTimer = new Timer();
            flickerTimer.Interval = 500; // Set the interval for color change (milliseconds)
            flickerTimer.Tick += FlickerTimer_Tick;
            flickerTimer.Start();

            computerTurnTimer = new Timer();
            computerTurnTimer.Interval = 1000;
            computerTurnTimer.Tick += ComputerTurnTimer_Tick;

        }

        public GameForm(Guid sessionId)
        {
            InitializeComponent();
            InitializeServices();
            _gameSessionId = sessionId;

            flickerTimer = new Timer();
            flickerTimer.Interval = 500; // Set the interval for color change (milliseconds)
            flickerTimer.Tick += FlickerTimer_Tick;
            flickerTimer.Start();

            computerTurnTimer = new Timer();
            computerTurnTimer.Interval = 500;
            computerTurnTimer.Tick += RestoreGame_Tick;
            computerTurnTimer.Start();

        }



        public async Task FetchGameSessionAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/start/{_player.Id}");

            response.EnsureSuccessStatusCode();

            var gameSession = await GetResponseObjectAsync<GameSessionBoundary>(response);

            _gameSessionId = gameSession.SessionId;
        }


        private void InitializeServices()
        {
            LoadConfiguration();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration["GameApiUrl"]);
            sessionRecordsService = new SessionRecordsService(new ClientDbContext());

        }
        private void LoadConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        private async void PlacePawn(object sender, EventArgs e)
        {
            var button = (Button)sender;

            if (button is null)
            {
                return;
            }

            PlacePawnResponse response = await TryToPlacePawnAsync(_gameSessionId, button.TabIndex);

            await SaveStepToDatabase(response);

            DrawPawnOnBoard(response.Position, playerColor);

            SetFormEnabled(false);

            ShowMessageIfGameEnded(response);

        }

        private Task SaveStepToDatabase(PlacePawnResponse response)
        {

            var gameStep = new GameStep();

            gameStep.PawnType = response.IsPlayerTurn ? PawnType.Player : PawnType.Server;
            gameStep.Position = response.Position;

            return sessionRecordsService.SaveStep(_gameSessionId, gameStep);
        }

        private async void ShowMessageIfGameEnded(PlacePawnResponse response)
        {
            if (response.IsGameEndedWithWin)
            {
                computerTurnTimer.Enabled = false;
                winerSequence = await GetWinnerSequenceAsync(_gameSessionId);
                winnerColor = response.IsPlayerTurn ? playerColor : computerColor;
                string status = response.IsPlayerTurn ? "WON" : "LOSE";
                AlertEndWithStatusMessage(status);
                SetFormEnabled(false);

                computerTurnTimer.Stop();

                flickerTimer.Start();


                return;
            }

            if (response.LeftMoves == 0)
            {
                AlertGameOverMessage();
            }

            computerTurnTimer.Start();
        }

        private void AlertEndWithStatusMessage(string status)
        {
            ShowMessageBoxWithButton($"You {status} !");
        }

        private void AlertGameOverMessage()
        {
            ShowMessageBoxWithButton($"GameOver");
        }
        private void ShowMessageBoxWithExitButton(string text)
        {
            if(restoreEnded) { return; }

            restoreEnded = true;

            // Create a new instance of the MessageBox
            var messageBox = new Form();

            // Set the properties of the message box
            messageBox.Text = text;
            messageBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            messageBox.StartPosition = FormStartPosition.CenterParent;
            messageBox.Name = "Connect Four";
            messageBox.Width = 40;
            messageBox.Height = 100;
            // Create a button inside the message box
            var restartButton = new Button();
            restartButton.Text = "Exit";
            restartButton.Dock = DockStyle.Fill;


            // Add a click event handler for the button
            restartButton.Click += async (s, args) =>
            {
                // start new game session when the button is clicked
                Application.Exit();
            };

            // Add the button to the message box
            messageBox.Controls.Add(restartButton);

            // Show the message box as a modal dialog
            messageBox.ShowDialog();
        }

        private void ShowMessageBoxWithButton(string text)
        {
            // Create a new instance of the MessageBox
            var messageBox = new Form();

            // Set the properties of the message box
            messageBox.Text = text;
            messageBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            messageBox.StartPosition = FormStartPosition.CenterParent;
            messageBox.Name = "Connect Four";
            messageBox.Width = 40;
            messageBox.Height = 100;
            // Create a button inside the message box
            var restartButton = new Button();
            restartButton.Text = "Start new game";
            restartButton.Dock = DockStyle.Fill;
            

            // Add a click event handler for the button
            restartButton.Click += async (s, args) =>
            {
                // start new game session when the button is clicked
                new GameForm(_player)
                .Show();
                computerTurnTimer.Stop(); 
                flickerTimer.Stop();
                this.Close();
            };

            // Add the button to the message box
            messageBox.Controls.Add(restartButton);

            // Show the message box as a modal dialog
            messageBox.ShowDialog();
        }

        private void DrawPawnOnBoard(Tuple<int, int> position, Color color)
        {
            var boardPawn = BoardGridLayoutPanel
                .GetControlFromPosition(position.Item2, position.Item1);

            boardPawn.Enabled = false;

            boardPawn.BackColor = color;
        }

        public async Task<GameSessionBoundary> GetGameSessionAsync(Guid sessionId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{sessionId}");

            response.EnsureSuccessStatusCode();

            return await GetResponseObjectAsync<GameSessionBoundary>(response);
        }

        private async Task<PlacePawnResponse> TryToPlacePawnAsync(Guid SessionId, int column)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{SessionId}/{column}");

            response.EnsureSuccessStatusCode();

            return await GetResponseObjectAsync<PlacePawnResponse>(response);

        }

        private async Task<PlacePawnResponse> GetOpponentMoveAsync(Guid sessionId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/getopponentmove/{sessionId}");

            response.EnsureSuccessStatusCode();

            return await GetResponseObjectAsync<PlacePawnResponse>(response);

        }
        private async Task<List<Tuple<int, int>>> GetWinnerSequenceAsync(Guid sessionId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/getwinnersequence/{sessionId}");

            response.EnsureSuccessStatusCode();

            return await GetResponseObjectAsync<List<Tuple<int, int>>>(response);

        }

        private async void GameForm_Load(object sender, EventArgs e)
        {
            _ = FetchGameSessionAsync();
        }

        private IEnumerable<GameStep> GetNextStepToRestore()
        {
            foreach(var step in sessionRecord.GameSteps)
            {
                yield return step;
            }

        }

        private async void RestoreGame_Tick(object? sender, EventArgs e) 
        {
            if(sessionRecord == null)
            {
                SetFormEnabled(false);

                sessionRecord = await sessionRecordsService.GetRecordBySessionId(_gameSessionId);

                gameSteps = sessionRecord.GameSteps.GetEnumerator();
            }


            if (!gameSteps.MoveNext())
            {

                ShowMessageBoxWithExitButton("Game Was Restored");
                
                computerTurnTimer.Stop();

                return;

            }

            var nextStep = gameSteps.Current;

            var color = nextStep.PawnType == PawnType.Player ? playerColor : computerColor;

            DrawPawnOnBoard(nextStep.Position, color);

            GetNextStepToRestore()
                .GetEnumerator()
                .MoveNext();

        }
        private async void ComputerTurnTimer_Tick(object? sender, EventArgs e)
        {
            PlacePawnResponse response = await GetOpponentMoveAsync(_gameSessionId);

            await SaveStepToDatabase(response);

            DrawPawnOnBoard(response.Position, computerColor);

            SetFormEnabled(false);

            ShowMessageIfGameEnded(response);

            computerTurnTimer.Stop();

            SetFormEnabled(true);
        }

        private void FlickerTimer_Tick(object? sender, EventArgs e)
        {
            if (winerSequence != null)
            {
                foreach (var item in winerSequence)
                {
                    var boardPawn = BoardGridLayoutPanel
                        .GetControlFromPosition(item.Item2, item.Item1);

                    boardPawn.BackColor = boardPawn.BackColor == winnerColor ?
                        flickerColor : winnerColor;
                }
            }
        }


        private void button_SetNormalStyle(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button is null)
            {
                return;
            }

            StyleAppander.SetDefaultButtonStyle(button);
        }

        private void button_SetHoverStyle(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button is null)
            {
                return;
            }

            StyleAppander.SetHoverButtonStyle(button);
        }



        private void SetFormEnabled(bool enabled)
        {
            foreach (Control control in InsertButtonPanel.Controls)
            {
                if (control is TriangleButton)
                {
                    control.Enabled = enabled;
                }
            }
        }


        private async Task<T> GetResponseObjectAsync<T>(HttpResponseMessage responseMessage)
        {
            string responseContent = await responseMessage.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            T responseObject = JsonSerializer.Deserialize<T>(responseContent, options) ??
                throw new InvalidCastException();

            return responseObject;
        }


    }
}
