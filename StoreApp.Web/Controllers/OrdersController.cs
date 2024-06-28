using Microsoft.AspNetCore.Mvc;
using StoreApp.Data.Concrete;
using StoreApp.Web.Common;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreApp.Web.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System;
using Iyzipay.Request;
using Iyzipay.Model;

namespace StoreApp.Web.Controllers
{
    public class OrdersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            ViewBag.Baskets = await DataControl.GetBaskets();
            string basketNames = string.Empty;

            foreach(Data.Concrete.BasketItem basket in await DataControl.GetBaskets())
            {
                basketNames += basket.Name + ",";
            }

            basketNames = basketNames.Substring(0, basketNames.Length - 1);
            ViewBag.BasketNames = basketNames;
            ViewBag.Users = new SelectList(await DataControl.GetUsers(), "Id", "FullName");
            ViewBag.Cities = new SelectList(await DataControl.GetCities(), "Id", "Name");

            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
                ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
                return View();
            }

            return RedirectToAction("Login", "Home", new { lang = HttpContext.Request.Query["lang"].ToString() });
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderItemViewModel model, string totalPrice)
        {
            if (ModelState.IsValid)
            {
                Payment payment = await ProcessPayment(model, totalPrice);

                if(payment.AuthCode != null)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var response = await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteAllBasketProducts"))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                using (var httpClientOrder = new HttpClient())
                                {
                                    var serializedModel = JsonSerializer.Serialize(model);
                                    StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
                                    var responseOrder = await httpClientOrder.PostAsync("http://localhost:5292/api/StoreApp/CreateOrder", content);

                                    if(responseOrder.IsSuccessStatusCode)
                                    {
                                        string jsonData = await responseOrder.Content.ReadAsStringAsync();
                                        int orderId = JsonSerializer.Deserialize<Data.Concrete.OrderItem>(jsonData)!.Id;
                                        return RedirectToAction("Completed", new { OrderId = orderId, lang = HttpContext.Request.Query["lang"].ToString() });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
            }

            ViewBag.Baskets = await DataControl.GetBaskets();
            ViewBag.Users = new SelectList(await DataControl.GetUsers(), "Id", "FullName");
            ViewBag.Cities = new SelectList(await DataControl.GetCities(), "Id", "Name");
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(model);
        }

        public IActionResult Completed(int OrderId, string lang)
        {
            ViewBag.OrderId = OrderId;
            ViewBag.Lang = lang;

            if (HttpContext.Request.Cookies["token"] != null)
            {
                ViewBag.IsAuthenticated = true;
                ViewBag.IsAdmin = HttpContext.Request.Cookies["isAdmin"]!.ToString();
                ViewBag.UserId = HttpContext.Request.Cookies["userId"];
            }

            return View();
        }

        public async Task<IActionResult> List()
        {
            ViewBag.Lang = HttpContext.Request.Query["lang"].ToString();
            return View(await DataControl.GetOrders());
        }

        private async Task<Payment> ProcessPayment(OrderItemViewModel model, string totalPrice)
        {
            Iyzipay.Options options = new Iyzipay.Options();
            options.ApiKey = "sandbox-S5bWjUZ39U6N99mVtIF4aOVG1NxueSJ3";
            options.SecretKey = "sandbox-XcBveJs3sAEfXhpnzTFoYRgpMoAEPQY9";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(111111111, 999999999).ToString();
            request.Price = totalPrice.Split(" ")[0];
            request.PaidPrice = totalPrice.Split(" ")[0];
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CartName;
            paymentCard.CardNumber = model.CartNumber;
            paymentCard.ExpireMonth = model.ExpirationMonth;
            paymentCard.ExpireYear = model.ExpirationYear;
            paymentCard.Cvc = model.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = model.UserId;

            AppUserViewModel? user = await DataControl.GetUser(model.UserId);
            string firtname = string.Empty;
            string lastName = string.Empty;

            foreach (string name in user!.FullName.Split(" "))
            {
                if (name != user.FullName.Split(" ").Last())
                {
                    firtname += name + " ";
                }

                else
                {
                    lastName = name;
                }
            }

            buyer.Name = firtname.Trim();
            buyer.Surname = lastName;
            buyer.GsmNumber = model.PhoneNumber;
            buyer.Email = model.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = model.OrderDate.ToString("yyyy-MM-dd HH:mm:ss");
            buyer.RegistrationAddress = model.Address;
            buyer.Ip = "85.34.78.112";

            City? city = await DataControl.GetCity(model.CityId);
            buyer.City = city!.Name;
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";

            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = user!.FullName;
            shippingAddress.City = city.Name;
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = model.Address;
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;
            request.BillingAddress = shippingAddress;

            List<Iyzipay.Model.BasketItem> basketItems = new List<Iyzipay.Model.BasketItem>();

            foreach (var basket in await DataControl.GetBaskets())
            {
                Product? prd = await DataControl.GetProduct(basket.Name!);

                if (prd != null)
                {
                    Iyzipay.Model.BasketItem basketItem = new Iyzipay.Model.BasketItem();
                    basketItem.Id = basket.Id.ToString();
                    basketItem.Name = basket.Name;
                    basketItem.Category1 = prd.SubCategory.Name;
                    basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                    basketItem.Price = (basket.Price * basket.Count).ToString();
                    basketItems.Add(basketItem);
                }
            }

            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);
            return payment;
        }
    }
}
