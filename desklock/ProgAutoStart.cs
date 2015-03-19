using System;
using System.IO;
using System.Windows.Forms;

namespace desklock
{
    public partial class ProgAutoStart : Form
    {
        public ProgAutoStart()
        {
            InitializeComponent();

            gwxMainTBox1.Select();
            VerifyForm();
            if (!File.Exists("C:\\Program Files\\ICONICS\\GENESIS64\\Components\\GwxConfigApp.exe"))
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                GenNotFoundLabel.Visible = true;
            }
        }

        private void BrowseGwXbut(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            OpenFileDialog dialog = new OpenFileDialog { Filter = "Файлы GraphWorX64|*.gdfx|Все|*.*" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (b.Name == "gwxBut1")
                {
                    gwxMainTBox1.Text = dialog.FileName;
                }
                if (b.Name == "gwxBut2")
                {
                    gwxMainTBox2.Text = dialog.FileName;
                }
            }
        }
        private void BrowseGwXtbox(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            OpenFileDialog dialog = new OpenFileDialog { Filter = "Файлы GraphWorX64|*.gdfx|Все|*.*" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (tb.Name == "gwxMainTBox1")
                {
                    gwxMainTBox1.Text = dialog.FileName;
                }
                if (tb.Name == "gwxMainTBox2")
                {
                    gwxMainTBox2.Text = dialog.FileName;
                }
            }
        }

