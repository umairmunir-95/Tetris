using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;
namespace Tetris
{
    public partial class LogIn : Form
    {
        public  String id;
        public  String password;
         
        public LogIn()
        {
            InitializeComponent();
            id = "";
            password = "";
        }

        private void LogIn_Load(object sender, System.EventArgs e)
        {
            lblErrorPassword.Hide();
            lblErrorId.Hide();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            String allIDs = "";
            String allPasswords = "";
            try
            {
                string OracleServer = "Data Source=(DESCRIPTION=" + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(Port=1521))"
                    + "(CONNECT_Data=(SERVICE_NAME=XE)));" + "User Id=TetrisPlayer;Password=123";
                OracleConnection conn = new OracleConnection(OracleServer);
                conn.Open();
                string s = "select * from LogIn";
                OracleCommand oc = new OracleCommand(s, conn);
                OracleDataReader dr = oc.ExecuteReader();
                while (dr.Read())
                {
                    allIDs+=dr["ID"].ToString()+" ";
                    allPasswords+=dr["PASSWORD"].ToString()+" ";
                }
                dr.Close();
                conn.Close();
                id = txtID.Text;
                password = txtPassword.Text;
                string[] array1 = allIDs.Split(' ');
                string[] array2 = allPasswords.Split(' ');
                for (int i = 0; i < array1.Length; i++)
                {
                    if (array1[i].Contains(id))
                    {
                        lblErrorId.Hide();
                        if (array2[i].Contains(password))
                        {
                            MessageBox.Show("Login Successfully");
                            lblErrorId.Hide();
                            lblErrorPassword.Hide();
                        }
                        else
                        {
                            lblErrorPassword.Show();
                        }
                    }

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Error in DB connection.");
            }
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignUp_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            SignUp obj = new SignUp();
            obj.Show();
        }
    }
}
