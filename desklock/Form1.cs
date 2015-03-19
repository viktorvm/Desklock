using System;
using System.Windows.Forms;

using System.IO;

namespace desklock
{
    public partial class Form1 : Form
    {
        private bool firstStart = true;
        public static bool passEx;
        public static bool logedIn;
        private static bool locked;
        private readonly Engine eng = new Engine();
        public static string xmlPath = "C:\\CONFIG.xml";

        //Auto Log out Timer
        // ?? таймер с каждым новым срабатыванием стартует метод timer_Tick (1+n) раз, где n-кол-во срабатываний таймера до этого ??
        public static Timer autolgTimer = new Timer { Interval = 600000 };

        public Form1()
        {
            InitializeComponent();

            //проверка на наличие файла конфигурации
            if (!File.Exists(xmlPath))
            {
                Engine.CreateConfig();
            }
            //здесь защита на случай,копировании файла в системный каталог и запуск оттуда при автостарте с реестра
            if (Application.ExecutablePath.IndexOf(@"\Windows\Sys") == -1)
            {
                //записываем свое местоположение
                string[,] values = { { "path", Application.ExecutablePath } };
                Engine.WriteToConfig("application", values);
            }
            if (Engine.ReadFromConfig("application", "path") == null)
            {
                onBut.Visible = false;
            }

            //проверка на наличие пароля и на блокировку
            passEx = Engine.ReadFromConfig("password", "value") != null;
            locked = Engine.ReadFromConfig("locked", "value") == "true";
        }

        private void totalBut_Click(object sender, EventArgs e)
        {
            if (logedIn)
            {
                eng.StartTotal();
            }
            else
            {
                MessageBox.Show("Вы не вошли в систему!");
                VerifyMainFrom();
            }
        }

        private void onBut_Click(object sender, EventArgs e)
        {
            if (logedIn)
            {
                eng.EnableProtection();
                string[,] values = { { "value", "true" } };
                Engine.WriteToConfig("locked", values);
                locked = true;
                VerifyMainFrom();
            }
            else
            {
                MessageBox.Show("Вы не вошли в систему!");
                VerifyMainFrom();
            }
        }

        private void offBut_Click(object sender, EventArgs e)
        {
            if (logedIn)
            {
                eng.DisableProtection();
                string[,] values = { { "value", "false" } };
                Engine.WriteToConfig("locked", values);
                locked = false;
                VerifyMainFrom();
            }
            else
            {
                MessageBox.Show("Вы не вошли в систему!");
                VerifyMainFrom();
            }
        }

        private void passBut_Click(object sender, EventArgs e)
        {
            if (logedIn & passEx)
            {
                var f = new ChangePass();
                f.ShowDialog();
                f.Dispose();
            }
            if (!logedIn & passEx)
            {
                MessageBox.Show("Вы не вошли в систему!");
                VerifyMainFrom();
            }
            if (!passEx)
            {
                var f = new NewPass();
                f.ShowDialog();
                f.Dispose();
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            if (firstStart)
            {
                firstStart = false;
                this.SendToBack();
            }
            if (autolgTimer.Enabled)
            {
                //stop auto log out timer
                autolgTimer.Stop();
            }

            VerifyMainFrom();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
            }
        }

        private void logInBut_Click(object sender, EventArgs e)
        {
            if (logedIn)
            {
                logedIn = false;
                VerifyMainFrom();
            }
            else
            {
                var f = new PassForm();
                f.ShowDialog();
                f.Dispose();
            }
        }

        private void VerifyMainFrom()
        {
            if (passEx)
            {
                if (logedIn)
                {
                    passBut.Text = "Изменить пароль";
                    logInBut.Text = "Выйти";
                    if (locked)
                    {
                        onBut.Enabled = false;
                        offBut.Enabled = true;
                    }
                    else
                    {
                        onBut.Enabled = true;
                        offBut.Enabled = false;
                    }
                    totalBut.Enabled = true;
                    passBut.Enabled = true;
                    autoStartBut.Enabled = true;
                }
                else
                {
                    passBut.Text = "Изменить пароль";
                    logInBut.Text = "Войти";
                    onBut.Enabled = false;
                    offBut.Enabled = false;
                    totalBut.Enabled = false;
                    passBut.Enabled = false;
                    autoStartBut.Enabled = false;
                    logInBut.Enabled = true;
                }
            }
            else
            {
                passBut.Text = "Задать пароль";
                logInBut.Text = "Войти";
                onBut.Enabled = false;
                offBut.Enabled = false;
                totalBut.Enabled = false;
                logInBut.Enabled = false;
                autoStartBut.Enabled = false;
            }
        }

        private void autoStartBut_Click(object sender, EventArgs e)
        {
            if (logedIn)
            {
                ProgAutoStart f = new ProgAutoStart();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Вы не вошли в систему!");
                VerifyMainFrom();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (locked)
            {
                Engine.ProgramsStart();
            }
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            //start auto log out timer
            if (logedIn)
            {
                autolgTimer.Start();
                autolgTimer.Tick += (s, a) => logedIn = false;
            }
        }

        private void restartBut_Click(object sender, EventArgs e)
        {
            string[,] values = { { "dateTime", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString()}, {"logedIn",logedIn.ToString()}};
            Engine.WriteToConfig("lastReboot", values);
            Engine.GwxRestart();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            restartBut.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var REG_DWORD = Microsoft.Win32.RegistryValueKind.DWord;
                var REG_SZ = Microsoft.Win32.RegistryValueKind.String;

                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
                key.SetValue("NoDriveTypeAutoRun", 255, REG_DWORD);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отключении автозапуска \n\n" + ex.Message
                    + "\nВетка - Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer\\NoDriveTypeAutoRun");
            }
            MessageBox.Show("Готово!");
        }
    }
}