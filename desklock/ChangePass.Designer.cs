namespace desklock
{
    partial class ChangePass
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.newPassBox4 = new System.Windows.Forms.TextBox();
            this.newPassBox3 = new System.Windows.Forms.TextBox();
            this.oldPassBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.changePassBut = new System.Windows.Forms.Button();
            this.langLab1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Введите еще раз:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Новый пароль:";
            // 
            // newPassBox4
            // 
            this.newPassBox4.Location = new System.Drawing.Point(8, 96);
            this.newPassBox4.Name = "newPassBox4";
            this.newPassBox4.PasswordChar = '*';
            this.newPassBox4.Size = new System.Drawing.Size(165, 20);
            this.newPassBox4.TabIndex = 2;
            this.newPassBox4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.newPassBox4_KeyPress);
            // 
            // newPassBox3
            // 
            this.newPassBox3.Location = new System.Drawing.Point(8, 57);
            this.newPassBox3.Name = "newPassBox3";
            this.newPassBox3.PasswordChar = '*';
            this.newPassBox3.Size = new System.Drawing.Size(165, 20);
            this.newPassBox3.TabIndex = 1;
            this.newPassBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.newPassBox3_KeyPress);
            // 
            // oldPassBox
            // 
            this.oldPassBox.Location = new System.Drawing.Point(8, 18);
            this.oldPassBox.Name = "oldPassBox";
            this.oldPassBox.PasswordChar = '*';
            this.oldPassBox.Size = new System.Drawing.Size(165, 20);
            this.oldPassBox.TabIndex = 0;
            this.oldPassBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.oldPass_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Старый пароль:";
            // 
            // changePassBut
            // 
            this.changePassBut.Location = new System.Drawing.Point(38, 122);
            this.changePassBut.Name = "changePassBut";
            this.changePassBut.Size = new System.Drawing.Size(75, 23);
            this.changePassBut.TabIndex = 3;
            this.changePassBut.Text = "Изменить";
            this.changePassBut.UseVisualStyleBackColor = true;
            this.changePassBut.Click += new System.EventHandler(this.changePassBut_Click);
            // 
            // langLab1
            // 
            this.langLab1.AutoSize = true;
            this.langLab1.BackColor = System.Drawing.SystemColors.Control;
            this.langLab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.langLab1.Location = new System.Drawing.Point(119, 125);
            this.langLab1.Name = "langLab1";
            this.langLab1.Size = new System.Drawing.Size(24, 15);
            this.langLab1.TabIndex = 6;
            this.langLab1.Text = "ER";
            // 
            // ChangePass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(178, 152);
            this.Controls.Add(this.langLab1);
            this.Controls.Add(this.changePassBut);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.newPassBox4);
            this.Controls.Add(this.oldPassBox);
            this.Controls.Add(this.newPassBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangePass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изменение пароль";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox newPassBox4;
        private System.Windows.Forms.TextBox newPassBox3;
        private System.Windows.Forms.TextBox oldPassBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button changePassBut;
        private System.Windows.Forms.Label langLab1;
    }
}