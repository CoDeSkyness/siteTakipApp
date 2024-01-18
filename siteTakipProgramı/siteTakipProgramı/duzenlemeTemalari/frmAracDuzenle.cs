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
    public partial class frmAracDuzenle : Form
    {

        // MySql Database Bağlantıları
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=sitetakipdb;Uid=root;Pwd=skydevsql");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        MySqlDataReader dr;

        public void textBoxTemizle()
        {
            txtPlaka.Text = "";
            txtMarka.Text = "";
            txtModel.Text = "";
            txtDaireNo.Text = "";
            txtİrtibatTel.Text = "";
        }

        // Ve bu çağırdığımız Class ile aşağıdaki DLL'leri implement ediyoruz
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public frmAracDuzenle()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void frmAracDuzenle_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblSaat.Text = DateTime.Now.ToLongDateString();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            // Veritabanına ekleme yapma işlemi
            string sqlsorgu = "INSERT INTO tblarac(aracPlaka, marka, model, binaBilgisi, irtibatNo)" + "VALUES(@aracPlaka, @marka, @model, @binaBilgisi, @irtibatNo)";
            cmd = new MySqlCommand(sqlsorgu, con);
            cmd.Parameters.AddWithValue("aracPlaka", txtPlaka.Text);
            cmd.Parameters.AddWithValue("marka", txtMarka.Text);
            cmd.Parameters.AddWithValue("model", txtModel.Text);
            cmd.Parameters.AddWithValue("binaBilgisi", txtDaireNo.Text);
            cmd.Parameters.AddWithValue("irtibatNo", txtİrtibatTel.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBoxTemizle();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // veritabanı silme işlemi
            string sqlsorgu = "DELETE FROM tblarac WHERE aracPlaka=@aracPlaka" +
                "";
            cmd = new MySqlCommand(sqlsorgu, con);
            cmd.Parameters.AddWithValue("aracPlaka", txtPlaka.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBoxTemizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            // veritabanı güncelleme işlemi
            string sqlsorgu = "UPDATE tblarac SET aracPlaka=@aracPlaka, marka=@marka, model=@model, binaBilgisi=@binaBilgisi, irtibatNo=@irtibatNo " + "WHERE aracPlaka=@aracPlaka";
            cmd = new MySqlCommand(sqlsorgu, con);
            cmd.Parameters.AddWithValue("@aracPlaka", txtPlaka.Text);
            cmd.Parameters.AddWithValue("@marka", txtMarka.Text);
            cmd.Parameters.AddWithValue("@model", txtModel.Text);
            cmd.Parameters.AddWithValue("@binaBilgisi", txtDaireNo.Text);
            cmd.Parameters.AddWithValue("@irtibatNo", txtİrtibatTel.Text);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Kayıt Güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            textBoxTemizle();
        }

        private void btnBilgi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Merhaba Değerli Kullanıcımız Bu Sayfada araç Tablosuna Ekleme, Silme ve Güncelleme İşlemleri Yapabilirsiniz. Öncelikle" +
               "Metin Kutularına Bilgileri Giiyoruz Sonrasında İşlemimizi Gerçekleştiriyoruz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmAracDuzenle_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmArac f1 = (frmArac)Application.OpenForms["frmArac"];
            f1.tablogetir();
        }
    }
}
