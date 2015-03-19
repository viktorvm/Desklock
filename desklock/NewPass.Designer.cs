namespace desklock
{
    partial class NewPass
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.newPassBox2 = new System.Windows.Forms.TextBox();
            this.newPassBox1 = new System.Windows.Forms.TextBox();
            this.setPassBut = new System.Windows.Forms.Button();
            this.langLab1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Новый пароль:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Введите еще раз:";
            // 
            // newPassBox2
            // 
            this.newPassBox2.Location = new System.Drawing.Point(7, 59);
            this.newPassBox2.Name = "newPassBox2";
            this.newPassBox2.PasswordChar = '*';
            this.newPassBox2.Size = new System.Drawing.Size(165, 20);
            this.newPassBox2.TabIndex = 1;
            this.newPassBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.newPassBox2_KeyPress);
            // 
            // newPassBox1
            // 
            this.newPassBox1.Location = new System.Drawing.Point(7, 20);
            this.newPassBox1.Name = "newPassBox1";
            this.newPassBox1.PasswordChar = '*';
            this.newPassBox1.Size = new System.Drawing.Size(165, 20);
            this.newPassBox1.TabIndex = 0;
            this.newPassBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.newPassBox1_KeyPress);
            // 
            // setPassBut
            // 
            this.setPassBut.Location = new System.Drawing.Point(36, 85);
            this.setPassBut.Name = "setPassBut";
            this.setPassBut.Size = new System.Drawing.Size(75, 23);
            this.setPassBut.TabIndex = 2;
            this.setPassBut.Text = "Задать";
            this.setPassBut.UseVisualStyleBackColor = true;
            this.setPassBut.Click += new System.EventHandler(this.setPassBut_Click);
            // 
            // langLab1
            // 
            this.langLab1.AutoSize = true;
            this.langLab1.BackColor = System.Drawing.SystemColors.Control;
            this.langLab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.langLab1.Location = new System.Drawing.Point(117, 88);
            this.langLab1.Name = "langLab1";
            this.langLab1.Size = new System.Drawing.Size(24, 15);
            this.langLab1.TabIndex = 3;
            this.langLab1.Text = "ER";
            // 
            // NewPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(178, 115);
            this.Controls.Add(this.langLab1);
            this.Controls.Add(this.setPassBut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newPassBox2);
            this.Controls.Add(this.newPassBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewPass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Задание пароля";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox newPassBox2;
        private System.Windows.Forms.TextBox newPassBox1;
        private System.Windows.Forms.Button setPassBut;
        private System.Windows.Forms.Label langLab1;
    }
}