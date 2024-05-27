using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StoreApp.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Image = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateAdded = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PasswordConfirmed = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<float>(type: "float", nullable: true),
                    Url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BrandSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandSubCategories_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandSubCategories_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<float>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Url = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    BrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CommentText = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CommentDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Samsung" },
                    { 2, "Apple" },
                    { 3, "Casio" },
                    { 4, "Wainer" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Elektronik" },
                    { 2, "Moda" },
                    { 3, "Ev, Yaşam, Kırtasiye, Ofis" },
                    { 4, "Oto, Bahçe, Yapı Market" },
                    { 5, "Anne, Bebek, Oyuncak" },
                    { 6, "Spor, Outdoor" },
                    { 7, "Kozmetik, Kişisel Bakım" },
                    { 8, "Süpermarket, Pet Shop" },
                    { 9, "Kitap, Müzik, Film, Hobi" }
                });

            migrationBuilder.InsertData(
                table: "SubCategories",
                columns: new[] { "Id", "CategoryId", "Name", "Url" },
                values: new object[,]
                {
                    { 1, 1, "Bilgisayar/Tablet", "bilgisayar-tablet" },
                    { 2, 1, "Yazıcılar & Projeksiyon", "yazicilar-projeksiyon" },
                    { 3, 1, "Telefon & Telefon Aksesuarları", "telefon" },
                    { 4, 1, "TV, Görüntü & Ses Sistemleri", "tv-goruntu" },
                    { 5, 1, "Beyaz Eşya", "beyaz-esya" },
                    { 6, 1, "Klima ve Isıtıcılar", "klima-isiticilar" },
                    { 7, 1, "Elektrikli Ev Aletleri", "elektrikli-ev-aletleri" },
                    { 8, 1, "Foto & Kamera", "foto-kamera" },
                    { 9, 1, "Oyun & Oyun Konsolları", "oyun" },
                    { 10, 2, "Kadın Giyim", "kadin-giyim" },
                    { 11, 2, "Kadın Aksesuar & Takı", "kadin-aksesuar" },
                    { 12, 2, "Erkek Giyim", "erkek-giyim" },
                    { 13, 2, "Erkek Aksesuar & Takı", "erkek-aksesuar" },
                    { 14, 2, "Altın", "altin" },
                    { 15, 2, "Outdoor Giyim & Ayakkabı", "outdoor-giyim" },
                    { 16, 2, "Ayakkabı & Çanta", "ayakkabi-canta" },
                    { 17, 2, "Çocuk", "cocuk" },
                    { 18, 2, "Yurt Dışından", "yurt-disindan" },
                    { 19, 3, "Sofra & Mutfak", "sofra-mutfak" },
                    { 20, 3, "Ev Gereçleri & Ütü Masaları", "ev-gerecleri" },
                    { 21, 3, "Mobilya", "mobilya" },
                    { 22, 3, "Aydınlatma", "aydinlatma" },
                    { 23, 3, "Ev Tekstili", "ev-tekstili" },
                    { 24, 3, "Yatak", "yatak" },
                    { 25, 3, "Ev Dekorasyon", "ev-dekorasyon" },
                    { 26, 3, "Ofis Mobilyaları", "ofis-mobilyalari" },
                    { 27, 3, "Ofis / Kırtasiye", "ofis-kirtasiye" },
                    { 28, 3, "Banyo & Mutfak", "banyo-mutfak" },
                    { 29, 3, "Elektrikli Ev Aletleri", "elektrikli-ev-aletleri" },
                    { 30, 3, "Yurt Dışından", "yurt-disindan" },
                    { 31, 4, "Yapı Market", "yapi-market" },
                    { 32, 4, "Hırdavat", "hirdavat" },
                    { 33, 4, "İş Güvenliği", "is-guvenligi" },
                    { 34, 4, "Yurt Dışından", "yurt-disindan" },
                    { 35, 4, "Banyo & Mutfak", "banyo-mutfak" },
                    { 36, 4, "Elektrik & Tesisat", "elektrik-tesisat" },
                    { 37, 4, "Bahçe", "bahce" },
                    { 38, 4, "Oto Aksesuar", "oto-aksesuar" },
                    { 39, 4, "Tüm Motosiklet Ürünleri", "motosiklet-urunleri" },
                    { 40, 5, "Anne Bebek Ürünleri", "anne-bebek-urunleri" },
                    { 41, 5, "Oyuncaklar", "oyuncaklar" },
                    { 42, 5, "Araç & Gereç", "arac-gerec" },
                    { 43, 5, "Emzirme & Bebek Beslenme", "emzirme-bebek" },
                    { 44, 5, "Bebek Odası & Güvenlik", "bebek-odasi-guvenlik" },
                    { 45, 5, "Bebek Bezi & Islak Mendil", "bebek-bezi-islak-mendil" },
                    { 46, 5, "Bebek Bakım & Banyo & Sağlık", "bebek-bakim-banyo-saglik" },
                    { 47, 5, "Giyim", "giyim" },
                    { 48, 5, "Yurt Dışından", "yurt-disindan" },
                    { 49, 6, "Tüm Spor Ürünleri", "spor-urunleri" },
                    { 50, 6, "Tüm Outdoor Ürünleri", "outdoor-urunleri" },
                    { 51, 6, "Spor Giyim - Ayakkabı", "spor-giyim-ayakkabi" },
                    { 52, 6, "Outdoor Giyim - Ayakkabı", "outdoor-giyim-ayakkabi" },
                    { 53, 6, "Fitness - Kondisyon", "fitness-kondisyon" },
                    { 54, 6, "Elektrikli Scooter - Paten - Kaykay", "elektrikli-scooter-paten-kaykay" },
                    { 55, 6, "Spor Branşları", "spor-branslari" },
                    { 56, 6, "Bisiklet", "bisiklet" },
                    { 57, 6, "Taraftar Ürünleri", "taraftar-urunleri" },
                    { 58, 6, "Kamp & Kampçılık Malzemeleri", "kamp-kampcilik" },
                    { 59, 6, "Şişme Su Ürünleri", "sisme-su-urunleri" },
                    { 60, 6, "Balıkçılık - Avcılık", "balikcilik-avcilik" },
                    { 61, 6, "Aksiyon Kamera", "aksiyon-kamera" },
                    { 62, 6, "Outdoor Elektronik & Optik", "outdoor-elektronik-optik" },
                    { 63, 6, "Tekne Malzemeleri", "tekne-malzemeleri" },
                    { 64, 6, "Doğa Sporları", "doga-sporları" },
                    { 65, 6, "Yurt Dışından", "yurt-disindan" },
                    { 66, 7, "Kozmetik", "kozmetik" },
                    { 67, 7, "Parfüm", "parfum" },
                    { 68, 7, "Makyaj", "makyaj" },
                    { 69, 7, "Cilt Bakımı", "cilt-bakimi" },
                    { 70, 7, "Güneş Bakım", "gunes-bakim" },
                    { 71, 7, "Kişisel Bakım", "kisisel-bakim" },
                    { 72, 7, "Ağız Bakım", "agiz-bakim" },
                    { 73, 7, "Tıraş Ürünleri", "tiras-urunleri" },
                    { 74, 7, "Duş Jeli", "dus-jeli" },
                    { 75, 7, "Saç Bakımı", "sac-bakimi" },
                    { 76, 7, "Epilasyon & Ağda", "epilasyon-agda" },
                    { 77, 7, "Aile Planlaması ve Cinsel Sağlık Ürünleri", "aile-planlamasi" },
                    { 78, 7, "Deodorant", "deodorant" },
                    { 79, 7, "Sıvı Sabun", "sivi-sabun" },
                    { 80, 7, "Kolonya", "kolonya" },
                    { 81, 7, "El Dezenfektanı", "el-dezenfektani" },
                    { 82, 7, "Maske & Eldiven", "maske-eldiven" },
                    { 83, 7, "Besin Takviyeleri", "besin-takviyeleri" },
                    { 84, 7, "Yurt Dışından", "yurt-disindan" },
                    { 85, 8, "Süpermarket Anasayfa", "supermarket-anasayfa" },
                    { 86, 8, "Deterjan & Temizlik Ürünleri", "deterjan-temizlik-urunleri" },
                    { 87, 8, "Bebek Bezleri ve Islak Mendiller", "bebek-bezleri-islak-mendiller" },
                    { 88, 8, "Kağıt Ürünleri", "kagit-urunleri" },
                    { 89, 8, "İçecekler", "icecekler" },
                    { 90, 8, "Gıda Ürünleri", "gida-urunleri" },
                    { 91, 8, "Petshop", "petshop" },
                    { 92, 8, "Ev Tüketim Malzemeleri", "ev-tuketim-malzemeleri" },
                    { 93, 8, "Ofis Tüketim Malzemeleri", "ofis-tuketim-malzemeleri" },
                    { 94, 8, "Yurt Dışından", "yurt-disindan" },
                    { 95, 9, "Kitap & Dergi", "kitap-dergi" },
                    { 96, 9, "Müzik Enstrümanları ve Ekipmanları", "muzik-enstrumanlari-ekipmanlari" },
                    { 97, 9, "Drone Multikopter", "drone-multikopter" },
                    { 98, 9, "Hobi & Oyun", "hobi-oyun" },
                    { 99, 9, "Film", "film" },
                    { 100, 9, "Yurt Dışından", "yurt-disindan" },
                    { 101, 9, "Müzik (Medya)", "muzik" },
                    { 102, 9, "Dijital Ürünler", "dijital-urunler" },
                    { 103, 9, "Ön Ödemeli Kart", "on-odemeli-Kart" }
                });

            migrationBuilder.InsertData(
                table: "BrandSubCategories",
                columns: new[] { "Id", "BrandId", "SubCategoryId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 1, 5 },
                    { 6, 1, 6 },
                    { 7, 1, 7 },
                    { 8, 1, 8 },
                    { 9, 1, 9 },
                    { 10, 2, 1 },
                    { 11, 2, 2 },
                    { 12, 2, 3 },
                    { 13, 2, 4 },
                    { 14, 2, 5 },
                    { 15, 2, 6 },
                    { 16, 2, 7 },
                    { 17, 2, 8 },
                    { 18, 2, 9 },
                    { 19, 3, 11 },
                    { 20, 3, 13 },
                    { 21, 4, 11 },
                    { 22, 4, 13 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrandId", "Description", "Name", "Price", "SubCategoryId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "Samsung Galaxy S24", "Samsung Galaxy S24", 68000.75f, 3, "samsung-galaxy-s24" },
                    { 2, 1, "Samsung Galaxy S24 Ultra", "Samsung Galaxy S24 Ultra", 40000f, 3, "samsung-galaxy-s24-ultra" },
                    { 3, 2, "Iphone 15 Pro Max", "Iphone 15 Pro Max", 86700f, 3, "iphone-15-pro-max" },
                    { 4, 2, "Iphone 15", "Iphone 15", 62074f, 3, "iphone-15" },
                    { 5, 3, "Casio Edifice EQS-920DB-2AVUDF Erkek Kol Saati", "Casio Edifice EQS-920DB-2AVUDF Erkek Kol Saati", 4899f, 13, "casio-edifice-eqs-320db-2avudf-erkek-kol-saati" },
                    { 6, 4, "Wainer Wa.19672-F Erkek Kol Saati", "Wainer Wa.19672-F Erkek Kol Saati", 12027f, 13, "wainer-wa.19672-f-erkek-kol-saati" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_BrandId",
                table: "Baskets",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSubCategories_BrandId",
                table: "BrandSubCategories",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandSubCategories_SubCategoryId",
                table: "BrandSubCategories",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "BrandSubCategories");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
