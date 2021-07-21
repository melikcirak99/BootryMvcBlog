# BootryMvcBlog
bir blog sitesinde olması gereken tüm özelliklerin eklendiği, admin panel entegrasyonlu bir blog projesidir. bu proje asp.net mvc teknolojisi ile oluşturulmuştur, mssql veritabanı ile bağlanmıştır.

üç tür giriş yöntemi tasarlanmıştır ( /giris-{type} )

1)admin girişi, /giris-admin
  - admin tüm sistem üzerinde değişiklik yetkisine sahiptir, yazarlar, gönderiler, yorumlar ve site ayarlarını manipüle edebilmektedir.

2)yazar girişi, /giris-yazar
  - yazarlar sadece sisteme gönderi ekleyebilmekte ve ekledikleri gönderileri düzenleyebilmektedirler. 

3)kullanıcı girişi, /giris-kullanici
  - kullanıcılar yorum yapabilmek için kayıt olmalıdırlar. kaydolan kullanıcı bilgilerinde güncelleme yapabilir.
    admin paneli

admin panelinin ön yüz tasarımında adminLTE açık kaynak teması entegre edilmiştir.

site yöneticisi sisteme giriş yaptığında site için önemli istatistikleri görsel bir biçimde listelenir.

yönetici, bu kısımdaki menüleri kullanarak siteyi rahat bir biçimde yönetebilir.

yönetici veya yazarlar, gönderi eklerken önemli seo ayarlarını yapabilmektedir.

SAYFALAR

- ana sayfa (/)
- yazı sayfası ( /gonderi/{kategori}/{seotitle}-{postID} )
- arama sayfası (/search)
- kategoriye göre sayfa ( /kategori/{kategoriadı}-{kategoriID} )
- iletişim sayfası (/iletisim)
- 404 hata sayfası
- giriş sayfası ( /giris-{type} )

ADMİN SAYFALARI
- Gönderi oluşturma ( posts/create )
- çöp kutusu ( admins/trash )
- onay bekleyen yorumlar sayfası ( admins/comments )
- kategori ekleme düzenleme sayfası ( /categories )
- yazar yönetim sayfası ( /writers )
- site ayarları ( /settings ), site adı, açıklama, logo vs.


PROJENİN TASARIM DOSYALARI NEREDE?
- Projeye ait css, scss, js vs. dosyaları Materials klasörü altındadır. - bir css değişikliği yapmak istiyorsanız style.scss dosyasını değiştirip compile etmeniz veya doğrudan style.min.css  dosyasından değişiklik yapmanız mümkün. 


BİR GÖNDERİ RESMİ NASIL SEÇERİM 
- Bir gönderi eklemeden önce, Materials/images/news klasörüne bir resim ekleyin ve gönderi oluştururken ilgili alana bu resmin adını yazmanız yeterlidir. metin içine resim eklemek için ise doğrudan editörden resim seçebilirsiiz.

VERİTABANI
- Sql server 2019
![boootryVt](https://user-images.githubusercontent.com/59912391/126438856-addbff8f-222c-427e-9aee-16d5d6ef1009.JPG)


NOT : PROJEDE KULLANILAN TÜM PARTİALVİEW'LER TEK BİR CONTROLLER (PartialViewsController.cs) VE VİEW TARAFINDA TEK BİR KLASÖRDE TOPLANMIŞTIR.

