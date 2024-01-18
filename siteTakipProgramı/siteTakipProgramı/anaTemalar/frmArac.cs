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
    public partial class frmArac : Form
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
            adapter = new MySqlDataAdapter("SELECT * FROM tblarac", con);
            dataGridView1.DataSource = dt;
            adapter.Fill(dt);
            con.Close();
        }

        public void dataİsimler()
        {
            //gelen ilk verimin başlığı
            dataGridView1.Columns[0].HeaderText = "Araç Plakası";
            //gelen ikinci verimin başlığı
            dataGridView1.Columns[1].HeaderText = "Marka";
            //gelen üçüncü verimin başlığı
            dataGridView1.Columns[2].HeaderText = "Modeli";
            //gelen dördüncü verimin başlığı
            dataGridView1.Columns[3].HeaderText = "Bina Bilgisi";
            //gelen beşinci verimin başlığı
            dataGridView1.Columns[4].HeaderText = "İrtibat Numarası";
        }

            // Ve bu çağırdığımız Class ile aşağıdaki DLL'leri implement ediyoruz
            [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public frmArac()
        {
            InitializeComponent();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            frmMenu menu = new frmMenu();
            menu.Show();
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

        private void btnBilgi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Değerli Kullanıcı Programın Bu Sayfasında Araçlarınızı Görebileceksiniz Araç Ekleme, Silme ve Düzenleme yapmak için " +
               "'Ekle' Butonuna Basabilirsiniz.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }

        private void frmArac_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblSaat.Text = DateTime.Now.ToLongDateString();
            // Tablo getirme voidi
            tablogetir();
            dataİsimler();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            // veritabanı içerisindeki verileri arama işlemleri radioButtonlar ile
            if (rBtnPlaka.Checked == true)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "aracPlaka LIKE'" + txtAra.Text + "%'";
                dataGridView1.DataSource = dv;
            }
            if (rBtnMarka.Checked == true)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "marka LIKE'" + txtAra.Text + "%'";
                dataGridView1.DataSource = dv;
            }
            if (rBtnModel.Checked == true)
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "model LIKE'" + txtAra.Text + "%'";
                dataGridView1.DataSource = dv;
            }
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            tablogetir();
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            frmAracDuzenle aracDuzenle = new frmAracDuzenle();
            aracDuzenle.Show();
        }
    }
}
