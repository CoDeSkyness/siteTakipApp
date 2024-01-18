using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace siteTakipProgramı
{
    public partial class frmSifreDegistir : Form
    {

        // MySql Database Bağlantıları
        MySqlConnection con = new MySqlConnection("Server=localhost;Database=sitetakipdb;Uid=root;Pwd=skydevsql");
        MySqlCommand cmd;
        MySqlDataAdapter adapter;
        DataTable dt;
        MySqlDataReader dr;

        // Ve bu çağırdığımız Class ile aşağıdaki DLL'leri implement ediyoruz
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public void SifreDeğiştir()
        {
            try
            {
                con.Open();
                string kayit = "UPDATE tblmail SET sifre=@sifre WHERE kullaniciMail='" + lblKullaniciAd.Text + "'";
                MySqlCommand cmd = new MySqlCommand(kayit, con);
                cmd.Parameters.AddWithValue("@sifre", txtYeniSifre.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                lblHata.Visible = true;
                lblHata.ForeColor = Color.Green;
                lblHata.Text = "Şifre Başarıyla Değiştirildi";
            }
            catch (Exception)
            {
                lblHata.Visible = true;
                lblHata.ForeColor = Color.Red;
                lblHata.Text = "Şifre Değiştirme Hatası";
            }

        }

        public frmSifreDegistir()
        {
            InitializeComponent();
        }

        private void frmSifreDegistir_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            lblKullaniciAd.Hide();
            try
            {
                MySqlCommand mevcutsifre = new MySqlCommand();
                con.Open();
                mevcutsifre.Connection = con;
                mevcutsifre.CommandText = "SELECT * FROM tblmail WHERE kullaniciMail='" + lblKullaniciAd.Text + "'";
                MySqlDataReader dr = mevcutsifre.ExecuteReader();
                if (dr.Read())
                {
                    txtMevcutSifre.Text = dr["sifre"].ToString();
                }
                con.Close();
            }
            catch (Exception)
            {
                lblHata.Visible = true;
                lblHata.ForeColor = Color.Red;
                lblHata.Text = "Mevcut Şifre Getirilemiyor";
            }
        
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtMevcutSifre.UseSystemPasswordChar = true;
                txtYeniSifre.UseSystemPasswordChar = true;
                txtYeniTekrar.UseSystemPasswordChar = true;
            }
            else
            {
                txtMevcutSifre.UseSystemPasswordChar = false;
                txtYeniSifre.UseSystemPasswordChar = false;
                txtYeniTekrar.UseSystemPasswordChar = false;
            }
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if (txtYeniSifre.Text == txtYeniTekrar.Text)
            {
                if (txtYeniSifre.Text != "" && txtYeniTekrar.Text != "" && txtMevcutSifre.Text != "")
                {
                    SifreDeğiştir();
                }
                else
                {
                    lblHata.Visible = true;
                    lblHata.ForeColor = Color.Red;
                    lblHata.Text = "Alanları Boş Bırakmayınız";
                }
            }
            else
            {
                lblHata.Visible = true;
                lblHata.ForeColor = Color.Red;
                lblHata.Text = "Şifreler Eşleşmiyor";
            }
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
            frmGiris giris = new frmGiris();
            giris.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSifremiUnuttum sifreunuttum = new frmSifremiUnuttum();
            sifreunuttum.Show();
            this.Hide();
        }
    }
}
