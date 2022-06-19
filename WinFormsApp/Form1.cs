using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
        // Джерело даних = VICTORIACHU21\SQLEXPRESS;Integrated Security = True; Час очікування підключення = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        string connectString = @"Data Source = VICTORIACHU21\SQLEXPRESS;Initial Catalog=News; Integrated Security=true;";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection myConnection = new SqlConnection(connectString))
            {
                myConnection.Open();
                SqlDataAdapter sqlD = new SqlDataAdapter("SELECT * FROM News", myConnection);
                DataTable dtbl = new DataTable();
                sqlD.Fill(dtbl);

                dataGridView1.DataSource = dtbl;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
