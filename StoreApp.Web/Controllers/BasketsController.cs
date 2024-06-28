using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Concrete;
using StoreApp.Web.Common;
using System.Text.Json;
using System.Text;

namespace StoreApp.Web.Controllers
{
    public class BasketsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
            }

            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(await DataControl.GetBaskets());
        }

        [HttpPost]
        public async Task<IActionResult> Index(string productName)
        {
            var httpClient = new HttpClient();

            if (!string.IsNullOrEmpty(productName))
            {
                await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteBasketByProductName/" + productName);
            }

            else
            {
                await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteAllBasketProducts");
            }

            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
            }

            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(await DataControl.GetBaskets());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBasket(int ProductId)
        {
            using(var httpClient = new HttpClient())
            {
                await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteBasket/" + ProductId);
            }

            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
            }

            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(await DataControl.GetBaskets());
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket(BasketItem basketItem)
        {
            using (var httpClient = new HttpClient())
            {
                var serializedModel = JsonSerializer.Serialize(basketItem);
                StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
                await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateBasket", content);
            }

            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
            }

            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(await DataControl.GetBaskets());
        }
    }
}
