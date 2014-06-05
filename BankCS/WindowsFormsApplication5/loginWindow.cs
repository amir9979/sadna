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
    public partial class loginWindow : Form
    {
        ForumConnection myConnection;
        CurrentForumState CurrentState;
        public loginWindow(ForumConnection myConnectionP, CurrentForumState State)
        {
            this.myConnection = myConnectionP;
            CurrentState = State;

            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //userPreNew userPresentat = new userPreNew(myConnection,CurrentState);
            
            string userName = this.userName.Text;
            string passWord = this.passWord.Text;
            if (myConnection.login(userName, passWord))
            {
                MessageBox.Show(""+userName +"  welcome to "+ CurrentState.myForum.name);
         //       userPresentat.Show();
                this.Close();

                
            }
            else
            {
                MessageBox.Show("not exist username or password");
            }
        }

        private void userName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void passWord_TextChanged(object sender, EventArgs e)
        {

        }

        private void loginWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
