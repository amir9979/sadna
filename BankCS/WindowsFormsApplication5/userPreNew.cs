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
    public partial class userPreNew : Form
    {
        ForumConnection myConnection;
        List<PostInfo> allThreads;
        CurrentForumState CurrentState;
        public userPreNew(ForumConnection myConnectionP, CurrentForumState State)
        {
            CurrentState = State;
            int currentTread;
            InitializeComponent();
            this.myConnection = myConnectionP;
            this.allThreads = this.myConnection.WatchAllThreads(CurrentState.currentSubForumInfo);
            for (int i = 0; i < allThreads.Count; i++)
            {
                listBox2.Items.Add(allThreads.ElementAt(i).msg);
            }


        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PostInfo> AllComments = myConnection.WatchAllComments(CurrentState.currentPostInfo);
            TheNewCommetWindow NewCommetWindow = new TheNewCommetWindow(myConnection, this.CurrentState, AllComments.ElementAt(listBox3.SelectedIndex));
            NewCommetWindow.Show();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i=0;
            listBox3.Items.Clear();
            this.allThreads = this.myConnection.WatchAllThreads(CurrentState.currentSubForumInfo);
            this.CurrentState.currentPostInfo = allThreads.ElementAt(listBox2.SelectedIndex);
            List<PostInfo> AllComments = myConnection.WatchAllComments(this.CurrentState.currentPostInfo);
            for (i = 0; i < AllComments.Count; i++)
            {
                listBox3.Items.Add(AllComments.ElementAt(i).msg);
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void logout_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            List<PostInfo> replys = new List<PostInfo>();
            for ( i = 0; i < listBox2.Items.Count; i++)
                if (listBox2.GetSelected(i))
                {
              //      replys = myConnection.WatchAllComments()
                    /// ThreadsWindow.Show();
                    break;
                }
           // for (i = 0; i < threads.Count; i++)
          //  {
         //       listBox3.Items.Add(threads.ElementAt(i).
         //   }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<PostInfo> AllComments;
            if (this.myConnection.PublishCommentPost(textBox1.Text, CurrentState.currentPostInfo))
            {
                this.listBox3.Items.Clear();
                AllComments = myConnection.WatchAllComments(this.CurrentState.currentPostInfo);
                for (int i = 0; i < AllComments.Count; i++)
                {
                    listBox3.Items.Add(AllComments.ElementAt(i).msg);
                }
            }
            else
                MessageBox.Show("לכאורה אינך מחובר");
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void userPreNew_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (this.myConnection.PublishNewThread(textBox1.Text, this.CurrentState.currentSubForumInfo))
            {
                listBox2.Items.Clear();
                this.allThreads = this.myConnection.WatchAllThreads(CurrentState.currentSubForumInfo);
                for (int i = 0; i < allThreads.Count; i++)
                {
                    listBox2.Items.Add(allThreads.ElementAt(i).msg);
                }
                textBox1.Clear();
            }
            else
                MessageBox.Show("לכאורה אינך מחובר");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int ind = listBox2.SelectedIndex;
            if (ind >= 0)
            {
                PostInfo p = CurrentState.currentPostInfo;
                myConnection.deletePost(p);
                CurrentState.currentPostInfo = null;
                listBox3.Items.Clear();
                this.allThreads = myConnection.WatchAllThreads(this.CurrentState.currentSubForumInfo);
                for (int i = 0; i < this.allThreads.Count; i++)
                    listBox2.Items.Add(allThreads.ElementAt(i));

            }
        }
    }
}
