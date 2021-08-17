using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;//to md5 hash use this 
using System.Data.SqlClient;//to use sql commands use this
using System.Text.RegularExpressions;//to user regex input this
using System.IO;//to use get the file path use this


namespace Encryption
{
    public partial class Form1 : Form
    {
        //////////////////////////////////////
        //https://www.youtube.com/watch?v=da5GSeJV9qE - hashing, refer this video
        string newpwd;
        static string path = Path.GetFullPath(Environment.CurrentDirectory);
        static string databasename = "pwddb.mdf";
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=" + path + @"\" + databasename + ";Integrated Security=True");
        //SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\USERNAME\Documents\Visual Studio 2013\Projects\C#\Encryption\Encryption\pwddb.mdf;Integrated Security=True");
        /////////////////////////////////////
        public Form1()
        {
            InitializeComponent();
        }

        static string Encrypt(string value)//encrypting function
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(utf8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
      
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string encpwd = Encrypt(input2.Text);//hashing input2 text
               

                string searchqry = "Select * from login where userid= " + input.Text + " ";
                SqlCommand cmd = new SqlCommand(searchqry, con);
                con.Open();
                SqlDataReader r = cmd.ExecuteReader();
                

                while (r.Read())
                {
                 newpwd=r[1].ToString();
                }

              
                /*without this the data type in table ex: varchar(100) say your pwd is vishwa 
                its 6 char right the rest of 94 space you left will write spaces.
                so here we are removing those spaces
                with spaces if statement won't work*/

                newpwd = Regex.Replace(newpwd, @"\s", "");
                

                if (encpwd == newpwd)
                {
                    //input2.Text = newpwd;
                    MessageBox.Show("Login Successfull!");
                }
                else
                {
                    MessageBox.Show("Login Failed!");
                }

            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error Occured" + ex.ToString());
            }
            finally
            {
                con.Close();
            }
   
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string encpwd = Encrypt(input2.Text);//hashing the input2 text

            string qry = "INSERT INTO login VALUES(" + input.Text + ",'" + encpwd + "')";
            SqlCommand cmd = new SqlCommand(qry, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Inserted Successfully!");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error Occured" + ex.ToString());
            }
            finally
            {
                con.Close();
            }


        }

        private void showbtn_Click(object sender, EventArgs e)
        {
            string qry = "SELECT * FROM login";

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(qry,con);
                DataSet ds = new DataSet();
                da.Fill(ds, "login");
                dataGridView1.DataSource = ds.Tables["login"];
            }
            catch(SqlException Ex)
            {
                MessageBox.Show("Error Occured" + Ex.ToString());
            }
        }

      
    }
}
