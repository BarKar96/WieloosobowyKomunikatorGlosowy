namespace WieloosobowyKomunikatorGlosowy
{
    partial class Register
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
            this.password = new System.Windows.Forms.Label();
            this.text_password = new System.Windows.Forms.TextBox();
            this.text_user = new System.Windows.Forms.TextBox();
            this.user_name = new System.Windows.Forms.Label();
            this.Return = new System.Windows.Forms.Button();
            this.RegisterButton = new System.Windows.Forms.Button();
            this.LabelRepeatPassword = new System.Windows.Forms.Label();
            this.TextRepeatPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // password
            // 
            this.password.AutoSize = true;
            this.password.Location = new System.Drawing.Point(12, 80);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(36, 13);
            this.password.TabIndex = 13;
            this.password.Text = "Hasło";
            // 
            // text_password
            // 
            this.text_password.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.text_password.Location = new System.Drawing.Point(120, 73);
            this.text_password.Name = "text_password";
            this.text_password.PasswordChar = '*';
            this.text_password.Size = new System.Drawing.Size(152, 20);
            this.text_password.TabIndex = 12;
            // 
            // text_user
            // 
            this.text_user.Location = new System.Drawing.Point(120, 32);
            this.text_user.Name = "text_user";
            this.text_user.Size = new System.Drawing.Size(152, 20);
            this.text_user.TabIndex = 11;
            // 
            // user_name
            // 
            this.user_name.AutoSize = true;
            this.user_name.Location = new System.Drawing.Point(12, 35);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(102, 13);
            this.user_name.TabIndex = 10;
            this.user_name.Text = "Nazwa użytkownika";
            // 
            // Return
            // 
            this.Return.Location = new System.Drawing.Point(12, 207);
            this.Return.Name = "Return";
            this.Return.Size = new System.Drawing.Size(75, 23);
            this.Return.TabIndex = 9;
            this.Return.Text = "Wróć";
            this.Return.UseVisualStyleBackColor = true;
            this.Return.Click += new System.EventHandler(this.Return_Click);
            // 
            // RegisterButton
            // 
            this.RegisterButton.Location = new System.Drawing.Point(183, 207);
            this.RegisterButton.Name = "RegisterButton";
            this.RegisterButton.Size = new System.Drawing.Size(89, 23);
            this.RegisterButton.TabIndex = 8;
            this.RegisterButton.Text = "Zarejestruj się";
            this.RegisterButton.UseVisualStyleBackColor = true;
            this.RegisterButton.Click += new System.EventHandler(this.RegisterButton_Click);
            // 
            // LabelRepeatPassword
            // 
            this.LabelRepeatPassword.AutoSize = true;
            this.LabelRepeatPassword.Location = new System.Drawing.Point(12, 128);
            this.LabelRepeatPassword.Name = "LabelRepeatPassword";
            this.LabelRepeatPassword.Size = new System.Drawing.Size(75, 13);
            this.LabelRepeatPassword.TabIndex = 15;
            this.LabelRepeatPassword.Text = "Powtórz hasło";
            // 
            // TextRepeatPassword
            // 
            this.TextRepeatPassword.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TextRepeatPassword.Location = new System.Drawing.Point(120, 121);
            this.TextRepeatPassword.Name = "TextRepeatPassword";
            this.TextRepeatPassword.PasswordChar = '*';
            this.TextRepeatPassword.Size = new System.Drawing.Size(152, 20);
            this.TextRepeatPassword.TabIndex = 14;
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.ControlBox = false;
            this.Controls.Add(this.LabelRepeatPassword);
            this.Controls.Add(this.TextRepeatPassword);
            this.Controls.Add(this.password);
            this.Controls.Add(this.text_password);
            this.Controls.Add(this.text_user);
            this.Controls.Add(this.user_name);
            this.Controls.Add(this.Return);
            this.Controls.Add(this.RegisterButton);
            this.Name = "Register";
            this.Text = "Rejestracja";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label password;
        private System.Windows.Forms.TextBox text_password;
        private System.Windows.Forms.TextBox text_user;
        private System.Windows.Forms.Label user_name;
        private System.Windows.Forms.Button Return;
        private System.Windows.Forms.Button RegisterButton;
        private System.Windows.Forms.Label LabelRepeatPassword;
        private System.Windows.Forms.TextBox TextRepeatPassword;
    }
}