using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WinFormsAppIndividual
{
    public partial class Form1 : Form
    {
        //Data Source = VICTORIACHU21\SQLEXPRESS;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        string connectString = @"Data Source = VICTORIACHU21\SQLEXPRESS;Initial Catalog=News; Integrated Security=true;";
        public Form1()
        {
            InitializeComponent();
           
          //  LoadData();
        }

        private void LoadData()
        {
            
            

            SqlConnection myConnection = new SqlConnection(connectString);

            myConnection.Open();

            string query = "SELECT * FROM dbo.News";

            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();

            List<string[]> data = new List<string[]>();

            while (reader.Read())
            {
                data.Add(new string[3]);

                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
            }

            reader.Close();

            myConnection.Close();

            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using(SqlConnection myConnection = new SqlConnection(connectString))
            {
                myConnection.Open();
                SqlDataAdapter sqlD = new SqlDataAdapter("SELECT * FROM News", myConnection);
                DataTable dtbl = new DataTable();
                sqlD.Fill(dtbl);

                dataGridView1.DataSource = dtbl;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                
                FileName = "cmd.exe",
                WorkingDirectory = @"C:\Users\Asus\",
                Arguments = "docker run --rm -it --hostname my-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management",
                // WindowStyle = ProcessWindowStyle.Hidden
            });

            //ProcessStartInfo proStart = new ProcessStartInfo();
            //Process pro = new Process();
            //proStart.FileName = "cmd.exe";
            //proStart.WorkingDirectory = @"C:\Users\Asus";
            //// string arg = "/c triangle.exe -pq20a0.004 vika.poly";
            //string arg = /*" >" +*/ "docker run --rm -it --hostname my-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management";
            //proStart.Arguments = arg;
            //// proStart.WindowStyle = ProcessWindowStyle.Hidden;
            ////pro.StartInfo = pro;
            ////proStart.
            //Process.Start(proStart);

            //Process.Start(new ProcessStartInfo
            //{
            //    FileName = "cmd.exe",
            //    WorkingDirectory = @"C:\Users\Asus\",
            //    Arguments = "docker run --rm -it --hostname my-rabbit -p 15672:15672 -p 5672:5672 rabbitmq:3-management",
            //   // WindowStyle = ProcessWindowStyle.Hidden
            //});

        }


    }
}
