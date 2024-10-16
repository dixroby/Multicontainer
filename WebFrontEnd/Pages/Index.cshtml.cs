using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public async Task OnPost() 
        {
            using HttpClient client = new HttpClient();
            var Request = new HttpRequestMessage();

            Request.RequestUri = new Uri("http://webapi:8080/counter");

            var Response = await client.SendAsync(Request);

            string Counter = await Response.Content.ReadAsStringAsync();

            ViewData["Message"] = $"Counter value from cache: {Counter}";
        }
    }
}
