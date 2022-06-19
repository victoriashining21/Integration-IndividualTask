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
                myConnection.Close();
                //this.dataGridView1.DataBindings["Value2"].DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged;

                //  SqlCommandBuilder cb = new SqlCommandBuilder(sqlD);
                int userVal = int.Parse(textBox1.Text);

                ////Create the SqlCommand to execute the stored procedure.
                //sqlD.InsertCommand = new SqlCommand("dbo.News", myConnection);
                //sqlD.InsertCommand.CommandType = CommandType.StoredProcedure;
             //   string queryString = "UPDATE News SET Likes = +1 WHERE id>1";
              //  SqlCommand command = new SqlCommand(queryString, myConnection);

                //for (int i = 0; i < dtbl.Rows.Count; i++)
                //{
                //    if (userVal == i)
                //    {

                //       //int like = Convert.ToInt32(dataGridView1[i, 7].Value) + 1;
                //       // cb = new SqlCommandBuilder(sqlD);
                //       // cb.GetUpdateCommand();


                //        SqlDataAdapter adapter = sqlD;
                //        SqlCommandBuilder sqlBld = new SqlCommandBuilder(adapter);
                //        adapter.UpdateCommand = sqlBld.GetUpdateCommand();

                //        // sqlD.Update(dataGridView1);
                //        DataSet ds = new DataSet();
                //        SelectSqlRows(connectString, "", "News");
                //    }
            }
            // string queryString = "INSERT INTO Customers " + "(CustomerID, CompanyName) Values('NWIND', 'Northwind Traders')";

        }

        private static void CreateCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            using (SqlConnection myConnection = new SqlConnection(connectString))
            {
                myConnection.Open();
               // SqlDataAdapter sqlD = new SqlDataAdapter("SELECT * FROM News", myConnection);
                //DataTable dtbl = new DataTable();
                //sqlD.Fill(dtbl);

                //dataGridView1.DataSource = dtbl;

                SqlCommandBuilder cb = new SqlCommandBuilder();
                int userVal = int.Parse(textBox1.Text);
                string queryString = "UPDATE News SET Likes = 1 WHERE id>5";
                SqlCommand command = new SqlCommand(queryString, myConnection);
                myConnection.Close();
            }

        }

    }
}
