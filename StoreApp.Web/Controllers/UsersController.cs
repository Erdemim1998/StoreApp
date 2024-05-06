using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreApp.Data.Concrete;
using StoreApp.Web.Common;
using StoreApp.Web.Models;
using System.Text;
using System.Text.Json;

namespace StoreApp.Web.Controllers
{
    public class UsersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await DataControl.GetUsers();
            return View(users);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AppUserViewModel? appUser = await DataControl.GetUser(id);
            ViewBag.Roles = new SelectList(await DataControl.GetRoles(), "Id", "Name");
            return View(appUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, AppUserViewModel model, string[] Roles)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var httpClient = new HttpClient())
                {
                    var serializedModel = JsonSerializer.Serialize(model);
                    StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                    using (var response = await httpClient.PutAsync("http://localhost:5292/api/StoreApp/EditUser/", content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            if (Roles.Length > 0)
                            {
                                using (var responseDeleteSubCategories = await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteUserRoles/" + id))
                                {
                                    if (responseDeleteSubCategories.IsSuccessStatusCode)
                                    {
                                        foreach (string roleId in Roles)
                                        {
                                            AppUserRolesViewModel mdlUserRoles = new AppUserRolesViewModel();
                                            mdlUserRoles.UserId = model.Id;
                                            mdlUserRoles.RoleId = roleId;
                                            var serializedModelUserRoles = JsonSerializer.Serialize(mdlUserRoles);
                                            StringContent contentUserRoles = new StringContent(serializedModelUserRoles, Encoding.UTF8, "application/json");
                                            await httpClient.PostAsync("http://localhost:5292/api/StoreApp/CreateUserRoles", contentUserRoles);
                                        }

                                        return RedirectToAction("Index");
                                    }
                                }
                            }

                            else
                            {
                                return RedirectToAction("Index");
                            }
                        }
                    }
                }
            }

            ViewBag.Roles = new SelectList(await DataControl.GetRoles(), "Id", "Name", Roles);
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(await DataControl.GetUser(id));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, AppUserViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                var serializedModel = JsonSerializer.Serialize(model);
                StringContent content = new StringContent(serializedModel, Encoding.UTF8, "application/json");

                using (var response = await httpClient.DeleteAsync("http://localhost:5292/api/StoreApp/DeleteUser/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(model);
        }
    }
}
