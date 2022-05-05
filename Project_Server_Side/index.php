<?php 
include("vt.php"); // veritabanı bağlantımızı sayfamıza ekliyoruz. 
?>

<html>
<head>
<meta charset="utf-8">
<title>Veritabanı İşlemleri</title>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous">
</head>
<script src="jquery.js"></script>

<body>

<div class="container">
<div class="col-md-6">

<form action="" method="post">
	
	<table class="table">
		<tr><td></td>
	<td><img src="ytu_logo.png"/></td>
	</tr>
		<tr>
				
		    <td width="256">Sera Seç</td>
			<td width="439">
			<select name="sera_name" id="sera_name">
			<option value="" disabled selected>Seçiniz</option>
				<option value="1" name="1">Sera1</option>
				<option value="2" name="2"  >Sera2</option>
				<option value="3" name="3"  >Sera3</option>
				<option value="4" name="4"  >Sera4</option>
				<option value="5" name="5"  >Sera5</option>
			</select>
<?php
    if(isset($_POST['submit'])){
    if(!empty($_POST['sera_name'])) {
		
        $selected = $_POST['sera_name'];
        echo 'Seçilen sera: ' . $selected;
    } else {
        echo 'Sera seçiniz.';
    }
    }

	$sorgu = $baglanti->query("SELECT * FROM  sera_onoff where sera_no='$selected'");
	while ($sonuc = $sorgu->fetch_assoc()) { 
	$sera_durum=$sonuc['sera_durum'];
	}


?>

		
			</td>
		</tr>
		<tr>
			<td></td>
			<td><input class="btn btn-primary" type="submit" name="submit" value="Görüntüle"></td>
		</tr>
		<tr>
			<td>Sera Durumu</td>
			<td><input type="text" value="<?php echo $sera_durum;?>"  name="sera_durum" readonly></td>
			
		</tr>
		<tr>	
			<td></td>
			<td>Sera durumu: 1->AÇIK 0->KAPALI</td>
		</tr>
		<tr>
			<td></td>
			<td><a href="../cdtp_project/durum_degistir.php">DURUM DEĞİŞTİR</a></td>

		</tr>
	</table>

</form>


<!--------------------------------------------------------->




</div>

<div class="col-md-7">
<table class="table">
	
	<tr>
		<th>Sera No</th>
		<th>Sıcaklık</th>
		<th>Tarih - <?php echo $sera_name; ?></th>
		
		<th></th>
		<th></th>
	</tr>

<!-- Şimdi ise verileri sıralayarak çekmek için PHP kodlamamıza geçiyoruz. -->

<?php 
if(isset($_POST['submit'])){
	
$sorgu = $baglanti->query("SELECT * FROM  seralar where sera_no='$selected'"); // Makale tablosundaki tüm verileri çekiyoruz.

while ($sonuc = $sorgu->fetch_assoc()) { 

 // Veritabanından çektiğimiz id satırını $id olarak tanımlıyoruz.
 $sera_no = $sonuc['sera_no'];
$sera_sonSicaklik = $sonuc['sera_sonSicaklik'];
$sera_tarih= $sonuc['sera_tarih'];

// While döngüsü ile verileri sıralayacağız. Burada PHP tagını kapatarak tırnaklarla uğraşmadan tekrarlatabiliriz. 
?>
	
	<tr>
		
		<td><?php echo $sera_no; ?></td>
		<td><?php echo $sera_sonSicaklik; ?></td>
		<td><?php echo $sera_tarih; ?></td>

	</tr>

<?php } // Tekrarlanacak kısım bittikten sonra PHP tagının içinde while döngüsünü süslü parantezi kapatarak sonlandırıyoruz. ?>

</table>
<?php } ?>
</div>
</div>
</body>

</html>


