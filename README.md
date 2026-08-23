# 🚗 Traffic Penalty Management

.NET 9 ve ASP.NET Core MVC kullanılarak geliştirilmiş, trafik cezalarının oluşturulması, takip edilmesi ve rol bazlı onay süreçlerinin yönetilmesini sağlayan Trafik Cezası Yönetim ve Onay Uygulaması.

---

## 💡 Proje Özellikleri

### Araç Yönetimi
Araçların plaka, tip, marka ve model bilgileriyle sisteme kaydedilmesini ve listelenmesini sağlar.

### Rol Bazlı Onay Süreci
Trafik cezaları, kullanıcı rollerine göre belirlenen aşağıdaki iş akışından geçer:
`New -> Manager Approval -> FinanceApproval -> Completed`

Yetkili kullanıcılar kendi sorumluluklarındaki aşamalarda cezaları onaylayabilir. Uygun aşamalarda ceza reddedilebilir ve ret nedeni zorunlu olarak kayıt altına alınır.

### Onay Geçmişi
Cezalar üzerinde gerçekleştirilen tüm onay ve ret işlemleri kayıt altına alınır. Onay geçmişinde aşağıdaki bilgiler görüntülenebilir:
* İşlemi gerçekleştiren kullanıcı
* İşlem tarihi
* İşlem tipi
* Ret nedeni
* Önceki durum
* Yeni durum

---

## 🛠️ Kullanılan Teknolojiler
* .NET 9
* ASP.NET Core MVC
* Entity Framework Core
* SQL Server
* ASP.NET Core Identity
* AutoMapper
* Bootstrap
* Git

---

## 📐 Proje Mimarisi

Proje, katmanlı mimari yaklaşımı kullanılarak geliştirilmiştir.

```text
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
```

### Katmanların Sorumlulukları
* **Domain:** Entity ve enum gibi temel iş modellerini içerir.
* **Application:** DTO, interface, servis ve iş kurallarını içerir.
* **Infrastructure:** Entity Framework Core, SQL Server, Identity ve repository işlemlerini içerir.
* **WebUI:** MVC Controller ve View katmanını içerir.

---

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler
* .NET 9 SDK
* Microsoft SQL Server
* Visual Studio 2022
* Git

### 1. Projeyi Klonlama
```bash
git clone <REPOSITORY_URL>
cd TrafficPenaltyManagement
```

### 2. Veritabanı Bağlantısını Yapılandırma
`TrafficPenaltyManagement.WebUI/appsettings.json` içerisindeki connection string'i kendi SQL Server ortamınıza göre düzenleyin.

Örnek:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TrafficPenaltyManagementDb;Uid=sa;Pwd=SIFRENIZ;TrustServerCertificate=True;"
}
```

### 3. Veritabanını Oluşturma
Visual Studio içerisinde **Tools -> NuGet Package Manager -> Package Manager Console** kullanılarak mevcut migration'lar veritabanına uygulanabilir:
```powershell
Update-Database
```

Yeni bir entity veya veritabanı değişikliği yapılması durumunda yeni migration oluşturulabilir:
```powershell
Add-Migration MigrationName
Update-Database
```

### 4. Projeyi Başlatma
`TrafficPenaltyManagement.WebUI` projesini başlangıç projesi olarak seçin ve **F5** tuşuna basarak uygulamayı çalıştırın.

---

## 💻 İlk Giriş ve Test Akışı

1. Uygulama çalıştırıldıktan sonra `/Register/Index` adresinden yeni bir kullanıcı oluşturun.
2. `/Login/Index` üzerinden sisteme giriş yapın.
3. Onay sürecini test etmek için gerekli rollere sahip kullanıcılar oluşturun.
4. Bir araç oluşturun.
5. Oluşturulan araç üzerinden yeni bir trafik cezası oluşturun.
6. Cezanın rol bazlı onay sürecini takip edin.


## Ekran Görüntüleri

### Login

<img width="1912" height="493" alt="Image" src="https://github.com/user-attachments/assets/05c73f6f-0676-426e-a6a9-a195e2b8366b" />

### Araç Yönetimi

<img width="1914" height="774" alt="Image" src="https://github.com/user-attachments/assets/70420516-6f33-48f8-8b0c-0190b8894593" />

### Ceza Listesi

<img width="1918" height="541" alt="Image" src="https://github.com/user-attachments/assets/286ce84e-bc6c-4ae9-bcb9-e240596c6d85" />

### Onay Geçmişi

![Onay Geçmişi](images/history.png)


