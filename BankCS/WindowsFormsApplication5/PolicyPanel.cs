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
    public partial class PolicyPanel : Form
    {
        ForumConnection myConnection;
        CurrentForumState CurrentState;
        public PolicyPanel(ForumConnection myConnectionP, CurrentForumState State)
        {
            CurrentState = State;
            this.myConnection = myConnectionP;
            InitializeComponent();

            for (int i = 0; i < this.CurrentState.allPolicy.Count; i++)
            {
                listBox2.Items.Add(this.CurrentState.allPolicy.ElementAt(i));
            }
            this.textBox3.Text = Convert.ToString(this.CurrentState.vaildPass);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.CurrentState.allPolicy.Contains(textBox1.Text))
            {
                this.CurrentState.allPolicy.Add(textBox1.Text);
                listBox2.Items.Add(textBox1.Text);
            }
            else
                MessageBox.Show("המילה המוזנת קיימת כבר");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.CurrentState.vaildPass = Convert.ToInt16(this.textBox3.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // this.myConnection.upDatePolicy(this.CurrentState.myForum, CurrentState.hituch, CurrentState.vaildPass, CurrentState.allPolicy);
        }

        private void הקודם_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.CurrentState.hituch =Convert.ToInt16(this.textBox2.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex > -1)
            {
                this.CurrentState.allPolicy.RemoveAt(listBox2.SelectedIndex);
            }
            else
                MessageBox.Show("לא נבחרה מדיניות");
        }
    }
}
