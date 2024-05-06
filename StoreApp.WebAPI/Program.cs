using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<StoreContext>(options =>
{
    var config = builder.Configuration;
    string? connStr = config.GetConnectionString("mysql_connection");
    options.UseMySql(connStr, new MySqlServerVersion(new Version(8, 3, 0)), options => options.MigrationsAssembly("StoreApp.WebAPI"));
});
builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<StoreContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.AddScoped<IProductRepository, EfProductRepository>();
builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(mail => 
    new SmtpEmailSender(
        builder.Configuration["EmailSender:Host"],
        builder.Configuration.GetValue<int>("EmailSender:Port"),
        builder.Configuration.GetValue<bool>("EmailSender:EnableSSL"),
        builder.Configuration["EmailSender:Username"],
        builder.Configuration["EmailSender:Password"]
    )
);
builder.Services.AddScoped<IProductImageRepository, EfProductImageRepository>();
builder.Services.AddScoped<IBrandRepository, EfBrandRepository>();
builder.Services.AddScoped<IBrandSubCategoryRepository, EfBrandSubCategoryRepository>();
builder.Services.AddScoped<ICommentRepository, EfCommentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
