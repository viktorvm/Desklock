using System;
using System.Diagnostics;
using System.Linq;

using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;
using System.Xml;

namespace desklock
{
    internal class Engine
    {
        //не используется
        #region Реализация метода перезагрузки Halt(true, true)

        //halt(true, false) //мягкая перезагрузка
        //halt(true, true) //жесткая перезагрузка
        //halt(false, false) //мягкое выключение
        //halt(false, true) //жесткое выключение

        //импортируем API функцию InitiateSystemShutdown
        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        [DllImport("advapi32.dll", EntryPoint = "InitiateSystemShutdownEx")]
        private static extern int InitiateSystemShutdown(string lpMachineName, string lpMessage, int dwTimeout,
            bool bForceAppsClosed, bool bRebootAfterShutdown);

        //импортируем API функцию AdjustTokenPrivileges
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall,
            ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

        //импортируем API функцию GetCurrentProcess
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GetCurrentProcess();

        //импортируем API функцию OpenProcessToken
        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        internal static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        //импортируем API функцию LookupPrivilegeValue
        [DllImport("advapi32.dll", SetLastError = true)]
        internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

        //объявляем структуру TokPriv1Luid для работы с привилегиями
        //функция SetPriv для повышения привилегий процесса
        private void SetPriv()
        {
            TokPriv1Luid tkp; //экземпляр структуры TokPriv1Luid 
            IntPtr htok = IntPtr.Zero;
            //открываем "интерфейс" доступа для своего процесса
            if (OpenProcessToken(GetCurrentProcess(), TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok))
            {
                //заполняем поля структуры
                tkp.Count = 1;
                tkp.Attr = SE_PRIVILEGE_ENABLED;
                tkp.Luid = 0;
                //получаем системный идентификатор необходимой нам привилегии
                LookupPrivilegeValue(null, SE_SHUTDOWN_NAME, ref tkp.Luid);
                //повышем привилигеию своему процессу
                AdjustTokenPrivileges(htok, false, ref tkp, 0, IntPtr.Zero, IntPtr.Zero);
            }
        }

        public int Halt(bool RSh, bool Force)
        {
            SetPriv(); //получаем привилегии
            //вызываем функцию InitiateSystemShutdown, передавая ей необходимые параметры
            return InitiateSystemShutdown(null, null, 0, Force, RSh);
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokPriv1Luid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        #endregion
        //

        #region Методы шифрования EncryptData(string), DecryptData(string)

        public static string EncryptData(string s)
        {
            char[] ch = s.ToCharArray();
            var b = new byte[ch.Length];
            for (int i = 0; i < ch.Length; i++)
            {
                b[i] = (byte)ch[i];
            }
            return Convert.ToBase64String(b);
        }

        public static string DecryptData(string s)
        {
            byte[] arB = Convert.FromBase64String(s);
            string result = string.Empty;
            foreach (byte b in arB)
            {
                result += ((char)b).ToString();
            }
            return result;
        }

        #endregion

        public void StartTotal()
        {
            string path = ReadFromConfig("totalcmd", "value");
            if (path == null)
            {
                SetTotalPath();
            }
            else
            {
                try
                {
                    Process.Start(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    SetTotalPath();
                }
            }
        }

        static void SetTotalPath()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Исполняемый файл Total|totalcmd.exe|Все файлы|*.exe";
            var dRes = dialog.ShowDialog();
            if (dRes == DialogResult.OK)
            {
                string[,] values = { { "value", dialog.FileName } };
                WriteToConfig("totalcmd", values);
                MessageBox.Show("Путь к Total Commander установлен");
            }
            else
            {
                if (dRes == DialogResult.Cancel) { }
                else
                {
                    MessageBox.Show("Не удалось установить путь к файлу Total Commander");
                }
            }

        }

        public void EnableProtection()
        {
            var REG_DWORD = RegistryValueKind.DWord;
            var REG_SZ = RegistryValueKind.String;
            //RegistryKey locMach = Registry.LocalMachine;

            try
            {
                //replace explorer.exe with myself
                string path = ReadFromConfig("application", "path");
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
                key.SetValue("Shell", path, REG_SZ);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при записи себя вместо explorer.exe \n\n" + ex.Message
                    + "\nВетка - SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\\Shell");
            }
            try
            {
                //disable TaskManager
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies", true);
                RegistryKey nkey = key.CreateSubKey("System", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
                nkey.SetValue("DisableTaskMgr", 00000001, REG_DWORD);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отключении Диспетчера задач \n\n" + ex.Message
                    + "\nВетка - Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\DisableTaskMgr");
            }

            ////disable My Computer
            //path = @"Software\Microsoft\Windows\CurrentVersion\Policies";
            //curUs = curUs.OpenSubKey(path, true);
            //curUs.CreateSubKey("NonEnum", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
            //curUs = curUs.OpenSubKey("NonEnum", true);
            //key = "20D04FE0-3AEA-1069-A2D8-08002B30309D";
            //curUs.SetValue(key, 00000001, REG_DWORD);

            try
            {
                //disable autorun
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer", true);
                key.SetValue("NoDriveTypeAutoRun", 255, REG_DWORD);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при отключении автозапуска \n\n" + ex.Message
                    + "\nВетка - Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer\\NoDriveTypeAutoRun");
            }

