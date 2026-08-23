## Traffic Penalty Management

.NET 9 ve ASP.NET Core MVC kullanýlarak geliþtirilmiþ, trafik cezalarýnýn oluþturulmasý, takip edilmesi ve rol bazlý onay süreçlerinin yönetilmesini saðlayan Trafik Cezasý Yönetim ve Onay Uygulamasý.

## Proje Özellikleri

## Araç Yönetimi 
Araçlarýn plaka, tip, marka ve model bilgileriyle sisteme kaydedilmesini ve listelenmesini saðlar.

## Rol Bazlý Onay Süreci
Trafik cezalarý, kullanýcý rollerine göre belirlenen aþaðýdaki iþ akýþýndan geçer: New -> Manager Approval -> FinanceApproval -> Completed

Yetkili kullanýcýlar kendi sorumluluklarýndaki aþamalarda cezalarý onaylayabilir. Uygun aþamalarda ceza reddedilebilir ve ret nedeni zorunlu olarak kayýt altýna alýnýr.

## Onay Geçmiþi

Cezalar üzerinde gerçekleþtirilen tüm onay ve ret iþlemleri kayýt altýna alýnýr.

Onay geçmiþinde aþaðýdaki bilgiler görüntülenebilir:

Ýþlemi gerçekleþtiren kullanýcý, Ýþlem tarihi, Ýþlem tip, Ret nedeni, Önceki durum, Yeni durum

## Kullanýlan Teknolojiler

.NET 9
ASP.NET Core MVC
Entity Framework Core
SQL Server
ASP.NET Core Identity
AutoMapper
Bootstrap
Git

## Proje Mimarisi

Proje, katmanlý mimari yaklaþýmý kullanýlarak geliþtirilmiþtir.

TrafficPenaltyManagement
|
+-- Domain
|   +-- Entities
|   +-- Enums
|
+-- Application
|   +-- DTOs
|   +-- Interfaces
|   +-- Services
|   +-- Mapping
|
+-- Infrastructure
|   +-- Identity
|   +-- Repositories
|   +-- Data
|
+-- WebUI
    +-- Controllers
    +-- Views
    +-- wwwroot


## Katmanlarýn Sorumluluklarý
Domain: Entity ve enum gibi temel iþ modellerini içerir.
Application: DTO, interface, servis ve iþ kurallarýný içerir.
Infrastructure: Entity Framework Core, SQL Server, Identity ve repository iþlemlerini içerir.
WebUI: MVC Controller ve View katmanýný içerir.

Kurulum ve Çalýþtýrma
Gereksinimler
.NET 9 SDK
Microsoft SQL Server
Visual Studio 2022
Git

## 1. Projeyi Klonlama
git clone <REPOSITORY_URL>
cd TrafficPenaltyManagement

## 2. Veritabaný Baðlantýsýný Yapýlandýrma

TrafficPenaltyManagement.WebUI/appsettings.json içerisindeki connection string'i kendi SQL Server ortamýnýza göre düzenleyin.

Örnek:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TrafficPenaltyManagementDb;Uid=sa;Pwd=SIFRENIZ;TrustServerCertificate=True;"
}


## 3. Veritabanýný Oluþturma

Visual Studio içerisinde Tools, NuGet Package Manager ve Package Manager Console kullanýlarak mevcut migration'lar veritabanýna uygulanabilir:

Update-Database : Yeni bir entity veya veritabaný deðiþikliði yapýlmasý durumunda yeni migration oluþturulabilir:

Add-Migration MigrationName
Update-Database

## 4. Projeyi Baþlatma

TrafficPenaltyManagement.WebUI projesini baþlangýç projesi olarak seçin ve F5 tuþuna basarak uygulamayý çalýþtýrýn.



## Ýlk Giriþ ve Test Akýþý
Uygulama çalýþtýrýldýktan sonra /Register/Index adresinden yeni bir kullanýcý oluþturun.
/Login/Index üzerinden sisteme giriþ yapýn.
Onay sürecini test etmek için gerekli rollere sahip kullanýcýlar oluþturun.
Bir araç oluþturun.
Oluþturulan araç üzerinden yeni bir trafik cezasý oluþturun.
Cezanýn rol bazlý onay sürecini takip edin.


## Ekran Görüntüleri

### Login

![Login](images/login.png)

### Araç Yönetimi

![Araç Yönetimi](images/vehicles.png)

### Ceza Listesi

![Ceza Listesi](images/penalties.png)

### Onay Geçmiþi

![Onay Geçmiþi](images/history.png)


