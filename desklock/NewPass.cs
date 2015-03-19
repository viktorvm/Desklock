using System;
using System.IO;
using System.Windows.Forms;

namespace desklock
{
    public partial class NewPass : Form
    {
        public NewPass()
        {
            InitializeComponent();

            this.langLab1.Text = InputLanguage.CurrentInputLanguage.Culture.Name.Substring(0, 2).ToUpper();

            Timer t = new Timer() {Interval = 500};
            t.Start();
            t.Tick += (s, a) => langLab1.Text = InputLanguage.CurrentInputLanguage.Culture.Name.Substring(0, 2).ToUpper();
        }

        private void newPassBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 27)
            {
                Close();
            }
        }

        private void newPassBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 27)
            {
                Close();
            }
            else
            {
                if (Convert.ToInt16(e.KeyChar) == 13)
                {
                    SetPassword();
                }
            }
        }

        private void setPassBut_Click(object sender, EventArgs e)
        {
            SetPassword();
        }

        private void SetPassword()
        {
            if (newPassBox1.Text == newPassBox2.Text)
            {
                Form1.passEx = true;
                string[,] values = { { "value", Engine.EncryptData(newPassBox2.Text) } };
                Engine.WriteToConfig("password",values);
                Close();
            }
            else
            {
                MessageBox.Show("Пароли не совпадают...");
                newPassBox1.Focus();
                newPassBox1.SelectionStart = 0;
                newPassBox1.SelectionLength = newPassBox1.Text.Length;
            }
        }
    }
}