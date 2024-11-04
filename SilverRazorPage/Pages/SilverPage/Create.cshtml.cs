using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SilverPE_BOs.Models;

namespace SilverRazorPage.Pages.SilverPage
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult OnGet()
        {
            HttpResponseMessage response = _httpClient.GetAsync("http://localhost:5270/api/Category").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var categories = JsonSerializer.Deserialize<IList<Category>>(data, options);

                // Gán danh sách categories cho ViewData để sử dụng trong dropdown
                ViewData["Categories"] = new SelectList(categories, "CategoryId", "CategoryName");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Không thể lấy danh sách Category.");
            }

            return Page();
        }



        [BindProperty]
        public SilverJewelry SilverJewelry { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string token = HttpContext.Session.GetString("token");
            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5270/api/SilverJewelry");
            
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var json = JsonSerializer.Serialize(SilverJewelry);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.SendAsync(request).Result;
            if (response.IsSuccessStatusCode) {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Không thể tạo mới SilverJewelry.");
            }

            return RedirectToPage("./Index");
        }
    }
}
