using System;
using System.Windows.Forms;

using System.IO;

namespace desklock
{
    public partial class ChangePass : Form
    {
        public ChangePass()
        {
            InitializeComponent();

            langLab1.Text = InputLanguage.CurrentInputLanguage.Culture.Name.Substring(0, 2).ToUpper();

            Timer t = new Timer() {Interval = 500};
            t.Start();
            t.Tick += (s, a) => langLab1.Text = InputLanguage.CurrentInputLanguage.Culture.Name.Substring(0, 2).ToUpper();
        }

        private void oldPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 27)
            {
                Close();
            }
        }

        private void newPassBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 27)
            {
                Close();
            }
        }

        private void newPassBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt16(e.KeyChar) == 27)
            {
                Close();
            }
            else
            {
                if (Convert.ToInt16(e.KeyChar) == 13)
                {
                    ChangePassword();
                }
            }
        }

        private void changePassBut_Click(object sender, EventArgs e)
        {
            ChangePassword();
        }

        void ChangePassword()
        {
            string oldPass = Engine.DecryptData(Engine.ReadFromConfig("password", "value"));

            if (oldPass == oldPassBox.Text)
            {
                if (newPassBox3.Text == newPassBox4.Text)
                {
                    string[,] values = { { "value", Engine.EncryptData(newPassBox4.Text) } };
                    Engine.WriteToConfig("password", values);
                    Close();
                }
                else
                {
                    MessageBox.Show("Пароли не совпадают...");
                    newPassBox3.Focus();
                    newPassBox3.SelectionStart = 0;
                    newPassBox3.SelectionLength = newPassBox3.Text.Length;
                }
            }
            else
            {
                MessageBox.Show("Не верно указан старый пароль!");
                oldPassBox.Focus();
                oldPassBox.SelectionStart = 0;
                oldPassBox.SelectionLength = oldPassBox.Text.Length;
            }
        }
    }
}
