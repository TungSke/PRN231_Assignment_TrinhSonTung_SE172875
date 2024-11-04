using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using SilverPE_BOs.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SilverRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration, HttpClient httpClient)
        {
            _logger = logger;
            _configuration=configuration;
            _httpClient=httpClient;
        }

        public void OnGet()
        {

        }

        public async Task OnPost(string email, string password)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5270/api/JWT?email={email}&password={password}");
            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("Error when calling API");
                Response.Redirect("/Error");
                return;
            }
            var data = await response.Content.ReadAsStringAsync();

            var jsonData = JsonSerializer.Deserialize<JsonElement>(data, opt);

            string token = jsonData.GetProperty("token").GetString();
            int role = jsonData.GetProperty("role").GetInt32();

            HttpContext.Session.SetString("token", token);
            HttpContext.Session.SetString("role", role.ToString());

            Response.Redirect("/SilverPage/Index");
        }

    }
}
