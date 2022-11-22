using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Vodafon
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=HKILIC\\SQLEXPRESS;Initial Catalog=Vodafon;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        string coin = "";
        string net = "";

        public void grid()
        {
            con = new SqlConnection("Data Source=HKILIC\\SQLEXPRESS;Initial Catalog=Vodafon;Integrated Security=True");
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Deneme where Telefon='" + Form1.telefon + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                coin = dr[8].ToString();
                net = dr[7].ToString();
                con.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form7 gecis = new Form7();
            gecis.Show();
            this.Close();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            label4.Text = Form1.isim + " " + Form1.soyisim;
            grid();
            label2.Text = coin;
            label3.Text = (Math.Round((Convert.ToDouble(net) / 1024),2)).ToString();
            label5.Text = (Math.Round((Convert.ToDouble(net) / 1024), 2)).ToString();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Form3 gec = new Form3();
            gec.Show();
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Form2 gec = new Form2();
            gec.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form1 gec = new Form1();
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
