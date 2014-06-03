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
        PostInfo ChoosenPost;
        public TheNewCommetWindow(ForumConnection myConnectionP, CurrentForumState State,PostInfo relevatPost)
        {
            InitializeComponent();
            ChoosenPost = relevatPost;
            this.myConnection = myConnectionP;
            CurrentState = State;
            textBox1.Text = ChoosenPost.msg;
            ShowAllPost();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<PostInfo> allPost =myConnection.WatchAllComments(ChoosenPost);
            TheNewCommetWindow newWind = new TheNewCommetWindow(myConnection, CurrentState, allPost.ElementAt(listBox1.SelectedIndex));
            newWind.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myConnection.PublishCommentPost(textBox2.Text, ChoosenPost);
            textBox2.Clear();
            ShowAllPost();

        }

        private void ShowAllPost()
        {
            listBox1.Items.Clear();
            List<PostInfo> allPosts =myConnection.WatchAllComments(ChoosenPost);
            for (int i=0; i < allPosts.Count; i++)
            {
                listBox1.Items.Add(allPosts.ElementAt(i).msg);
            }
        }
    }
}
