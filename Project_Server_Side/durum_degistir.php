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

<form action="degistir.php" method="post">
	
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


		
			</td>
		</tr>
		
		<tr>
			<td>Sera Durumu</td>
			<td><input type="text"  name="sera_durum" ></td>
			
		</tr>
		<tr>	
			<td></td>
			<td>Sera durumu: 1->AÇIK 0->KAPALI</td>
		</tr>
		<tr>
			<td></td>
			<td><input class="btn btn-primary" type="submit" name="submit" value="Değiştir"></td>
			
		</tr>
		<tr><td></td>
		<td><a href="../cdtp_project/index.php"><-- SERALARIN SICAKLIĞINI GÖRÜNTÜLE</a></td>
		</tr>
	</table>

</form>






</div>

</div>
</body>

</html>


