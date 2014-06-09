namespace WindowsFormsApplication5
{
    partial class reg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.passWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.nickName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.forum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(347, 572);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 51);
            this.button1.TabIndex = 19;
            this.button1.Text = "סגור";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(875, 572);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(104, 51);
            this.button6.TabIndex = 14;
            this.button6.Text = "התחבר";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(684, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "שם משתמש";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(684, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "סיסמא";
            // 
            // userName
            // 
            this.userName.Location = new System.Drawing.Point(509, 40);
            this.userName.Multiline = true;
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(144, 34);
            this.userName.TabIndex = 16;
            this.userName.TextChanged += new System.EventHandler(this.userName_TextChanged);
            // 
            // passWord
            // 
            this.passWord.Location = new System.Drawing.Point(509, 120);
            this.passWord.Multiline = true;
            this.passWord.Name = "passWord";
            this.passWord.Size = new System.Drawing.Size(144, 39);
            this.passWord.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(684, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "מייל";
            // 
            // mail
            // 
            this.mail.Location = new System.Drawing.Point(509, 218);
            this.mail.Multiline = true;
            this.mail.Name = "mail";
            this.mail.Size = new System.Drawing.Size(144, 39);
            this.mail.TabIndex = 20;
            this.mail.TextChanged += new System.EventHandler(this.mail_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(684, 323);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "כינוי";
            // 
            // nickName
            // 
            this.nickName.Location = new System.Drawing.Point(509, 308);
            this.nickName.Multiline = true;
            this.nickName.Name = "nickName";
            this.nickName.Size = new System.Drawing.Size(144, 39);
            this.nickName.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(684, 434);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "שם הפורום";
            // 
            // forum
            // 
            this.forum.Location = new System.Drawing.Point(509, 419);
            this.forum.Multiline = true;
            this.forum.Name = "forum";
            this.forum.Size = new System.Drawing.Size(144, 39);
            this.forum.TabIndex = 24;
            this.forum.TextChanged += new System.EventHandler(this.forum_TextChanged);
            // 
            // reg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1263, 685);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.forum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.nickName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mail);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.passWord);
            this.Name = "reg";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.reg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TextBox passWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox nickName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox forum;

    }
}