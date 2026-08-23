raffic Penalty Management

.NET 9 ve ASP.NET Core MVC kullanılarak geliştirilmiş, trafik cezalarının oluşturulması, takip edilmesi ve rol bazlı onay süreçlerinin yönetilmesini sağlayan Trafik Cezası Yönetim ve Onay Uygulaması.

📌 Proje Özellikleri
🚗 Araç Yönetimi

Araçların plaka, tip, marka ve model bilgileriyle sisteme kaydedilmesini ve listelenmesini sağlar.

🔐 Rol Bazlı Onay Süreci

Trafik cezaları, kullanıcı rollerine göre belirlenen aşağıdaki iş akışından geçer:

New → Manager Approval → Finance Approval → Completed

Yetkili kullanıcılar kendi sorumluluklarındaki aşamalarda cezaları onaylayabilir. Uygun aşamalarda ceza reddedilebilir ve ret nedeni zorunlu olarak kayıt altına alınır.

📋 Onay Geçmişi

Cezalar üzerinde gerçekleştirilen tüm onay ve ret işlemleri kayıt altına alınır.

Onay geçmişinde aşağıdaki bilgiler görüntülenebilir:

İşlemi gerçekleştiren kullanıcı
İşlem tarihi
İşlem tipi
Ret nedeni
Önceki durum
Yeni durum
🛠️ Kullanılan Teknolojiler
.NET 9
ASP.NET Core MVC
Entity Framework Core
SQL Server
ASP.NET Core Identity
AutoMapper
Bootstrap
Git
🏗️ Proje Mimarisi

Proje, katmanlı mimari yaklaşımı kullanılarak geliştirilmiştir.

TrafficPenaltyManagement
│
├── Domain
│   ├── Entities
│   └── Enums
│
├── Application
│   ├── DTOs
│   ├── Interfaces
│   ├── Services
│   └── Mapping
│
├── Infrastructure
│   ├── Identity
│   ├── Repositories
│   └── Data
│
└── WebUI
    ├── Controllers
    ├── Views
    └── wwwroot
📦 Katmanların Sorumlulukları

Domain: Entity ve enum gibi temel iş modellerini içerir.

Application: DTO, interface, servis ve iş kurallarını içerir.

Infrastructure: Entity Framework Core, SQL Server, Identity ve repository işlemlerini içerir.

WebUI: MVC Controller ve View katmanını içerir.

🚀 Kurulum ve Çalıştırma
Gereksinimler
.NET 9 SDK
Microsoft SQL Server
Visual Studio 2022
Git
1. Projeyi Klonlama
git clone <REPOSITORY_URL>
cd TrafficPenaltyManagement
2. Veritabanı Bağlantısını Yapılandırma

TrafficPenaltyManagement.WebUI/appsettings.json içerisindeki connection string'i kendi SQL Server ortamınıza göre düzenleyin.

Örnek:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TrafficPenaltyManagementDb;Uid=sa;Pwd=SIFRENIZ;TrustServerCertificate=True;"
}
3. Veritabanını Oluşturma

Visual Studio içerisinde:

Tools → NuGet Package Manager → Package Manager Console

kullanılarak mevcut migration'lar veritabanına uygulanabilir:

Update-Database

Yeni bir entity veya veritabanı değişikliği yapılması durumunda yeni migration oluşturulabilir:

Add-Migration MigrationName
Update-Database
4. Projeyi Başlatma

TrafficPenaltyManagement.WebUI projesini başlangıç projesi olarak seçin ve F5 tuşuna basarak uygulamayı çalıştırın.

🧪 İlk Giriş ve Test Akışı

Uygulama çalıştırıldıktan sonra:

/Register/Index adresinden yeni bir kullanıcı oluşturun.
/Login/Index üzerinden sisteme giriş yapın.
Onay sürecini test etmek için gerekli rollere sahip kullanıcılar oluşturun.
Bir araç oluşturun.
Oluşturulan araç üzerinden yeni bir trafik cezası oluşturun.
Cezanın rol bazlı onay sürecini takip edin.

## Ekran Görüntüleri

### Login

<img width="1914" height="774" alt="Image" src="https://github.com/user-attachments/assets/70420516-6f33-48f8-8b0c-0190b8894593" />

### Araç Yönetimi

![Araç Yönetimi](images/vehicles.png)

### Ceza Listesi

![Ceza Listesi](images/penalties.png)

### Onay Geçmişi

![Onay Geçmişi](images/history.png)


