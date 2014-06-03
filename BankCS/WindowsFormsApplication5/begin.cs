using client.Network;
using DataTypes;
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
    public partial class Form1 : Form
    {
        ForumConnection myConnection;
        CurrentForumState CurrentState;
        public Form1(ForumConnection myConnectionP, CurrentForumState State)
        {
            myConnection = myConnectionP;
            CurrentState = State;
            InitializeComponent();
            try{
                myConnection.connect();
            }
            catch(Exception e){
                MessageBox.Show(e.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            string myText="";
            for (i=0; i<listBox2.Items.Count;i++){
                 if (listBox2.GetSelected(i))
                {
                    myText = (string)listBox2.Items[i];
                     break;
                 }
            }
            if (myConnection.entry(myText))
            {
                List<ForumInfo> ForumInfoList = myConnection.WatchAllForums();
                CurrentState.myForum = ForumInfoList.ElementAt(i);
                panel1.Visible = true;
            }
            else
            {
                MessageBox.Show("cant connet to " + myText);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            List<SubForumInfo> SubForumList = myConnection.WatchAllSubForum();
            for (int i=0;i<SubForumList.Count;i++)
                listBox1.Items.Add(SubForumList.ElementAt(i).Name);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void userName_TextChanged(object sender, EventArgs e)
        {

        }


        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            loginWindow loginForm = new loginWindow(myConnection, CurrentState);
            ///this.Close();
            loginForm.Show();
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            reg regWindows = new reg(myConnection,CurrentState);
            regWindows.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ManagerLogin managerLogin = new ManagerLogin(myConnection, CurrentState);
            managerLogin.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            List<ForumInfo> ForumInfoList = myConnection.WatchAllForums();
            for (int i = 0; i < ForumInfoList.Count; i++)
            {
                listBox2.Items.Add(ForumInfoList.ElementAt(i).name);
            }
        }

        private void הבא_Click(object sender, EventArgs e)
        {

            int i;
            string myText = "";
            for (i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i))
                {
                    myText = (string)listBox1.Items[i];
                    break;
                }
            }
            this.CurrentState.currentSubForumInfo= myConnection.WatchAllSubForum().ElementAt(i);
         //   myConnection.currentSubForumInfo = myConnection.WatchAllSubForum().ElementAt(i);

            userPreNew userPrenew = new userPreNew(myConnection,CurrentState);
            userPrenew.Show();



        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ManagerLogin Managerlogin = new ManagerLogin(this.myConnection,CurrentState);
            Managerlogin.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }




    }
}
