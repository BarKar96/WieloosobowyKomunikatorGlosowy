namespace WieloosobowyKomunikatorGlosowy
{
    partial class Server_IP
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
            this.server_ip_text = new System.Windows.Forms.TextBox();
            this.server_ip_label = new System.Windows.Forms.Label();
            this.connect_button = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // server_ip_text
            // 
            this.server_ip_text.Location = new System.Drawing.Point(120, 63);
            this.server_ip_text.Name = "server_ip_text";
            this.server_ip_text.Size = new System.Drawing.Size(152, 20);
            this.server_ip_text.TabIndex = 5;
            this.server_ip_text.Text = "192.168.0.101";
            // 
            // server_ip_label
            // 
            this.server_ip_label.AutoSize = true;
            this.server_ip_label.Location = new System.Drawing.Point(12, 66);
            this.server_ip_label.Name = "server_ip_label";
            this.server_ip_label.Size = new System.Drawing.Size(87, 13);
            this.server_ip_label.TabIndex = 4;
            this.server_ip_label.Text = "Adres IP serwera";
            // 
            // connect_button
            // 
            this.connect_button.Location = new System.Drawing.Point(197, 210);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(75, 23);
            this.connect_button.TabIndex = 6;
            this.connect_button.Text = "Połącz się";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(15, 210);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(75, 23);
            this.exit.TabIndex = 7;
            this.exit.Text = "Wyjdź";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // Server_IP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.connect_button);
            this.Controls.Add(this.server_ip_text);
            this.Controls.Add(this.server_ip_label);
            this.Name = "Server_IP";
            this.Text = "Komunikator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox server_ip_text;
        private System.Windows.Forms.Label server_ip_label;
        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.Button exit;
    }
}