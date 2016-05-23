using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

using U8PackageTools.code;

namespace U8PackageTools.Froms
{
    public partial class GameManagerForm : Form
    {
        private bool m_changeSaveStatus = true;
        private static int SelectStatusChannel = 1;
        private static int SelectStatusAttribute = 2;
        private int m_currentSelectStatus = SelectStatusChannel;
        private GameManager.Game m_selectGame;

        private void save()
        {
            try
            {
                String xmlfile = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\")) + "\\config.xml";
                XmlDocument doc = new XmlDocument();
                if (File.Exists(xmlfile))
                {
                    File.Delete(xmlfile);
                }

                XmlElement root = doc.CreateElement("root");
                doc.AppendChild(root);

                XmlElement u8pathnode = doc.CreateElement("u8path");
                u8pathnode.InnerText = ProjectConfig.getInstance().getU8Workspace();
                root.AppendChild(u8pathnode);

                doc.Save(xmlfile);
            }
            catch (System.Exception ex)
            {
                return;
            }
        }

        private void load()
        {
            try
            {
                String xmlfile = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\")) + "\\config.xml";
                if (File.Exists(xmlfile) == false)
                {
                    return;
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(xmlfile);
                XmlNode root = doc.SelectSingleNode("root");
                XmlNode u8pathnode = root.SelectSingleNode("u8path");

                ProjectConfig.getInstance().setU8Workspace(u8pathnode.InnerText);
                SDKManager.getInstance().readSDKDirectory();
                refreshGameList();

                doc.Save(xmlfile);
            }
            catch (System.Exception ex)
            {
                return;	
            }
        }

        public GameManagerForm()
        {
            InitializeComponent();
        }

        private void GameManager_Load(object sender, EventArgs e)
        {
            //item.Visible = false;
            GameManager.getInstance();
            AttPanel.Visible = false;
            AttPanel.Enabled = false;
            ChannelPanel.Visible = false;
            ChannelPanel.Enabled = false;
            gameList.SmallImageList = imageList1;
            channelList.SmallImageList = imageList1;

            load();
        }

        private void GameManager_Shown(object sender, EventArgs e)
        {
            
        }

        private void GameManagerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            save();
        }

        private void refreshGameList()
        {
            GameManager.getInstance().readGameXML(ProjectConfig.getInstance().getGameListXML());
            gameList.Items.Clear();
            foreach (GameManager.Game g in GameManager.getInstance().GameList)
            {
                ListViewItem item = new ListViewItem();
                item.Text = g.Params["appDesc"].value.value;
                item.Tag = g;
                gameList.Items.Add(item);
            }
            if (gameList.Items.Count > 0)
            {
                gameList.Items[0].Selected = true;
            }
        }

        private void att_Paint(object sender, PaintEventArgs e)
        {

        }

