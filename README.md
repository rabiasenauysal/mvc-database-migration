# ASP.NET Core Ürün ve Sipariş Yönetim Sistemi

Bu proje kapsamında ASP.NET Core Web API, Entity Framework Core, LINQ, Lambda ve nesne yönelimli programlama prensipleri kullanılarak ürün, sipariş ve müşteri yönetimi yapan bir backend sistemi geliştirilmiştir. Projede hem DB First hem Code First yaklaşımları uygulanmıştır.

## 🎯 Projenin Amacı

Bu proje ile aşağıdaki yetkinlikler kazanılmıştır:
- ASP.NET Core Web API geliştirme
- Entity Framework Core ile veritabanı işlemleri
- DB First ve Code First yaklaşımlarının uygulanması
- Katmanlı mimari prensiplerinin kullanılması
- LINQ ve Lambda ile veri analizleri yapılması
- OOP prensiplerinin projede uygulanması

## 🧱 Veritabanı Modelleri

### Product (DB First)
- Id (int)
- Name (string)
- Price (decimal)
- Stock (int)

### Order (DB First)
- Id (int)
- ProductId (int)
- Quantity (int)
- OrderDate (DateTime)

### Customer (Code First)
- Id (int)
- Name (string)
- Email (string)

Tüm entityler BaseEntity sınıfından türetilmiştir:
- Id
- CreatedAt
- UpdatedAt

## 🧩 Mimari Yapı

Proje Katmanlı Mimariye uygun olarak geliştirilmiştir:

| Katman | Görev |
|--------|------|
| Controllers | API endpoint’leri |
| Services | Business logic |
| Repositories | Veritabanı CRUD işlemleri |
| Models | Entity tanımları ve DTO’lar |

Repository yapısı interface temelli olup Dependency Injection uygulanmıştır.

## 🔌 API Endpointleri

### Ürün İşlemleri
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | /api/products | Tüm ürünleri listeleme |
| GET | /api/products/{id} | Ürün detaylarını getirme |
| POST | /api/products | Yeni ürün ekleme |
| PUT | /api/products/{id} | Ürün güncelleme |
| DELETE | /api/products/{id} | Ürün silme |

### Sipariş İşlemleri
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | /api/orders | Tüm siparişleri listeleme |
| GET | /api/orders/{id} | Sipariş detaylarını getirme |
| POST | /api/orders | Yeni sipariş ekleme |
| GET | /api/orders/total | Toplam sipariş tutarını hesaplama |

### Müşteri İşlemleri
| Metot | Endpoint | Açıklama |
|-------|----------|----------|
| GET | /api/customers | Tüm müşterileri listeleme |
| POST | /api/customers | Yeni müşteri ekleme |

## 🧮 LINQ ve Lambda Sorguları

- Fiyatı 500 TL üzerindeki ürünleri listeleme
- En çok sipariş edilen ürünü bulma
- Tüm ürünlerin toplam stok miktarını hesaplama
- Belirli bir tarihten sonra verilen siparişleri filtreleme

## 📝 Logger Yapısı

Polimorfizm örneği olarak ILogger interface’i kullanılmıştır:
- FileLogger: Logları dosyaya yazar
- DatabaseLogger: Logları veritabanına yazar

## 🛠️ Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|----------|----------|
| ASP.NET Core 7.0+ | Web API geliştirme |
| Entity Framework Core | ORM |
| SQL Server | Veritabanı |
| C# OOP | Inheritance, Polymorphism, Interfaces |
| LINQ & Lambda | Veri sorgulama |

## 🚀 Kurulum ve Çalıştırma

### 1️⃣ Bağımlılıkların Yüklenmesi
dotnet restore
### 2️⃣ Veritabanı Bağlantı Ayarları
appsettings.json içerisine eklenir:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ProductOrderDb;Trusted_Connection=True;"
}
### 3️⃣ Migration İşlemleri
dotnet ef database update
### 4️⃣ Uygulamayı Başlatma
dotnet run
### ✅ Proje Durumu
Tüm CRUD işlemleri, validasyonlar, migration işlemleri ve LINQ sorguları başarılı şekilde uygulanmıştır.

### 📌 Geliştiren
Bu proje bilgisayar mühendisliği öğrencisi tarafından ders ödevi kapsamında geliştirilmiştir.
