using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace siteTakipProgramı
{
    public partial class frmSikayet : Form
    {

        // MySql Database Bağlantıları
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=sitetakipdb;Uid=root;Pwd=skydevsql");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        MySqlDataReader dr;

        public void tablogetir()
        {
            dt = new DataTable();
            con.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM tblsikayet", con);
            dataGridView1.DataSource = dt;
            adapter.Fill(dt);
            con.Close();
        }

        public void dataİsimler()
        {
            //gelen ilk verimin başlığı
            dataGridView1.Columns[0].HeaderText = "Bina Kodu";
            //gelen ikinci verimin başlığı
            dataGridView1.Columns[1].HeaderText = "Bina Adı";
            //gelen üçüncü verimin başlığı
            dataGridView1.Columns[2].HeaderText = "Daire Bilgisi";
            //gelen dördüncü verimin başlığı
            dataGridView1.Columns[3].HeaderText = "Ad Soyad";
            //gelen beşinci verimin başlığı
            dataGridView1.Columns[4].HeaderText = "Şikayet";
        }

        // Ve bu çağırdığımız Class ile aşağıdaki DLL'leri implement ediyoruz
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public frmSikayet()
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

        private void button1_Click(object sender, EventArgs e)
        {
            frmMenu menu = new frmMenu();
            menu.Show();
            this.Hide();
        }

        private void frmSikayet_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblSaat.Text = DateTime.Now.ToLongDateString();
            tablogetir();
            dataİsimler();
        }

        private void btnDuzenle_Click_1(object sender, EventArgs e)
        {
            frmSikayetDuzenle sikayetDuzenle = new frmSikayetDuzenle();
            sikayetDuzenle.Show();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            // veritabanı içerisindeki verileri arama işlemleri radioButtonlar ile
            if (rBtnKod.Checked == true)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "binaKodu LIKE'" + txtAra.Text + "%'";
                dataGridView1.DataSource = dv;
            }
            if (rBtnBinaAd.Checked == true)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "binaAdi LIKE'" + txtAra.Text + "%'";
                dataGridView1.DataSource = dv;
            }
        }

        private void btnBilgi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Değerli Kullanıcı Programın Bu Sayfasında Binalar Genelindeki Sorun Ve Şikayetleri Görebileceksiniz. " +
              "Bina İçerisindeki Şikayetleri Düzenlemek İçin 'Ekle' Butonuna Basınız.");
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            tablogetir();
        }
    }
}
