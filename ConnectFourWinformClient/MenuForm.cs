using Microsoft.Extensions.Configuration;
using Model.Boundaries;
using Model.bounderies;
using System.Text.Json;

namespace ConnectFourWinformClient
{
    public partial class MenuForm : Form
    {
        private HttpClient _httpClient;
        private IConfiguration _configuration;


        public MenuForm()
        {
            InitializeComponent();
            InitializeServices();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            var playerId = PlayerIdTextBox.Text;

            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/{playerId}");

            response.EnsureSuccessStatusCode();

            string responseContent = await response.Content.ReadAsStringAsync();

            var player = JsonSerializer.Deserialize<PlayerBoundary>(responseContent) ??
                throw new InvalidCastException();

            this.Visible = false;
            GameForm gameForm = new(player);
            gameForm.Show();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }

        private void InitializeServices()
        {
            LoadConfiguration();
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration["PlayerApiUrl"]);

        }
        private void LoadConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Guid sessionId = Guid.Parse(GuidToRestoreTextBox.Text);
            this.Visible = false;
            GameForm gameForm = new(sessionId);
            gameForm.Show();

        }
    }
}