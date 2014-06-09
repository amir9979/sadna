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
    public partial class ManagerPresentation : Form
    {
        ForumConnection myConnection;
        CurrentForumState CurrentState;
        public ManagerPresentation(ForumConnection myConnectionP, CurrentForumState State)
        {
            CurrentState = State;
            this.myConnection = myConnectionP;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myConnection.BuildForum(textBox1.Text);
            textBox1.Clear();
            listBox1.Items.Clear();

            CurrentState.allForum = myConnection.WatchAllForums();
            for (int i = 0; i < CurrentState.allForum.Count(); i++)
            {
                listBox1.Items.Add(CurrentState.allForum.ElementAt(i).name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // myConnection.promoteMemberToAdmin(listBox1.Text);
            listBox1.Items.Clear();

          
                    ForumInfo ourforum = this.CurrentState.myForum;
                    MemberInfo admin= this.CurrentState.allMembers.ElementAt(listBox2.SelectedIndex);
                    myConnection.promoteMemberToAdmin(admin);
                
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.myConnection.loggout();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.listBox1.Items.Count; i++)
            {
                if (listBox1.GetSelected(i))
                {
                    this.myConnection.CancelForum(CurrentState.allForum.ElementAt(i));
                }
            }
            listBox1.Items.Clear();
            this.CurrentState.allForum = myConnection.WatchAllForums();
            for (int i = 0; i < CurrentState.allForum.Count(); i++)
            {
                listBox1.Items.Add(CurrentState.allForum.ElementAt(i).name);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CurrentState.allForum = myConnection.WatchAllForums();
            for (int i = 0; i < CurrentState.allForum.Count(); i++)
            {
                listBox1.Items.Add(CurrentState.allForum.ElementAt(i).name);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.ClearSelected();
            this.CurrentState.myForum = this.CurrentState.allForum.ElementAt(this.listBox1.SelectedIndex);
            this.CurrentState.allMembers = myConnection.WatchAllMembers(this.CurrentState.myForum);
            for (int i = 0; i < this.CurrentState.allMembers.Count; i++)
            {
                listBox2.Items.Add(this.CurrentState.allMembers.ElementAt(i).fullname);
            }
        }
    }
}
