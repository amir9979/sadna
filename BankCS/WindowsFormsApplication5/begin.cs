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
            try
            {
                myConnection.connect();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i;
            string myText = "";
            for (i = 0; i < listBox2.Items.Count; i++)
            {
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
            this.CurrentState.allSubForum = myConnection.WatchAllSubForum();
            listBox1.Items.Clear();
            //   List<SubForumInfo> SubForumList = myConnection.WatchAllSubForum();
            for (int i = 0; i < this.CurrentState.allSubForum.Count; i++)
                listBox1.Items.Add(this.CurrentState.allSubForum.ElementAt(i).Name);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Temp = listBox1.SelectedIndex;
            this.CurrentState.currentSubForumInfo = this.CurrentState.allSubForum.ElementAt(Temp);
            this.CurrentState.allMembers = myConnection.WatchAllMembers(this.CurrentState.myForum);
            this.listBox3.Items.Clear();
            for (int i = 0; i < CurrentState.allMembers.Count; i++)
                listBox3.Items.Add(this.CurrentState.allMembers.ElementAt(i).fullname);

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
            int i;
            string myText = "";
            for (i = 0; i < listBox2.Items.Count; i++)
            {
                if (listBox2.GetSelected(i))
                {
                    myText = (string)listBox2.Items[i];
                    break;
                }
            }
            reg regWindows = new reg(myConnection, CurrentState, myText);
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

            List<SubForumInfo> allTreadsTemp = myConnection.WatchAllSubForum();
            this.CurrentState.currentSubForumInfo = allTreadsTemp.ElementAt(i);
            //   myConnection.currentSubForumInfo = myConnection.WatchAllSubForum().ElementAt(i);

            userPreNew userPrenew = new userPreNew(myConnection, CurrentState);
            userPrenew.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ManagerLogin Managerlogin = new ManagerLogin(this.myConnection, CurrentState);
            Managerlogin.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            myConnection.AddNewSubForum(textBox1.Text, new MemberInfo { id = Int2Guid(-1) });
            this.CurrentState.allSubForum = this.myConnection.WatchAllSubForum();
            textBox1.Clear(); listBox1.Items.Clear();

            for (int i = 0; i < this.CurrentState.allSubForum.Count; i++)
                listBox1.Items.Add(this.CurrentState.allSubForum.ElementAt(i).Name);

        }

        private static Guid Int2Guid(int value)
        {
            byte[] bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            return new Guid(bytes);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            loginWindow loginWindowForMember = new loginWindow(myConnection, CurrentState);
            loginWindowForMember.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            myConnection.loggout();
            MessageBox.Show("logout succ");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox3.SelectedIndex >= 0)
            {
                if (myConnection.promoteMemberToModerator(this.CurrentState.allMembers.ElementAt(listBox3.SelectedIndex), this.CurrentState.currentSubForumInfo))
                    MessageBox.Show("promoteMemberToModerator succ");
                else
                    MessageBox.Show("promoteMemberToModerator fail");
            }
            else
                MessageBox.Show("אנא בחר תת-פורום");
        }
    }
}
