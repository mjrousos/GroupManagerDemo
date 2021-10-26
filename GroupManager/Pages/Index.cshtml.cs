using GroupManager.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace B2CDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IGroupManagerApiClient _apiClient;
        private readonly ILogger<IndexModel> _logger;

        public string CodeName { get; set; }

        public IndexModel(IGroupManagerApiClient apiClient, ILogger<IndexModel> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
            CodeName = "User";
        }

        public async Task OnGet()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                CodeName = await _apiClient.GetCodeName();
            }
        }
    }
}
