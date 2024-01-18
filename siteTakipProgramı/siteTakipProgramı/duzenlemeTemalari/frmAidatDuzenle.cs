using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace siteTakipProgramı
{
    public partial class frmAidatDuzenle : Form
    {

        // MySql Database Bağlantıları
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=sitetakipdb;Uid=root;Pwd=skydevsql");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        MySqlDataReader dr;

        public void textBoxTemizle()
        {
            txtBinaAd.Text = "";
            txtDaireNo.Text = "";
            txtAdSoyad.Text = "";
            txtYil.Text = "";
            txtOcak.Text = "0";
            txtSubat.Text = "0";
            txtMart.Text = "0";
            txtNisan.Text = "0";
            txtMayis.Text = "0";
            txtHaziran.Text = "0";
            txtTemmuz.Text = "0";
            txtAgustos.Text = "0";
            txtEylul.Text = "0";
            txtEkim.Text = "0";
            txtKasim.Text = "0";
            txtAralik.Text = "0";
        }

        // Ve bu çağırdığımız Class ile aşağıdaki DLL'leri implement ediyoruz
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public frmAidatDuzenle()
        {
            InitializeComponent();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void frmAidatDuzenle_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblSaat.Text = DateTime.Now.ToLongDateString();
            textBoxTemizle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBilgi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Merhaba Değerli Kullanıcımız Bu Sayfada Aidatlar Tablosuna Ekleme, Silme ve Güncelleme İşlemleri Yapabilirsiniz. Öncelikle" +
               "Metin Kutularına Bilgileri Giiyoruz Sonrasında İşlemimizi Gerçekleştiriyoruz. \n NOT: BİNA ADLARI DEĞİŞTİRİLEMEZ!!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                // Veritabanına ekleme yapma işlemi
                string sqlsorgu = "INSERT INTO tblaidat(binaAd,daireNo, adSoyad, yil, ocak, subat, mart, nisan, mayis, haziran, temmuz, agustos, eylul, ekim, kasim, aralik)" + "VALUES(@binaAd,@daireNo, @adSoyad, @yil, @ocak, @subat, @mart, @nisan, @mayis, @haziran, @temmuz, @agustos, @eylul, @ekim, @kasim, @aralik)";
                cmd = new MySqlCommand(sqlsorgu, con);
                cmd.Parameters.AddWithValue("binaAd", txtBinaAd.Text);
                cmd.Parameters.AddWithValue("daireNo", txtDaireNo.Text);
                cmd.Parameters.AddWithValue("adSoyad", txtAdSoyad.Text);
                cmd.Parameters.AddWithValue("yil", txtYil.Text);
                cmd.Parameters.AddWithValue("ocak", txtOcak.Text);
                cmd.Parameters.AddWithValue("subat", txtSubat.Text);
                cmd.Parameters.AddWithValue("mart", txtMart.Text);
                cmd.Parameters.AddWithValue("nisan", txtNisan.Text);
                cmd.Parameters.AddWithValue("mayis", txtMayis.Text);
                cmd.Parameters.AddWithValue("haziran", txtHaziran.Text);
                cmd.Parameters.AddWithValue("temmuz", txtTemmuz.Text);
                cmd.Parameters.AddWithValue("agustos", txtAgustos.Text);
                cmd.Parameters.AddWithValue("eylul", txtEylul.Text);
                cmd.Parameters.AddWithValue("ekim", txtEkim.Text);
                cmd.Parameters.AddWithValue("kasim", txtKasim.Text);
                cmd.Parameters.AddWithValue("aralik", txtAralik.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                textBoxTemizle();
            }
            catch 
            {
                MessageBox.Show("Aynı Bina Adı birden fazla girilemez değişiklik için lütfen güncelle butonunu kullanın.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // veritabanı silme işlemi
            string sqlsorgu = "DELETE FROM tblaidat WHERE adSoyad=@adSoyad";
            cmd = new MySqlCommand(sqlsorgu, con);
            cmd.Parameters.AddWithValue("adSoyad", txtAdSoyad.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBoxTemizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // veritabanı güncelleme işlemi
            string sqlsorgu = "UPDATE tblaidat SET binaAd=@binaAd,daireNo=@daireNo, adSoyad=@adSoyad, yil=@yil, ocak=@ocak, subat=@subat, mart=@mart, nisan=@nisan, mayis=@mayis, haziran=@haziran, temmuz=@temmuz, agustos=@agustos, eylul=@eylul, ekim=@ekim, kasim=@kasim, aralik=@aralik " + "WHERE adSoyad=@adSoyad";
            cmd = new MySqlCommand(sqlsorgu, con);
            cmd.Parameters.AddWithValue("@binaAd", txtBinaAd.Text);
            cmd.Parameters.AddWithValue("@daireNo", txtDaireNo.Text);
            cmd.Parameters.AddWithValue("@adSoyad", txtAdSoyad.Text);
            cmd.Parameters.AddWithValue("@yil", txtYil.Text);
            cmd.Parameters.AddWithValue("@ocak", txtOcak.Text);
            cmd.Parameters.AddWithValue("@subat", txtSubat.Text);
            cmd.Parameters.AddWithValue("@mart", txtMart.Text);
            cmd.Parameters.AddWithValue("@nisan", txtNisan.Text);
            cmd.Parameters.AddWithValue("@mayis", txtMayis.Text);
            cmd.Parameters.AddWithValue("@haziran", txtHaziran.Text);
            cmd.Parameters.AddWithValue("@temmuz", txtTemmuz.Text);
            cmd.Parameters.AddWithValue("@agustos", txtAgustos.Text);
            cmd.Parameters.AddWithValue("@eylul", txtEylul.Text);
            cmd.Parameters.AddWithValue("@ekim", txtEkim.Text);
            cmd.Parameters.AddWithValue("@kasim", txtKasim.Text);
            cmd.Parameters.AddWithValue("@aralik", txtAralik.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBoxTemizle();
        }

        private void frmAidatDuzenle_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmAidat f1 = (frmAidat)Application.OpenForms["frmAidat"];
            f1.tablogetir();
        }
    }
}
