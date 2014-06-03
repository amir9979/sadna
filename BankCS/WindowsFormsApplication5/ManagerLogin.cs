using client.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApplication5
{
    public partial class ManagerLogin : Form
    {
        ForumConnection myConnection;
        CurrentForumState CurrentState;
        public ManagerLogin(ForumConnection myConnectionP, CurrentForumState State)
        {
            CurrentState = State;
            this.myConnection = myConnectionP;
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ManagerPresentation managerPresentation = new ManagerPresentation(myConnection , this.CurrentState);
            if (myConnection.SPlogin(userName.Text, passWord.Text))
            {
                managerPresentation.Show();
                this.Close();
            }
            else
                MessageBox.Show("wrong pass");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void userName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
