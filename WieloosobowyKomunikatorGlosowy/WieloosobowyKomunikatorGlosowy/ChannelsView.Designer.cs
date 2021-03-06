﻿namespace WieloosobowyKomunikatorGlosowy
{
    partial class ChannelsView
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.logout_button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Nazwa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Liczba_osob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Opis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Haslo = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.mute_button = new System.Windows.Forms.Button();
            this.join_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_endCall = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lb_UserList = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // logout_button
            // 
            this.logout_button.Location = new System.Drawing.Point(285, 412);
            this.logout_button.Name = "logout_button";
            this.logout_button.Size = new System.Drawing.Size(75, 35);
            this.logout_button.TabIndex = 0;
            this.logout_button.Text = "Wyloguj";
            this.logout_button.UseVisualStyleBackColor = true;
            this.logout_button.Click += new System.EventHandler(this.logout_button_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 394);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista dostępnych kanałów";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nazwa,
            this.Liczba_osob,
            this.Opis,
            this.Haslo});
            this.dataGridView1.Location = new System.Drawing.Point(0, 19);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(344, 369);
            this.dataGridView1.TabIndex = 0;
            // 
            // Nazwa
            // 
            this.Nazwa.HeaderText = "Nazwa kanału";
            this.Nazwa.Name = "Nazwa";
            this.Nazwa.ReadOnly = true;
            // 
            // Liczba_osob
            // 
            this.Liczba_osob.HeaderText = "Liczba osób";
            this.Liczba_osob.Name = "Liczba_osob";
            this.Liczba_osob.ReadOnly = true;
            this.Liczba_osob.Width = 50;
            // 
            // Opis
            // 
            this.Opis.HeaderText = "Opis";
            this.Opis.Name = "Opis";
            this.Opis.ReadOnly = true;
            this.Opis.Width = 150;
            // 
            // Haslo
            // 
            this.Haslo.FillWeight = 40F;
            this.Haslo.HeaderText = "Hasło";
            this.Haslo.Name = "Haslo";
            this.Haslo.ReadOnly = true;
            this.Haslo.Width = 40;
            // 
            // mute_button
            // 
            this.mute_button.Location = new System.Drawing.Point(146, 412);
            this.mute_button.Name = "mute_button";
            this.mute_button.Size = new System.Drawing.Size(75, 37);
            this.mute_button.TabIndex = 2;
            this.mute_button.Text = "Wycisz aplikację";
            this.mute_button.UseVisualStyleBackColor = true;
            this.mute_button.Click += new System.EventHandler(this.mute_button_Click);
            // 
            // join_button
            // 
            this.join_button.Location = new System.Drawing.Point(12, 412);
            this.join_button.Name = "join_button";
            this.join_button.Size = new System.Drawing.Size(75, 37);
            this.join_button.TabIndex = 3;
            this.join_button.Text = "Dołącz do kanału";
            this.join_button.UseVisualStyleBackColor = true;
            this.join_button.Click += new System.EventHandler(this.join_button_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(549, 412);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 37);
            this.button2.TabIndex = 6;
            this.button2.Text = "Wyjdź z aplikacji";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_endCall
            // 
            this.btn_endCall.Location = new System.Drawing.Point(12, 412);
            this.btn_endCall.Name = "btn_endCall";
            this.btn_endCall.Size = new System.Drawing.Size(75, 37);
            this.btn_endCall.TabIndex = 7;
            this.btn_endCall.Text = "Wyjdź z kanału";
            this.btn_endCall.UseVisualStyleBackColor = true;
            this.btn_endCall.Click += new System.EventHandler(this.btn_endCall_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(393, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nazwa kanału:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(498, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(394, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Opis kanału:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(393, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Osoby na kanale:";
            // 
            // lb_UserList
            // 
            this.lb_UserList.FormattingEnabled = true;
            this.lb_UserList.Location = new System.Drawing.Point(396, 132);
            this.lb_UserList.Name = "lb_UserList";
            this.lb_UserList.Size = new System.Drawing.Size(117, 173);
            this.lb_UserList.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(498, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 13;
            // 
            // ChannelsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 459);
            this.ControlBox = false;
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lb_UserList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_endCall);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.join_button);
            this.Controls.Add(this.mute_button);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.logout_button);
            this.Name = "ChannelsView";
            this.Text = "Komunikator";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button logout_button;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button mute_button;
        private System.Windows.Forms.Button join_button;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nazwa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Liczba_osob;
        private System.Windows.Forms.DataGridViewTextBoxColumn Opis;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Haslo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_endCall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lb_UserList;
        private System.Windows.Forms.Label label5;
    }
}

