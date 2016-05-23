namespace U8PackageTools.Froms
{
    partial class GameManagerForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameManagerForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.导入U8SDK工作目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameList = new System.Windows.Forms.ListView();
            this.name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.att = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.changeAndSave = new System.Windows.Forms.Button();
            this.AttPanel = new System.Windows.Forms.Panel();
            this.viewAtt = new System.Windows.Forms.Button();
            this.viewChannel = new System.Windows.Forms.Button();
            this.ChannelPanel = new System.Windows.Forms.Panel();
            this.packAll = new System.Windows.Forms.Button();
            this.addChannel = new System.Windows.Forms.Button();
            this.channelAtt = new System.Windows.Forms.Panel();
            this.viewapkdir = new System.Windows.Forms.Button();
            this.pack = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.channelAttPanel = new System.Windows.Forms.Panel();
            this.ChannelChangeAndSave = new System.Windows.Forms.Button();
            this.channelList = new System.Windows.Forms.ListView();
            this.渠道列表 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.exportapk = new System.Windows.Forms.Button();
            this.exporticon = new System.Windows.Forms.Button();
            this.viewDirctory = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.AttPanel.SuspendLayout();
            this.ChannelPanel.SuspendLayout();
            this.channelAtt.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入U8SDK工作目录ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1102, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 导入U8SDK工作目录ToolStripMenuItem
            // 
            this.导入U8SDK工作目录ToolStripMenuItem.Name = "导入U8SDK工作目录ToolStripMenuItem";
            this.导入U8SDK工作目录ToolStripMenuItem.Size = new System.Drawing.Size(132, 21);
            this.导入U8SDK工作目录ToolStripMenuItem.Text = "导入U8SDK工作目录";
            this.导入U8SDK工作目录ToolStripMenuItem.Click += new System.EventHandler(this.导入U8SDK工作目录ToolStripMenuItem_Click);
            // 
            // gameList
            // 
            this.gameList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gameList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name});
            this.gameList.Location = new System.Drawing.Point(2, 50);
            this.gameList.MultiSelect = false;
            this.gameList.Name = "gameList";
            this.gameList.Size = new System.Drawing.Size(204, 558);
            this.gameList.TabIndex = 2;
            this.gameList.UseCompatibleStateImageBehavior = false;
            this.gameList.View = System.Windows.Forms.View.Details;
            this.gameList.SelectedIndexChanged += new System.EventHandler(this.gameList_SelectedIndexChanged);
            // 
            // name
            // 
            this.name.Text = "游戏列表";
            this.name.Width = 200;
            // 
            // att
            // 
            this.att.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.att.AutoScroll = true;
            this.att.Location = new System.Drawing.Point(5, 30);
            this.att.Name = "att";
            this.att.Size = new System.Drawing.Size(763, 478);
            this.att.TabIndex = 4;
            this.att.Paint += new System.Windows.Forms.PaintEventHandler(this.att_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "属性";
            // 
            // changeAndSave
            // 
            this.changeAndSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.changeAndSave.Location = new System.Drawing.Point(690, 514);
            this.changeAndSave.Name = "changeAndSave";
            this.changeAndSave.Size = new System.Drawing.Size(75, 23);
            this.changeAndSave.TabIndex = 6;
            this.changeAndSave.Text = "修改";
            this.changeAndSave.UseVisualStyleBackColor = true;
            this.changeAndSave.Click += new System.EventHandler(this.changeAndSave_Click);
            // 
            // AttPanel
            // 
            this.AttPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AttPanel.Controls.Add(this.att);
            this.AttPanel.Controls.Add(this.changeAndSave);
            this.AttPanel.Controls.Add(this.label1);
            this.AttPanel.Location = new System.Drawing.Point(309, 51);
            this.AttPanel.Name = "AttPanel";
            this.AttPanel.Size = new System.Drawing.Size(781, 556);
            this.AttPanel.TabIndex = 7;
            // 
            // viewAtt
            // 
            this.viewAtt.Location = new System.Drawing.Point(216, 87);
            this.viewAtt.Name = "viewAtt";
            this.viewAtt.Size = new System.Drawing.Size(75, 23);
            this.viewAtt.TabIndex = 7;
            this.viewAtt.Text = "查看属性";
            this.viewAtt.UseVisualStyleBackColor = true;
            this.viewAtt.Click += new System.EventHandler(this.viewAtt_Click);
            // 
            // viewChannel
            // 
            this.viewChannel.Location = new System.Drawing.Point(216, 50);
            this.viewChannel.Name = "viewChannel";
            this.viewChannel.Size = new System.Drawing.Size(75, 23);
            this.viewChannel.TabIndex = 8;
            this.viewChannel.Text = "查看渠道";
            this.viewChannel.UseVisualStyleBackColor = true;
            this.viewChannel.Click += new System.EventHandler(this.viewChannel_Click);
            // 
            // ChannelPanel
            // 
            this.ChannelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChannelPanel.Controls.Add(this.packAll);
            this.ChannelPanel.Controls.Add(this.addChannel);
            this.ChannelPanel.Controls.Add(this.channelAtt);
            this.ChannelPanel.Controls.Add(this.channelList);
            this.ChannelPanel.Controls.Add(this.label2);
            this.ChannelPanel.Location = new System.Drawing.Point(309, 50);
            this.ChannelPanel.Name = "ChannelPanel";
            this.ChannelPanel.Size = new System.Drawing.Size(781, 556);
            this.ChannelPanel.TabIndex = 8;
            // 
            // packAll
            // 
            this.packAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.packAll.Location = new System.Drawing.Point(693, 519);
            this.packAll.Name = "packAll";
            this.packAll.Size = new System.Drawing.Size(75, 23);
            this.packAll.TabIndex = 10;
            this.packAll.Text = "全部打包";
            this.packAll.UseVisualStyleBackColor = true;
            this.packAll.Click += new System.EventHandler(this.packAll_Click);
            // 
            // addChannel
            // 
            this.addChannel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addChannel.Location = new System.Drawing.Point(8, 519);
            this.addChannel.Name = "addChannel";
            this.addChannel.Size = new System.Drawing.Size(121, 32);
            this.addChannel.TabIndex = 9;
            this.addChannel.Text = "添加渠道";
            this.addChannel.UseVisualStyleBackColor = true;
            this.addChannel.Click += new System.EventHandler(this.addChannel_Click);
            // 
            // channelAtt
            // 
            this.channelAtt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.channelAtt.Controls.Add(this.viewapkdir);
            this.channelAtt.Controls.Add(this.pack);
            this.channelAtt.Controls.Add(this.label3);
            this.channelAtt.Controls.Add(this.channelAttPanel);
            this.channelAtt.Controls.Add(this.ChannelChangeAndSave);
            this.channelAtt.Location = new System.Drawing.Point(158, 31);
            this.channelAtt.Name = "channelAtt";
            this.channelAtt.Size = new System.Drawing.Size(526, 513);
            this.channelAtt.TabIndex = 8;
            // 
            // viewapkdir
            // 
            this.viewapkdir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.viewapkdir.Location = new System.Drawing.Point(431, 487);
            this.viewapkdir.Name = "viewapkdir";
            this.viewapkdir.Size = new System.Drawing.Size(92, 23);
            this.viewapkdir.TabIndex = 10;
            this.viewapkdir.Text = "打开apk目录";
            this.viewapkdir.UseVisualStyleBackColor = true;
            this.viewapkdir.Click += new System.EventHandler(this.viewapkdir_Click);
            // 
            // pack
            // 
            this.pack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pack.Location = new System.Drawing.Point(350, 487);
            this.pack.Name = "pack";
            this.pack.Size = new System.Drawing.Size(75, 23);
            this.pack.TabIndex = 9;
            this.pack.Text = "打包";
            this.pack.UseVisualStyleBackColor = true;
            this.pack.Click += new System.EventHandler(this.pack_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "渠道属性";
            // 
            // channelAttPanel
            // 
            this.channelAttPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.channelAttPanel.AutoScroll = true;
            this.channelAttPanel.Location = new System.Drawing.Point(3, 21);
            this.channelAttPanel.Name = "channelAttPanel";
            this.channelAttPanel.Size = new System.Drawing.Size(520, 460);
            this.channelAttPanel.TabIndex = 7;
            // 
            // ChannelChangeAndSave
            // 
            this.ChannelChangeAndSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChannelChangeAndSave.Location = new System.Drawing.Point(269, 488);
            this.ChannelChangeAndSave.Name = "ChannelChangeAndSave";
            this.ChannelChangeAndSave.Size = new System.Drawing.Size(75, 23);
            this.ChannelChangeAndSave.TabIndex = 6;
            this.ChannelChangeAndSave.Text = "修改";
            this.ChannelChangeAndSave.UseVisualStyleBackColor = true;
            this.ChannelChangeAndSave.Click += new System.EventHandler(this.ChannelChangeAndSave_Click);
            // 
            // channelList
            // 
            this.channelList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.channelList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.渠道列表});
            this.channelList.Location = new System.Drawing.Point(8, 31);
            this.channelList.MultiSelect = false;
            this.channelList.Name = "channelList";
            this.channelList.Size = new System.Drawing.Size(121, 482);
            this.channelList.TabIndex = 7;
            this.channelList.UseCompatibleStateImageBehavior = false;
            this.channelList.View = System.Windows.Forms.View.Details;
            this.channelList.SelectedIndexChanged += new System.EventHandler(this.channelList_SelectedIndexChanged);
            // 
            // 渠道列表
            // 
            this.渠道列表.Text = "渠道列表";
            this.渠道列表.Width = 100;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "渠道";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "entrace.bmp");
            this.imageList1.Images.SetKeyName(1, "key.bmp");
            this.imageList1.Images.SetKeyName(2, "key_entrace.bmp");
            this.imageList1.Images.SetKeyName(3, "none.bmp");
            // 
            // exportapk
            // 
            this.exportapk.Location = new System.Drawing.Point(216, 169);
            this.exportapk.Name = "exportapk";
            this.exportapk.Size = new System.Drawing.Size(75, 23);
            this.exportapk.TabIndex = 9;
            this.exportapk.Text = "导入新APK";
            this.exportapk.UseVisualStyleBackColor = true;
            this.exportapk.Click += new System.EventHandler(this.exportapk_Click);
            // 
            // exporticon
            // 
            this.exporticon.Location = new System.Drawing.Point(216, 128);
            this.exporticon.Name = "exporticon";
            this.exporticon.Size = new System.Drawing.Size(75, 23);
            this.exporticon.TabIndex = 10;
            this.exporticon.Text = "导入新ICON";
            this.exporticon.UseVisualStyleBackColor = true;
            this.exporticon.Click += new System.EventHandler(this.exporticon_Click);
            // 
            // viewDirctory
            // 
            this.viewDirctory.Location = new System.Drawing.Point(216, 211);
            this.viewDirctory.Name = "viewDirctory";
            this.viewDirctory.Size = new System.Drawing.Size(75, 23);
            this.viewDirctory.TabIndex = 11;
            this.viewDirctory.Text = "打开目录";
            this.viewDirctory.UseVisualStyleBackColor = true;
            this.viewDirctory.Click += new System.EventHandler(this.viewDirctory_Click);
            // 
            // GameManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 622);
            this.Controls.Add(this.viewDirctory);
            this.Controls.Add(this.exporticon);
            this.Controls.Add(this.exportapk);
            this.Controls.Add(this.ChannelPanel);
            this.Controls.Add(this.viewAtt);
            this.Controls.Add(this.viewChannel);
            this.Controls.Add(this.AttPanel);
            this.Controls.Add(this.gameList);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameManagerForm";
            this.Text = "GameManager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameManagerForm_FormClosed);
            this.Load += new System.EventHandler(this.GameManager_Load);
            this.Shown += new System.EventHandler(this.GameManager_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.AttPanel.ResumeLayout(false);
            this.AttPanel.PerformLayout();
            this.ChannelPanel.ResumeLayout(false);
            this.ChannelPanel.PerformLayout();
            this.channelAtt.ResumeLayout(false);
            this.channelAtt.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 导入U8SDK工作目录ToolStripMenuItem;
        private System.Windows.Forms.ListView gameList;
        private System.Windows.Forms.ColumnHeader name;
        private System.Windows.Forms.Panel att;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button changeAndSave;
        private System.Windows.Forms.Panel AttPanel;
        private System.Windows.Forms.Button viewAtt;
        private System.Windows.Forms.Button viewChannel;
        private System.Windows.Forms.Panel ChannelPanel;
        private System.Windows.Forms.Button ChannelChangeAndSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel channelAtt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel channelAttPanel;
        private System.Windows.Forms.ListView channelList;
        private System.Windows.Forms.ColumnHeader 渠道列表;
        private System.Windows.Forms.Button addChannel;
        private System.Windows.Forms.Button pack;
        private System.Windows.Forms.Button packAll;
        private System.Windows.Forms.Button viewapkdir;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button exportapk;
        private System.Windows.Forms.Button exporticon;
        private System.Windows.Forms.Button viewDirctory;
    }
}