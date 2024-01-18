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

namespace siteTakipProgramı
{
    public partial class frmMenu : Form
    {

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        public frmMenu()
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
            DialogResult x = MessageBox.Show("Programdan Çıkmak İstediğinizden Emin Misiniz?", "Çıkış Mesajı!", MessageBoxButtons.YesNo);
            if (x == DialogResult.Yes)
            {
                //Evet tıklandığında Yapılacak İşlemler
                Application.Exit(); // Evet tıklandığında uygulama kapanacak

            }
        }

        private void btnBilgi_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Merhaba Değerli Kullanıcımız Öncelikle Programımızı Kullandığınız İçin Çok Teşekkür Ediyoruz! " +
                "Bu Program Sizin Site İçerisindeki Bina ve Dairelerinizi Elinizde Tutmak Ve Rahat İşlem Yapmanız İçin Özenle Tasarlanmıştır.",
                "Kullanıcı Bilgi Sayfası", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBina_Click(object sender, EventArgs e)
        {
            frmBina bina = new frmBina();   
            bina.Show();
            this.Hide();
        }

        private void btnDaire_Click(object sender, EventArgs e)
        {
            frmDaire daire = new frmDaire();    
            daire.Show();
            this.Hide();
        }

        private void btnSikayet_Click(object sender, EventArgs e)
        {
            frmSikayet sikayet = new frmSikayet();
            sikayet.Show();
            this.Hide();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            timer1.Start();
            lblSaat.Text = DateTime.Now.ToLongDateString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmAidat aidat = new frmAidat();
            aidat.Show();
            this.Hide();
        }

        private void btnAraclar_Click(object sender, EventArgs e)
        {
            frmArac arac = new frmArac();
            arac.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmGiderler giderler = new frmGiderler();
            giderler.Show();
            this.Hide();
        }
    }
}
