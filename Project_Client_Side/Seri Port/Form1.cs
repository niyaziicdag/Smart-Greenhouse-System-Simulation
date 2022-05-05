using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace Seri_Port
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        SerialPort serialPort1;
        string port;
        int baud;
        int databit;
        Parity eslik;
        StopBits dur;
        int sera_durum_kontrol_sayac = 10;
        int sera_id;
        int sayac1 = 10,sayac2=30;
        double sicaklik;
        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            string[] portlar = SerialPort.GetPortNames();
            comboPortAdi.Items.Clear();
            foreach (string prt in portlar)
            {
                comboPortAdi.Items.Add(prt);
            }

            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox4.Enabled = false;
            this.Size = new Size(920, 465);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            btnYeni.Hide();
            int adet = lstBxDurum.Items.Count;

            lstBxDurum.SelectedIndex = adet - 1;
        }
        public void sendSerialPort_OnOffState()
        {
            //Serverdan çekilen On/Off durumu seraya gönderilmektedir.
            try
            {
                if (serialPort1 == null)
                {
                    MessageBox.Show("Lütfen Port Seçiniz ve 'BAĞLAN' butonuna tıklayınız.");
                }
                else
                {
                    serialPort1.Write(textBox2.Text);
                    lstBxDurum.Items.Add(DateTime.Now.ToString() + " - ON/OFF durumu seraya gönderildi.");
                }

            }
            catch
            {
                lstBxDurum.Items.Add(DateTime.Now.ToString() + " - Hata! ON/OFF durumu güncellenemedi.");
            }
        }
        public void saveDb(double deger)
        {
            //Bu fonksiyonda sera'dan gelen sıcaklık değeri server'a gönderilmektedir.
            string connStr = "server=94.73.151.142;user=u9591458_sicaklik_user;database=u9591458_sicaklik;port=3306;password=2VZ8D2HSZ84W";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
               // MessageBox.Show("sicaklik" + (deger-1));
                String tarih = Convert.ToString(DateTime.Now);
                string sql = "INSERT INTO seralar(sera_no,sera_sonSicaklik,sera_tarih) VALUES (@serano,@sicaklik,@simdi)";   //('"+deger+"','"+tarih+"');
                //sera_no	sera_sonSicaklik	sera_tarih
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@serano", sera_id);
                cmd.Parameters.Add("@sicaklik", MySqlDbType.Decimal).Value = deger;
                cmd.Parameters.AddWithValue("@simdi", DateTime.Parse(tarih));


                cmd.ExecuteNonQuery();
                lstBxDurum.Items.Add(DateTime.Now.ToString() + " - Sera Sıcaklığı: "+deger+" derece server'a gönderildi.");
            }
            catch
            {
                lstBxDurum.Items.Add(DateTime.Now.ToString() + " - HATA! Sera sıcaklığı server'a gönderilemedi.");
            }
            conn.Close();
        }
        public void checkState()
        {
            //Bu fonksiyonda server'dan sera'nın On/Off durumu çekilmektedir.
            //0 ise ısıtıcı kapalı 1 ise açıktır.
            string connStr = "server=94.73.151.142;user=u9591458_sicaklik_user;database=u9591458_sicaklik;port=3306;password=2VZ8D2HSZ84W";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT * from sera_onoff where sera_no= @serano";   //('"+deger+"','"+tarih+"');
                //sera_no	sera_sonSicaklik	sera_tarih
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@serano", sera_id);
                MySqlDataReader oku = cmd.ExecuteReader();
                oku.Read();
                textBox2.Text= oku["sera_durum"].ToString();
                lstBxDurum.Items.Add(DateTime.Now.ToString()+" - ON/OFF bilgisi serverdan çekildi.");
            }
            catch
            {
                lstBxDurum.Items.Add(DateTime.Now.ToString() + " - HATA! Sera'nın son on/off durumu server'dan çekilemedi.");
            }
            conn.Close();
            

        }
        private void btnBaglan_Click(object sender, EventArgs e)
        {
            if (textBox1.Text =="")
            {
                MessageBox.Show("HATA! Sera id girmeniz gerekmektedir.","HATA");
            }
            else
            {
                sera_id = Convert.ToInt32(textBox1.Text.ToString());
            try
            {
                baud = 9600;
                databit = 8;
                eslik = Parity.None;
                dur = StopBits.One;
                lstBxDurum.Items.Clear();
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                groupBox4.Enabled = true;
                textBox1.Enabled = false;
                port = comboPortAdi.SelectedItem.ToString();
                lstBxDurum.Items.Add(DateTime.Now.ToString() + " - Port: " + port);
                lstBxDurum.Items.Add(DateTime.Now.ToString() +  " - Veri Hızı: " + baud.ToString());
                lstBxDurum.Items.Add(DateTime.Now.ToString() + " - Veri Biti Sayısı: " + databit);
                lstBxDurum.Items.Add(DateTime.Now.ToString() + " - Parity (Eşlik): " + eslik);
                lstBxDurum.Items.Add(DateTime.Now.ToString() + " - Stop Biti: " + dur);
                checkState();
                serialPort1 = new SerialPort(port, baud, eslik, databit, dur);
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);

                    comboPortAdi.Text = port;



                serialPort1.Open();

                comboPortAdi.Enabled = false;

                btnBaglan.Enabled = false;
                btnYenile.Enabled = false;


                btnBaglan.BackColor = Color.LightGreen;
                btnBaglan.Text = "BAĞLI: " + port;

                btnYeni.Show();

                timer1.Start();
                timer2.Start();
                sendSerialPort_OnOffState();

                }
                catch
            {
                lstBxDurum.Items.Add(DateTime.Now.ToString() + " - HATA! Bağlantı gerçekleştirilemedi.");
            }

        }
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            btnYeni.Hide();
            serialPort1.Close();
            comboPortAdi.Enabled = true;
            timer1.Stop();
            timer2.Stop();
            btnBaglan.Enabled = true;
            btnYenile.Enabled = true;
            btnBaglan.BackColor = Color.WhiteSmoke;
            btnBaglan.Text = "BAĞLAN";
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox4.Enabled = false;
            textBox1.Enabled = true;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            txtAlinan.Clear();
            listBox1.Items.Clear();
            lstBxDurum.Items.Clear();
            lstBxDurum.Items.Add("Bağlantı Kapatıldı!");
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            string[] portlar = SerialPort.GetPortNames();
            comboPortAdi.Items.Clear();
            foreach (string prt in portlar)
            {
                comboPortAdi.Items.Add(prt);
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

                txtAlinan.Text = txtAlinan.Text + serialPort1.ReadExisting();// + "\r\n";
                txtAlinan.Select(txtAlinan.Text.Length, 0);
        }

            private void timer1_Tick(object sender, EventArgs e)
        {

            sera_durum_kontrol_sayac--;
          if (sera_durum_kontrol_sayac == 0) {
                checkState();   
                sendSerialPort_OnOffState();
                int i = lstBxDurum.Items.Count;
                lstBxDurum.SelectedIndex = i - 1;
                sera_durum_kontrol_sayac = 10;
            }
            
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac1--;
            sayac2--;
            if (sayac1 == 0)
            {
                try
                {
                    string[] metinler = txtAlinan.Text.Split('*');
                    string sonMetin2 = metinler[metinler.Length - 1];
                    string cozum = sonMetin2.Substring(0, sonMetin2.Length - 5);
                    listBox1.Items.Add(cozum);
                    txtAlinan.Clear();
                    int adet = listBox1.Items.Count;
                    textBox3.Clear();
                    textBox3.Text = listBox1.Items[adet - 1].ToString();
                    sicaklik = 0;
                    double.TryParse(textBox3.Text, out sicaklik);
                    sicaklik = sicaklik / 100;
                    int i = lstBxDurum.Items.Count;
                    lstBxDurum.SelectedIndex = i - 1;
                    //lstBxDurum.Items.Add("sicaklik double= " + sicaklik.ToString());
                }
                catch
                {
                    MessageBox.Show("HATA! Isıtıcıdan veri okunamadı.");

                }
                sayac1 = 10;
                

            }
            if (sayac2 == 0)
            {
                //lstBxDurum.Items.Add("2sicaklik double= " + sicaklik.ToString());
                saveDb(sicaklik);
                sayac2 = 30;

            }
        }
    }
}
