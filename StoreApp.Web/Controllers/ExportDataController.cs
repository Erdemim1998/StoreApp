using ClosedXML.Excel;
using DocumentFormat.OpenXml.Bibliography;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Iyzipay.Model.V2.Subscription;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using StoreApp.Data.Concrete;
using StoreApp.Web.Common;
using StoreApp.Web.Models;
using System.Data;
using System.Text;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace StoreApp.Web.Controllers
{
    public class ExportDataController : Controller
    {
        private readonly IWebHostEnvironment _webHost;

        public ExportDataController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        [HttpPost]
        public async Task<IActionResult> ExportProductsDataToFile(string Language)
        {
            var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var exportType = dictioneryexportType["Export"];
            DataTable products = await GetProductsDetail(Language);

            switch (exportType)
            {
                case "Excel":
                    ExportProductsToExcel(products, Language);
                    break;
                case "Pdf":
                    ExportProductsToPdf(products, Language);
                    break;
                case "Word":
                    ExportToWord(products, Language, "Products");
                    break;
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> ExportBrandsDataToFile(string Language)
        {
            var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var exportType = dictioneryexportType["Export"];
            DataTable brands = await GetBrandsDetail(Language);

            switch (exportType)
            {
                case "Excel":
                    ExportBrandsToExcel(brands, Language);
                    break;
                case "Pdf":
                    ExportBrandsToPdf(brands, Language);
                    break;
                case "Word":
                    ExportToWord(brands, Language, "Brands");
                    break;
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> ExportCategoriesDataToFile(string Language)
        {
            var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var exportType = dictioneryexportType["Export"];
            DataTable categories = await GetCategoriesDetail(Language);

            switch (exportType)
            {
                case "Excel":
                    ExportCategoriesToExcel(categories, Language);
                    break;
                case "Pdf":
                    ExportCategoriesToPdf(categories, Language);
                    break;
                case "Word":
                    ExportToWord(categories, Language, "Categories");
                    break;
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> ExportSubCategoriesDataToFile(string Language, int CategoryId)
        {
            var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var exportType = dictioneryexportType["SubExport"];
            DataTable subCategories = await GetSubCategoriesDetail(Language, CategoryId);

            switch (exportType)
            {
                case "Excel":
                    ExportSubCategoriesToExcel(subCategories, Language);
                    break;
                case "Pdf":
                    ExportSubCategoriesToPdf(subCategories, Language);
                    break;
                case "Word":
                    ExportToWord(subCategories, Language, "SubCategories");
                    break;
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> ExportCitiesDataToFile(string Language)
        {
            var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var exportType = dictioneryexportType["Export"];
            DataTable cities = await GetCitiesDetail(Language);

            switch (exportType)
            {
                case "Excel":
                    ExportCitiesToExcel(cities, Language);
                    break;
                case "Pdf":
                    ExportCitiesToPdf(cities, Language);
                    break;
                case "Word":
                    ExportToWord(cities, Language, "Cities");
                    break;
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> ExportUsersDataToFile(string Language)
        {
            var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var exportType = dictioneryexportType["Export"];
            DataTable users = await GetUsersDetail(Language);

            switch (exportType)
            {
                case "Excel":
                    ExportUsersToExcel(users, Language);
                    break;
                case "Pdf":
                    ExportUsersToPdf(users, Language);
                    break;
                case "Word":
                    ExportToWord(users, Language, "Users");
                    break;
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> ExportRolesDataToFile(string Language)
        {
            var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var exportType = dictioneryexportType["Export"];
            DataTable roles = await GetRolesDetail(Language);

            switch (exportType)
            {
                case "Excel":
                    ExportRolesToExcel(roles, Language);
                    break;
                case "Pdf":
                    ExportRolesToPdf(roles, Language);
                    break;
                case "Word":
                    ExportToWord(roles, Language, "Roles");
                    break;
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> ExportOrdersDataToFile(string Language)
        {
            var dictioneryexportType = Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());
            var exportType = dictioneryexportType["Export"];
            DataTable orders = await GetOrdersDetail(Language);

            switch (exportType)
            {
                case "Excel":
                    ExportOrdersToExcel(orders, Language);
                    break;
                case "Pdf":
                    ExportOrdersToPdf(orders, Language);
                    break;
                case "Word":
                    ExportToWord(orders, Language, "Orders");
                    break;
            }

            return null;
        }

        private async Task<DataTable> GetProductsDetail(string lang)
        {
            var products = await DataControl.GetProducts();

            DataTable dtProduct = new DataTable("Products");
            dtProduct.Columns.AddRange(new DataColumn[7] { new DataColumn("Id"),
                                            new DataColumn((lang == "en") ? "Product Name" : "Ürün Adı"),
                                            new DataColumn((lang == "en") ? "Price" : "Ücreti"),
                                            new DataColumn((lang == "en") ? "Description" : "Açıklama"),
                                            new DataColumn("Url"),
                                            new DataColumn((lang == "en") ? "Category" : "Kategori"),
                                            new DataColumn((lang == "en") ? "Brand" : "Marka") });

            foreach (var product in products)
            {
                if(lang == "en")
                {
                    dtProduct.Rows.Add(product.Id, (product.Ml2Name == null) ? product.Name : product.Ml2Name, product.Price, product.Description,
                                   product.Url, (product.SubCategory.Ml2Name == null) ? product.SubCategory.Name : product.SubCategory.Ml2Name, (product.Brand.Ml2Name == null) ? product.Brand.Name : product.Brand.Ml2Name);
                }

                else
                {
                    dtProduct.Rows.Add(product.Id, (product.Ml1Name == null) ? product.Name : product.Ml1Name, product.Price, product.Description,
                                   product.Url, (product.SubCategory.Ml1Name == null) ? product.SubCategory.Name : product.SubCategory.Ml1Name, (product.Brand.Ml1Name == null) ? product.Brand.Name : product.Brand.Ml1Name);
                }
            }

            return dtProduct;
        }

        private async Task<DataTable> GetBrandsDetail(string lang)
        {
            var brands = await DataControl.GetBrands();

            DataTable dtBrand = new DataTable("Brands");
            dtBrand.Columns.AddRange(new DataColumn[3] { new DataColumn("Id"),
                                            new DataColumn((lang == "en") ? "Brand Name" : "Marka Adı"),
                                            new DataColumn((lang == "en") ? "Categories" : "Kategoriler") });

            foreach (var brand in brands)
            {
                List<BrandSubCategory>? brandSubCategories = await DataControl.GetBrandSubCategories(brand.Id);

                if (lang == "en")
                {
                    foreach (var subCategory in brandSubCategories)
                    {
                        dtBrand.Rows.Add(brand.Id, (brand.Ml2Name == null) ? brand.Name : brand.Ml2Name, (subCategory.SubCategory.Ml2Name == null) ? subCategory.SubCategory.Name : subCategory.SubCategory.Ml2Name);
                    }
                    
                }

                else
                {
                    foreach(var subCategory in brandSubCategories)
                    {
                        dtBrand.Rows.Add(brand.Id, (brand.Ml1Name == null) ? brand.Name : brand.Ml1Name, (subCategory.SubCategory.Ml1Name == null) ? subCategory.SubCategory.Name : subCategory.SubCategory.Ml1Name);
                    }
                }
            }

            return dtBrand;
        }

        private async Task<DataTable> GetCategoriesDetail(string lang)
        {
            var categories = await DataControl.GetCategories();

            DataTable dtCategory = new DataTable("Categories");
            dtCategory.Columns.AddRange(new DataColumn[2] { new DataColumn("Id"),
                                            new DataColumn((lang == "en") ? "Category Name" : "Kategori Adı") });

            foreach (var category in categories)
            {
                if (lang == "en")
                {
                    dtCategory.Rows.Add(category.Id, (category.Ml2Name == null) ? category.Name : category.Ml2Name);
                }

                else
                {
                    dtCategory.Rows.Add(category.Id, (category.Ml1Name == null) ? category.Name : category.Ml1Name);
                }
            }

            return dtCategory;
        }

        private async Task<DataTable> GetSubCategoriesDetail(string lang, int CategoryId)
        {
            var subCategories = await DataControl.GetSubCategories(CategoryId);

            DataTable dtCategory = new DataTable("SubCategories");
            dtCategory.Columns.AddRange(new DataColumn[3] { new DataColumn("Id"),
                                            new DataColumn((lang == "en") ? "Sub Category Name" : "Alt Kategori Adı"),
                                            new DataColumn((lang == "en") ? "Category" : "Kategori") });

            foreach (var subCategory in subCategories)
            {
                if (lang == "en")
                {
                    dtCategory.Rows.Add(subCategory.Id, (subCategory.Ml2Name == null) ? subCategory.Name : subCategory.Ml2Name, (subCategory.Category.Ml2Name == null) ? subCategory.Category.Name : subCategory.Category.Ml2Name);
                }

                else
                {
                    dtCategory.Rows.Add(subCategory.Id, (subCategory.Ml1Name == null) ? subCategory.Name : subCategory.Ml1Name, (subCategory.Category.Ml1Name == null) ? subCategory.Category.Name : subCategory.Category.Ml1Name);
                }
            }

            return dtCategory;
        }

        private async Task<DataTable> GetCitiesDetail(string lang)
        {
            var cities = await DataControl.GetCities();

            DataTable dtCity = new DataTable("Cities");
            dtCity.Columns.AddRange(new DataColumn[2] { new DataColumn("Id"),
                                            new DataColumn((lang == "en") ? "City Name" : "Şehir Adı") });

            foreach (var city in cities)
            {
                dtCity.Rows.Add(city.Id, city.Name);
            }

            return dtCity;
        }

        private async Task<DataTable> GetUsersDetail(string lang)
        {
            var users = await DataControl.GetUsers();

            DataTable dtUser = new DataTable("Users");
            dtUser.Columns.AddRange(new DataColumn[6] { new DataColumn("Id"),
                                            new DataColumn((lang == "en") ? "Image" : "Resim"),
                                            new DataColumn((lang == "en") ? "User Name" : "Kullanıcı Adı"),
                                            new DataColumn((lang == "en") ? "Full Name" : "Adı Soyadı"),
                                            new DataColumn((lang == "en") ? "Record Date" : "Kayıt Tarihi"),
                                            new DataColumn((lang == "en") ? "Roles" : "Roller") });

            foreach (var user in users)
            {
                List<AppUserRolesViewModel>? userRoles = await DataControl.GetUserRoles(user.Id);

                if(userRoles.Count > 0)
                {
                    foreach (var userRole in userRoles)
                    {
                        AppRole? role = await DataControl.GetRole(userRole.RoleId);
                        dtUser.Rows.Add(user.Id, user.Image, user.UserName, user.FullName, user.DateAdded.ToString("dd.MM.yyyy HH:mm"), role!.Name);
                    }
                }

                else
                {
                    dtUser.Rows.Add(user.Id, user.Image, user.UserName, user.FullName, user.DateAdded.ToString("dd.MM.yyyy HH:mm"), null);
                }
            }

            return dtUser;
        }

        private async Task<DataTable> GetRolesDetail(string lang)
        {
            var roles = await DataControl.GetRoles();

            DataTable dtRole = new DataTable("Roles");
            dtRole.Columns.AddRange(new DataColumn[2] { new DataColumn("Id"),
                                            new DataColumn((lang == "en") ? "Role Name" : "Rol Adı") });

            foreach (var role in roles)
            {
                dtRole.Rows.Add(role.Id, role.Name);
            }

            return dtRole;
        }

        private async Task<DataTable> GetOrdersDetail(string lang)
        {
            var orders = await DataControl.GetOrders();

            DataTable dtOrder = new DataTable("Orders");
            dtOrder.Columns.AddRange(new DataColumn[5] { new DataColumn("Id"),
                                            new DataColumn((lang == "en") ? "Order Date" : "Sipariş Tarihi"),
                                            new DataColumn((lang == "en") ? "User" : "Kullanıcı"),
                                            new DataColumn((lang == "en") ? "City" : "Şehir"),
                                            new DataColumn((lang == "en") ? "Address" : "Adres") });

            foreach (var order in orders)
            {
                dtOrder.Rows.Add(order.Id, order.OrderDate, order.CartName, order.City.Name, order.Address);
            }

            return dtOrder;
        }

        private void ExportProductsToExcel(DataTable products, string lang)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Products");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = (lang == "en") ? "Product Name" : "Ürün Adı";
                worksheet.Cell(currentRow, 3).Value = (lang == "en") ? "Price" : "Ücreti";
                worksheet.Cell(currentRow, 4).Value = (lang == "en") ? "Description" : "Açıklama";
                worksheet.Cell(currentRow, 5).Value = "Url";
                worksheet.Cell(currentRow, 6).Value = (lang == "en") ? "Category" : "Kategori";
                worksheet.Cell(currentRow, 7).Value = (lang == "en") ? "Brand" : "Marka";

                for (int i = 0; i < products.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = products.Rows[i]["Id"].ToString();
                    worksheet.Cell(currentRow, 2).Value = products.Rows[i][(lang == "en") ? "Product Name" : "Ürün Adı"].ToString();
                    worksheet.Cell(currentRow, 3).Value = products.Rows[i][(lang == "en") ? "Price" : "Ücreti"].ToString();
                    worksheet.Cell(currentRow, 4).Value = products.Rows[i][(lang == "en") ? "Description" : "Açıklama"].ToString();
                    worksheet.Cell(currentRow, 5).Value = products.Rows[i]["Url"].ToString();
                    worksheet.Cell(currentRow, 6).Value = products.Rows[i][(lang == "en") ? "Category" : "Kategori"].ToString();
                    worksheet.Cell(currentRow, 7).Value = products.Rows[i][(lang == "en") ? "Brand" : "Marka"].ToString();
                }

                DownloadExcel("Products", lang, workbook);
            }
        }

        private void ExportBrandsToExcel(DataTable brands, string lang)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Brands");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = (lang == "en") ? "Brand Name" : "Marka Adı";
                worksheet.Cell(currentRow, 3).Value = (lang == "en") ? "Categories" : "Kategoriler";

                for (int i = 0; i < brands.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = brands.Rows[i]["Id"].ToString();
                    worksheet.Cell(currentRow, 2).Value = brands.Rows[i][(lang == "en") ? "Brand Name" : "Marka Adı"].ToString();
                    worksheet.Cell(currentRow, 3).Value = brands.Rows[i][(lang == "en") ? "Categories" : "Kategoriler"].ToString();
                }

                DownloadExcel("Brands", lang, workbook);
            }
        }

        private void ExportCategoriesToExcel(DataTable categories, string lang)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Categories");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = (lang == "en") ? "Category Name" : "Kategori Adı";

                for (int i = 0; i < categories.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = categories.Rows[i]["Id"].ToString();
                    worksheet.Cell(currentRow, 2).Value = categories.Rows[i][(lang == "en") ? "Category Name" : "Kategori Adı"].ToString();
                }

                DownloadExcel("Categories", lang, workbook);
            }
        }

        private void ExportSubCategoriesToExcel(DataTable subCategories, string lang)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SubCategories");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = (lang == "en") ? "Sub Category Name" : "Alt Kategori Adı";
                worksheet.Cell(currentRow, 3).Value = (lang == "en") ? "Category" : "Kategori";

                for (int i = 0; i < subCategories.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = subCategories.Rows[i]["Id"].ToString();
                    worksheet.Cell(currentRow, 2).Value = subCategories.Rows[i][(lang == "en") ? "Sub Category Name" : "Alt Kategori Adı"].ToString();
                    worksheet.Cell(currentRow, 3).Value = subCategories.Rows[i][(lang == "en") ? "Category" : "Kategori"].ToString();
                }

                DownloadExcel("SubCategories", lang, workbook);
            }
        }

        private void ExportCitiesToExcel(DataTable cities, string lang)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Cities");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = (lang == "en") ? "City Name" : "Şehir Adı";

                for (int i = 0; i < cities.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = cities.Rows[i]["Id"].ToString();
                    worksheet.Cell(currentRow, 2).Value = cities.Rows[i][(lang == "en") ? "City Name" : "Şehir Adı"].ToString();
                }

                DownloadExcel("Cities", lang, workbook);
            }
        }

        private void ExportUsersToExcel(DataTable users, string lang)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = (lang == "en") ? "Image" : "Resim";
                worksheet.Cell(currentRow, 3).Value = (lang == "en") ? "User Name" : "Kullanıcı Adı";
                worksheet.Cell(currentRow, 4).Value = (lang == "en") ? "Full Name" : "Adı Soyadı";
                worksheet.Cell(currentRow, 5).Value = (lang == "en") ? "Record Date" : "Kayıt Tarihi";
                worksheet.Cell(currentRow, 6).Value = (lang == "en") ? "Roles" : "Roller";

                for (int i = 0; i < users.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = users.Rows[i]["Id"].ToString();
                    worksheet.Cell(currentRow, 2).Value = users.Rows[i][(lang == "en") ? "Image" : "Resim"].ToString();
                    worksheet.Cell(currentRow, 3).Value = users.Rows[i][(lang == "en") ? "User Name" : "Kullanıcı Adı"].ToString();
                    worksheet.Cell(currentRow, 4).Value = users.Rows[i][(lang == "en") ? "Full Name" : "Adı Soyadı"].ToString();
                    worksheet.Cell(currentRow, 5).Value = users.Rows[i][(lang == "en") ? "Record Date" : "Kayıt Tarihi"].ToString();
                    worksheet.Cell(currentRow, 6).Value = users.Rows[i][(lang == "en") ? "Roles" : "Roller"].ToString();
                }

                DownloadExcel("Users", lang, workbook);
            }
        }

        private void ExportRolesToExcel(DataTable roles, string lang)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Users");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = (lang == "en") ? "Role Name" : "Rol Adı";

                for (int i = 0; i < roles.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = roles.Rows[i]["Id"].ToString();
                    worksheet.Cell(currentRow, 2).Value = roles.Rows[i][(lang == "en") ? "Role Name" : "Rol Adı"].ToString();
                }

                DownloadExcel("Roles", lang, workbook);
            }
        }

        private void ExportOrdersToExcel(DataTable orders, string lang)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Orders");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = (lang == "en") ? "Order Date" : "Sipariş Tarihi";
                worksheet.Cell(currentRow, 3).Value = (lang == "en") ? "User" : "Kullanıcı";
                worksheet.Cell(currentRow, 4).Value = (lang == "en") ? "City" : "Şehir";
                worksheet.Cell(currentRow, 5).Value = (lang == "en") ? "Address" : "Adres";

                for (int i = 0; i < orders.Rows.Count; i++)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = orders.Rows[i]["Id"].ToString();
                    worksheet.Cell(currentRow, 2).Value = orders.Rows[i][(lang == "en") ? "Order Date" : "Sipariş Tarihi"].ToString();
                    worksheet.Cell(currentRow, 3).Value = orders.Rows[i][(lang == "en") ? "User" : "Kullanıcı"].ToString();
                    worksheet.Cell(currentRow, 4).Value = orders.Rows[i][(lang == "en") ? "City" : "Şehir"].ToString();
                    worksheet.Cell(currentRow, 5).Value = orders.Rows[i][(lang == "en") ? "Address" : "Adres"].ToString();
                }

                DownloadExcel("Orders", lang, workbook);
            }
        }

        private async void DownloadExcel(string fileName, string lang, XLWorkbook workbook)
        {
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();
            Response.Clear();

            if (fileName == "Products")
            {
                Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Products.xlsx" : "Urunler.xlsx"));
            }

            else if(fileName == "Brands")
            {
                Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Brands.xlsx" : "Markalar.xlsx"));
            }

            else if (fileName == "Categories")
            {
                Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Categories.xlsx" : "Kategoriler.xlsx"));
            }

            else if (fileName == "SubCategories")
            {
                Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "SubCategories.xlsx" : "AltKategoriler.xlsx"));
            }

            else if (fileName == "Cities")
            {
                Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Cities.xlsx" : "Sehirler.xlsx"));
            }

            else if (fileName == "Users")
            {
                Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Users.xlsx" : "Kullanicilar.xlsx"));
            }

            else if (fileName == "Roles")
            {
                Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Roles.xlsx" : "Roller.xlsx"));
            }

            else
            {
                Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Orders.xlsx" : "Siparisler.xlsx"));
            }

            Response.Headers.Append("Content-Length", content.Length.ToString());
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            await Response.Body.WriteAsync(content, 0, content.Length);
            await Response.Body.FlushAsync();
        }

        private void ExportProductsToPdf(DataTable products, string lang)
        {
            if (products.Rows.Count > 0)
            {
                string filename = (lang == "en") ? "Products-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt") : "Urunler-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt");
                string filepath = Path.Combine(_webHost.WebRootPath, "documents") + "\\" + filename + ".pdf";
                Document document = new(PageSize.A4, 5f, 5f, 10f, 10f);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
                Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

                float[] columnDefinitionSize = { 2F, 5F, 3F, 5F, 5F, 5F, 5F };
                PdfPTable table;

                table = new PdfPTable(columnDefinitionSize)
                {
                    WidthPercentage = 100
                };

                table.AddCell(new Phrase("Id", font1));
                table.AddCell(new Phrase((lang == "en") ? "Product Name" : "Ürün Adi", font1));
                table.AddCell(new Phrase((lang == "en") ? "Price" : "Ücreti", font1));
                table.AddCell(new Phrase((lang == "en") ? "Description" : "Açiklama", font1));
                table.AddCell(new Phrase("Url", font1));
                table.AddCell(new Phrase((lang == "en") ? "Category" : "Kategori", font1));
                table.AddCell(new Phrase((lang == "en") ? "Brand" : "Marka", font1));
                table.HeaderRows = 1;

                foreach (DataRow data in products.Rows)
                {
                    table.AddCell(new Phrase(data["Id"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Product Name" : "Ürün Adı"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Price" : "Ücreti"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Description" : "Açıklama"].ToString(), font2));
                    table.AddCell(new Phrase(data["Url"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Category" : "Kategori"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Brand" : "Marka"].ToString(), font2));
                }

                DownloadPdf(document, table, writer, fs, filepath, filename);
            }
        }

        private void ExportBrandsToPdf(DataTable brands, string lang)
        {
            if (brands.Rows.Count > 0)
            {
                string filename = (lang == "en") ? "Brands-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt") : "Markalar-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt");
                string filepath = Path.Combine(_webHost.WebRootPath, "documents") + "\\" + filename + ".pdf";
                Document document = new(PageSize.A4, 5f, 5f, 10f, 10f);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
                Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

                float[] columnDefinitionSize = { 2F, 5F, 5F };
                PdfPTable table;

                table = new PdfPTable(columnDefinitionSize)
                {
                    WidthPercentage = 100
                };

                table.AddCell(new Phrase("Id", font1));
                table.AddCell(new Phrase((lang == "en") ? "Brand Name" : "Marka Adi", font1));
                table.AddCell(new Phrase((lang == "en") ? "Categories" : "Kategoriler", font1));
                table.HeaderRows = 1;

                foreach (DataRow data in brands.Rows)
                {
                    table.AddCell(new Phrase(data["Id"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Brand Name" : "Marka Adı"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Categories" : "Kategoriler"].ToString(), font2));
                }

                DownloadPdf(document, table, writer, fs, filepath, filename);
            }
        }

        private void ExportCategoriesToPdf(DataTable categories, string lang)
        {
            if (categories.Rows.Count > 0)
            {
                string filename = (lang == "en") ? "Categories-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt") : "Kategoriler-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt");
                string filepath = Path.Combine(_webHost.WebRootPath, "documents") + "\\" + filename + ".pdf";
                Document document = new(PageSize.A4, 5f, 5f, 10f, 10f);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
                Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

                float[] columnDefinitionSize = { 2F, 5F };
                PdfPTable table;

                table = new PdfPTable(columnDefinitionSize)
                {
                    WidthPercentage = 100
                };

                table.AddCell(new Phrase("Id", font1));
                table.AddCell(new Phrase((lang == "en") ? "Category Name" : "Kategori Adi", font1));
                table.HeaderRows = 1;

                foreach (DataRow data in categories.Rows)
                {
                    table.AddCell(new Phrase(data["Id"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Category Name" : "Kategori Adı"].ToString(), font2));
                }

                DownloadPdf(document, table, writer, fs, filepath, filename);
            }
        }

        private void ExportSubCategoriesToPdf(DataTable subCategories, string lang)
        {
            if (subCategories.Rows.Count > 0)
            {
                string filename = (lang == "en") ? "SubCategories-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt") : "AltKategoriler-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt");
                string filepath = Path.Combine(_webHost.WebRootPath, "documents") + "\\" + filename + ".pdf";
                Document document = new(PageSize.A4, 5f, 5f, 10f, 10f);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
                Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

                float[] columnDefinitionSize = { 2F, 5F, 5F };
                PdfPTable table;

                table = new PdfPTable(columnDefinitionSize)
                {
                    WidthPercentage = 100
                };

                table.AddCell(new Phrase("Id", font1));
                table.AddCell(new Phrase((lang == "en") ? "Sub Category Name" : "Alt Kategori Adi", font1));
                table.AddCell(new Phrase((lang == "en") ? "Category" : "Kategori", font1));
                table.HeaderRows = 1;

                foreach (DataRow data in subCategories.Rows)
                {
                    table.AddCell(new Phrase(data["Id"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Sub Category Name" : "Alt Kategori Adı"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Category" : "Kategori"].ToString(), font2));
                }

                DownloadPdf(document, table, writer, fs, filepath, filename);
            }
        }

        private void ExportCitiesToPdf(DataTable cities, string lang)
        {
            if (cities.Rows.Count > 0)
            {
                string filename = (lang == "en") ? "Cities-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt") : "Sehirler-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt");
                string filepath = Path.Combine(_webHost.WebRootPath, "documents") + "\\" + filename + ".pdf";
                Document document = new(PageSize.A4, 5f, 5f, 10f, 10f);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
                Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

                float[] columnDefinitionSize = { 2F, 5F };
                PdfPTable table;

                table = new PdfPTable(columnDefinitionSize)
                {
                    WidthPercentage = 100
                };

                table.AddCell(new Phrase("Id", font1));
                table.AddCell(new Phrase((lang == "en") ? "City Name" : "Sehir Adi", font1));
                table.HeaderRows = 1;

                foreach (DataRow data in cities.Rows)
                {
                    table.AddCell(new Phrase(data["Id"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "City Name" : "Şehir Adı"].ToString(), font2));
                }

                DownloadPdf(document, table, writer, fs, filepath, filename);
            }
        }

        private void ExportUsersToPdf(DataTable users, string lang)
        {
            if (users.Rows.Count > 0)
            {
                string filename = (lang == "en") ? "Users-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt") : "Kullanicilar-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt");
                string filepath = Path.Combine(_webHost.WebRootPath, "documents") + "\\" + filename + ".pdf";
                Document document = new(PageSize.A4, 5f, 5f, 10f, 10f);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
                Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

                float[] columnDefinitionSize = { 2F, 5F, 5F, 5F, 5F, 5F };
                PdfPTable table;

                table = new PdfPTable(columnDefinitionSize)
                {
                    WidthPercentage = 100
                };

                table.AddCell(new Phrase("Id", font1));
                table.AddCell(new Phrase((lang == "en") ? "Image" : "Resim", font1));
                table.AddCell(new Phrase((lang == "en") ? "User Name" : "Kullanici Adi", font1));
                table.AddCell(new Phrase((lang == "en") ? "Full Name" : "Adi Soyadi", font1));
                table.AddCell(new Phrase((lang == "en") ? "Record Date" : "Kayit Tarihi", font1));
                table.AddCell(new Phrase((lang == "en") ? "Roles" : "Roller", font1));
                table.HeaderRows = 1;

                foreach (DataRow data in users.Rows)
                {
                    table.AddCell(new Phrase(data["Id"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Image" : "Resim"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "User Name" : "Kullanıcı Adı"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Full Name" : "Adı Soyadı"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Record Date" : "Kayıt Tarihi"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Roles" : "Roller"].ToString(), font2));
                }

                DownloadPdf(document, table, writer, fs, filepath, filename);
            }
        }

        private void ExportRolesToPdf(DataTable roles, string lang)
        {
            if (roles.Rows.Count > 0)
            {
                string filename = (lang == "en") ? "Roles-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt") : "Roller-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt");
                string filepath = Path.Combine(_webHost.WebRootPath, "documents") + "\\" + filename + ".pdf";
                Document document = new(PageSize.A4, 5f, 5f, 10f, 10f);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
                Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

                float[] columnDefinitionSize = { 2F, 5F };
                PdfPTable table;

                table = new PdfPTable(columnDefinitionSize)
                {
                    WidthPercentage = 100
                };

                table.AddCell(new Phrase("Id", font1));
                table.AddCell(new Phrase((lang == "en") ? "Role Name" : "Rol Adi", font1));
                table.HeaderRows = 1;

                foreach (DataRow data in roles.Rows)
                {
                    table.AddCell(new Phrase(data["Id"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Role Name" : "Rol Adı"].ToString(), font2));
                }

                DownloadPdf(document, table, writer, fs, filepath, filename);
            }
        }

        private void ExportOrdersToPdf(DataTable orders, string lang)
        {
            if (orders.Rows.Count > 0)
            {
                string filename = (lang == "en") ? "Orders-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt") : "Siparisler-" + DateTime.Now.ToString("dd-MM-yyyy hh_mm_s_tt");
                string filepath = Path.Combine(_webHost.WebRootPath, "documents") + "\\" + filename + ".pdf";
                Document document = new(PageSize.A4, 5f, 5f, 10f, 10f);
                FileStream fs = new FileStream(filepath, FileMode.Create);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                Font font1 = FontFactory.GetFont(FontFactory.COURIER_BOLD, 10);
                Font font2 = FontFactory.GetFont(FontFactory.COURIER, 8);

                float[] columnDefinitionSize = { 2F, 5F, 5F, 5F, 5F };
                PdfPTable table;

                table = new PdfPTable(columnDefinitionSize)
                {
                    WidthPercentage = 100
                };

                table.AddCell(new Phrase("Id", font1));
                table.AddCell(new Phrase((lang == "en") ? "Order Date" : "Siparis Tarihi", font1));
                table.AddCell(new Phrase((lang == "en") ? "User" : "Kullanici", font1));
                table.AddCell(new Phrase((lang == "en") ? "City" : "Sehir", font1));
                table.AddCell(new Phrase((lang == "en") ? "Address" : "Adres", font1));
                table.HeaderRows = 1;

                foreach (DataRow data in orders.Rows)
                {
                    table.AddCell(new Phrase(data["Id"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Order Date" : "Sipariş Tarihi"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "User" : "Kullanıcı"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "City" : "Şehir"].ToString(), font2));
                    table.AddCell(new Phrase(data[(lang == "en") ? "Address" : "Adres"].ToString(), font2));
                }

                DownloadPdf(document, table, writer, fs, filepath, filename);
            }
        }

        private void DownloadPdf(Document document, PdfPTable table, PdfWriter writer, FileStream fs, string filepath, string filename)
        {
            document.Add(table);
            document.Close();
            document.CloseDocument();
            document.Dispose();
            writer.Close();
            writer.Dispose();
            fs.Close();
            fs.Dispose();

            FileStream sourceFile = new FileStream(filepath, FileMode.Open);
            float fileSize = sourceFile.Length;
            byte[] getContent = new byte[Convert.ToInt32(Math.Truncate(fileSize))];
            sourceFile.Read(getContent, 0, Convert.ToInt32(sourceFile.Length));
            sourceFile.Close();
            Response.Clear();
            Response.Headers.Clear();
            Response.ContentType = "application/pdf";
            Response.Headers.Append("Content-Length", getContent.Length.ToString());
            Response.Headers.Append("Content-Disposition", "attachment; filename=" + filename + ".pdf;");
            Response.Body.WriteAsync(getContent);
            Response.Body.Flush();
        }

        private void ExportToWord(DataTable dataTable, string lang, string filename)
        {
            DataTable dt = dataTable;

            if (dt.Rows.Count > 0)
            {
                StringBuilder sbDocumentBody = new StringBuilder();
                sbDocumentBody.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' " +
                              "xmlns:w='urn:schemas-microsoft-com:office:word' " +
                              "xmlns='http://www.w3.org/TR/REC-html40'>");

                if(filename == "Products")
                {
                    sbDocumentBody.Append("<head><meta charset='utf-8'><title>" + ((lang == "en") ? "Products" : "Ürünler") + "</title></head><body>");
                }

                else if(filename == "Brands")
                {
                    sbDocumentBody.Append("<head><meta charset='utf-8'><title>" + ((lang == "en") ? "Brands" : "Markalar") + "</title></head><body>");
                }

                else if(filename == "Categories")
                {
                    sbDocumentBody.Append("<head><meta charset='utf-8'><title>" + ((lang == "en") ? "Categories" : "Kategoriler") + "</title></head><body>");
                }

                else if(filename == "SubCategories")
                {
                    sbDocumentBody.Append("<head><meta charset='utf-8'><title>" + ((lang == "en") ? "Sub Categories" : "Alt Kategoriler") + "</title></head><body>");
                }

                else if (filename == "Cities")
                {
                    sbDocumentBody.Append("<head><meta charset='utf-8'><title>" + ((lang == "en") ? "Cities" : "Şehirler") + "</title></head><body>");
                }

                else if (filename == "Users")
                {
                    sbDocumentBody.Append("<head><meta charset='utf-8'><title>" + ((lang == "en") ? "Users" : "Kullanıcılar") + "</title></head><body>");
                }

                else if (filename == "Roles")
                {
                    sbDocumentBody.Append("<head><meta charset='utf-8'><title>" + ((lang == "en") ? "Roles" : "Roller") + "</title></head><body>");
                }

                else
                {
                    sbDocumentBody.Append("<head><meta charset='utf-8'><title>" + ((lang == "en") ? "Orders" : "Siparişler") + "</title></head><body>");
                }

                if (dt.Rows.Count > 0)
                {
                    sbDocumentBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
                    sbDocumentBody.Append("<tr><td>");
                    sbDocumentBody.Append("<table width=\"600\" cellpadding=0 cellspacing=0 style=\"border: 1px solid gray;\">");

                    // Add Column Headers dynamically from datatable  
                    sbDocumentBody.Append("<tr>");

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sbDocumentBody.Append("<td class=\"Header\" width=\"60\" style=\"border: 1px solid gray; text-align:center; font-family:Verdana; font-size:12px; font-weight:bold;\">" + dt.Columns[i].ToString().Replace(".", "<br>") + "</td>");
                    }

                    sbDocumentBody.Append("</tr>");

                    // Add Data Rows dynamically from datatable  
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sbDocumentBody.Append("<tr>");

                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            sbDocumentBody.Append("<td class=\"Content\" style=\"border: 1px solid gray;\">" + dt.Rows[i][j].ToString() + "</td>");
                        }

                        sbDocumentBody.Append("</tr>");
                    }

                    sbDocumentBody.Append("</table>");
                    sbDocumentBody.Append("</td></tr></table>");
                }

                sbDocumentBody.Append("</body></html>");
                byte[] byteArray = Encoding.UTF8.GetBytes(sbDocumentBody.ToString());
                Response.Clear();
                Response.Headers.Append("Content-Type", "application/msword");

                if (filename == "Products")
                {
                    Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Products.doc" : "Urunler.doc"));
                }

                else if (filename == "Brands")
                {
                    Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Brands.doc" : "Markalar.doc"));
                }

                else if (filename == "Categories")
                {
                    Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Categories.doc" : "Kategoriler.doc"));
                }

                else if (filename == "SubCategories")
                {
                    Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "SubCategories.doc" : "AltKategoriler.doc"));
                }

                else if (filename == "Cities")
                {
                    Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Cities.doc" : "Sehirler.doc"));
                }

                else if (filename == "Users")
                {
                    Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Users.doc" : "Kullanicilar.doc"));
                }

                else if (filename == "Roles")
                {
                    Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Roles.doc" : "Roller.doc"));
                }

                else
                {
                    Response.Headers.Append("Content-Disposition", "attachment; filename=" + ((lang == "en") ? "Orders.doc" : "Siparisler.doc"));
                }
                
                Response.Headers.Append("Content-Length", byteArray.Length.ToString());
                Response.Body.WriteAsync(byteArray);
                Response.Body.Flush();
            }
        }
    }
}
