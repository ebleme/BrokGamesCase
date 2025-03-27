1.  Karakter Seçim ve Yükleme Sistemi 
    PlayerChoosePanel.cs den başlar

2. Hareket Sistemi
    New Inpout system ile (InputHandler) elde edilen verilerle hareket yapılır
    'PlayerPreset' scriptable objetleri ile parametreler ile load edilir. State Machine kullanıldı

3. Bağımlılık Yönetimi ve Tasarım Desenleri 
    - Zenject kullanıldı
    - Player ların yüklenmesi 'PlayerFactory' ile sağlandı

4. Karakter Özelliklerinin Güncellenmesi (Upgrade Sistemi) - Yapıldı
    - PlayerUpgradePanel.cs ile başlayıp Player.cs üzerinde dataları ile çarpılarak sonuçlar elde edilir.

5.  Kalıcı Veri Kaydı
    - GameManager içerisindeki metodlarda yapılır. SavaDataManager kullanır

6. Performans Optimizasyonu 
    - Addressable kullanıldı


