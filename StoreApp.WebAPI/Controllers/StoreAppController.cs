using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Common;
using StoreApp.Web.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;

namespace StoreApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreAppController : ControllerBase
    {
        private readonly IProductRepository _repository;

        private readonly ICategoryRepository _catRepository;

        private readonly IUserRepository _userRepository;

        private readonly IProductImageRepository _pImageRepository;

        private readonly IBrandRepository _brandRepository;

        private readonly IBrandSubCategoryRepository _brandSubCategoryRepository;

        private readonly ICommentRepository _commentRepository;

        private readonly IBasketRepository _basketRepository;

        private readonly IOrderRepository _orderRepository;

        private readonly ICityRepository _cityRepository;

        private readonly IPasswordHasher<AppUser> _passwordHasher;

        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<AppRole> _roleManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IEmailSender _mailSender;

        private readonly IConfiguration _configuration;

        public StoreAppController(IProductRepository repository, ICategoryRepository catRepository, IUserRepository userRepository, IProductImageRepository pImageRepository, IBrandRepository brandRepository, IBrandSubCategoryRepository brandSubCategoryRepository, ICommentRepository commentRepository, IBasketRepository basketRepository, IOrderRepository orderRepository, ICityRepository cityRepository, IPasswordHasher<AppUser> passwordHasher, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IEmailSender mailSender, IConfiguration configuration)
        {
            _repository = repository;
            _catRepository = catRepository;
            _userRepository = userRepository;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mailSender = mailSender;
            _pImageRepository = pImageRepository;
            _brandRepository = brandRepository;
            _brandSubCategoryRepository = brandSubCategoryRepository;
            _commentRepository = commentRepository;
            _basketRepository = basketRepository;
            _orderRepository = orderRepository;
            _cityRepository = cityRepository;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        [HttpGet("GetProducts")]
        public async Task<List<Product>> GetProducts()
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsMinPrice/{MinPrice}")]
        public async Task<List<Product>> GetProductsMinPrice(float MinPrice)
        {
            return await _repository.Products.Where(p => p.Price >= MinPrice).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsMaxPrice/{MaxPrice}")]
        public async Task<List<Product>> GetProductsMaxPrice(float MaxPrice)
        {
            return await _repository.Products.Where(p => p.Price <= MaxPrice).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsMinMaxPrice/{MinPrice}/{MaxPrice}")]
        public async Task<List<Product>> GetProductsMinMaxPrice(float MinPrice, float MaxPrice)
        {
            return await _repository.Products.Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsBrands/{Brands}")]
        public async Task<List<Product>> GetProductsBrands(string Brands)
        {
            int[] brands = new int[Brands.Split(",").Length];
            int i = 0;

            if (!string.IsNullOrEmpty(Brands))
            {
                foreach (string brandId in Brands.Split(","))
                {
                    brands[i] = int.Parse(brandId);
                    i++;
                }
            }

            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => brands.Contains(p.BrandId)).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsBrandsMaxPrice/{Brands}/{MaxPrice}")]
        public async Task<List<Product>> GetProductsBrandsMaxPrice(string Brands, float MaxPrice)
        {
            int[] brands = new int[Brands.Split(",").Length];
            int i = 0;

            if (!string.IsNullOrEmpty(Brands))
            {
                foreach (string brandId in Brands.Split(","))
                {
                    brands[i] = int.Parse(brandId);
                    i++;
                }
            }

            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => brands.Contains(p.BrandId) && p.Price <= MaxPrice).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsBrandsMinMaxPrice/{Brands}/{MinPrice}/{MaxPrice}")]
        public async Task<List<Product>> GetProductsBrandsMinMaxPrice(string Brands, float MinPrice, float MaxPrice)
        {
            int[] brands = new int[Brands.Split(",").Length];
            int i = 0;

            if (!string.IsNullOrEmpty(Brands))
            {
                foreach (string brandId in Brands.Split(","))
                {
                    brands[i] = int.Parse(brandId);
                    i++;
                }
            }

            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => brands.Contains(p.BrandId) && p.Price >= MinPrice && p.Price <= MaxPrice).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProducts/{categoryUrl}")]
        public async Task<List<Product>> GetProducts(string categoryUrl)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.SubCategory.Url == categoryUrl).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsUrlMinPrice/{categoryUrl}/{MinPrice}")]
        public async Task<List<Product>> GetProductsUrlMinPrice(string categoryUrl, float MinPrice)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.SubCategory.Url == categoryUrl && p.Price >= MinPrice).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsUrlMaxPrice/{categoryUrl}/{MaxPrice}")]
        public async Task<List<Product>> GetProductsUrlMaxPrice(string categoryUrl, float MaxPrice)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.SubCategory.Url == categoryUrl && p.Price <= MaxPrice).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsUrlMinMaxPrice/{categoryUrl}/{MinPrice}/{MaxPrice}")]
        public async Task<List<Product>> GetProductsUrlMinMaxPrice(string categoryUrl, float MinPrice, float MaxPrice)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.SubCategory.Url == categoryUrl && p.Price >= MinPrice && p.Price <= MaxPrice).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsUrlBrands/{categoryUrl}/{Brands}")]
        public async Task<List<Product>> GetProductsUrlBrands(string categoryUrl, string Brands)
        {
            int[] brands = new int[Brands.Split(",").Length];
            int i = 0;

            if (!string.IsNullOrEmpty(Brands))
            {
                foreach(string brandId in Brands.Split(","))
                {
                    brands[i] = int.Parse(brandId);
                    i++;
                }
            }

            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.SubCategory.Url == categoryUrl && brands.Contains(p.BrandId)).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsUrlBrandsMinPrice/{categoryUrl}/{Brands}/{MinPrice}")]
        public async Task<List<Product>> GetProductsUrlBrandsMinPrice(string categoryUrl, string Brands, float MinPrice)
        {
            int[] brands = new int[Brands.Split(",").Length];
            int i = 0;

            if (!string.IsNullOrEmpty(Brands))
            {
                foreach (string brandId in Brands.Split(","))
                {
                    brands[i] = int.Parse(brandId);
                    i++;
                }
            }

            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.SubCategory.Url == categoryUrl && brands.Contains(p.BrandId) && p.Price >= MinPrice).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsUrlBrandsMaxPrice/{categoryUrl}/{Brands}/{MaxPrice}")]
        public async Task<List<Product>> GetProductsUrlBrandsMaxPrice(string categoryUrl, string Brands, float MaxPrice)
        {
            int[] brands = new int[Brands.Split(",").Length];
            int i = 0;

            if (!string.IsNullOrEmpty(Brands))
            {
                foreach (string brandId in Brands.Split(","))
                {
                    brands[i] = int.Parse(brandId);
                    i++;
                }
            }

            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.SubCategory.Url == categoryUrl && brands.Contains(p.BrandId) && p.Price <= MaxPrice).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsUrlBrandsMinMaxPrice/{categoryUrl}/{Brands}/{MinPrice}/{MaxPrice}")]
        public async Task<List<Product>> GetProductsUrlBrandsMinMaxPrice(string categoryUrl, string Brands, float MinPrice, float MaxPrice)
        {
            int[] brands = new int[Brands.Split(",").Length];
            int i = 0;

            if (!string.IsNullOrEmpty(Brands))
            {
                foreach (string brandId in Brands.Split(","))
                {
                    brands[i] = int.Parse(brandId);
                    i++;
                }
            }

            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.SubCategory.Url == categoryUrl && brands.Contains(p.BrandId) && p.Price >= MinPrice && p.Price <= MaxPrice).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProducts/{categoryUrl}/{PageIndex}")]
        public async Task<List<Product>> GetProducts(string categoryUrl, int PageIndex)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.SubCategory.Url == categoryUrl).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndex/{PageIndex}")]
        public async Task<List<Product>> GetProductsByPageIndex(int PageIndex)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexBrands/{PageIndex}/{Brands}")]
        public async Task<List<Product>> GetProductsByPageIndexBrands(int PageIndex, string Brands)
        {
            int[] brands = new int[Brands.Split(",").Count()];
            int i = 0;

            foreach (string brandId in Brands.Split(","))
            {
                brands[i] = int.Parse(brandId);
                i++;
            }

            return await _repository.Products.Where(p => brands.Contains(p.BrandId)).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexBrands/{PageIndex}/{Brands}/{Url}")]
        public async Task<List<Product>> GetProductsByPageIndexBrands(int PageIndex, string Brands, string Url)
        {
            int[] brands = new int[Brands.Split(",").Count()];
            int i = 0;

            foreach (string brandId in Brands.Split(","))
            {
                brands[i] = int.Parse(brandId);
                i++;
            }

            return await _repository.Products.Where(p => brands.Contains(p.BrandId) && p.SubCategory.Url == Url).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexBrandsMaxPrice/{PageIndex}/{Brands}/{MaxPrice}")]
        public async Task<List<Product>> GetProductsByPageIndexBrandsMaxPrice(int PageIndex, string Brands, float MaxPrice)
        {
            int[] brands = new int[Brands.Split(",").Count()];
            int i = 0;

            foreach (string brandId in Brands.Split(","))
            {
                brands[i] = int.Parse(brandId);
                i++;
            }

            return await _repository.Products.Where(p => brands.Contains(p.BrandId) && p.Price <= MaxPrice).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexBrandsMaxPrice/{PageIndex}/{Brands}/{MaxPrice}/{Url}")]
        public async Task<List<Product>> GetProductsByPageIndexBrandsMaxPrice(int PageIndex, string Brands, float MaxPrice, string Url)
        {
            int[] brands = new int[Brands.Split(",").Count()];
            int i = 0;

            foreach (string brandId in Brands.Split(","))
            {
                brands[i] = int.Parse(brandId);
                i++;
            }

            return await _repository.Products.Where(p => brands.Contains(p.BrandId) && p.Price <= MaxPrice && p.SubCategory.Url == Url).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexBrandsMinPrice/{PageIndex}/{Brands}/{MinPrice}")]
        public async Task<List<Product>> GetProductsByPageIndexBrandsMinPrice(int PageIndex, string Brands, float MinPrice)
        {
            int[] brands = new int[Brands.Split(",").Count()];
            int i = 0;

            foreach (string brandId in Brands.Split(","))
            {
                brands[i] = int.Parse(brandId);
                i++;
            }

            return await _repository.Products.Where(p => brands.Contains(p.BrandId) && p.Price >= MinPrice).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexBrandsMinPrice/{PageIndex}/{Brands}/{MinPrice}/{Url}")]
        public async Task<List<Product>> GetProductsByPageIndexBrandsMinPrice(int PageIndex, string Brands, float MinPrice, string Url)
        {
            int[] brands = new int[Brands.Split(",").Count()];
            int i = 0;

            foreach (string brandId in Brands.Split(","))
            {
                brands[i] = int.Parse(brandId);
                i++;
            }

            return await _repository.Products.Where(p => brands.Contains(p.BrandId) && p.Price >= MinPrice && p.SubCategory.Url == Url).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexBrandsMinMaxPrice/{PageIndex}/{Brands}/{MinPrice}/{MaxPrice}")]
        public async Task<List<Product>> GetProductsByPageIndexBrandsMinMaxPrice(int PageIndex, string Brands, float MinPrice, float MaxPrice)
        {
            int[] brands = new int[Brands.Split(",").Count()];
            int i = 0;

            foreach (string brandId in Brands.Split(","))
            {
                brands[i] = int.Parse(brandId);
                i++;
            }

            return await _repository.Products.Where(p => brands.Contains(p.BrandId) && p.Price >= MinPrice && p.Price <= MaxPrice).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexBrandsMinMaxPrice/{PageIndex}/{Brands}/{MinPrice}/{MaxPrice}/{Url}")]
        public async Task<List<Product>> GetProductsByPageIndexBrandsMinMaxPrice(int PageIndex, string Brands, float MinPrice, float MaxPrice, string Url)
        {
            int[] brands = new int[Brands.Split(",").Count()];
            int i = 0;

            foreach (string brandId in Brands.Split(","))
            {
                brands[i] = int.Parse(brandId);
                i++;
            }

            return await _repository.Products.Where(p => brands.Contains(p.BrandId) && p.Price >= MinPrice && p.Price <= MaxPrice && p.SubCategory.Url == Url).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexMaxPrice/{PageIndex}/{MaxPrice}")]
        public async Task<List<Product>> GetProductsByPageIndexMaxPrice(int PageIndex, float MaxPrice)
        {
            return await _repository.Products.Where(p => p.Price <= MaxPrice).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexMaxPrice/{PageIndex}/{MaxPrice}/{Url}")]
        public async Task<List<Product>> GetProductsByPageIndexMaxPrice(int PageIndex, float MaxPrice, string Url)
        {
            return await _repository.Products.Where(p => p.Price <= MaxPrice && p.SubCategory.Url == Url).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexMinPrice/{PageIndex}/{MinPrice}")]
        public async Task<List<Product>> GetProductsByPageIndexMinPrice(int PageIndex, float MinPrice)
        {
            return await _repository.Products.Where(p => p.Price >= MinPrice).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexMinPrice/{PageIndex}/{MinPrice}/{Url}")]
        public async Task<List<Product>> GetProductsByPageIndexMinPrice(int PageIndex, float MinPrice, string Url)
        {
            return await _repository.Products.Where(p => p.Price >= MinPrice && p.SubCategory.Url == Url).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexMinMaxPrice/{PageIndex}/{MinPrice}/{MaxPrice}")]
        public async Task<List<Product>> GetProductsByPageIndexMinMaxPrice(int PageIndex, float MinPrice, float MaxPrice)
        {
            return await _repository.Products.Where(p => p.Price >= MinPrice && p.Price <= MaxPrice).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsByPageIndexMinMaxPrice/{PageIndex}/{MinPrice}/{MaxPrice}/{Url}")]
        public async Task<List<Product>> GetProductsByPageIndexMinMaxPrice(int PageIndex, float MinPrice, float MaxPrice, string Url)
        {
            return await _repository.Products.Where(p => p.Price >= MinPrice && p.Price <= MaxPrice && p.SubCategory.Url == Url).Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).Skip(8 * (PageIndex - 1)).Take(8).ToListAsync();
        }

        [HttpGet("GetProductsBySubCategoryId/{subCategoryId}")]
        public async Task<List<Product>> GetProductsBySubCategoryId(int subCategoryId)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.SubCategoryId == subCategoryId).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsByBrandId/{brandId}")]
        public async Task<List<Product>> GetProductsByBrandId(int brandId)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.BrandId == brandId).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProductsBySearchText/{SearchText}")]
        public async Task<List<Product>> GetProductsBySearchText(string SearchText)
        {
            return await _repository.Products.Where(p => p.Name!.StartsWith(SearchText)).Include(p => p.SubCategory).Include(p => p.Brand).ToListAsync();
        }

        [HttpGet("GetProductByUrl/{productUrl}")]
        public async Task<Product?> GetProductByUrl(string productUrl)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).FirstOrDefaultAsync(p => p.Url == productUrl);
        }

        [HttpGet("GetProduct/{productId}")]
        public async Task<Product?> GetProduct(int productId)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == productId);
        }

        [HttpGet("GetProductByProductName/{productName}")]
        public async Task<Product?> GetProductByProductName(string productName)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).FirstOrDefaultAsync(p => p.Name == productName || p.Ml1Name == productName || p.Ml2Name == productName);
        }

        [HttpPost("CreateProduct")]
        public async Task<Product?> CreateProduct(ProductViewModel product)
        {
            _repository.CreateProduct(new Product { Id = product.Id, Name = product.Name, Ml1Name = product.Ml1Name, Ml2Name = product.Ml2Name, Price = product.Price, Description = product.Description, Url = product.Url, SubCategoryId = product.SubCategoryId, BrandId = product.BrandId });
            return await _repository.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        }

        [HttpPut("EditProduct")]
        public async Task<Product?> EditProduct(ProductViewModel product)
        {
            await _repository.Products.Where(p => p.Id == product.Id).ExecuteUpdateAsync(set => set.SetProperty(p => p.Name, product.Name)
                                                                                                .SetProperty(p => p.Ml1Name, product.Ml1Name)
                                                                                                .SetProperty(p => p.Ml2Name, product.Ml2Name)
                                                                                                .SetProperty(p => p.Price, product.Price)
                                                                                                .SetProperty(p => p.Description, product.Description)
                                                                                                .SetProperty(p => p.Url, product.Url)
                                                                                                .SetProperty(p => p.SubCategoryId, product.SubCategoryId)
                                                                                                .SetProperty(p => p.BrandId, product.BrandId));

            return await _repository.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        }

        [HttpDelete("DeleteProduct/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _repository.Products.Where(p => p.Id == productId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpGet("GetBaskets")]
        public async Task<List<BasketItem>> GetBaskets()
        {
            var results = from b in _basketRepository.Baskets
                          join brands in _brandRepository.Brands on b.BrandId equals brands.Id
                          group b by b.Name into g
                          select new BasketItem
                          {
                             Id = g.Max(b => b.Id),
                             Name = g.Key,
                             Price = g.Max(x => x.Price),
                             Url = g.Max(x => x.Url),
                             BrandId = g.Max(x => x.BrandId),
                             Count = g.Count()
                          };

            return await results.ToListAsync();
        }

        [HttpPost("CreateBasket")]
        public async Task<BasketItem?> CreateBasket(BasketItemViewModel basket)
        {
            _basketRepository.CreateBasket(new BasketItem { Id = basket.Id, Name = basket.Name, Price = basket.Price, Url = basket.Url, BrandId = basket.BrandId });
            return await _basketRepository.Baskets.Include(b => b.Brand).FirstOrDefaultAsync(b => b.Id == basket.Id);
        }

        [HttpDelete("DeleteBasket/{productId}")]
        public async Task<IActionResult> DeleteBasket(int productId)
        {
            await _basketRepository.Baskets.Where(b => b.Id == productId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpDelete("DeleteBasketByProductName/{productName}")]
        public async Task<IActionResult> DeleteBasketByProductName(string productName)
        {
            await _basketRepository.Baskets.Where(b => b.Name == productName).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpDelete("DeleteAllBasketProducts")]
        public async Task<IActionResult> DeleteAllBasketProducts()
        {
            await _basketRepository.Baskets.ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpGet("GetOrders")]
        public async Task<List<OrderItem>> GetOrders()
        {
            return await _orderRepository.Orders.Include(o => o.User).Include(o => o.City).OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        [HttpPost("CreateOrder")]
        public async Task<OrderItem?> CreateOrder(OrderItemViewModel order)
        {
            _orderRepository.CreateOrder(new OrderItem { Id = order.Id, OrderDate = order.OrderDate, UserId = order.UserId, CityId = order.CityId, PhoneNumber = order.PhoneNumber, Address = order.Address, CartName = order.CartName, CartNumber = order.CartNumber, ExpirationMonth = order.ExpirationMonth, ExpirationYear = order.ExpirationYear, Cvc = order.Cvc, Email = order.Email });
            return await _orderRepository.Orders.Include(o => o.User).Include(o => o.City).FirstOrDefaultAsync(o => o.OrderDate == order.OrderDate);
        }

        [HttpGet("GetBrands")]
        public async Task<List<Brand>> GetBrands()
        {
            return await _brandRepository.Brands.ToListAsync();
        }

        [HttpGet("GetBrandsBySearchText/{SearchText}")]
        public async Task<List<Brand>> GetBrandsBySearchText(string SearchText)
        {
            return await _brandRepository.Brands.Where(b => b.Name.StartsWith(SearchText)).ToListAsync();
        }

        [HttpGet("GetBrand/{brandId}")]
        public async Task<Brand?> GetBrand(int brandId)
        {
            return await _brandRepository.Brands.FirstOrDefaultAsync(b => b.Id == brandId);
        }

        [HttpPost("CreateBrand")]
        public async Task<Brand?> CreateBrand(Brand brand)
        {
            _brandRepository.CreateBrand(brand);
            return await _brandRepository.Brands.FirstOrDefaultAsync(b => b.Id == brand.Id);
        }

        [HttpPut("EditBrand")]
        public async Task<Brand?> EditBrand(Brand brand)
        {
            await _brandRepository.Brands.Where(b => b.Id == brand.Id).ExecuteUpdateAsync(set => set.SetProperty(s => s.Name, brand.Name)
                                                                                                    .SetProperty(s => s.Ml1Name, brand.Ml1Name)
                                                                                                    .SetProperty(s => s.Ml2Name, brand.Ml2Name));

            return await _brandRepository.Brands.FirstOrDefaultAsync(b => b.Id == brand.Id);
        }

        [HttpDelete("DeleteBrand/{brandId}")]
        public async Task<IActionResult> DeleteBrand(int brandId)
        {
            await _brandRepository.Brands.Where(b => b.Id == brandId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpGet("GetBrandSubCategories")]
        public async Task<List<BrandSubCategory>> GetBrandSubCategories()
        {
            return await _brandSubCategoryRepository.BrandSubCategories.Include(b => b.Brand).Include(b => b.SubCategory).ThenInclude(s => s.Category).ToListAsync();
        }

        [HttpGet("GetBrandSubCategories/{brandId}")]
        public async Task<List<BrandSubCategory>> GetBrandSubCategories(int brandId)
        {
            return await _brandSubCategoryRepository.BrandSubCategories.Where(b => b.BrandId == brandId).Include(b => b.Brand).Include(b => b.SubCategory).ThenInclude(s => s.Category).ToListAsync();
        }

        [HttpGet("GetBrandSubCategoriesByCategoryId/{subCategoryId}")]
        public async Task<List<BrandSubCategory>> GetBrandSubCategoriesByCategoryId(int subCategoryId)
        {
            return await _brandSubCategoryRepository.BrandSubCategories.Where(b => b.SubCategoryId == subCategoryId).Include(b => b.Brand).Include(b => b.SubCategory).ThenInclude(s => s.Category).ToListAsync();
        }

        [HttpPost("CreateBrandSubCategory")]
        public async Task<BrandSubCategory?> CreateBrandSubCategory(BrandSubCategoryViewModel model)
        {
            _brandSubCategoryRepository.CreateBrand(new BrandSubCategory { BrandId = model.BrandId, SubCategoryId = model.SubCategoryId });
            return await _brandSubCategoryRepository.BrandSubCategories.FirstOrDefaultAsync(b => b.BrandId == model.BrandId && b.SubCategoryId == model.SubCategoryId);
        }

        [HttpDelete("DeleteBrandSubCategories/{brandId}")]
        public async Task<IActionResult> DeleteBrandSubCategories(int brandId)
        {
            await _brandSubCategoryRepository.BrandSubCategories.Where(b => b.BrandId == brandId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpGet("GetCities")]
        public async Task<List<City>> GetCities()
        {
            return await _cityRepository.Cities.ToListAsync();
        }

        [HttpGet("GetCity/{cityId}")]
        public async Task<City?> GetCity(int cityId)
        {
            return await _cityRepository.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
        }

        [HttpPost("CreateCity")]
        public async Task<City?> CreateCity(City city)
        {
            _cityRepository.CreateCity(city);
            return await _cityRepository.Cities.FirstOrDefaultAsync(c => c.Id == city.Id);
        }

        [HttpPut("EditCity")]
        public async Task<City?> EditCity(City city)
        {
            await _cityRepository.Cities.Where(c => c.Id == city.Id).ExecuteUpdateAsync(set => set.SetProperty(s => s.Name, city.Name));
            return await _cityRepository.Cities.FirstOrDefaultAsync(c => c.Id == city.Id);
        }

        [HttpDelete("DeleteCity/{cityId}")]
        public async Task<IActionResult> DeleteCity(int cityId)
        {
            await _cityRepository.Cities.Where(c => c.Id == cityId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpGet("GetProductImages/{productId}")]
        public async Task<List<ProductImage>> GetProductImages(int productId)
        {
            return await _pImageRepository.ProductImages.Where(p => p.ProductId == productId).Include(p => p.Product).ToListAsync();
        }

        [HttpGet("GetCardProductImages/{productId}")]
        public async Task<List<ProductImage>> GetCardProductImages(int productId)
        {
            return await _pImageRepository.ProductImages.Where(p => p.ProductId == productId).Include(p => p.Product).Take(4).ToListAsync();
        }

        [HttpGet("GetProductImage/{productImageId}")]
        public async Task<ProductImage?> GetProductImage(int productImageId)
        {
            return await _pImageRepository.ProductImages.Include(p => p.Product).FirstOrDefaultAsync(p => p.Id == productImageId);
        }

        [HttpPost("CreateProductImage")]
        public async Task<ProductImage?> CreateProductImage(ProductImageViewModel productImage)
        {
            _pImageRepository.CreateProductImages(new ProductImage { Id = productImage.Id, ProductId = productImage.ProductId, Url = productImage.Url });
            return await _pImageRepository.ProductImages.FirstOrDefaultAsync(p => p.Id == productImage.Id);
        }

        [HttpPut("EditProductImage")]
        public async Task<ProductImage?> EditProductImage(ProductImageViewModel productImage)
        {
            await _pImageRepository.ProductImages.Where(p => p.Id == productImage.Id).ExecuteUpdateAsync(set => set.SetProperty(p => p.Url, productImage.Url)
                                                                                                          .SetProperty(p => p.ProductId, productImage.ProductId));

            return await _pImageRepository.ProductImages.FirstOrDefaultAsync(p => p.Id == productImage.Id);
        }

        [HttpDelete("DeleteProductImage/{productImageId}")]
        public async Task<IActionResult> DeleteProductImage(int productImageId)
        {
            await _pImageRepository.ProductImages.Where(p => p.Id == productImageId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpGet("GetCategories")]
        public async Task<List<Category>> GetCategories()
        {
            return await _catRepository.Categories.ToListAsync();
        }

        [HttpGet("GetCategory/{categoryId}")]
        public async Task<Category?> GetCategory(int categoryId)
        {
            return await _catRepository.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        [HttpPost("CreateCategory")]
        public async Task<Category?> CreateCategory(Category category)
        {
            _catRepository.CreateCategory(category);
            return await _catRepository.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
        }

        [HttpPut("EditCategory")]
        public async Task<Category?> EditCategory(Category category)
        {
            await _catRepository.Categories.Where(c => c.Id == category.Id).ExecuteUpdateAsync(set => set.SetProperty(c => c.Name, category.Name)
                                                                                                         .SetProperty(c => c.Ml1Name, category.Ml1Name)
                                                                                                         .SetProperty(c => c.Ml2Name, category.Ml2Name));

            return await _catRepository.Categories.FirstOrDefaultAsync(c => c.Id == category.Id);
        }

        [HttpDelete("DeleteCategory/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            await _catRepository.Categories.Where(p => p.Id == categoryId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpGet("GetSubCategories")]
        public async Task<List<SubCategory>> GetSubCategories()
        {
            return await _catRepository.SubCategories.Include(c => c.Category).ToListAsync();
        }

        [HttpGet("GetSubCategoriesBySearchText/{SearchText}")]
        public async Task<List<SubCategory>> GetSubCategoriesBySearchText(string SearchText)
        {
            return await _catRepository.SubCategories.Where(c => c.Name!.StartsWith(SearchText)).ToListAsync();
        }

        [HttpGet("GetSubCategories/{categoryId}")]
        public async Task<List<SubCategory>> GetSubCategories(int categoryId)
        {
              return await _catRepository.SubCategories.Where(s => s.CategoryId == categoryId).Include(s => s.Category).ToListAsync();
        }

        [HttpGet("GetSubCategory/{subCategoryId}")]
        public async Task<SubCategory?> GetSubCategory(int subCategoryId)
        {
            return await _catRepository.SubCategories.Include(s => s.Category).FirstOrDefaultAsync(sc => sc.Id == subCategoryId);
        }

        [HttpGet("GetSubCategoryByUrl/{url}")]
        public async Task<SubCategory?> GetSubCategoryByUrl(string url)
        {
            return await _catRepository.SubCategories.Include(s => s.Category).FirstOrDefaultAsync(sc => sc.Url == url);
        }

        [HttpPost("CreateSubCategory")]
        public async Task<Category?> CreateSubCategory(SubCategoryViewModel subCategory)
        {
            _catRepository.CreateSubCategory(new SubCategory { Id = subCategory.Id, Name = subCategory.Name, Ml1Name = subCategory.Ml1Name, Ml2Name = subCategory.Ml2Name, Url = subCategory.Url, CategoryId = subCategory.CategoryId });
            return await _catRepository.Categories.FirstOrDefaultAsync(c => c.Id == subCategory.Id);
        }

        [HttpPut("EditSubCategory")]
        public async Task<SubCategory?> EditSubCategory(SubCategoryViewModel subCategory)
        {
            await _catRepository.SubCategories.Where(p => p.Id == subCategory.Id).ExecuteUpdateAsync(set => set.SetProperty(p => p.Name, subCategory.Name)
                                                                                                .SetProperty(p => p.Ml1Name, subCategory.Ml1Name)
                                                                                                .SetProperty(p => p.Ml2Name, subCategory.Ml2Name)
                                                                                                .SetProperty(p => p.Url, subCategory.Url)
                                                                                                .SetProperty(p => p.CategoryId, subCategory.CategoryId));
            
            return await _catRepository.SubCategories.FirstOrDefaultAsync(c => c.Id == subCategory.Id);
        }

        [HttpDelete("DeleteSubCategory/{subCategoryId}")]
        public async Task<IActionResult> DeleteSubCategory(int subCategoryId)
        {
            await _catRepository.SubCategories.Where(p => p.Id == subCategoryId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpGet("GetUsers")]
        public async Task<List<AppUser>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        [HttpGet("GetUser/{userId}")]
        public async Task<AppUser?> GetUser(string userId)
        {
            return await _userManager.Users.FirstOrDefaultAsync(user => user.Id == userId);
        }

        [HttpGet("GetUserByEmail/{email}")]
        public async Task<AppUser?> GetUserByEmail(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(user => user.Email == email);
        }

        [HttpGet("GetUserIdByUserName/{userName}")]
        public async Task<string?> GetUserIdByUserName(string userName)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(user => user.FullName == userName);

            if(user != null)
            {
                return user.Id;
            }
            
            return null;
        }

        [HttpPost("Register")]
        public async Task<AppUser?> Register(AppUser model)
        {
            model.SecurityStamp = Guid.NewGuid().ToString();
            model.PasswordHash = _passwordHasher.HashPassword(model, model.Password);
            var result = await _userManager.CreateAsync(model);

            if (result.Succeeded)
            {
                return await _userManager.Users.FirstOrDefaultAsync(u => u.Id == model.Id);
            }

            return null;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            List<AppUser>? users = (await _userManager.GetUsersInRoleAsync("admin")).ToList();
            bool isAdmin = users.Contains(user!);

            if(user == null)
            {
                return BadRequest(new { message = "Email hatalý" });
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return Ok(new { token = GenerateJWT(user), isAuthenticated = true, isAdmin, userId = user.Id });
            }

            return Unauthorized();
        }

        private object GenerateJWT(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? "");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                    }
                ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpGet("GetUserRoles/{userId}")]
        public async Task<List<IdentityUserRole<string>>> GetUserRoles(string userId)
        {
            return await _userRepository.UserRoles.Where(u => u.UserId == userId).ToListAsync();
        }

        [HttpPost("CreateUserRoles")]
        public async Task<IdentityUserRole<string>?> CreateUserRoles(AppUserRolesViewModel model)
        {
            _userRepository.CreateUserRole(new IdentityUserRole<string> { UserId = model.UserId, RoleId = model.RoleId });
            return await _userRepository.UserRoles.FirstOrDefaultAsync(user => user.UserId == model.UserId && user.RoleId == model.RoleId);
        }

        [HttpDelete("DeleteUserRoles/{userId}")]
        public async Task<IActionResult> DeleteUserRoles(string userId)
        {
            await _userRepository.UserRoles.Where(ur => ur.UserId == userId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpPut("EditUser")]
        public async Task<AppUser?> EditUser(AppUserViewModel model)
        {
            await _userManager.Users.Where(user => user.Id == model.Id).ExecuteUpdateAsync(set => set.SetProperty(u => u.UserName, model.UserName)
                                                                                               .SetProperty(u => u.FullName, model.FullName)
                                                                                               .SetProperty(u => u.Email, model.Email)
                                                                                               .SetProperty(u => u.Image, model.Image)
                                                                                               .SetProperty(u => u.NormalizedUserName, model.UserName.ToUpper())
                                                                                               .SetProperty(u => u.NormalizedEmail, model.Email.ToUpper()));

            return await _userManager.Users.FirstOrDefaultAsync(user => user.Id == model.Id);
        }

        [HttpPut("EditUserPassword")]
        public async Task<AppUser?> EditUserPassword(ForgotPasswordViewModel model)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == model.Email);
            await _userManager.Users.Where(user => user.Email == model.Email).ExecuteUpdateAsync(set => set.SetProperty(u => u.Password, model.Password)
                                                                                               .SetProperty(u => u.PasswordConfirmed, model.PasswordConfirmed)
                                                                                               .SetProperty(u => u.PasswordHash, _passwordHasher.HashPassword(user!, model.Password)));

            return user;
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _userManager.Users.Where(user => user.Id == userId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpGet("GetRoles")]
        public async Task<List<AppRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        [HttpGet("GetRoleByRoleName/{roleName}")]
        public async Task<AppRole?> GetRoleByRoleName(string roleName)
        {
            return await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name!.ToUpper() == roleName.ToUpper());
        }

        [HttpGet("GetRole/{roleId}")]
        public async Task<AppRole?> GetRole(string roleId)
        {
            return await _roleManager.Roles.FirstOrDefaultAsync(role => role.Id == roleId);
        }

        [HttpPost("CreateRole")]
        public async Task<AppRole?> CreateRole(AppRole model)
        {
            await _roleManager.CreateAsync(model);
            return await _roleManager.Roles.FirstOrDefaultAsync(role => role.Id == model.Id);
        }

        [HttpPut("EditRole")]
        public async Task<AppRole?> EditRole(AppRole model)
        {
            await _roleManager.Roles.Where(role => role.Id == model.Id).ExecuteUpdateAsync(set => set.SetProperty(u => u.Name, model.Name)
                                                                                                  .SetProperty(u => u.NormalizedName, model.Name!.ToUpper()));
            return await _roleManager.Roles.FirstOrDefaultAsync(role => role.Id == model.Id);
        }

        [HttpDelete("DeleteRole/{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            await _roleManager.Roles.Where(role => role.Id == roleId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpGet("GetComments")]
        public async Task<List<Comment>> GetComments()
        {
            return await _commentRepository.Comments.Include(c => c.Product).Include(c => c.User).OrderBy(c => c.CommentDate).ToListAsync();
        }

        [HttpGet("GetComments/{productId}")]
        public async Task<List<Comment>> GetComments(int productId)
        {
            return await _commentRepository.Comments.Where(c => c.ProductId == productId).Include(c => c.Product).Include(c => c.User).OrderBy(c => c.CommentDate).ToListAsync();
        }

        [HttpPost("CreateComment")]
        public async Task<Comment?> CreateComment(CommentViewModel model)
        {
            _commentRepository.CreateComment(new Comment { CommentText = model.CommentText, ProductId = model.ProductId, UserId = model.UserId });
            return await _commentRepository.Comments.Include(c => c.Product).ThenInclude(p => p.SubCategory).Include(c => c.User).FirstOrDefaultAsync(c => c.Id == model.Id);
        }
    }
}
