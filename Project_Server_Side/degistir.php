<?php 
include("vt.php"); // veritabanı bağlantımızı sayfamıza ekliyoruz. 
?>
			<?php
    if(isset($_POST['submit'])){
    if(!empty($_POST['sera_name'])) {
		
        $selected = $_POST['sera_name'];
		$durum = $_POST['sera_durum'];

		$sql = "UPDATE sera_onoff SET sera_durum='$durum' where sera_no='$selected'";

	if ($baglanti->query($sql) === TRUE) {
		echo "Sera durumu değiştirildi.";
	} else {
		echo "HATA: " . $baglanti->error;
	}
    } else {
        echo 'Sera seçiniz.';
    }
    
	}

?><br>
<a href="durum_degistir.php">Geri Dön</a>