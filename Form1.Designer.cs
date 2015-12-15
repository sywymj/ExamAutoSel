namespace ExamAutoSel
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButtonEntrySel = new System.Windows.Forms.ToolStripDropDownButton();
            this.入口一ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.入口二ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonSet = new System.Windows.Forms.ToolStripDropDownButton();
            this.标识ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动选择ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动翻页ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonLMSSet = new System.Windows.Forms.ToolStripDropDownButton();
            this.快速学习ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动学习ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.savePageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.离线更新题库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.公务员题库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.公务员入口ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButtonEntrySel,
            this.toolStripDropDownButtonSet,
            this.toolStripDropDownButtonLMSSet,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(738, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButtonEntrySel
            // 
            this.toolStripDropDownButtonEntrySel.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.入口一ToolStripMenuItem,
            this.入口二ToolStripMenuItem,
            this.公务员入口ToolStripMenuItem});
            this.toolStripDropDownButtonEntrySel.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonEntrySel.Image")));
            this.toolStripDropDownButtonEntrySel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonEntrySel.Name = "toolStripDropDownButtonEntrySel";
            this.toolStripDropDownButtonEntrySel.Size = new System.Drawing.Size(85, 22);
            this.toolStripDropDownButtonEntrySel.Text = "入口选择";
            // 
            // 入口一ToolStripMenuItem
            // 
            this.入口一ToolStripMenuItem.Name = "入口一ToolStripMenuItem";
            this.入口一ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.入口一ToolStripMenuItem.Text = "入口一";
            this.入口一ToolStripMenuItem.Click += new System.EventHandler(this.入口一ToolStripMenuItem_Click);
            // 
            // 入口二ToolStripMenuItem
            // 
            this.入口二ToolStripMenuItem.Name = "入口二ToolStripMenuItem";
            this.入口二ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.入口二ToolStripMenuItem.Text = "入口二";
            this.入口二ToolStripMenuItem.Click += new System.EventHandler(this.入口二ToolStripMenuItem_Click);
            // 
            // toolStripDropDownButtonSet
            // 
            this.toolStripDropDownButtonSet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.标识ToolStripMenuItem,
            this.自动选择ToolStripMenuItem,
            this.自动翻页ToolStripMenuItem});
            this.toolStripDropDownButtonSet.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonSet.Image")));
            this.toolStripDropDownButtonSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonSet.Name = "toolStripDropDownButtonSet";
            this.toolStripDropDownButtonSet.Size = new System.Drawing.Size(109, 22);
            this.toolStripDropDownButtonSet.Text = "考试参数设置";
            // 
            // 标识ToolStripMenuItem
            // 
            this.标识ToolStripMenuItem.Checked = true;
            this.标识ToolStripMenuItem.CheckOnClick = true;
            this.标识ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.标识ToolStripMenuItem.Name = "标识ToolStripMenuItem";
            this.标识ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.标识ToolStripMenuItem.Text = "自动标记";
            this.标识ToolStripMenuItem.CheckedChanged += new System.EventHandler(this.标识ToolStripMenuItem_CheckedChanged);
            this.标识ToolStripMenuItem.Click += new System.EventHandler(this.标识ToolStripMenuItem_Click);
            // 
            // 自动选择ToolStripMenuItem
            // 
            this.自动选择ToolStripMenuItem.Checked = true;
            this.自动选择ToolStripMenuItem.CheckOnClick = true;
            this.自动选择ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.自动选择ToolStripMenuItem.Name = "自动选择ToolStripMenuItem";
            this.自动选择ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.自动选择ToolStripMenuItem.Text = "自动选择";
            this.自动选择ToolStripMenuItem.CheckedChanged += new System.EventHandler(this.自动选择ToolStripMenuItem_CheckedChanged);
            // 
            // 自动翻页ToolStripMenuItem
            // 
            this.自动翻页ToolStripMenuItem.Checked = true;
            this.自动翻页ToolStripMenuItem.CheckOnClick = true;
            this.自动翻页ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.自动翻页ToolStripMenuItem.Name = "自动翻页ToolStripMenuItem";
            this.自动翻页ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.自动翻页ToolStripMenuItem.Text = "自动翻页";
            this.自动翻页ToolStripMenuItem.CheckedChanged += new System.EventHandler(this.自动翻页ToolStripMenuItem_CheckedChanged);
            // 
            // toolStripDropDownButtonLMSSet
            // 
            this.toolStripDropDownButtonLMSSet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.快速学习ToolStripMenuItem,
            this.自动学习ToolStripMenuItem});
            this.toolStripDropDownButtonLMSSet.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonLMSSet.Image")));
            this.toolStripDropDownButtonLMSSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonLMSSet.Name = "toolStripDropDownButtonLMSSet";
            this.toolStripDropDownButtonLMSSet.Size = new System.Drawing.Size(109, 22);
            this.toolStripDropDownButtonLMSSet.Text = "学习参数设置";
            // 
            // 快速学习ToolStripMenuItem
            // 
            this.快速学习ToolStripMenuItem.Checked = true;
            this.快速学习ToolStripMenuItem.CheckOnClick = true;
            this.快速学习ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.快速学习ToolStripMenuItem.Name = "快速学习ToolStripMenuItem";
            this.快速学习ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.快速学习ToolStripMenuItem.Text = "快速学习";
            this.快速学习ToolStripMenuItem.CheckedChanged += new System.EventHandler(this.快速学习ToolStripMenuItem_CheckedChanged);
            // 
            // 自动学习ToolStripMenuItem
            // 
            this.自动学习ToolStripMenuItem.Checked = true;
            this.自动学习ToolStripMenuItem.CheckOnClick = true;
            this.自动学习ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.自动学习ToolStripMenuItem.Name = "自动学习ToolStripMenuItem";
            this.自动学习ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.自动学习ToolStripMenuItem.Text = "自动学习";
            this.自动学习ToolStripMenuItem.CheckedChanged += new System.EventHandler(this.自动学习ToolStripMenuItem_CheckedChanged);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.savePageToolStripMenuItem,
            this.离线更新题库ToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(85, 22);
            this.toolStripDropDownButton1.Text = "系统功能";
            // 
            // savePageToolStripMenuItem
            // 
            this.savePageToolStripMenuItem.Name = "savePageToolStripMenuItem";
            this.savePageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.savePageToolStripMenuItem.Text = "更新题库";
            this.savePageToolStripMenuItem.Click += new System.EventHandler(this.savePageToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 456);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(738, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(738, 431);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.webBrowser1);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(712, 423);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "浏览器";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(706, 417);
            this.webBrowser1.TabIndex = 0;
            // 
            // 离线更新题库ToolStripMenuItem
            // 
            this.离线更新题库ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.公务员题库ToolStripMenuItem});
            this.离线更新题库ToolStripMenuItem.Name = "离线更新题库ToolStripMenuItem";
            this.离线更新题库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.离线更新题库ToolStripMenuItem.Text = "离线更新题库";
            // 
            // 公务员题库ToolStripMenuItem
            // 
            this.公务员题库ToolStripMenuItem.Name = "公务员题库ToolStripMenuItem";
            this.公务员题库ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.公务员题库ToolStripMenuItem.Text = "公务员题库";
            this.公务员题库ToolStripMenuItem.Click += new System.EventHandler(this.公务员题库ToolStripMenuItem_Click);
            // 
            // 公务员入口ToolStripMenuItem
            // 
            this.公务员入口ToolStripMenuItem.Name = "公务员入口ToolStripMenuItem";
            this.公务员入口ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.公务员入口ToolStripMenuItem.Text = "公务员入口";
            this.公务员入口ToolStripMenuItem.Click += new System.EventHandler(this.公务员入口ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 478);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ExamAutoSel  Version:6.01(本程序仅供学习交流之用，请勿用于非法用途，违者后果自负)";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonSet;
        private System.Windows.Forms.ToolStripMenuItem 标识ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自动选择ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自动翻页ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonEntrySel;
        private System.Windows.Forms.ToolStripMenuItem 入口一ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 入口二ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonLMSSet;
        private System.Windows.Forms.ToolStripMenuItem 快速学习ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自动学习ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem savePageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 离线更新题库ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 公务员题库ToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem 公务员入口ToolStripMenuItem;
    }
}

