namespace WieloosobowyKomunikatorGlosowy
{
    partial class Login
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
            this.log = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.user_name = new System.Windows.Forms.Label();
            this.text_user = new System.Windows.Forms.TextBox();
            this.password = new System.Windows.Forms.Label();
            this.label_register = new System.Windows.Forms.Label();
            this.register = new System.Windows.Forms.LinkLabel();
            this.text_password = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // log
            // 
            this.log.Location = new System.Drawing.Point(197, 221);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(75, 23);
            this.log.TabIndex = 0;
            this.log.Text = "Zaloguj się";
            this.log.UseVisualStyleBackColor = true;
            this.log.Click += new System.EventHandler(this.log_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(12, 221);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(75, 23);
            this.exit.TabIndex = 1;
            this.exit.Text = "Powrót";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // user_name
            // 
            this.user_name.AutoSize = true;
            this.user_name.Location = new System.Drawing.Point(12, 49);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(102, 13);
            this.user_name.TabIndex = 2;
            this.user_name.Text = "Nazwa użytkownika";
            // 
            // text_user
            // 
            this.text_user.Location = new System.Drawing.Point(120, 46);
            this.text_user.Name = "text_user";
            this.text_user.Size = new System.Drawing.Size(152, 20);
            this.text_user.TabIndex = 3;
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Location = new System.Drawing.Point(12, 94);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(36, 13);
            this.password.TabIndex = 5;
            this.password.Text = "Hasło";
            // 
            // label_register
            // 
            this.label_register.AutoSize = true;
            this.label_register.Location = new System.Drawing.Point(12, 136);
            this.label_register.Name = "label_register";
            this.label_register.Size = new System.Drawing.Size(86, 13);
            this.label_register.TabIndex = 6;
            this.label_register.Text = "Nie masz konta?";
            // 
            // register
            // 
            this.register.AutoSize = true;
            this.register.Location = new System.Drawing.Point(120, 136);
            this.register.Name = "register";
            this.register.Size = new System.Drawing.Size(72, 13);
            this.register.TabIndex = 7;
            this.register.TabStop = true;
            this.register.Text = "Zarejestruj się";
            this.register.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.register_LinkClicked);
            // 
            // text_password
            // 
            this.text_password.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.text_password.Location = new System.Drawing.Point(120, 87);
            this.text_password.Name = "text_password";
            this.text_password.PasswordChar = '*';
            this.text_password.Size = new System.Drawing.Size(152, 20);
            this.text_password.TabIndex = 4;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.register);
            this.Controls.Add(this.label_register);
            this.Controls.Add(this.password);
            this.Controls.Add(this.text_password);
            this.Controls.Add(this.text_user);
            this.Controls.Add(this.user_name);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.log);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button log;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Label user_name;
        private System.Windows.Forms.TextBox text_user;
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.Label label_register;
        private System.Windows.Forms.LinkLabel register;
        private System.Windows.Forms.TextBox text_password;
    }
}