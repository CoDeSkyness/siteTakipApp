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
    public partial class frmSikayetDuzenle : Form
    {

        // MySql Database Bağlantıları
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=sitetakipdb;Uid=root;Pwd=skydevsql");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        MySqlDataReader dr;

        public void textBoxTemizle()
        {
            txtBinaKod.Text = "";
            txtBinaAd.Text = "";
            txtBinaBilgisi.Text = "";
            txtAdSoyad.Text = "";
            txtSikayet.Text = "";
        }

        // Ve bu çağırdığımız Class ile aşağıdaki DLL'leri implement ediyoruz
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;


        public frmSikayetDuzenle()
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

        private void frmSikayetDuzenle_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblSaat.Text = DateTime.Now.ToLongDateString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBilgi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Merhaba Değerli Kullanıcımız Bu Sayfada Şikayetler Tablosuna Ekleme, Silme ve Güncelleme İşlemleri Yapabilirsiniz. Öncelikle" +
               "Metin Kutularına Bilgileri Giiyoruz Sonrasında İşlemimizi Gerçekleştiriyoruz. \n NOT: BİNA KODLARI DEĞİŞTİRİLEMEZ!!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Veritabanına ekleme yapma işlemi
            string sqlsorgu = "INSERT INTO tblsikayet(binaKodu, binaAdi, daireBilgisi, adSoyad, sikayet)" + "VALUES(@binaKodu, @binaAdi, @daireBilgisi, @adSoyad, @sikayet)";
            cmd = new MySqlCommand(sqlsorgu, con);
            cmd.Parameters.AddWithValue("binaKodu", txtBinaKod.Text);
            cmd.Parameters.AddWithValue("binaAdi", txtBinaAd.Text);
            cmd.Parameters.AddWithValue("daireBilgisi", txtBinaBilgisi.Text);
            cmd.Parameters.AddWithValue("adSoyad", txtAdSoyad.Text);
            cmd.Parameters.AddWithValue("sikayet", txtSikayet.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBoxTemizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // veritabanı silme işlemi
            string sqlsorgu = "DELETE FROM tblsikayet WHERE binaKodu=@binaKodu";
            cmd = new MySqlCommand(sqlsorgu, con);
            cmd.Parameters.AddWithValue("binaKodu", txtBinaKod.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBoxTemizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // veritabanı güncelleme işlemi
            string sqlsorgu = "UPDATE tblsikayet SET binaKodu=@binaKodu, binaAdi=@binaAdi, daireBilgisi=@daireBilgisi, adSoyad=@adSoyad, sikayet=@sikayet " + "WHERE binaKodu=@binaKodu";
            cmd = new MySqlCommand(sqlsorgu, con);
            cmd.Parameters.AddWithValue("@binaKodu", txtBinaKod.Text);
            cmd.Parameters.AddWithValue("@binaAdi", txtBinaAd.Text);
            cmd.Parameters.AddWithValue("@daireBilgisi", txtBinaBilgisi.Text);
            cmd.Parameters.AddWithValue("@adSoyad", txtAdSoyad.Text);
            cmd.Parameters.AddWithValue("@sikayet", txtSikayet.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBoxTemizle();
        }

        private void frmSikayetDuzenle_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmSikayet f1 = (frmSikayet)Application.OpenForms["frmSikayet"];
            f1.tablogetir();
        }
    }
}
