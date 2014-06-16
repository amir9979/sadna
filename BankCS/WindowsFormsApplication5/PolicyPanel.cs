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
            CurrentState.currentPolicyInfo = myConnection.GetPolicyParam(CurrentState.myForum);

            for (int i = 0; i < CurrentState.currentPolicyInfo.ileg.Count; i++)
            {
                listBox2.Items.Add(this.CurrentState.currentPolicyInfo.ileg.ElementAt(i));
            }
            this.textBox3.Text = Convert.ToString(this.CurrentState.currentPolicyInfo.maxmoth);
            this.textBox2.Text = Convert.ToString(this.CurrentState.currentPolicyInfo.minword);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(this.CurrentState.currentPolicyInfo.ileg.Contains(textBox1.Text))) {
                this.CurrentState.currentPolicyInfo.ileg.Add(textBox1.Text);
               // this.myConnection.UpdatePolicyParams(this.CurrentState.myForum, this.CurrentState.currentPolicyInfo.minword, this.CurrentState.currentPolicyInfo.maxmoth, this.CurrentState.currentPolicyInfo.ileg);
                List<String> ans = CurrentState.currentPolicyInfo.ileg.ToList<String>();
                myConnection.UpdatePolicyParams(CurrentState.myForum, CurrentState.currentPolicyInfo.minword, CurrentState.currentPolicyInfo.maxmoth, CurrentState.currentPolicyInfo.ileg.ToList<String>());
                this.CurrentState.currentPolicyInfo = myConnection.GetPolicyParam(this.CurrentState.myForum);
                for (int i = 0; i < this.CurrentState.currentPolicyInfo.ileg.Count; i++)
                    listBox2.Items.Add(this.CurrentState.currentPolicyInfo.ileg.ElementAt(i));
                }
                else
                   MessageBox.Show("המילה המוזנת קיימת כבר");          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.CurrentState.currentPolicyInfo.maxmoth = Convert.ToInt16(this.textBox3.Text);
            myConnection.UpdatePolicyParams(this.CurrentState.myForum, this.CurrentState.currentPolicyInfo.minword, this.CurrentState.currentPolicyInfo.maxmoth, CurrentState.currentPolicyInfo.ileg.ToList<String>());

        }


        private void הקודם_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.CurrentState.currentPolicyInfo.maxmoth = Convert.ToInt16(this.textBox2.Text);
            myConnection.UpdatePolicyParams(this.CurrentState.myForum, this.CurrentState.currentPolicyInfo.minword, this.CurrentState.currentPolicyInfo.maxmoth, CurrentState.currentPolicyInfo.ileg.ToList<String>());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex > -1)
            {
                this.CurrentState.currentPolicyInfo.ileg.RemoveAt(listBox2.SelectedIndex);
                myConnection.UpdatePolicyParams(this.CurrentState.myForum, this.CurrentState.currentPolicyInfo.minword, this.CurrentState.currentPolicyInfo.maxmoth, CurrentState.currentPolicyInfo.ileg.ToList<String>());
                for (int i = 0; i < this.CurrentState.currentPolicyInfo.ileg.Count; i++)
                    listBox2.Items.Add(this.CurrentState.currentPolicyInfo.ileg.ElementAt(i));
            }
            else
                MessageBox.Show("לא נבחרה מדיניות");
        }
    }
}
