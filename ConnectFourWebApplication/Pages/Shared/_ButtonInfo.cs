using Microsoft.AspNetCore.Mvc;

namespace ConnectFourWebApplication.Pages.Shared
{
    public record _ButtonInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Endpoint { get; set; } = string.Empty;
        public Dictionary<string, string> Arguments { get; set; } = new Dictionary<string, string>();


    }
}
