# ⚽ Serie A Football

İtalya Serie A ligi için hazırlanmış bir **.NET 8** bootcamp case projesi. Proje iki uygulamadan oluşur. **Web API** verileri SQL Server'da tutar ve iş kurallarını uygular. **MVC arayüzü** bu API'yi `HttpClient` ile kullanarak puan durumunu, fikstürü ve maç detaylarını gösterir. Arayüzde ayrıca maçların yönetildiği bir admin paneli bulunur.

> Arayüz veritabanına hiçbir zaman doğrudan erişmez. Bütün veriler API üzerinden gelir.

![Puan durumu]
<p align="center">
  <img src="https://github.com/user-attachments/assets/015312e0-13a6-487b-87ed-8c96b53206f9" width="100%" alt="Puan durumu" />
  <br/><sub><b>Puan Durumu</b></sub>
</p>



---

## İçindekiler

- [Özellikler](#özellikler)
- [Ekran Görüntüleri](#ekran-görüntüleri)
- [Mimari](#mimari)
- [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
- [İş Kuralları](#i̇ş-kuralları)
- [API Endpoint'leri](#api-endpointleri)
- [Kurulum](#kurulum)
- [Proje Yapısı](#proje-yapısı)
- [Geliştirme Fikirleri](#geliştirme-fikirleri)

---

## Özellikler

### Genel sayfalar 

**Standings (puan durumu)**
- Tablo, tamamlanan maçlardan anlık olarak hesaplanır. Ayrı bir tabloda saklanmaz.
- Takımlar puan, averaj, atılan gol ve takım adı sırasıyla sıralanır.
- Son 5 maçın formu G/B/M (W/D/L) rozetleriyle gösterilir.
- Şampiyonlar Ligi, Avrupa ve küme düşme bölgeleri renkle işaretlenir.
- Puan dağılımı bir şerit üzerinde gösterilir. Şeritte bir takıma tıklayınca tablodaki satırına gidilir.
- Özet kartları: lider, en golcü takım, en az gol yiyen takım, küme düşme hattı ve formda olan takımlar.
- Bölge filtresi ve takım arama.

**Fixtures (fikstür)**
- Haftalar arasında geçiş yapılır (1–38).
- Maçlar günlere göre gruplanır.
- Haftalık özet: oynanan maç, gol sayısı, maç başı gol ortalaması, galibiyet ve beraberlik dağılımı.
- Öne çıkan maç seçilir: önce canlı maç, yoksa en çok gol atılan maç.

**Match Detail (maç detayı)**
- Skor tabelası ve ilk yarı / ikinci yarı skorları.
- Gol, kart ve oyuncu değişikliklerini gösteren zaman çizelgesi, olay türüne göre filtrelenebilir.
- Maç akış şeridi.
- Takım bazında goller, kartlar ve değişiklikler.

### Admin paneli 

- Çerez tabanlı oturum açma. Şifreler PBKDF2 (SHA-256, 100.000 iterasyon) ile hash'lenir.
- Maç listesi; hafta, takım ve durum filtreleri.
- Maç **ekleme, düzenleme ve silme** işlemleri modallar üzerinden yapılır. API'den gelen hata mesajları formda gösterilir.
- Sadece okunur takım listesi.

---

## Ekran Görüntüleri

### Genel Sayfalar

<table>
  <tr>
    <td width="50%" valign="top" align="center">
      <img src="https://github.com/user-attachments/assets/4c0a8b92-6f51-48a8-81e7-016846824f12" width="100%" alt="Puan durumu: özet kartları ve puan şeridi" />
      <br/><sub><b>Puan Durumu</b> · Özet kartları ve puan şeridi</sub>
    </td>
    <td width="50%" valign="top" align="center">
      <img src="https://github.com/user-attachments/assets/490a6835-e101-4519-9891-3aa7178262c2" width="100%" alt="Puan durumu tablosu ve son 5 maç formu" />
      <br/><sub><b>Puan Tablosu</b> · Bölge filtresi, arama ve son 5 maç formu</sub>
    </td>
  </tr>
  <tr>
    <td width="50%" valign="top" align="center">
      <img src="https://github.com/user-attachments/assets/0bcd3594-94a0-4335-a100-ce24fb58ca3a" width="100%" alt="Fikstür: hafta seçici ve maç kartları" />
      <br/><sub><b>Fikstür</b> · Hafta seçici ve günlere göre maçlar</sub>
    </td>
    <td width="50%" valign="top" align="center">
      <img src="https://github.com/user-attachments/assets/31a27109-96b8-4390-bddc-cb7ad103a68d" width="100%" alt="Fikstür: haftalık özet ve öne çıkan maç" />
      <br/><sub><b>Haftalık Özet</b> · İstatistikler ve öne çıkan maç</sub>
    </td>
  </tr>
  <tr>
    <td width="50%" valign="top" align="center">
      <img src="https://github.com/user-attachments/assets/f80cad06-a432-4a98-9f9b-3a4e9f1450c3" width="100%" alt="Maç detayı: skor tabelası ve maç akışı" />
      <br/><sub><b>Maç Detayı</b> · Skor tabelası ve maç akışı</sub>
    </td>
    <td width="50%" valign="top" align="center">
      <img src="https://github.com/user-attachments/assets/d4e00d14-d060-4bb1-8f59-9e451d004a11" width="100%" alt="Maç detayı: olay zaman çizelgesi" />
      <br/><sub><b>Zaman Çizelgesi</b> · Goller, kartlar ve oyuncu değişiklikleri</sub>
    </td>
  </tr>
</table>

### Admin Paneli

<p align="center">
  <img src="https://github.com/user-attachments/assets/c72bd2c7-8557-496b-bb5f-dcca2b10b235" width="100%" alt="Admin maç listesi" />
  <br/><sub><b>Maç Yönetimi</b> · Hafta, takım ve durum filtreleriyle maç listesi</sub>
</p>

<table>
  <tr>
    <td width="50%" valign="top" align="center">
      <img src="https://github.com/user-attachments/assets/4c9679f0-155a-435c-a70a-41e9aebe6ebd" width="100%" alt="Yeni maç ekleme modalı" />
      <br/><sub><b>Yeni Maç Ekle</b></sub>
    </td>
    <td width="50%" valign="top" align="center">
      <img src="https://github.com/user-attachments/assets/2610c850-da98-497c-8a7b-70eb0c9b1cd6" width="100%" alt="Maç düzenleme modalı" />
      <br/><sub><b>Maçı Düzenle</b> · Skor alanları duruma göre açılır</sub>
    </td>
  </tr>
  <tr>
    <td width="50%" valign="top" align="center">
      <img src="https://github.com/user-attachments/assets/0653ab61-0e16-432d-8aca-5e8b64e1add8" width="100%" alt="Admin giriş ekranı" />
      <br/><sub><b>Giriş</b> · Çerez tabanlı oturum</sub>
    </td>
    <td width="50%" valign="top" align="center">
      <img src="https://github.com/user-attachments/assets/8df9d251-1fac-40b0-acd0-de9892c97c64" width="100%" alt="Maç silme onay modalı" />
      <br/><sub><b>Maçı Sil</b> · Onay penceresi</sub>
    </td>
  </tr>
</table>

<p align="center">
  <img src="https://github.com/user-attachments/assets/15e49c6f-3c0b-4162-8684-54a74b028761" width="100%" alt="Admin takım listesi" />
  <br/><sub><b>Takım Yönetimi</b> · Lig özeti ve takım listesi</sub>
</p>
---

## Mimari

```
┌──────────────────────────┐   HTTP (JSON)   ┌──────────────────────────┐   EF Core   ┌────────────┐
│  SeriA_Football.WebUI    │ ──────────────▶ │  SeriA_Football.API      │ ──────────▶ │ SQL Server │
│  ASP.NET Core MVC        │ ◀────────────── │  ASP.NET Core Web API    │ ◀────────── │            │
│  • Genel sayfalar        │                 │  • Controller'lar + DTO  │             └────────────┘
│  • Admin Area (Cookie)   │                 │  • İş kuralı doğrulama   │
└──────────────────────────┘                 └──────────────────────────┘
```

- **API** entity'leri dışarıya açmaz. Veriyi `Select` ile DTO'lara dönüştürerek döner.
- **WebUI**, API'ye `IHttpClientFactory` ile oluşturulan isimli bir client (`"SerieAApi"`) üzerinden bağlanır. Gelen DTO'lar sayfaya özel ViewModel'lere aktarılır. Hesaplanan değerler (form puanı, bölge, haftalık özet vb.) ViewModel içinde tutulur.
- Arayüz partial view'lar (`_TeamBadge`, `_FixtureCard`, `_StandingsRow` …) ve bir ViewComponent (puan şeridi) ile parçalara bölünmüştür.

---

## Kullanılan Teknolojiler

| Katman | Teknoloji |
|---|---|

| ASP.NET Core Web API - veri erişimi ve REST endpointleri
| ASP.NET Core MVC - kullanıcı arayüzü ve admin paneli
| Entity Framework Core + SQL Server - ilişkisel veri yönetimi
| Bootstrap, HTML, CSS, JavaScript - responsive arayüz ve etkileşimler
| Ai ile örnek veri oluşturma


---

## İş Kuralları

Maç ekleme ve güncelleme isteklerinde API şu kontrolleri yapar:

1. Ev sahibi ve deplasman takımı aynı olamaz. Bu kural veritabanında check constraint olarak da tanımlıdır.
2. İki takımın da veritabanında kayıtlı olması gerekir.
3. Bir takım aynı haftada yalnızca bir maç oynayabilir.
4. **Tamamlandı** durumundaki bir maçın skoru girilmiş olmalıdır.
5. **Planlandı** durumundaki bir maça skor girilemez.

Diğer kurallar:

- Bir takım silinmek istendiğinde, o takıma bağlı maçlar varsa silme işlemi engellenir (`DeleteBehavior.Restrict`).
- Bir maç silindiğinde ona bağlı goller, kartlar ve oyuncu değişiklikleri de silinir (`Cascade`).
- Puan durumu yalnızca **Tamamlandı** durumundaki maçlardan hesaplanır. Galibiyet 3 puan, beraberlik 1 puan, mağlubiyet 0 puandır.

Maç durumları: `Scheduled` (Planlandı), `Live` (Oynanıyor), `Completed` (Tamamlandı), `Postponed` (Ertelendi).

---

## API Endpoint'leri

| Metot | Endpoint | Açıklama |
|---|---|---|
| `GET` | `/api/teams` | Bütün takımlar |
| `GET` | `/api/standings` | Puan durumu |
| `GET` | `/api/matches/week/{week}` | Bir haftanın maçları |
| `GET` | `/api/matches/{id}` | Maç detayı (goller, kartlar, oyuncu değişiklikleri) |
| `POST` | `/api/matches` | Yeni maç ekler → `201 Created` |
| `PUT` | `/api/matches/{id}` | Maçı günceller → `204 No Content` |
| `DELETE` | `/api/matches/{id}` | Maçı siler → `204 No Content` |
| `POST` | `/api/auth/login` | Admin girişi → `200` veya `401` |

Bir iş kuralı ihlal edildiğinde API **400 Bad Request** ve Türkçe bir hata mesajı döner. Kayıt bulunamazsa **404 Not Found** döner.

---

## Kurulum

### Gereksinimler

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (LocalDB ya da Express yeterlidir)
- Visual Studio 2022 veya `dotnet` CLI

### Adımlar

**1. Projeyi klonla**

```bash
git clone https://github.com/<kullanici-adi>/SeriA_Football.git
cd SeriA_Football
```

**2. Bağlantı dizesini ayarla.** `SeriA_Football.API/appsettings.json` dosyasında:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=SeriA_FootballDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

**3. Veritabanını oluştur.** Seed verisi (20 takım, 30 maç ve admin kullanıcısı) migration'larla birlikte yüklenir.

Visual Studio'da Package Manager Console'u aç, varsayılan proje olarak API'yi seç ve şunu çalıştır:

```powershell
Update-Database
```

CLI kullanıyorsan:

```bash
dotnet ef database update --project SeriA_Football.API
```

**4. API adresini kontrol et.** `SeriA_Football.WebUI/appsettings.json` dosyasında:

```json
"ApiSettings": {
  "BaseUrl": "https://localhost:7154/"
}
```

**5. İki projeyi birlikte çalıştır.** Visual Studio'da Solution'a sağ tıkla, *Configure Startup Projects* → *Multiple startup projects* seçeneğini aç, iki projeyi de **Start** yap.

### Admin girişi

| Kullanıcı adı | Şifre |
|---|---|
| `admin` | `Admin123!` |

Giriş sayfası: `/Account/Login`. Admin paneli: `/Admin/Matches`.

> Bu bilgiler demo amaçlı seed verisidir. Gerçek bir ortamda kullanılmamalıdır.

---

## Proje Yapısı

```
SeriA_Football
├── SeriA_Football.API
│   ├── Controllers        # Teams, Matches, Standings, Auth
│   ├── Data               # DbContext, SeedData
│   ├── DTOs
│   ├── Entities           # Team, Match, MatchGoal, MatchCard, Substitution, AdminUser
│   ├── Enums              # MatchStatus, CardType
│   ├── Helpers            # PasswordHelper (PBKDF2)
│   └── Migrations
│
└── SeriA_Football.WebUI
    ├── Areas/Admin        # Maç ve takım yönetimi (Türkçe)
    ├── Controllers        # Standings, Fixtures, Matches, Account
    ├── Helpers            # TeamHelper (arma kısaltma ve renkleri), enum uzantıları
    ├── Models             # Sayfa ViewModel'leri
    ├── ViewComponents
    ├── Views
    └── wwwroot            # site.css, admin script'leri
```

---

## Geliştirme Fikirleri

- [ ] API'nin yazma endpoint'lerini JWT ile korumak. Şu an yalnızca arayüzdeki admin paneli oturum kontrolü yapıyor.
- [ ] Bütün maçlar için gol, kart ve oyuncu değişikliği verisi eklemek. Seed'de yalnızca birkaç maçın detaylı verisi var.
- [ ] Admin panelinden maç olaylarını (gol, kart) yönetmek.
- [ ] Unit ve integration testleri yazmak.

---

## Lisans

Bu proje eğitim amaçlı hazırlanmış bir bootcamp case çalışmasıdır.