        private void item_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void 导入U8SDK工作目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "导入U8SDK工作目录。";
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.ShowNewFolderButton = false;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                //fbd.SelectedPath = @"E:\\workspace\\program\\WonderKnight\\u8sdk\\U8SDK_20160323130827-old\\U8SDKTool-Win-P34";
                try
                {
                    ProjectConfig.getInstance().setU8Workspace(fbd.SelectedPath);
                    SDKManager.getInstance().readSDKDirectory();
                    refreshGameList();
                    MessageBox.Show("导入成功");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void refreshSelectGameAttribute(GameManager.Game game)
        {
            AttPanel.Enabled = true;
            AttPanel.Visible = true;
            ChannelPanel.Enabled = false;
            ChannelPanel.Visible = false;

            m_changeSaveStatus = true;
            changeAndSave.Text = "修改";
            int i = 0;
            att.Controls.Clear();
            int height = 0;
            foreach (KeyValuePair<String, Param> entry in game.Params)
            {
                Param p = entry.Value;
                TextBox content = new TextBox();
                content.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                content.Size = new Size(att.Size.Width - 4, 20);
                content.Name = "content";
                content.ReadOnly = m_changeSaveStatus;

                Label desc = new Label();
                desc.MaximumSize = new Size(att.Size.Width - 4, 0);
                desc.Name = "desc";
                desc.AutoSize = true;
                att.Controls.Add(desc);
                att.Controls.Add(content);
                ++i;

                
                if (p.require)
                {
                    desc.Text = "*" + p.desc.value;
                }
                else
                {
                    desc.Text = p.desc.value;
                }
                
                content.Text = p.value.value;

                // location
                desc.Location = new Point(2, height + 12);
                content.Location = new Point(2, desc.Location.Y + desc.Size.Height + 2);
                height += desc.Size.Height;
                height += content.Size.Height;
                height += 14;
                //EVENT
                content.TextChanged += new EventHandler((object sender, EventArgs e) =>
                {
                    p.value.value = content.Text;
                });
            }
        }

        private void refreshSelectGameChannel(GameManager.Game game)
        {
            AttPanel.Enabled = false;
            AttPanel.Visible = false;
            ChannelPanel.Enabled = true;
            ChannelPanel.Visible = true;

            refreshChannelList();
        }

        private void refreshSelectGame()
        {
            if (m_selectGame != null)
            {
                if (m_currentSelectStatus == SelectStatusChannel)
                {
                    refreshSelectGameChannel(m_selectGame);
                }
                else if (m_currentSelectStatus == SelectStatusAttribute)
                {
                    refreshSelectGameAttribute(m_selectGame);
                }
            }
        }

        private void changeAndSave_Click(object sender, EventArgs e)
        {
            if (changeAndSave.Text == "修改")
            {
                foreach (object p in att.Controls)
                {
                    TextBox t = p as TextBox;
                    if (t!=null)
                    {
                        t.ReadOnly = false;
                    }
                }
                changeAndSave.Text = "保存";
            }
            else
            {
                foreach (object p in att.Controls)
                {
                    TextBox t = p as TextBox;
                    if (t != null)
                    {
                        t.ReadOnly = true;
                    }
                }
                changeAndSave.Text = "修改";
                GameManager.getInstance().saveGameXML(ProjectConfig.getInstance().getGameListXML());
            }
        }

        private void viewAtt_Click(object sender, EventArgs e)
        {
            m_currentSelectStatus = SelectStatusAttribute;
            refreshSelectGame();
        }

        private void viewChannel_Click(object sender, EventArgs e)
        {
            m_currentSelectStatus = SelectStatusChannel;
            refreshSelectGame();
        }

        private void refreshChannelList()
        {
            GameChannel gc = m_selectGame.GameChannel;
            channelList.Items.Clear();
            if (gc != null)
            {
                foreach (GameChannel.Channel c in gc.ChannelList)
                {
                    ListViewItem it = new ListViewItem();
                    if (c.sdk == null)
                    {
                        it.Text = c.Params["sdk"].value.value;
                    }
                    else
                    {
                        it.Text = c.sdk.ShowName;
                    }
                    it.Tag = c;

                    channelList.Items.Add(it);
                }
            }
            if (channelList.Items.Count > 0)
            {
                channelList.Items[0].Selected = true;
            }
        }

        private void refreshChannelAttribute(GameChannel.Channel c)
        {
            int i = 0;
            channelAttPanel.Controls.Clear();
            int height = 0;
            foreach (KeyValuePair<String, Param> entry in c.Params)
            {
                Param p = entry.Value;
                TextBox content = new TextBox();
                content.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                content.Size = new Size(channelAttPanel.Size.Width - 4, 20);
                content.Name = "content";
                content.ReadOnly = true;

                Label desc = new Label();
                desc.MaximumSize = new Size(channelAttPanel.Size.Width - 4, 0);
                desc.Name = "desc";
                desc.AutoSize = true;
                channelAttPanel.Controls.Add(desc);
                channelAttPanel.Controls.Add(content);
                ++i;


                if (p.require)
                {
                    desc.Text = "*" + p.desc.value;
                }
                else
                {
                    desc.Text = p.desc.value;
                }

                content.Text = p.value.value;

                // location
                desc.Location = new Point(2, height + 12);
                content.Location = new Point(2, desc.Location.Y + desc.Size.Height + 2);
                height += desc.Size.Height;
                height += content.Size.Height;
                height += 14;
                //EVENT
                content.TextChanged += new EventHandler((object sender, EventArgs e) =>
                {
                    p.value.value = content.Text;
                });
                content.Tag = p;
            }

            Label sdktitle = new Label();
            sdktitle.AutoSize = true;
            sdktitle.Text = "------------渠道SDK相关参数-------------";
            sdktitle.ForeColor = Color.Red;
            sdktitle.Location = new Point(0, height + 16);
            height += 18;
            channelAttPanel.Controls.Add(sdktitle);

            foreach (KeyValuePair<String, Param> entry in c.SDKParams)
            {
                Param p = entry.Value;
                TextBox content = new TextBox();
                content.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                content.Size = new Size(channelAttPanel.Size.Width - 4, 20);
                content.Name = "content";
                content.ReadOnly = true;

                Label desc = new Label();
                desc.MaximumSize = new Size(channelAttPanel.Size.Width - 4, 0);
                desc.Name = "desc";
                desc.AutoSize = true;
                channelAttPanel.Controls.Add(desc);
                channelAttPanel.Controls.Add(content);
                ++i;


                if (p.require)
                {
                    desc.Text = "*" + p.name.value + ":" + p.desc.value;
                }
                else
                {
                    desc.Text = p.name.value + ":" + p.desc.value;
                }

                content.Text = p.value.value;

                // location
                desc.Location = new Point(2, height + 12);
                content.Location = new Point(2, desc.Location.Y + desc.Size.Height + 2);
                height += desc.Size.Height;
                height += content.Size.Height;
                height += 14;
                //EVENT
                content.TextChanged += new EventHandler((object sender, EventArgs e) =>
                {
                    p.value.value = content.Text;
                });
                content.Tag = p;
            }
        }

        private void gameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gameList.SelectedItems.Count > 0)
            {
                GameManager.Game selectdata = gameList.SelectedItems[0].Tag as GameManager.Game;
                foreach (ListViewItem lv in gameList.Items)
                {
                    lv.ImageIndex = 3;
                }
                gameList.SelectedItems[0].ImageIndex = 1;
                if (m_selectGame == selectdata)
                {
                    return;
                }
                else
                {
                    m_selectGame = selectdata;
                }
                refreshSelectGame();
            }
        }

