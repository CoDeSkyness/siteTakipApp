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
    public partial class frmAidat : Form
    {

        // MySql Database Bağlantıları
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=sitetakipdb;Uid=root;Pwd=skydevsql");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        MySqlDataReader dr;

        // Tabloları getirme komutu
        public void tablogetir()
        {
            dt = new DataTable();
            con.Open();
            adapter = new MySqlDataAdapter("SELECT * FROM tblaidat", con);
            dataGridView1.DataSource = dt;
            adapter.Fill(dt);
            con.Close();
        }

         public void dataİsimler()
        {
            //gelen ilk verimin başlığı
            dataGridView1.Columns[0].HeaderText = "Bina Adı";
            //gelen ikinci verimin başlığı
            dataGridView1.Columns[1].HeaderText = "Daire Numarası";
            //gelen üçüncü verimin başlığı
            dataGridView1.Columns[2].HeaderText = "Ad Soyad";
            //gelen dördüncü verimin başlığı
            dataGridView1.Columns[3].HeaderText = "Yıl";
            //gelen beşinci verimin başlığı
            dataGridView1.Columns[4].HeaderText = "Ocak";
            dataGridView1.Columns[5].HeaderText = "Şubat";
            dataGridView1.Columns[6].HeaderText = "Mart";
            dataGridView1.Columns[7].HeaderText = "Nisan";
            dataGridView1.Columns[8].HeaderText = "Mayıs";
            dataGridView1.Columns[9].HeaderText = "Haziran";
            dataGridView1.Columns[10].HeaderText = "Temmuz";
            dataGridView1.Columns[11].HeaderText = "Ağustos";
            dataGridView1.Columns[12].HeaderText = "Eylül";
            dataGridView1.Columns[13].HeaderText = "Ekim";
            dataGridView1.Columns[14].HeaderText = "Kasım";
            dataGridView1.Columns[15].HeaderText = "Aralık";
        }

        // Ve bu çağırdığımız Class ile aşağıdaki DLL'leri implement ediyoruz
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public frmAidat()
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

        private void frmAidat_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblSaat.Text = DateTime.Now.ToLongDateString();
            // Tablo getirme voidi
            tablogetir();
            dataİsimler();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            tablogetir();
        }

        private void btnBilgi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Değerli Kullanıcı Programın Bu Sayfasında Binaların Aidatlarını Görebileceksiniz. Ekleme, Silme ve Düzenleme yapmak için " +
              "'Ekle' Butonuna Basabilirsiniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            #region Arama Komutları    
            // veritabanı içerisindeki verileri arama işlemleri radioButtonlar ile
            if (cBoxAd.Checked == true)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "binaAd LIKE'" + txtAra.Text + "%'";
                dataGridView1.DataSource = dv;
            }
            if (cBoxYil.Checked == true)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "yil LIKE'" + txtAra.Text + "%'";
                dataGridView1.DataSource = dv;
            }
            #endregion
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            frmMenu menu = new frmMenu();
            menu.Show();
            this.Hide();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            frmAidatDuzenle aidatDuzenle = new frmAidatDuzenle();
            aidatDuzenle.Show();
        }
    }
}
