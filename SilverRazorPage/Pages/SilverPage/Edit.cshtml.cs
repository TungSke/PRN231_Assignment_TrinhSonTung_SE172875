using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SilverPE_BOs.Models;

namespace SilverRazorPage.Pages.SilverPage
{
    public class EditModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public EditModel()
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
            string token = HttpContext.Session.GetString("token");
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5270/api/SilverJewelry?&$filter=SilverJewelryId eq '{id}'");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            var data = await response.Content.ReadAsStringAsync();

            var opt = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var silverJewelryList = JsonSerializer.Deserialize<List<SilverJewelry>>(data, opt);
            var silverJewelryItem = silverJewelryList?.FirstOrDefault();
            if (silverJewelryItem == null)
            {
                return NotFound();
            }

            SilverJewelry = silverJewelryItem;

            HttpResponseMessage response2 = await _httpClient.GetAsync("http://localhost:5270/api/category");

            if (response2.IsSuccessStatusCode)
            {
                var data2 = await response2.Content.ReadAsStringAsync();

                var categories = JsonSerializer.Deserialize<List<Category>>(data2, opt);

                ViewData["Categories"] = new SelectList(categories, "CategoryId", "CategoryName");
                return Page();
            }
            return RedirectToPage("./Index");
        }

        // Bảo vệ khỏi các cuộc tấn công overposting, chỉ enable những thuộc tính cụ thể mà bạn muốn bind.
        // Thông tin thêm, xem tại: https://aka.ms/razorpagescrud
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            string token = HttpContext.Session.GetString("token");
            var json = JsonSerializer.Serialize(SilverJewelry);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, "http://localhost:5270/api/silverjewelry");
            request.Content = content;
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Không thể cập nhật SilverJewelry.");
            }

            return Page();
        }
    }
}
