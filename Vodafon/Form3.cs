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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=HKILIC\\SQLEXPRESS;Initial Catalog=Vodafon;Integrated Security=True");
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

        string coin = "";

        public void griddoldur()
        {
            da = new SqlDataAdapter("Select *From Pazar", con);
            ds = new DataSet();
            SqlDataReader dr;
            con.Open();
            
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Deneme where Telefon='" + Form1.telefon + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                coin = dr[8].ToString();
                con.Close();
            }

            da.Fill(ds, "Pazar");
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dakika = txtDk.Text;
            string sms = txtSms.Text;
            string net = txtNet.Text;
            string fiyat = txtFyt.Text; ;

            if (txtDk.Text == "" || txtSms.Text == "" || txtNet.Text == "" || txtFyt.Text =="")
            {
                MessageBox.Show("Tüm Bilgileri Doldurunuz");
            }
            else
            {
                if (Convert.ToInt32(dakika) < (Convert.ToDouble(Form1.dakika) / 10) && Convert.ToInt32(sms) < (Convert.ToDouble(Form1.sms) / 10) && Convert.ToInt32(net) < (Convert.ToDouble(Form1.net) / 10))
                {
                    con.Open();
                    string ekle;
                    ekle = "insert into Pazar(Telefon,Dakika,Sms,Internet,Fiyat)" + "values ('" + Form1.telefon + "','" + dakika + "','" + sms + "','" + net + "','" + fiyat + "')";
                    cmd = new SqlCommand(ekle, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    griddoldur();
                    MessageBox.Show("Başarıyla Pazara Eklenmiştir");
                }
                else
                {
                    MessageBox.Show("Girilen Değerler Kalan Paketinizin %10' undan fazla");
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form5 gec = new Form5();
            gec.Show();
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            griddoldur();
            label5.Text = Form1.isim + " " + Form1.soyisim;
            label6.Text = coin;
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
