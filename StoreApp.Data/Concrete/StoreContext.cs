using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Common;

namespace StoreApp.Data.Concrete
{
    public class StoreContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public StoreContext(DbContextOptions<StoreContext> context):base(context) 
        {

        }

        public DbSet<Product> Products => Set<Product>();

        public DbSet<BasketItem> Baskets => Set<BasketItem>();

        public DbSet<OrderItem> Orders => Set<OrderItem>();

        public DbSet<ProductImage> ProductImages => Set<ProductImage>();

        public DbSet<Brand> Brands => Set<Brand>();

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<SubCategory> SubCategories => Set<SubCategory>();

        public DbSet<BrandSubCategory> BrandSubCategories => Set<BrandSubCategory>();

        public DbSet<Comment> Comments => Set<Comment>();

        public DbSet<City> Cities => Set<City>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new List<Product>
            {
                new() { Id = 1, Name = "Samsung Galaxy S24", Description = "Samsung Galaxy S24", Price = 68000.75f, Url = "samsung-galaxy-s24", SubCategoryId = 3, BrandId = 1 },
                new() { Id = 2, Name = "Samsung Galaxy S24 Ultra", Description = "Samsung Galaxy S24 Ultra", Price = 40000, Url = "samsung-galaxy-s24-ultra", SubCategoryId = 3, BrandId = 1 },
                new() { Id = 3, Name = "Iphone 15 Pro Max", Description = "Iphone 15 Pro Max", Price = 86700, Url = "iphone-15-pro-max", SubCategoryId = 3, BrandId = 2 },
                new() { Id = 4, Name = "Iphone 15", Description = "Iphone 15", Price = 62074, Url = "iphone-15", SubCategoryId = 3, BrandId = 2 },
                new() { Id = 5, Name = "Casio Edifice EQS-920DB-2AVUDF Erkek Kol Saati", Description = "Casio Edifice EQS-920DB-2AVUDF Erkek Kol Saati", Url = "casio-edifice-eqs-320db-2avudf-erkek-kol-saati", Price = 4899, SubCategoryId = 13, BrandId = 3 },
                new() { Id = 6, Name = "Wainer Wa.19672-F Erkek Kol Saati", Description = "Wainer Wa.19672-F Erkek Kol Saati", Price = 12027, Url = "wainer-wa.19672-f-erkek-kol-saati", SubCategoryId = 13, BrandId = 4 }
            });
            modelBuilder.Entity<Brand>().HasData(new List<Brand>
            {
                new() { Id = 1, Name = "Samsung" },
                new() { Id = 2, Name = "Apple" },
                new() { Id = 3, Name = "Casio" },
                new() { Id = 4, Name = "Wainer" }
            });
            modelBuilder.Entity<Category>().HasData(new List<Category>
            {
                new() { Id = 1, Name = "Elektronik" },
                new() { Id = 2, Name = "Moda" },
                new() { Id = 3, Name = "Ev, Yaşam, Kırtasiye, Ofis" },
                new() { Id = 4, Name = "Oto, Bahçe, Yapı Market" },
                new() { Id = 5, Name = "Anne, Bebek, Oyuncak" },
                new() { Id = 6, Name = "Spor, Outdoor" },
                new() { Id = 7, Name = "Kozmetik, Kişisel Bakım" },
                new() { Id = 8, Name = "Süpermarket, Pet Shop" },
                new() { Id = 9, Name = "Kitap, Müzik, Film, Hobi" }
            });
            modelBuilder.Entity<SubCategory>().HasData(new List<SubCategory>
            {
                new() { Id = 1, Name = "Bilgisayar/Tablet", Url = "bilgisayar-tablet", CategoryId = 1 },
                new() { Id = 2, Name = "Yazıcılar & Projeksiyon", Url = "yazicilar-projeksiyon", CategoryId = 1 },
                new() { Id = 3, Name = "Telefon & Telefon Aksesuarları", Url = "telefon", CategoryId = 1 },
                new() { Id = 4, Name = "TV, Görüntü & Ses Sistemleri", Url = "tv-goruntu", CategoryId = 1 },
                new() { Id = 5, Name = "Beyaz Eşya", Url = "beyaz-esya", CategoryId = 1 },
                new() { Id = 6, Name = "Klima ve Isıtıcılar", Url = "klima-isiticilar", CategoryId = 1 },
                new() { Id = 7, Name = "Elektrikli Ev Aletleri", Url = "elektrikli-ev-aletleri", CategoryId = 1 },
                new() { Id = 8, Name = "Foto & Kamera", Url = "foto-kamera", CategoryId = 1 },
                new() { Id = 9, Name = "Oyun & Oyun Konsolları", Url = "oyun", CategoryId = 1 },
                new() { Id = 10, Name = "Kadın Giyim", Url = "kadin-giyim", CategoryId = 2 },
                new() { Id = 11, Name = "Kadın Aksesuar & Takı", Url = "kadin-aksesuar", CategoryId = 2 },
                new() { Id = 12, Name = "Erkek Giyim", Url = "erkek-giyim", CategoryId = 2 },
                new() { Id = 13, Name = "Erkek Aksesuar & Takı", Url = "erkek-aksesuar", CategoryId = 2 },
                new() { Id = 14, Name = "Altın", Url = "altin", CategoryId = 2 },
                new() { Id = 15, Name = "Outdoor Giyim & Ayakkabı", Url = "outdoor-giyim", CategoryId = 2 },
                new() { Id = 16, Name = "Ayakkabı & Çanta", Url = "ayakkabi-canta", CategoryId = 2 },
                new() { Id = 17, Name = "Çocuk", Url = "cocuk", CategoryId = 2 },
                new() { Id = 18, Name = "Yurt Dışından", Url = "yurt-disindan", CategoryId = 2 },
                new() { Id = 19, Name = "Sofra & Mutfak", Url = "sofra-mutfak", CategoryId = 3 },
                new() { Id = 20, Name = "Ev Gereçleri & Ütü Masaları", Url = "ev-gerecleri", CategoryId = 3 },
                new() { Id = 21, Name = "Mobilya", Url = "mobilya", CategoryId = 3 },
                new() { Id = 22, Name = "Aydınlatma", Url = "aydinlatma", CategoryId = 3 },
                new() { Id = 23, Name = "Ev Tekstili", Url = "ev-tekstili", CategoryId = 3 },
                new() { Id = 24, Name = "Yatak", Url = "yatak", CategoryId = 3 },
                new() { Id = 25, Name = "Ev Dekorasyon", Url = "ev-dekorasyon", CategoryId = 3 },
                new() { Id = 26, Name = "Ofis Mobilyaları", Url = "ofis-mobilyalari", CategoryId = 3 },
                new() { Id = 27, Name = "Ofis / Kırtasiye", Url = "ofis-kirtasiye", CategoryId = 3 },
                new() { Id = 28, Name = "Banyo & Mutfak", Url = "banyo-mutfak", CategoryId = 3 },
                new() { Id = 29, Name = "Elektrikli Ev Aletleri", Url = "elektrikli-ev-aletleri", CategoryId = 3 },
                new() { Id = 30, Name = "Yurt Dışından", Url = "yurt-disindan", CategoryId = 3 },
                new() { Id = 31, Name = "Yapı Market", Url = "yapi-market", CategoryId = 4 },
                new() { Id = 32, Name = "Hırdavat", Url = "hirdavat", CategoryId = 4 },
                new() { Id = 33, Name = "İş Güvenliği", Url = "is-guvenligi", CategoryId = 4 },
                new() { Id = 34, Name = "Yurt Dışından", Url = "yurt-disindan", CategoryId = 4 },
                new() { Id = 35, Name = "Banyo & Mutfak", Url = "banyo-mutfak", CategoryId = 4 },
                new() { Id = 36, Name = "Elektrik & Tesisat", Url = "elektrik-tesisat", CategoryId = 4 },
                new() { Id = 37, Name = "Bahçe", Url = "bahce", CategoryId = 4 },
                new() { Id = 38, Name = "Oto Aksesuar", Url = "oto-aksesuar", CategoryId = 4 },
                new() { Id = 39, Name = "Tüm Motosiklet Ürünleri", Url = "motosiklet-urunleri", CategoryId = 4 },
                new() { Id = 40, Name = "Anne Bebek Ürünleri", Url = "anne-bebek-urunleri", CategoryId = 5 },
                new() { Id = 41, Name = "Oyuncaklar", Url = "oyuncaklar", CategoryId = 5 },
                new() { Id = 42, Name = "Araç & Gereç", Url = "arac-gerec", CategoryId = 5 },
                new() { Id = 43, Name = "Emzirme & Bebek Beslenme", Url = "emzirme-bebek", CategoryId = 5 },
                new() { Id = 44, Name = "Bebek Odası & Güvenlik", Url = "bebek-odasi-guvenlik", CategoryId = 5 },
                new() { Id = 45, Name = "Bebek Bezi & Islak Mendil", Url = "bebek-bezi-islak-mendil", CategoryId = 5 },
                new() { Id = 46, Name = "Bebek Bakım & Banyo & Sağlık", Url = "bebek-bakim-banyo-saglik", CategoryId = 5 },
                new() { Id = 47, Name = "Giyim", Url = "giyim", CategoryId = 5 },
                new() { Id = 48, Name = "Yurt Dışından", Url = "yurt-disindan", CategoryId = 5 },
                new() { Id = 49, Name = "Tüm Spor Ürünleri", Url = "spor-urunleri", CategoryId = 6 },
                new() { Id = 50, Name = "Tüm Outdoor Ürünleri", Url = "outdoor-urunleri", CategoryId = 6 },
                new() { Id = 51, Name = "Spor Giyim - Ayakkabı", Url = "spor-giyim-ayakkabi", CategoryId = 6 },
                new() { Id = 52, Name = "Outdoor Giyim - Ayakkabı", Url = "outdoor-giyim-ayakkabi", CategoryId = 6 },
                new() { Id = 53, Name = "Fitness - Kondisyon", Url = "fitness-kondisyon", CategoryId = 6 },
                new() { Id = 54, Name = "Elektrikli Scooter - Paten - Kaykay", Url = "elektrikli-scooter-paten-kaykay", CategoryId = 6 },
                new() { Id = 55, Name = "Spor Branşları", Url = "spor-branslari", CategoryId = 6 },
                new() { Id = 56, Name = "Bisiklet", Url = "bisiklet", CategoryId = 6 },
                new() { Id = 57, Name = "Taraftar Ürünleri", Url = "taraftar-urunleri", CategoryId = 6 },
                new() { Id = 58, Name = "Kamp & Kampçılık Malzemeleri", Url = "kamp-kampcilik", CategoryId = 6 },
                new() { Id = 59, Name = "Şişme Su Ürünleri", Url = "sisme-su-urunleri", CategoryId = 6 },
                new() { Id = 60, Name = "Balıkçılık - Avcılık", Url = "balikcilik-avcilik", CategoryId = 6 },
                new() { Id = 61, Name = "Aksiyon Kamera", Url = "aksiyon-kamera", CategoryId = 6 },
                new() { Id = 62, Name = "Outdoor Elektronik & Optik", Url = "outdoor-elektronik-optik", CategoryId = 6 },
                new() { Id = 63, Name = "Tekne Malzemeleri", Url = "tekne-malzemeleri", CategoryId = 6 },
                new() { Id = 64, Name = "Doğa Sporları", Url = "doga-sporları", CategoryId = 6 },
                new() { Id = 65, Name = "Yurt Dışından", Url = "yurt-disindan", CategoryId = 6 },
                new() { Id = 66, Name = "Kozmetik", Url = "kozmetik", CategoryId = 7 },
                new() { Id = 67, Name = "Parfüm", Url = "parfum", CategoryId = 7 },
                new() { Id = 68, Name = "Makyaj", Url = "makyaj", CategoryId = 7 },
                new() { Id = 69, Name = "Cilt Bakımı", Url = "cilt-bakimi", CategoryId = 7 },
                new() { Id = 70, Name = "Güneş Bakım", Url = "gunes-bakim", CategoryId = 7 },
                new() { Id = 71, Name = "Kişisel Bakım", Url = "kisisel-bakim", CategoryId = 7 },
                new() { Id = 72, Name = "Ağız Bakım", Url = "agiz-bakim", CategoryId = 7 },
                new() { Id = 73, Name = "Tıraş Ürünleri", Url = "tiras-urunleri", CategoryId = 7 },
                new() { Id = 74, Name = "Duş Jeli", Url = "dus-jeli", CategoryId = 7 },
                new() { Id = 75, Name = "Saç Bakımı", Url = "sac-bakimi", CategoryId = 7 },
                new() { Id = 76, Name = "Epilasyon & Ağda", Url = "epilasyon-agda", CategoryId = 7 },
                new() { Id = 77, Name = "Aile Planlaması ve Cinsel Sağlık Ürünleri", Url = "aile-planlamasi", CategoryId = 7 },
                new() { Id = 78, Name = "Deodorant", Url = "deodorant", CategoryId = 7 },
                new() { Id = 79, Name = "Sıvı Sabun", Url = "sivi-sabun", CategoryId = 7 },
                new() { Id = 80, Name = "Kolonya", Url = "kolonya", CategoryId = 7 },
                new() { Id = 81, Name = "El Dezenfektanı", Url = "el-dezenfektani", CategoryId = 7 },
                new() { Id = 82, Name = "Maske & Eldiven", Url = "maske-eldiven", CategoryId = 7 },
                new() { Id = 83, Name = "Besin Takviyeleri", Url = "besin-takviyeleri", CategoryId = 7 },
                new() { Id = 84, Name = "Yurt Dışından", Url = "yurt-disindan", CategoryId = 7 },
                new() { Id = 85, Name = "Süpermarket Anasayfa", Url = "supermarket-anasayfa", CategoryId = 8 },
                new() { Id = 86, Name = "Deterjan & Temizlik Ürünleri", Url = "deterjan-temizlik-urunleri", CategoryId = 8 },
                new() { Id = 87, Name = "Bebek Bezleri ve Islak Mendiller", Url = "bebek-bezleri-islak-mendiller", CategoryId = 8 },
                new() { Id = 88, Name = "Kağıt Ürünleri", Url = "kagit-urunleri", CategoryId = 8 },
                new() { Id = 89, Name = "İçecekler", Url = "icecekler", CategoryId = 8 },
                new() { Id = 90, Name = "Gıda Ürünleri", Url = "gida-urunleri", CategoryId = 8 },
                new() { Id = 91, Name = "Petshop", Url = "petshop", CategoryId = 8 },
                new() { Id = 92, Name = "Ev Tüketim Malzemeleri", Url = "ev-tuketim-malzemeleri", CategoryId = 8 },
                new() { Id = 93, Name = "Ofis Tüketim Malzemeleri", Url = "ofis-tuketim-malzemeleri", CategoryId = 8 },
                new() { Id = 94, Name = "Yurt Dışından", Url = "yurt-disindan", CategoryId = 8 },
                new() { Id = 95, Name = "Kitap & Dergi", Url = "kitap-dergi", CategoryId = 9 },
                new() { Id = 96, Name = "Müzik Enstrümanları ve Ekipmanları", Url = "muzik-enstrumanlari-ekipmanlari", CategoryId = 9 },
                new() { Id = 97, Name = "Drone Multikopter", Url = "drone-multikopter", CategoryId = 9 },
                new() { Id = 98, Name = "Hobi & Oyun", Url = "hobi-oyun", CategoryId = 9 },
                new() { Id = 99, Name = "Film", Url = "film", CategoryId = 9 },
                new() { Id = 100, Name = "Yurt Dışından", Url = "yurt-disindan", CategoryId = 9 },
                new() { Id = 101, Name = "Müzik (Medya)", Url = "muzik", CategoryId = 9 },
                new() { Id = 102, Name = "Dijital Ürünler", Url = "dijital-urunler", CategoryId = 9 },
                new() { Id = 103, Name = "Ön Ödemeli Kart", Url = "on-odemeli-Kart", CategoryId = 9 },
            });
            modelBuilder.Entity<BrandSubCategory>().HasData(new List<BrandSubCategory>
            {
                new() { Id = 1, BrandId = 1, SubCategoryId = 1 },
                new() { Id = 2, BrandId = 1, SubCategoryId = 2 },
                new() { Id = 3, BrandId = 1, SubCategoryId = 3 },
                new() { Id = 4, BrandId = 1, SubCategoryId = 4 },
                new() { Id = 5, BrandId = 1, SubCategoryId = 5 },
                new() { Id = 6, BrandId = 1, SubCategoryId = 6 },
                new() { Id = 7, BrandId = 1, SubCategoryId = 7 },
                new() { Id = 8, BrandId = 1, SubCategoryId = 8 },
                new() { Id = 9, BrandId = 1, SubCategoryId = 9 },
                new() { Id = 10, BrandId = 2, SubCategoryId = 1 },
                new() { Id = 11, BrandId = 2, SubCategoryId = 2 },
                new() { Id = 12, BrandId = 2, SubCategoryId = 3 },
                new() { Id = 13, BrandId = 2, SubCategoryId = 4 },
                new() { Id = 14, BrandId = 2, SubCategoryId = 5 },
                new() { Id = 15, BrandId = 2, SubCategoryId = 6 },
                new() { Id = 16, BrandId = 2, SubCategoryId = 7 },
                new() { Id = 17, BrandId = 2, SubCategoryId = 8 },
                new() { Id = 18, BrandId = 2, SubCategoryId = 9 },
                new() { Id = 19, BrandId = 3, SubCategoryId = 11 },
                new() { Id = 20, BrandId = 3, SubCategoryId = 13 },
                new() { Id = 21, BrandId = 4, SubCategoryId = 11 },
                new() { Id = 22, BrandId = 4, SubCategoryId = 13 },
            });
        }
    }
}
