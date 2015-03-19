using System;
using System.Windows.Forms;

namespace desklock
{
    public partial class PassForm : Form
    {
        public PassForm()
        {
            InitializeComponent();

            langLab1.Text = InputLanguage.CurrentInputLanguage.Culture.Name.Substring(0, 2).ToUpper();

            Timer t = new Timer { Interval = 500 };
            t.Start();
            t.Tick += (s, a) => langLab1.Text = InputLanguage.CurrentInputLanguage.Culture.Name.Substring(0, 2).ToUpper();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (Convert.ToInt16(e.KeyChar))
            {
                case 27:
                    Close();
                    break;
                case 13:
                    {
                        LogIn();
                    }
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogIn();
        }

        private void LogIn()
        {
            string p = Engine.ReadFromConfig("password", "value");
            p = Engine.DecryptData(p);
            if (passBox.Text == p)
            {
                Form1.logedIn = true;
                Close();
            }
            else
            {
                MessageBox.Show("Пароль не верный!");
                passBox.Focus();
                passBox.SelectionStart = 0;
                passBox.SelectionLength = passBox.Text.Length;
            }
        }
    }
}
