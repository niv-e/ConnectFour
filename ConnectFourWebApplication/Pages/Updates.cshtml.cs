using BusinessLogic.Model.Boundaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConnectFourWebApplication.Pages
{
    public class UpdatesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdatesModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public int PlayerIdToUpdate { get; set; }

        [BindProperty]
        public string FirstNameToUpdate { get; set; }

        [BindProperty]
        public string PhoneNumberToUpdate { get; set; }

        [BindProperty]
        public string CountryToUpdate { get; set; }

        [BindProperty]
        public int PlayerIdToDelete { get; set; }

        [BindProperty]
        public string GameIdToDelete { get; set; }

        public void OnGet()
        {
            // Code for handling GET request
        }

        public async Task<IActionResult> OnPostUpdatePlayer()
        {
            var httpClient = _httpClientFactory.CreateClient("DefaultClient");

            var endpoint = $"api/profile/updateplayer";

            PlayerBoundary player = new PlayerBoundary
            {
                Country = CountryToUpdate,
                FirstName = FirstNameToUpdate,
                PhoneNumber = PhoneNumberToUpdate,
                PlayerId = PlayerIdToUpdate
            };

            var content = JsonContent.Create(player);

            var response = await httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }

            // If ModelState is not valid, return to the Updates page with errors
            return Page();
        }

        public async Task<IActionResult> OnPostDeletePlayer()
        {
                var httpClient = _httpClientFactory.CreateClient("DefaultClient");

                var endpoint = $"api/profile/deleteplayer?playerid={PlayerIdToDelete}";

                var response = await httpClient.DeleteAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteGame()
        {
            var httpClient = _httpClientFactory.CreateClient("DefaultClient");

            var endpoint = $"api/game/deletesession?guid={GameIdToDelete}";

            var response = await httpClient.DeleteAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Index");
            }

            return Page();

        }
    }

}
