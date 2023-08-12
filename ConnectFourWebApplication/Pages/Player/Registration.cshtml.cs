using BusinessLogic;
using BusinessLogic.Contracts;
using BusinessLogic.Model.Boundaries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ConnectFourWebApplication.Pages.Player
{
    public class RegistrationModel : PageModel
    {
        private readonly IPlayerService _playerService;

        [BindProperty]
        public PlayerBoundary PlayerBoundary { get; set; } = default!;

        public RegistrationModel(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid )
            {
                return Page();
            }
            await _playerService.RegisterPlayer(PlayerBoundary);

            return RedirectToPage("..//Index");
        }

    }
}
