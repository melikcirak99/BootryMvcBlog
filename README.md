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
- Projeye ait css, scss, js vs. dosyaları Materials klasörü altındadır. - bir css değişikliği yapmak istiyorsanız style.scss dosyasını değiştirip compile etmeniz veya doğrudan style.min.css değişiklik dosyasından yapmanız mümkün. 


BİR GÖNDERİ RESMİ NASIL SEÇERİM 
- Bir gönderi eklemeden önce, Materials/images/news klasörüne bir resim ekleyin ve gönderi oluştururken ilgili alana bu resmin adını yazmanız yeterlidir. metin içine resim eklemek için ise doğrudan editörden resim seçebilirsiiz.

NOT : PROJEDE KULLANILAN TÜM PARTİALVİEW'LER TEK BİR CONTROLLER (PartialViewsController.cs) VE VİEW TARAFINDA TEK BİR KLASÖRDE TOPLANMIŞTIR.

Lisans:

<a rel="license" href="http://creativecommons.org/licenses/by-nc-nd/4.0/"><img alt="Creative Commons Lisansı" style="border-width:0" src="https://i.creativecommons.org/l/by-nc-nd/4.0/88x31.png" /></a><br /><a xmlns:cc="http://creativecommons.org/ns#" href="https://github.com/melik65/BootryMvcBlog" property="cc:attributionName" rel="cc:attributionURL">melik çırak</a> isimli yazarın <span xmlns:dct="http://purl.org/dc/terms/" property="dct:title">Bootry mvc blog</span> başlıklı eseri bu <a rel="license" href="http://creativecommons.org/licenses/by-nc-nd/4.0/"> Creative Commons Atıf-GayriTicari-Türetilemez 4.0 Uluslararası Lisansı </a> ile lisanslanmıştır.
