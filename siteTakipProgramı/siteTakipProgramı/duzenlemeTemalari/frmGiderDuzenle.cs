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
    public partial class frmGiderDuzenle : Form
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
            txtGiderTuru.Text = "";
            txtTutar.Text = "";
            txtTarih.Text = "";
            txtAciklama.Text = "";
        }

        // Ve bu çağırdığımız Class ile aşağıdaki DLL'leri implement ediyoruz
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public frmGiderDuzenle()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void frmGiderDuzenle_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblSaat.Text = DateTime.Now.ToLongDateString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Veritabanına ekleme yapma işlemi
            string sqlsorgu = "INSERT INTO tblgider(binaAd, giderTuru, tutar, tarih, aciklama)" + "VALUES(@binaAd, @giderTuru, @tutar, @tarih, @aciklama)";
            cmd = new MySqlCommand(sqlsorgu, con);
            cmd.Parameters.AddWithValue("binaAd", txtBinaAd.Text);
            cmd.Parameters.AddWithValue("giderTuru", txtGiderTuru.Text);
            cmd.Parameters.AddWithValue("tutar", txtTutar.Text);
            cmd.Parameters.AddWithValue("tarih", txtTarih.Text);
            cmd.Parameters.AddWithValue("aciklama", txtAciklama.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBoxTemizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // veritabanı güncelleme işlemi
            string sqlsorgu = "UPDATE tblgider SET binaAd=@binaAd, giderTuru=@giderTuru, tutar=@tutar, tarih=@tarih, aciklama=@aciklama " + "WHERE binaAd=@binaAd";
            cmd = new MySqlCommand(sqlsorgu, con);
            cmd.Parameters.AddWithValue("@binaAd", txtBinaAd.Text);
            cmd.Parameters.AddWithValue("@giderTuru", txtGiderTuru.Text);
            cmd.Parameters.AddWithValue("@tutar", txtTutar.Text);
            cmd.Parameters.AddWithValue("@tarih", txtTarih.Text);
            cmd.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBoxTemizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // veritabanı silme işlemi
            string sqlsorgu = "DELETE FROM tblgider WHERE binaAd=@binaAd" +
                "";
            cmd = new MySqlCommand(sqlsorgu, con);
            cmd.Parameters.AddWithValue("binaAd", txtBinaAd.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBoxTemizle();
        }

        private void btnBilgi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Merhaba Değerli Kullanıcımız Bu Sayfada Gider Tablosuna Ekleme, Silme ve Güncelleme İşlemleri Yapabilirsiniz. Öncelikle" +
               "Metin Kutularına Bilgileri Giiyoruz Sonrasında İşlemimizi Gerçekleştiriyoruz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmGiderDuzenle_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmGiderler f1 = (frmGiderler)Application.OpenForms["frmGiderler"];
            f1.tablogetir();
        }
    }
}