        private void BrowseExEbut(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            OpenFileDialog dialog = new OpenFileDialog { Filter = "Программы|*.exe|Все|*.*" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (b.Name == "exeBut1")
                {
                    exeTBox1.Text = dialog.FileName;
                }
                if (b.Name == "exeBut2")
                {
                    exeTBox2.Text = dialog.FileName;
                }
                if (b.Name == "exeBut3")
                {
                    exeTBox3.Text = dialog.FileName;
                }
            }
        }
        private void BrowseExEtbox(object sender, MouseEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            OpenFileDialog dialog = new OpenFileDialog { Filter = "Программы|*.exe|Все|*.*" };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (tb.Name == "exeTBox1")
                {
                    exeTBox1.Text = dialog.FileName;
                }
                if (tb.Name == "exeTBox2")
                {
                    exeTBox2.Text = dialog.FileName;
                }
                if (tb.Name == "exeTBox3")
                {
                    exeTBox3.Text = dialog.FileName;
                }
            }
        }

        private void doneBut_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(gwxMainTBox1.Text))
            {
                string[,] gwxValues =
                {
                    {"filePath", gwxMainTBox1.Text}, {"delay", gwxDelayTBox1.Text},
                    {"state", gwxStateCBox1.Text}, {"left", gwxLeftTBox1.Text},
                    {"top", gwxTopTBox1.Text}, {"width", gwxWidthTBox1.Text}, 
                    {"height", gwxHeightTBox1.Text}, {"centerScreen", checkBox1.Checked.ToString()}
                };

                Engine.WriteToConfig("gwxAutoStart1", gwxValues);
            }
            else
            {
                Engine.DeleteFromConfig("gwxAutoStart1");
            }
            if (!string.IsNullOrEmpty(gwxMainTBox2.Text))
            {
                string[,] gwx2Values =
                {
                    {"filePath", gwxMainTBox2.Text}, {"delay", gwxDelayTBox2.Text},
                    {"state", gwxStateCBox2.Text}, {"left", gwxLeftTBox2.Text},
                    {"top", gwxTopTBox2.Text}, {"width", gwxWidthTBox2.Text}, 
                    {"height", gwxHeightTBox2.Text},{"centerScreen", checkBox2.Checked.ToString()}
                };

                Engine.WriteToConfig("gwxAutoStart2", gwx2Values);
            }
            else
            {
                Engine.DeleteFromConfig("gwxAutoStart2");
            }

            if (!string.IsNullOrEmpty(exeTBox1.Text))
            {
                string[,] exeValues = { {"filePath", exeTBox1.Text}};
                Engine.WriteToConfig("exeAutoStart1", exeValues);
            }
            else
            {
                Engine.DeleteFromConfig("exeAutoStart1");
            }
            if (!string.IsNullOrEmpty(exeTBox2.Text))
            {
                string[,] exeValues = { { "filePath", exeTBox2.Text } };
                Engine.WriteToConfig("exeAutoStart2", exeValues);
            }
            else
            {
                Engine.DeleteFromConfig("exeAutoStart2");
            }
            if (!string.IsNullOrEmpty(exeTBox3.Text))
            {
                string[,] exeValues = { { "filePath", exeTBox3.Text } };
                Engine.WriteToConfig("exeAutoStart3", exeValues);
            }
            else
            {
                Engine.DeleteFromConfig("exeAutoStart3");
            }
            
            Close();
        }

        private void VerifyForm()
        {
            if (string.IsNullOrEmpty(Engine.ReadFromConfig("gwxAutoStart1", "filePath")))
            {
                gwxMainTBox1.Text = Engine.ReadFromConfig("gwxAutoStart2", "filePath");
                gwxDelayTBox1.Text = Engine.ReadFromConfig("gwxAutoStart2", "delay");
                gwxStateCBox1.Text = Engine.ReadFromConfig("gwxAutoStart2", "state");
                gwxLeftTBox1.Text = Engine.ReadFromConfig("gwxAutoStart2", "left");
                gwxTopTBox1.Text = Engine.ReadFromConfig("gwxAutoStart2", "top");
                gwxWidthTBox1.Text = Engine.ReadFromConfig("gwxAutoStart2", "width");
                gwxHeightTBox1.Text = Engine.ReadFromConfig("gwxAutoStart2", "height");
                if (Engine.ReadFromConfig("gwxAutoStart2", "centerScreen") == "True")
                {
                    checkBox1.Checked = true;
                }
                if (Engine.ReadFromConfig("gwxAutoStart2", "centerScreen") == "False")
                {
                    checkBox1.Checked = false;
                }
            }
            else
            {
                gwxMainTBox1.Text = Engine.ReadFromConfig("gwxAutoStart1", "filePath");
                gwxDelayTBox1.Text = Engine.ReadFromConfig("gwxAutoStart1", "delay");
                gwxStateCBox1.Text = Engine.ReadFromConfig("gwxAutoStart1", "state");
                gwxLeftTBox1.Text = Engine.ReadFromConfig("gwxAutoStart1", "left");
                gwxTopTBox1.Text = Engine.ReadFromConfig("gwxAutoStart1", "top");
                gwxWidthTBox1.Text = Engine.ReadFromConfig("gwxAutoStart1", "width");
                gwxHeightTBox1.Text = Engine.ReadFromConfig("gwxAutoStart1", "height");
                if (Engine.ReadFromConfig("gwxAutoStart1", "centerScreen") == "True")
                {
                    checkBox1.Checked = true;
                }
                if (Engine.ReadFromConfig("gwxAutoStart1", "centerScreen") == "False")
                {
                    checkBox1.Checked = false;
                }

                gwxMainTBox2.Text = Engine.ReadFromConfig("gwxAutoStart2", "filePath");
                gwxDelayTBox2.Text = Engine.ReadFromConfig("gwxAutoStart2", "delay");
                gwxStateCBox2.Text = Engine.ReadFromConfig("gwxAutoStart2", "state");
                gwxLeftTBox2.Text = Engine.ReadFromConfig("gwxAutoStart2", "left");
                gwxTopTBox2.Text = Engine.ReadFromConfig("gwxAutoStart2", "top");
                gwxWidthTBox2.Text = Engine.ReadFromConfig("gwxAutoStart2", "width");
                gwxHeightTBox2.Text = Engine.ReadFromConfig("gwxAutoStart2", "height");
                if (Engine.ReadFromConfig("gwxAutoStart2", "centerScreen") == "True")
                {
                    checkBox2.Checked = true;
                }
                if (Engine.ReadFromConfig("gwxAutoStart2", "centerScreen") == "False")
                {
                    checkBox2.Checked = false;
                }
            }

            string[] exeFiles =
            {
                Engine.ReadFromConfig("exeAutoStart1", "filePath"),
                Engine.ReadFromConfig("exeAutoStart2", "filePath"), 
                Engine.ReadFromConfig("exeAutoStart3", "filePath")
            };

            int cnt = 1;
            foreach (string t in exeFiles)
            {
                if (!string.IsNullOrEmpty(t))
                {
                    if (cnt == 1) { exeTBox1.Text = t; }
                    if (cnt == 2) { exeTBox2.Text = t; }
                    if (cnt == 3) { exeTBox3.Text = t; }
                    cnt++;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                gwxLeftTBox1.Text = null;
                gwxTopTBox1.Text = null;
                gwxLeftTBox1.Enabled = false;
                gwxTopTBox1.Enabled = false;
            }
            else
            {
                gwxLeftTBox1.Enabled = true;
                gwxTopTBox1.Enabled = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                gwxLeftTBox2.Text = null;
                gwxTopTBox2.Text = null;
                gwxLeftTBox2.Enabled = false;
                gwxTopTBox2.Enabled = false;
            }
            else
            {
                gwxLeftTBox2.Enabled = true;
                gwxTopTBox2.Enabled = true;
            }
        }
    }
}
