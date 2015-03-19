namespace desklock
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.totalBut = new System.Windows.Forms.Button();
            this.offBut = new System.Windows.Forms.Button();
            this.onBut = new System.Windows.Forms.Button();
            this.passBut = new System.Windows.Forms.Button();
            this.logInBut = new System.Windows.Forms.Button();
            this.autoStartBut = new System.Windows.Forms.Button();
            this.restartBut = new System.Windows.Forms.Button();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // totalBut
            // 
            this.totalBut.Location = new System.Drawing.Point(12, 125);
            this.totalBut.Name = "totalBut";
            this.totalBut.Size = new System.Drawing.Size(133, 23);
            this.totalBut.TabIndex = 4;
            this.totalBut.Text = "Total Commander";
            this.totalBut.UseVisualStyleBackColor = true;
            this.totalBut.Click += new System.EventHandler(this.totalBut_Click);
            // 
            // offBut
            // 
            this.offBut.Location = new System.Drawing.Point(12, 67);
            this.offBut.Name = "offBut";
            this.offBut.Size = new System.Drawing.Size(133, 23);
            this.offBut.TabIndex = 2;
            this.offBut.Text = "Разблокировать";
            this.offBut.UseVisualStyleBackColor = true;
            this.offBut.Click += new System.EventHandler(this.offBut_Click);
            // 
            // onBut
            // 
            this.onBut.Location = new System.Drawing.Point(12, 38);
            this.onBut.Name = "onBut";
            this.onBut.Size = new System.Drawing.Size(133, 23);
            this.onBut.TabIndex = 1;
            this.onBut.Text = "Заблокировать";
            this.onBut.UseVisualStyleBackColor = true;
            this.onBut.Click += new System.EventHandler(this.onBut_Click);
            // 
            // passBut
            // 
            this.passBut.Location = new System.Drawing.Point(12, 154);
            this.passBut.Name = "passBut";
            this.passBut.Size = new System.Drawing.Size(133, 23);
            this.passBut.TabIndex = 5;
            this.passBut.Text = "Пароль";
            this.passBut.UseVisualStyleBackColor = true;
            this.passBut.Click += new System.EventHandler(this.passBut_Click);
            // 
            // logInBut
            // 
            this.logInBut.Location = new System.Drawing.Point(12, 183);
            this.logInBut.Name = "logInBut";
            this.logInBut.Size = new System.Drawing.Size(133, 23);
            this.logInBut.TabIndex = 0;
            this.logInBut.Text = "Войти";
            this.logInBut.UseVisualStyleBackColor = true;
            this.logInBut.Click += new System.EventHandler(this.logInBut_Click);
            // 
            // autoStartBut
            // 
            this.autoStartBut.Location = new System.Drawing.Point(12, 96);
            this.autoStartBut.Name = "autoStartBut";
            this.autoStartBut.Size = new System.Drawing.Size(133, 23);
            this.autoStartBut.TabIndex = 3;
            this.autoStartBut.Text = "Автозагрузка";
            this.autoStartBut.UseVisualStyleBackColor = true;
            this.autoStartBut.Click += new System.EventHandler(this.autoStartBut_Click);
            // 
            // restartBut
            // 
            this.restartBut.Enabled = false;
            this.restartBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.restartBut.Location = new System.Drawing.Point(12, 10);
            this.restartBut.Name = "restartBut";
            this.restartBut.Size = new System.Drawing.Size(133, 22);
            this.restartBut.TabIndex = 6;
            this.restartBut.Text = "Мнемосхема";
            this.restartBut.UseVisualStyleBackColor = true;
            this.restartBut.Click += new System.EventHandler(this.restartBut_Click);
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(165, 212);
            this.ControlBox = false;
            this.Controls.Add(this.restartBut);
            this.Controls.Add(this.autoStartBut);
            this.Controls.Add(this.logInBut);
            this.Controls.Add(this.passBut);
            this.Controls.Add(this.onBut);
            this.Controls.Add(this.offBut);
            this.Controls.Add(this.totalBut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "DeskLock v1.3";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button totalBut;
        private System.Windows.Forms.Button offBut;
        private System.Windows.Forms.Button onBut;
        private System.Windows.Forms.Button passBut;
        private System.Windows.Forms.Button logInBut;
        private System.Windows.Forms.Button autoStartBut;
        private System.Windows.Forms.Button restartBut;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.Timer timer1;


    }
}

