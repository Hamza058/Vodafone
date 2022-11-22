using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vodafon
{
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form6 gec = new Form6();
            gec.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form5 gec = new Form5();
            gec.Show();
            this.Close();
        }

        #region Form Hareketleri
        int hareket;
        int Mouse_X;
        int Mouse_Y;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }
        #endregion
    }
}
