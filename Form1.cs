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

namespace Jovan
{
    public partial class Form1 : Form
    {
        public int red = 0;
        DataTable Korisnik = new DataTable();
        string CS = "Data source=DESKTOP-81RM0A0; Initial catalog=Student; Integrated security=true";
        public Form1()
        {
            InitializeComponent();
        }
        private void Osvezi()
        {
            if (Korisnik.Rows.Count == 0)
            {
                txt_id.Text = "";
                txt_login.Text = "";
                txt_ime.Text = "";
                txt_prezime.Text = "";
                txt_passw.Text = "";
                txt_tip.Text = "";
                btn_next.Enabled = false;
                btn_prev.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                txt_id.Text = Korisnik.Rows[red]["id"].ToString();
                txt_login.Text = Korisnik.Rows[red]["login"].ToString();
                txt_passw.Text = Korisnik.Rows[red]["passw"].ToString();
                txt_ime.Text = Korisnik.Rows[red]["ime"].ToString();
                txt_prezime.Text = Korisnik.Rows[red]["prezime"].ToString();
                txt_tip.Text = Korisnik.Rows[red]["tip"].ToString();
                if (red == Korisnik.Rows.Count - 1)
                {
                    btn_next.Enabled = false;
                    button1.Enabled = false;
                }
                else
                {
                    btn_next.Enabled = true;
                    button1.Enabled = true;
                }
                btn_prev.Enabled = (red != 0);
                button2.Enabled = (red != 0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
            SqlConnection veza = new SqlConnection(CS);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, login, passw, ime, prezime, tip FROM Korisnik ORDER BY id", veza);
            adapter.Fill(Korisnik);
            Osvezi();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            red++;
            Osvezi();
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            red--;
            Osvezi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            red = Korisnik.Rows.Count - 1;
            Osvezi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            red = 0;
            Osvezi();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pom = "INSERT INTO Korisnik (login, passw, ime, prezime, tip) VALUES ('" + txt_login.Text + "', '" + txt_passw.Text + "', " + txt_ime.Text + "', " + txt_prezime.Text + "', " + txt_tip.Text + ")";
            SqlConnection veza = new SqlConnection(CS);
            SqlCommand naredba = new SqlCommand(pom, veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, login, passw, ime, prezime, tip FROM Korisnik ORDER BY id", veza);
            Korisnik.Clear();
            adapter.Fill(Korisnik);
            red = Korisnik.Rows.Count - 1;
            Osvezi();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string pom = "DELETE FROM Korisnik WHERE id = " + txt_id.Text;
            SqlConnection veza = new SqlConnection(CS);
            SqlCommand naredba = new SqlCommand(pom, veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, login, passw, ime, prezime, tip FROM Korisnik ORDER BY id", veza);
            Korisnik.Clear();
            adapter.Fill(Korisnik);
            if (red > Korisnik.Rows.Count - 1)
            {
                red = Korisnik.Rows.Count - 1;
            }
            Osvezi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string pom = "UPDATE Korisnik SET login='" + txt_login.Text + "', passw='" + txt_passw.Text + "', ime='" + txt_ime.Text + "', prezime='" + txt_prezime.Text + "', tip=" + txt_tip.Text + "WHERE id=" + txt_id.Text;
            // MessageBox.Show(pom);
            SqlConnection veza = new SqlConnection(CS);
            SqlCommand naredba = new SqlCommand(pom, veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT id, login, passw, ime, prezime, tip FROM Korisnik ORDER BY id", veza);
            Korisnik.Clear();
            adapter.Fill(Korisnik);
            Osvezi();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
