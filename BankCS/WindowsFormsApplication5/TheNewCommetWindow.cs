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
    public partial class TheNewCommetWindow : Form
    {
        ForumConnection myConnection;
        CurrentForumState CurrentState;
        PostInfo SubjectPost;
        int indexOfTheChoosenPost;
        List<PostInfo> allPost;
        public TheNewCommetWindow(ForumConnection myConnectionP, CurrentForumState State,PostInfo relevatPost)
        {
            InitializeComponent();
            SubjectPost = relevatPost;
            this.myConnection = myConnectionP;
            CurrentState = State;
            textBox1.Text = SubjectPost.msg;
            refreshAllPost();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
      //      this.allPost = myConnection.WatchAllComments(SubjectPost);
       //     this.indexOfTheChoosenPost = listBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myConnection.PublishCommentPost(textBox2.Text, SubjectPost);
            textBox2.Clear();
            refreshAllPost();

        }

        private void refreshAllPost()
        {
            listBox1.Items.Clear();
            this.allPost = myConnection.WatchAllComments(SubjectPost);
            for (int i = 0; i < allPost.Count; i++)
            {
                listBox1.Items.Add(allPost.ElementAt(i).msg + " ( " + allPost.ElementAt(i).owner.fullname + " )");
            }
        }

        private void TheNewCommetWindow_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex >= 0) { 
            TheNewCommetWindow newWind = new TheNewCommetWindow(myConnection, CurrentState, allPost.ElementAt(listBox1.SelectedIndex));
            newWind.Show();
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
                myConnection.deletePost(this.allPost.ElementAt(listBox1.SelectedIndex));
            refreshAllPost();
        }
    }
}
