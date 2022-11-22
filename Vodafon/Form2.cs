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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=HKILIC\\SQLEXPRESS;Initial Catalog=Vodafon;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        string coin = "";

        public void griddoldur()
        {
            con.Open();
            da = new SqlDataAdapter("Select *From Pazar where not Telefon='" + Form1.telefon + "'", con);
            ds = new DataSet();
            da.Fill(ds, "Pazar");
            dataGridView1.DataSource = ds.Tables["Pazar"];

            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Deneme where Telefon='" + Form1.telefon + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                coin = dr[8].ToString();
                con.Close();
            }

            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Saticifiyat = "";
            string Saticidakika = "";
            string Saticisms = "";
            string Saticinet = "";
            string telefon = "";

            string Alicifiyat = "";
            string Alicidakika = "";
            string Alicisms = "";
            string Alicinet = "";

            con = new SqlConnection("Data Source=HKILIC\\SQLEXPRESS;Initial Catalog=Vodafon;Integrated Security=True");
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Pazar where id='" + textBox1.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                telefon = dr[1].ToString();
                Saticifiyat = dr[5].ToString();
                Saticidakika = dr[2].ToString();
                Saticisms = dr[3].ToString();
                Saticinet = dr[4].ToString();
                con.Close();
            }
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Deneme where telefon='" + telefon + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Alicifiyat = dr[8].ToString();
                Alicidakika = dr[5].ToString();
                Alicisms = dr[6].ToString();
                Alicinet = dr[7].ToString();
                con.Close();
            }

            con.Open();

            int SonFiyat = Convert.ToInt32(Form1.coin) - Convert.ToInt32(Saticifiyat);
            int SonNet = Convert.ToInt32(Form1.net) + Convert.ToInt32(Saticinet);
            int SonDakika = Convert.ToInt32(Form1.dakika) + Convert.ToInt32(Saticidakika);
            int SonSms = Convert.ToInt32(Form1.sms) - Convert.ToInt32(Saticisms);


            cmd = new SqlCommand("update Deneme set Coin='" + SonFiyat.ToString() + "',Internet = '" + SonNet.ToString() + "',Dakika = '" + SonDakika.ToString() + "',Sms = '" + SonSms.ToString() + "'where Telefon='" + Form1.telefon + "'", con);
            cmd.ExecuteNonQuery();

            int SonNet1 = Convert.ToInt32(Alicinet) - Convert.ToInt32(Saticinet);
            int SonDakika1 = Convert.ToInt32(Alicidakika) - Convert.ToInt32(Saticidakika);
            int SonSms1 = Convert.ToInt32(Alicisms) - Convert.ToInt32(Saticisms);
            int SonFiyat1 = Convert.ToInt32(Saticifiyat) + Convert.ToInt32(Alicifiyat);
            
            cmd = new SqlCommand("update Deneme set Coin='" + SonFiyat1.ToString() + "',Internet = '" + SonNet1.ToString() + "',Dakika = '" + SonDakika1.ToString() + "',Sms = '" + SonSms1.ToString() + "'where Telefon='" + telefon + "'", con);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("delete from Pazar where id='" + textBox1.Text + "'", con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Satın Alma Başarılı");

            con.Close();
            griddoldur();
            label2.Text = coin;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'vodafonDataSet1.Pazar' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.pazarTableAdapter.Fill(this.vodafonDataSet1.Pazar);
            // TODO: Bu kod satırı 'vodafonDataSet.Deneme' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.denemeTableAdapter.Fill(this.vodafonDataSet.Deneme);
            griddoldur();
            label4.Text = Form1.isim + " " + Form1.soyisim;
            label2.Text = coin;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form5 gec = new Form5();
            gec.Show();
            this.Close();
        }

        #region Form Hareketleri
        int hareket;
        int Mouse_X;
        int Mouse_Y;

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            hareket = 0;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            hareket = 1;
            Mouse_X = e.X;
            Mouse_Y = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (hareket == 1)
            {
                this.SetDesktopLocation(MousePosition.X - Mouse_X, MousePosition.Y - Mouse_Y);
            }
        }
        #endregion
    }
}
