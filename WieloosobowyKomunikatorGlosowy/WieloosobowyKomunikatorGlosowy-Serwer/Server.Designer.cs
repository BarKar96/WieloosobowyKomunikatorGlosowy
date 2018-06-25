namespace WieloosobowyKomunikatorGlosowy_Serwer
{
    partial class Server
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.description = new System.Windows.Forms.Label();
            this.description_box = new System.Windows.Forms.TextBox();
            this.name_box = new System.Windows.Forms.TextBox();
            this.channel_name = new System.Windows.Forms.Label();
            this.password = new System.Windows.Forms.Label();
            this.password_box = new System.Windows.Forms.TextBox();
            this.add_channel = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(37, 25);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(194, 160);
            this.listBox1.TabIndex = 0;
            // 
            // description
            // 
            this.description.AutoSize = true;
            this.description.Location = new System.Drawing.Point(258, 69);
            this.description.Name = "description";
            this.description.Size = new System.Drawing.Size(28, 13);
            this.description.TabIndex = 9;
            this.description.Text = "Opis";
            // 
            // description_box
            // 
            this.description_box.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.description_box.Location = new System.Drawing.Point(366, 66);
            this.description_box.Name = "description_box";
            this.description_box.Size = new System.Drawing.Size(152, 20);
            this.description_box.TabIndex = 8;
            // 
            // name_box
            // 
            this.name_box.Location = new System.Drawing.Point(366, 25);
            this.name_box.Name = "name_box";
            this.name_box.Size = new System.Drawing.Size(152, 20);
            this.name_box.TabIndex = 7;
            // 
            // channel_name
            // 
            this.channel_name.AutoSize = true;
            this.channel_name.Location = new System.Drawing.Point(258, 28);
            this.channel_name.Name = "channel_name";
            this.channel_name.Size = new System.Drawing.Size(77, 13);
            this.channel_name.TabIndex = 6;
            this.channel_name.Text = "Nazwa kanału";
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Location = new System.Drawing.Point(258, 113);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(36, 13);
            this.password.TabIndex = 11;
            this.password.Text = "Hasło";
            // 
            // password_box
            // 
            this.password_box.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.password_box.Location = new System.Drawing.Point(366, 106);
            this.password_box.Name = "password_box";
            this.password_box.PasswordChar = '*';
            this.password_box.Size = new System.Drawing.Size(152, 20);
            this.password_box.TabIndex = 10;
            // 
            // add_channel
            // 
            this.add_channel.Location = new System.Drawing.Point(443, 162);
            this.add_channel.Name = "add_channel";
            this.add_channel.Size = new System.Drawing.Size(75, 23);
            this.add_channel.TabIndex = 12;
            this.add_channel.Text = "Dodaj kanał";
            this.add_channel.UseVisualStyleBackColor = true;
            this.add_channel.Click += new System.EventHandler(this.add_channel_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(260, 162);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 13;
            this.deleteButton.Text = "Usuń kanał";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 223);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.add_channel);
            this.Controls.Add(this.password);
            this.Controls.Add(this.password_box);
            this.Controls.Add(this.description);
            this.Controls.Add(this.description_box);
            this.Controls.Add(this.name_box);
            this.Controls.Add(this.channel_name);
            this.Controls.Add(this.listBox1);
            this.Name = "Server";
            this.Text = "Wieloosobowy komunikator głosowy - serwer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label description;
        private System.Windows.Forms.TextBox description_box;
        private System.Windows.Forms.TextBox name_box;
        private System.Windows.Forms.Label channel_name;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.TextBox password_box;
        private System.Windows.Forms.Button add_channel;
        private System.Windows.Forms.Button deleteButton;
    }
}