        private void channelList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (channelList.SelectedItems.Count > 0)
            {
                GameChannel.Channel c = channelList.SelectedItems[0].Tag as GameChannel.Channel;
                foreach (ListViewItem lv in channelList.Items)
                {
                    lv.ImageIndex = 3;
                }

                channelList.SelectedItems[0].ImageIndex = 1;
                refreshChannelAttribute(c);
                if (c.sdk == null)
                {
                    label3.Text = c.Params["sdk"].value.value + "渠道属性";
                }
                else
                {
                    label3.Text = c.sdk.ShowName + "渠道属性";
                }
                
            }
        }

        private void ChannelChangeAndSave_Click(object sender, EventArgs e)
        {
            if (ChannelChangeAndSave.Text == "修改")
            {
                foreach (object p in channelAttPanel.Controls)
                {
                    TextBox t = p as TextBox;
                    if (t != null)
                    {
                        Param pm = t.Tag as Param;
                        if (pm.readOnly == false)
                            t.ReadOnly = false;
                    }
                }
                ChannelChangeAndSave.Text = "保存";
            }
            else
            {
                foreach (object p in channelAttPanel.Controls)
                {
                    TextBox t = p as TextBox;
                    if (t != null)
                    {
                        t.ReadOnly = true;
                    }
                }
                ChannelChangeAndSave.Text = "修改";
                m_selectGame.GameChannel.writeGameConfig(ProjectConfig.getInstance().getGameList() + m_selectGame.AppName + "\\config.xml");
            }
        }

        private void addChannel_Click(object sender, EventArgs e)
        {
            SDKList d = new SDKList();
            d.ShowDialog();
            if (d.SelectSDKList.Count > 0 && m_selectGame != null)
            {
                GameChannel gc = m_selectGame.GameChannel;
                bool addflag = false;
                foreach (SDKManager.SDK sdk in d.SelectSDKList)
                {
                    bool find = false;
                    foreach (GameChannel.Channel c in gc.ChannelList)
                    {
                        if (c.sdk == sdk)
                        {
                            find = true;
                            break;
                        }
                    }
                    if (find == false)
                    {
                        gc.addChannlWithSDK(sdk);
                        addflag = true;
                    }
                }
                if (addflag)
                {
                    refreshChannelList();
                    m_selectGame.GameChannel.writeGameConfig(ProjectConfig.getInstance().getGameList() + m_selectGame.AppName + "\\config.xml");
                }
            }
        }

        public bool DeCompressionZip(string _depositPath, string _floderPath)
        {
            bool result = true;
            FileStream fs = null;
            try
            {
                ZipInputStream InpStream = new ZipInputStream(File.OpenRead(_depositPath));
                ZipEntry ze = InpStream.GetNextEntry();//获取压缩文件中的每一个文件
                Directory.CreateDirectory(_floderPath);//创建解压文件夹
                while (ze != null)//如果解压完ze则是null
                {
                    if (ze.IsFile)//压缩zipINputStream里面存的都是文件。带文件夹的文件名字是文件夹\\文件名
                    {
                        string[] strs = ze.Name.Split('\\');//如果文件名中包含’\\‘则表明有文件夹
                        if (strs.Length > 1)
                        {
                            //两层循环用于一层一层创建文件夹
                            for (int i = 0; i < strs.Length - 1; i++)
                            {
                                string floderPath = _floderPath;
                                for (int j = 0; j < i; j++)
                                {
                                    floderPath = floderPath + "\\" + strs[j];
                                }
                                floderPath = floderPath + "\\" + strs[i];
                                Directory.CreateDirectory(floderPath);
                            }
                        }
                        else
                        {
                            strs = ze.Name.Split('/');//如果文件名中包含’\\‘则表明有文件夹
                            if (strs.Length > 1)
                            {
                                //两层循环用于一层一层创建文件夹
                                for (int i = 0; i < strs.Length - 1; i++)
                                {
                                    string floderPath = _floderPath;
                                    for (int j = 0; j < i; j++)
                                    {
                                        floderPath = floderPath + "\\" + strs[j];
                                    }
                                    floderPath = floderPath + "\\" + strs[i];
                                    Directory.CreateDirectory(floderPath);
                                }
                            }
                        }
                        fs = new FileStream(_floderPath + "\\" + ze.Name, FileMode.OpenOrCreate, FileAccess.Write);//创建文件
                        //循环读取文件到文件流中
                        while (true)
                        {
                            byte[] bts = new byte[1024];
                            int i = InpStream.Read(bts, 0, bts.Length);
                            if (i > 0)
                            {
                                fs.Write(bts, 0, i);
                            }
                            else
                            {
                                fs.Flush();
                                fs.Close();
                                break;
                            }
                        }
                    }
                    ze = InpStream.GetNextEntry();
                }
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                MessageBox.Show(ex.ToString());
                result = false;
            }
            return result;
        }

        private void copyDirectory(String source, String dest)
        {
            if(! Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
            }

            String[] dirs = Directory.GetDirectories(source);
            foreach(String path in dirs)
            {
                String fileName = Path.GetFileName(path);
                copyDirectory(path, dest + "\\" + fileName);
            }
            String[] files = Directory.GetFiles(source);
            foreach(String file in files)
            {
                String fileName = Path.GetFileName(file);
                File.Copy(file, dest + "\\" + fileName, true);
            }
        }

        private void pack_Click(object sender, EventArgs e)
        {
            if (m_selectGame == null)
            {
                return;
            }

            GameChannel gc = m_selectGame.GameChannel;
            if (gc == null)
            {
                return;
            }

            if (channelList.SelectedItems.Count == 0)
            {
                return;
            }

            try
            {

                String dir = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));
                if (File.Exists(dir + "\\packGame.py"))
                {
                    File.Copy(dir + "\\packGame.py", ProjectConfig.getInstance().getU8Workspace() + "\\scripts\\packGame.py", true);
                }
                else if (File.Exists(dir + "\\..\\..\\packGame.py"))
                {
                    File.Copy(dir + "\\..\\..\\packGame.py", ProjectConfig.getInstance().getU8Workspace() + "\\scripts\\packGame.py", true);
                }

                if (!Directory.Exists(ProjectConfig.getInstance().getU8Workspace() + "\\Python27"))
                {
                    if (File.Exists(dir + "\\Python27.zip"))
                    {
                        DeCompressionZip(dir + "\\Python27.zip", ProjectConfig.getInstance().getU8Workspace());
                    }
                    else if (File.Exists(dir + "\\..\\..\\Python27.zip"))
                    {
                        DeCompressionZip(dir + "\\..\\..\\Python27.zip", ProjectConfig.getInstance().getU8Workspace());
                    }
                }
                
                GameChannel.Channel c = channelList.SelectedItems[0].Tag as GameChannel.Channel;
                String cmd = "@set PATH=%~dp0\\tool\\win;%PATH% \nPython27\\python.exe scripts\\packGame.py -r -s -t 1 --appID "
                            + m_selectGame.Params["appID"].value.value
                            + " --target " + c.Params["name"].value.value
                            + " \nif %ERRORLEVEL% == 0 (\n\texit(%ERRORLEVEL%)\n) else ( \n\t@pause \n\texit(%ERRORLEVEL%)\n)";

                StreamWriter sw = new StreamWriter(ProjectConfig.getInstance().getU8Workspace() + "\\runPackageGame.bat");
                sw.Write(cmd);
                sw.Flush();
                sw.Close();


                Process pro = new Process();
                pro.StartInfo.WorkingDirectory = ProjectConfig.getInstance().getU8Workspace();
                pro.StartInfo.FileName = ProjectConfig.getInstance().getU8Workspace() + "\\runPackageGame.bat";
                pro.StartInfo.CreateNoWindow = false;
                pro.Start();
                pro.WaitForExit();
                if (pro.ExitCode == 0)
                {
                    MessageBox.Show("打包成功！！！");
                }
                else if (pro.ExitCode == 1)
                {
                    MessageBox.Show("打包失败！！！");
                }
                else if (pro.ExitCode == 2)
                {
                    MessageBox.Show("没有找到母包！！！");
                }
                else if (pro.ExitCode == 3)
                {
                    MessageBox.Show("没有任何可以打包的渠道");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void packAll_Click(object sender, EventArgs e)
        {
            if (m_selectGame == null)
            {
                return;
            }
            try
            {
                String dir = Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"));
                if (File.Exists(dir + "\\packGame.py"))
                {
                    File.Copy(dir + "\\packGame.py", ProjectConfig.getInstance().getU8Workspace() + "\\scripts\\packGame.py", true);
                }
                else if (File.Exists(dir + "\\..\\..\\packGame.py"))
                {
                    File.Copy(dir + "\\..\\..\\packGame.py", ProjectConfig.getInstance().getU8Workspace() + "\\scripts\\packGame.py", true);
                }

                GameChannel.Channel c = channelList.SelectedItems[0].Tag as GameChannel.Channel;
                String cmd = "@set PATH=%~dp0\\tool\\win;%PATH% \nPython27\\python.exe scripts\\packGame.py -r -s -t 1 --appID "
                            + m_selectGame.Params["appID"].value.value
                            + " --target *"
                            + " \nif %ERRORLEVEL% == 0 (\n\texit(%ERRORLEVEL%)\n) else ( \n\t@pause \n\texit(%ERRORLEVEL%)\n)";

                StreamWriter sw = new StreamWriter(ProjectConfig.getInstance().getU8Workspace() + "\\runPackageGame.bat");
                sw.Write(cmd);
                sw.Flush();
                sw.Close();

                Process pro = new Process();
                pro.StartInfo.WorkingDirectory = ProjectConfig.getInstance().getU8Workspace();
                pro.StartInfo.FileName = ProjectConfig.getInstance().getU8Workspace() + "\\runPackageGame.bat";
                pro.StartInfo.CreateNoWindow = false;
                pro.Start();
                pro.WaitForExit();

                if (pro.ExitCode == 0)
                {
                    MessageBox.Show("打包成功！！！");
                }
                else if (pro.ExitCode == 1)
                {
                    MessageBox.Show("打包失败！！！");
                }
                else if (pro.ExitCode == 2)
                {
                    MessageBox.Show("没有找到母包！！！");
                }
                else if (pro.ExitCode == 3)
                {
                    MessageBox.Show("没有任何可以打包的渠道");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void viewapkdir_Click(object sender, EventArgs e)
        {
            if (m_selectGame == null)
            {
                return;
            }

            GameChannel gc = m_selectGame.GameChannel;
            if (gc == null)
            {
                return;
            }

            if (channelList.SelectedItems.Count == 0)
            {
                return;
            }

            Process proc = new Process();
            proc.StartInfo.FileName = "explorer";

            GameChannel.Channel c = channelList.SelectedItems[0].Tag as GameChannel.Channel;
            String arg = "";
            if (Directory.Exists(ProjectConfig.getInstance().getU8Workspace() + "\\output\\" + m_selectGame.AppName))
            {
                if (Directory.Exists(ProjectConfig.getInstance().getU8Workspace() + "\\output\\" + m_selectGame.AppName + "\\" + c.Params["name"].value.value))
                {
                    arg = ProjectConfig.getInstance().getU8Workspace() + "\\output\\" + m_selectGame.AppName + "\\" + c.Params["name"].value.value;
                }
                else
                {
                    arg = ProjectConfig.getInstance().getU8Workspace() + "\\output\\" + m_selectGame.AppName;
                }
            }
            else
            {
                arg = ProjectConfig.getInstance().getU8Workspace() + "\\output\\";
            }
            //proc.StartInfo.Arguments = "E:\\workspace\\program\\WonderKnight\\u8sdk\\U8SDK_20160323130827-old\\U8SDKTool-Win-P34\\output\\game13";
            proc.StartInfo.Arguments = arg.Replace("\\\\", "\\");
            proc.Start();
        }

        private void exporticon_Click(object sender, EventArgs e)
        {
            if (m_selectGame == null)
            {
                return;
            }

            OpenFileDialog fileDialog1 = new OpenFileDialog();
            fileDialog1.InitialDirectory = "C://";
            fileDialog1.Filter = "512x512icon文件|*.png";
            fileDialog1.FilterIndex = 1;
            fileDialog1.RestoreDirectory = true;
            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Copy(fileDialog1.FileName, ProjectConfig.getInstance().getGameList() + m_selectGame.AppName + "\\icon\\icon.png", true);
                    MessageBox.Show("导入成功！！");
                }
                catch (System.Exception ex)
                {
                	MessageBox.Show(ex.ToString());
                }
            }
        }

        private void exportapk_Click(object sender, EventArgs e)
        {
            if (m_selectGame == null)
            {
                return;
            }

            OpenFileDialog fileDialog1 = new OpenFileDialog();
            fileDialog1.InitialDirectory = "C://";
            fileDialog1.Filter = "母包文件|*.apk";
            fileDialog1.FilterIndex = 1;
            fileDialog1.RestoreDirectory = true;
            if (fileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Copy(fileDialog1.FileName, ProjectConfig.getInstance().getGameList() + m_selectGame.AppName + "\\u8.apk", true);
                    MessageBox.Show("导入成功！！");
                }
                catch (System.Exception ex)
                {
                	MessageBox.Show(ex.ToString());
                }
            }
        }

        private void viewDirctory_Click(object sender, EventArgs e)
        {
            if (m_selectGame == null)
            {
                return;
            }

            Process proc = new Process();
            proc.StartInfo.FileName = "explorer";

            GameChannel.Channel c = channelList.SelectedItems[0].Tag as GameChannel.Channel;
            String arg = ProjectConfig.getInstance().getU8Workspace() + "\\games\\" + m_selectGame.AppName; ;
            //proc.StartInfo.Arguments = "E:\\workspace\\program\\WonderKnight\\u8sdk\\U8SDK_20160323130827-old\\U8SDKTool-Win-P34\\output\\game13";
            proc.StartInfo.Arguments = arg.Replace("\\\\", "\\");
            proc.Start();
        }
    }
}
