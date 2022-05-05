<?php

@$baglanti = new mysqli('94.73.151.142:3306', 'u9591458_sicaklik_user', '2VZ8D2HSZ84W', 'u9591458_sicaklik'); // Veritabanı bağlantımızı yapıyoruz.
	if(mysqli_connect_error())
	{
		echo mysqli_connect_error();
		exit; //eğer bağlantıda hata varsa PHP çalışmasını sonlandırıyoruz.
	}

$baglanti->set_charset("utf8"); // Türkçe karakter sorunu olmaması için utf8'e çeviriyoruz.

?>
