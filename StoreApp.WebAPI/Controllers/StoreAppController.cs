using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Web.Models;

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

        private readonly UserManager<AppUser> _userManager;

        private readonly RoleManager<AppRole> _roleManager;

        private readonly SignInManager<AppUser> _signInManager;

        private readonly IEmailSender _mailSender;

        public StoreAppController(IProductRepository repository, ICategoryRepository catRepository, IUserRepository userRepository, IProductImageRepository pImageRepository, IBrandRepository brandRepository, IBrandSubCategoryRepository brandSubCategoryRepository, ICommentRepository commentRepository, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IEmailSender mailSender)
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
        }

        [HttpGet("GetProducts")]
        public async Task<List<Product>> GetProducts()
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).OrderBy(p => p.Id).ToListAsync();
        }

        [HttpGet("GetProducts/{categoryUrl}")]
        public async Task<List<Product>> GetProducts(string categoryUrl)
        {
            return await _repository.Products.Include(p => p.SubCategory).Include(p => p.Brand).Where(p => p.SubCategory.Url == categoryUrl).OrderBy(p => p.Id).ToListAsync();
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

        [HttpPost("CreateProduct")]
        public async Task<Product?> CreateProduct(ProductViewModel product)
        {
            _repository.CreateProduct(new Product { Id = product.Id, Name = product.Name, Price = product.Price, Description = product.Description, SubCategoryId = product.SubCategoryId, BrandId = product.BrandId });
            return await _repository.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
        }

        [HttpPut("EditProduct")]
        public async Task<Product?> EditProduct(ProductViewModel product)
        {
            await _repository.Products.Where(p => p.Id == product.Id).ExecuteUpdateAsync(set => set.SetProperty(p => p.Name, product.Name)
                                                                                                .SetProperty(p => p.Price, product.Price)
                                                                                                .SetProperty(p => p.Description, product.Description)
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

        [HttpGet("GetBrands")]
        public async Task<List<Brand>> GetBrands()
        {
            return await _brandRepository.Brands.ToListAsync();
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
            await _brandRepository.Brands.Where(b => b.Id == brand.Id).ExecuteUpdateAsync(set => set.SetProperty(s => s.Name, brand.Name));
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
            await _catRepository.Categories.Where(c => c.Id == category.Id).ExecuteUpdateAsync(set => set.SetProperty(c => c.Name, category.Name));
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
            _catRepository.CreateSubCategory(new SubCategory { Id = subCategory.Id, Name = subCategory.Name, Url = subCategory.Url, CategoryId = subCategory.CategoryId });
            return await _catRepository.Categories.FirstOrDefaultAsync(c => c.Id == subCategory.Id);
        }

        [HttpPut("EditSubCategory")]
        public async Task<SubCategory?> EditSubCategory(SubCategoryViewModel subCategory)
        {
            await _catRepository.SubCategories.Where(p => p.Id == subCategory.Id).ExecuteUpdateAsync(set => set.SetProperty(p => p.Name, subCategory.Name)
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
            return await _userRepository.Users.ToListAsync();
        }

        [HttpGet("GetUser/{userId}")]
        public async Task<AppUser?> GetUser(string userId)
        {
            return await _userRepository.Users.FirstOrDefaultAsync(user => user.Id == userId);
        }

        [HttpPost("Register")]
        public async Task<AppUser?> Register(AppUser model)
        {
            _userRepository.CreateUser(model);
            //var token = await _userManager.GenerateEmailConfirmationTokenAsync(model);
            //var url = Url.Action("ConfirmEmail", "Home", new { model.Id, token });
            ////email
            //await _mailSender.SendEmailAsync(model.Email, "Hesap Onayý", "Lütfen email hesabýnýzý onaylamak için linke " +
            //    "<a href='http://localhost:5219'>týklayýnýz.</a>");
            return await _userRepository.Users.FirstOrDefaultAsync(user => user.Id == model.Id);
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
            await _userRepository.Users.Where(user => user.Id == model.Id).ExecuteUpdateAsync(set => set.SetProperty(u => u.UserName, model.UserName)
                                                                                               .SetProperty(u => u.FullName, model.FullName)
                                                                                               .SetProperty(u => u.Email, model.Email));

            return await _userRepository.Users.FirstOrDefaultAsync(user => user.Id == model.Id);
        }

        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            await _userRepository.Users.Where(user => user.Id == userId).ExecuteDeleteAsync();
            return NoContent();
        }

        [HttpGet("GetRoles")]
        public async Task<List<AppRole>> GetRoles()
        {
            return await _userRepository.Roles.ToListAsync();
        }

        [HttpGet("GetRole/{roleId}")]
        public async Task<AppRole?> GetRole(string roleId)
        {
            return await _userRepository.Roles.FirstOrDefaultAsync(role => role.Id == roleId);
        }

        [HttpPost("CreateRole")]
        public async Task<AppRole?> CreateRole(AppRole model)
        {
            _userRepository.CreateRole(model);
            return await _userRepository.Roles.FirstOrDefaultAsync(role => role.Id == model.Id);
        }

        [HttpPut("EditRole")]
        public async Task<AppRole?> EditRole(AppRole model)
        {
            await _userRepository.Roles.Where(role => role.Id == model.Id).ExecuteUpdateAsync(set => set.SetProperty(u => u.Name, model.Name));
            return await _userRepository.Roles.FirstOrDefaultAsync(role => role.Id == model.Id);
        }

        [HttpDelete("DeleteRole/{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            await _userRepository.Roles.Where(role => role.Id == roleId).ExecuteDeleteAsync();
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
