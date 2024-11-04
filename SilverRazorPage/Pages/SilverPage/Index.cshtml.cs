using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SilverPE_BOs.Models;

namespace SilverRazorPage.Pages.SilverPage
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<SilverJewelry> SilverJewelry { get; set; } = new List<SilverJewelry>();

        public async Task<IActionResult> OnGetAsync(string search)
        {
            HttpRequestMessage request;
            string apiUrl = "http://localhost:5270/api/SilverJewelry";
            if (!string.IsNullOrEmpty(search))
            {
                bool isNumeric = double.TryParse(search, out double metalWeight);
                if (isNumeric)
                {
                    request = new HttpRequestMessage(HttpMethod.Get,
                        $"{apiUrl}?$filter=contains(tolower(silverJewelryName), '{search.ToLower()}') or metalWeight eq {metalWeight}");
                }
                else
                {
                    request = new HttpRequestMessage(HttpMethod.Get,
                        $"{apiUrl}?$filter=contains(tolower(silverJewelryName), '{search.ToLower()}')");
                }
            }
            else
            {
                request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            }

            string token = HttpContext.Session.GetString("token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return RedirectToPage("/Error");
            }

            var data = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            SilverJewelry = JsonSerializer.Deserialize<IList<SilverJewelry>>(data, options) ?? new List<SilverJewelry>();

            return Page();
        }
    }
}