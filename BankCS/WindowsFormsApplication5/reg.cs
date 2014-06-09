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
                this.panel1.Visible = true;
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


        private void button2_Click(object sender, EventArgs e)
        {
            myConnection.entry(this.forum.Text);
            string Temp = textBox1.Text;
            Int64 IntTemp = Convert.ToInt64(Temp);
            if (myConnection.EmailConfirm(IntTemp,this.userName.Text))
            {
                MessageBox.Show("ברכותי הינך חבר רשום ומאושר");
                panel1.Visible = false;
            }
            else
                MessageBox.Show("הקוד שגוי אנא נסה בשנית");
        }

        private void panel1_Paint(object sender, PaintEventArgs e){}

        private void forum_TextChanged(object sender, EventArgs e)

        {

        }
    }
}
