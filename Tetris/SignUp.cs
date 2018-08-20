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
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void SignUp_Load(object sender, System.EventArgs e)
        {
            lblErrorId.Hide();
            lblErrorName.Hide();
            lblErrorPass.Hide();
            lblErrorCell.Hide();
        }

        private void brnBack_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            LogIn obj = new LogIn();
            obj.Show();
        }

        private void btnSubmit_Click(object sender, System.EventArgs e)
        {
            String signUp_id = "";
            String signUp_name = "";
            String signUp_password = "";
            String signUp_cellNo = "";
            int flag = 0;
            signUp_id = txtID.Text;
            signUp_password = txtPassword.Text;
            signUp_name = txtName.Text;
            signUp_cellNo = txtCellNo.Text;
            if (signUp_id == "")
            {
                lblErrorId.Show();
                flag = 0;
            }
            else
            {
                lblErrorId.Hide();
                flag = 1;
            }
            if (signUp_password == "")
            {
                lblErrorPass.Show();
                flag = 0;
            }
            else
            {
                lblErrorPass.Hide();
                flag = 1;
            }
            if (signUp_name == "")
            {
                lblErrorName.Show();
                flag = 0;
            }
            else
            {
                lblErrorName.Hide();
                flag = 1;
            }
            if (signUp_cellNo == "")
            {
                lblErrorCell.Show();
                flag = 0;
            }
            else
            {
                lblErrorCell.Hide();
                flag = 1;
            }
            if (flag == 1)
            {
                try
                {
                    string OracleServer = "Data Source=(DESCRIPTION=" + "(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(Port=1521))"
                            + "(CONNECT_Data=(SERVICE_NAME=XE)));" + "User Id=TetrisPlayer;Password=123";
                    OracleConnection conn = new OracleConnection(OracleServer);
                    conn.Open();
                    string s = "insert into SignUp values (" + signUp_id + ",'" + signUp_name + "','" + signUp_password + "','" + signUp_cellNo + "')";
                    OracleCommand oc = new OracleCommand(s, conn);
                    oc.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Registered Succeessfully.", "Information!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Open();
                    string query = "insert into LogIn values ("+signUp_id+",'"+ signUp_password+"')";
                    OracleCommand command = new OracleCommand(query, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Please fill all credentials.", "Error!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}