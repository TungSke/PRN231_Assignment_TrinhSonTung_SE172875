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
    public class DeleteModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DeleteModel()
        {
            _httpClient = new HttpClient();
        }

        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var token = HttpContext.Session.GetString("token");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get,$"http://localhost:5270/api/SilverJewelry?&$filter=SilverJewelryId eq '{id}'");

            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            var data = await response.Content.ReadAsStringAsync();

            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var silverJewelryList = JsonSerializer.Deserialize<List<SilverJewelry>>(data, opt);
            var silverjewelry = silverJewelryList.FirstOrDefault();

            if (silverjewelry == null)
            {
                return NotFound();
            }
            else
            {
                SilverJewelry = silverjewelry;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var token = HttpContext.Session.GetString("token");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"http://localhost:5270/api/SilverJewelry?id={id}");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}