            MessageBox.Show("Блокировка включена. \nНекоторые изменения вступят в силу после перезагрузки системы.");
        }

        public void DisableProtection()
        {
            var REG_DWORD = RegistryValueKind.DWord;
            var REG_SZ = RegistryValueKind.String;

            try
            {
                //replace myself with explorer.exe
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
                key.SetValue("Shell", "explorer.exe", REG_SZ);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при записи explorer.exe вместо себя \n\n" + ex.Message);
            }
            try
            {
                //enable TaskManager
                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies", true);
                string[] st = key.GetSubKeyNames();
                foreach (string k in st.Where(k => k == "System"))
                {
                    key.DeleteSubKey("System");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при включении Диспетчера задач \n\n" + ex.Message);
            }
            //start explorer.exe
            try
            {
                Process.Start(@"C:\Windows\explorer.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при старте процесса explorer.exe \n\n" + ex.Message);
            }

            ////enable My Computer
            //path = @"Software\Microsoft\Windows\CurrentVersion\Policies";
            //curUs = curUs.OpenSubKey(path, true);
            //curUs.DeleteSubKey("NonEnum");
            //curUs.Close();

            ////enable autorun
            //path = @"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer";
            //curUs = curUs.OpenSubKey(path, true);
            //curUs.DeleteValue("NoDriveTypeAutoRun");
            //curUs.Close();

            MessageBox.Show("Блокировка выключена");
        }

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        //static void Main(string[] args)
        //{
        //    System.Diagnostics.Process[] p = System.Diagnostics.Process.GetProcessesByName("notepad");
        //    if (p.Length > 0)
        //    {
        //        ShowWindow(p[0].MainWindowHandle, 10);
        //        ShowWindow(p[0].MainWindowHandle, 5);
        //        SetForegroundWindow(p[0].MainWindowHandle);
        //    }
        //    Console.ReadLine();
        //}

        //[DllImport("user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        //[DllImport("user32.dll")]
        //public static extern bool SetForegroundWindow(IntPtr hWnd);

        public static void ProgramsStart()
        {
            for (int i = 1; i < 5; i++)
            {
                if (!string.IsNullOrEmpty(ReadFromConfig("exeAutoStart" + i, "filePath")))
                {
                    string execute = ReadFromConfig("exeAutoStart" + i, "filePath");
                    try
                    {
                        Process p = Process.Start(execute);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось выполнить - " + execute + "\n\n" + ex.Message);
                    }
                }
            }

            for (int i = 1; i < 5; i++)
            {
                if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "filePath")))
                {
                    string delay = "";
                    string position = "";
                    string state = "";
                    if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "delay")))
                    {
                        delay = "-showlicensewait=" + ReadFromConfig("gwxAutoStart" + i, "delay");
                    }
                    if (ReadFromConfig("gwxAutoStart" + i, "centerScreen") == "True")
                    {
                        position = "-start=CenterScreen";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "left")))
                        {
                            position = position + " -x=" + ReadFromConfig("gwxAutoStart" + i, "left");
                        }
                        if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "top")))
                        {
                            position = position + " -y=" + ReadFromConfig("gwxAutoStart" + i, "top");
                        }
                    }
                    if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "width")))
                    {
                        position = position + " -width=" + ReadFromConfig("gwxAutoStart" + i, "width");
                    }
                    if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "height")))
                    {
                        position = position + " -height=" + ReadFromConfig("gwxAutoStart" + i, "height");
                    }
                    if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "state")))
                    {
                        string readState = ReadFromConfig("gwxAutoStart" + i, "state");
                        switch (readState)
                        {
                            case "Нормально":
                                state = "-state=Normal";
                                break;
                            case "Развернуто":
                                state = "-state=Maximized";
                                break;
                            case "Свернуто":
                                state = "-state=Minimized";
                                break;
                        }
                    }

                    string execute = ReadFromConfig("gwxAutoStart" + i, "filePath") + " -runtime" + " " + delay + " " + position + " " + state;
                    try
                    {
                        Process p = Process.Start(@"C:\Program Files\ICONICS\GENESIS64\Components\GwxConfigApp.exe ", execute);
                        SetForegroundWindow(p.Handle);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось выполнить - " + execute + "\n\n" + ex.Message);
                    }
                }
            }
        }

        public static void CreateConfig()
        {
            XmlTextWriter writer = new XmlTextWriter(Form1.xmlPath, System.Text.Encoding.UTF8);
            writer.WriteStartDocument();
            writer.WriteStartElement("head");
            writer.WriteEndElement();
            writer.Close();
        }

        public static void WriteToConfig(string par, string[,] values)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Form1.xmlPath);

            foreach (XmlElement el in doc.GetElementsByTagName("node"))
            {
                if (el.GetAttribute("parName") == par)
                {
                    for (int i = 0; i < (values.Length / values.Rank); i++)
                    {
                        foreach (XmlElement chel in el.GetElementsByTagName(values[i, 0]))
                        {
                            chel.InnerText = values[i, 1];
                        }
                    }
                    doc.Save(Form1.xmlPath);
                    return;
                }
            }
            XmlElement node = doc.CreateElement("node");
            doc.DocumentElement.AppendChild(node);
            XmlAttribute nodeAttribute = doc.CreateAttribute("parName");
            nodeAttribute.Value = par;
            node.Attributes.Append(nodeAttribute);

            for (int i = 0; i < (values.Length / values.Rank); i++)
            {
                XmlElement subNode = doc.CreateElement(values[i, 0]);
                subNode.InnerText = values[i, 1];
                node.AppendChild(subNode);
            }
            doc.Save(Form1.xmlPath);
        }

        public static string ReadFromConfig(string par, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Form1.xmlPath);

            foreach (XmlElement el in doc.GetElementsByTagName("node"))
            {
                if (el.GetAttribute("parName") != par) continue;
                foreach (XmlElement w in el.GetElementsByTagName(value))
                {
                    return w.InnerText;
                }
            }
            return null;
        }

        public static void DeleteFromConfig(string par)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Form1.xmlPath);

            XmlElement root = doc.DocumentElement;
            XmlNodeList elList = doc.GetElementsByTagName("node");
            for (int i = 0; i < elList.Count; i++)
            {
                XmlElement el = (XmlElement)elList.Item(i);
                if (el.HasAttribute("parName"))
                {
                    if (el.GetAttribute("parName") == par)
                    {
                        root.RemoveChild(el);
                        doc.Save(Form1.xmlPath);
                    }
                }
            }
        }

        public static void GwxRestart()
        {
            try
            {
                foreach (Process p in Process.GetProcesses())
                {
                    if (p.ProcessName.Equals("GwxConfigApp", StringComparison.OrdinalIgnoreCase) || p.ProcessName.Equals("GwxRuntimeApp", StringComparison.OrdinalIgnoreCase))
                    {
                        p.Kill();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось остановить процесс - GwxConfigApp(GwxRuntimeApp)" + "\n\n" + ex.Message);
            }
            //запуск мнемосхем
            for (int i = 1; i < 5; i++)
            {
                if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "filePath")))
                {
                    string delay = "";
                    string position = "";
                    string state = "";
                    if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "delay")))
                    {
                        delay = "-showlicensewait=" + ReadFromConfig("gwxAutoStart" + i, "delay");
                    }
                    if (ReadFromConfig("gwxAutoStart" + i, "centerScreen") == "True")
                    {
                        position = "-start=CenterScreen";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "left")))
                        {
                            position = position + " -x=" + ReadFromConfig("gwxAutoStart" + i, "left");
                        }
                        if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "top")))
                        {
                            position = position + " -y=" + ReadFromConfig("gwxAutoStart" + i, "top");
                        }
                    }
                    if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "width")))
                    {
                        position = position + " -width=" + ReadFromConfig("gwxAutoStart" + i, "width");
                    }
                    if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "height")))
                    {
                        position = position + " -height=" + ReadFromConfig("gwxAutoStart" + i, "height");
                    }
                    if (!string.IsNullOrEmpty(ReadFromConfig("gwxAutoStart" + i, "state")))
                    {
                        string readState = ReadFromConfig("gwxAutoStart" + i, "state");
                        switch (readState)
                        {
                            case "Нормально":
                                state = "-state=Normal";
                                break;
                            case "Развернуто":
                                state = "-state=Maximized";
                                break;
                            case "Свернуто":
                                state = "-state=Minimized";
                                break;
                        }
                    }

                    string execute = ReadFromConfig("gwxAutoStart" + i, "filePath") + " -runtime" + " " + delay + " " + position + " " + state;
                    try
                    {
                        Process p = Process.Start(@"C:\Program Files\ICONICS\GENESIS64\Components\GwxConfigApp.exe ", execute);
                        SetForegroundWindow(p.Handle);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось выполнить - " + execute + "\n\n" + ex.Message);
                    }
                }
            }
        }
    }
}