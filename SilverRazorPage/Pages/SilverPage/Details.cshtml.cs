using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SilverPE_BOs.Models;

namespace SilverRazorPage.Pages.SilverPage
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel()
        {
            _httpClient = new HttpClient();
        }

        public SilverJewelry SilverJewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            string token = HttpContext.Session.GetString("token");
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5270/api/SilverJewelry?$expand=category&$filter=SilverJewelryId eq '{id}'");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var data = await response.Content.ReadAsStringAsync();

            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (string.IsNullOrEmpty(data))
            {
                return NotFound();
            }

            var silverJewelryList = JsonSerializer.Deserialize<List<SilverJewelry>>(data, opt);
            SilverJewelry = silverJewelryList?.FirstOrDefault();

            if (SilverJewelry == null)
            {
                return NotFound();
            }

            return Page();
        }

    }
}
