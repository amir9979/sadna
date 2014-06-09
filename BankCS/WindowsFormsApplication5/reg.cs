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
    public partial class reg : Form
    {
        ForumConnection myConnection;
        CurrentForumState CurrentState;
        public reg(ForumConnection ForumConnectionImp, CurrentForumState State,string forum_name)
        {
            CurrentState = State;
            myConnection = ForumConnectionImp;
            InitializeComponent();
            this.forum.Text = forum_name;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void userName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string userName = this.userName.Text;
            string passWord = this.passWord.Text;
            string mail = this.mail.Text;
            string forum = this.forum.Text;
            string nickName = this.nickName.Text;

            Int64 regConf = this.myConnection.Registration(forum, userName, passWord, mail, nickName);
            if (regConf != -1)
            {
                MessageBox.Show("the Registration succ");
                this.Close();
            }
            else
                MessageBox.Show("the Registration fail");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void reg_Load(object sender, EventArgs e)
        {

        }

        private void mail_TextChanged(object sender, EventArgs e)
        {

        }

        private void forum_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
