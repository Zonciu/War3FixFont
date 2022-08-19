using System.Windows.Forms;

namespace War3FixFont;

partial class Main
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.FixButton = new System.Windows.Forms.Button();
            this.EnableHotKeyCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LinkLabel = new System.Windows.Forms.LinkLabel();
            this.ResetBorderButton = new System.Windows.Forms.Button();
            this.FullScreenButton = new System.Windows.Forms.Button();
            this.EnableFullScreenCheckBox = new System.Windows.Forms.CheckBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.EnableTimerFixCheckBox = new System.Windows.Forms.CheckBox();
            this.IntervalInput = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.Mode2CheckBox = new System.Windows.Forms.CheckBox();
            this.ManualButton = new System.Windows.Forms.Button();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.PanelMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowFormMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitAppMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HotKeyInputBox = new War3FixFont.HotKeyInputBox();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalInput)).BeginInit();
            this.PanelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // FixButton
            // 
            this.FixButton.Location = new System.Drawing.Point(12, 12);
            this.FixButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FixButton.Name = "FixButton";
            this.FixButton.Size = new System.Drawing.Size(75, 23);
            this.FixButton.TabIndex = 0;
            this.FixButton.Text = "修复一次";
            this.FixButton.UseVisualStyleBackColor = true;
            this.FixButton.Click += new System.EventHandler(this.FixButton_Click);
            // 
            // EnableHotKeyCheckBox
            // 
            this.EnableHotKeyCheckBox.AutoSize = true;
            this.EnableHotKeyCheckBox.Checked = true;
            this.EnableHotKeyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnableHotKeyCheckBox.Location = new System.Drawing.Point(12, 92);
            this.EnableHotKeyCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EnableHotKeyCheckBox.Name = "EnableHotKeyCheckBox";
            this.EnableHotKeyCheckBox.Size = new System.Drawing.Size(84, 16);
            this.EnableHotKeyCheckBox.TabIndex = 1;
            this.EnableHotKeyCheckBox.Text = "使用快捷键";
            this.EnableHotKeyCheckBox.UseVisualStyleBackColor = true;
            this.EnableHotKeyCheckBox.CheckedChanged += new System.EventHandler(this.EnableHotKeyCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "作者：Zonciu";
            // 
            // LinkLabel
            // 
            this.LinkLabel.AutoSize = true;
            this.LinkLabel.Location = new System.Drawing.Point(10, 205);
            this.LinkLabel.Name = "LinkLabel";
            this.LinkLabel.Size = new System.Drawing.Size(227, 12);
            this.LinkLabel.TabIndex = 3;
            this.LinkLabel.TabStop = true;
            this.LinkLabel.Text = "https://github.com/Zonciu/War3FixFont";
            this.LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // ResetBorderButton
            // 
            this.ResetBorderButton.Location = new System.Drawing.Point(236, 39);
            this.ResetBorderButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ResetBorderButton.Name = "ResetBorderButton";
            this.ResetBorderButton.Size = new System.Drawing.Size(75, 23);
            this.ResetBorderButton.TabIndex = 4;
            this.ResetBorderButton.Text = "恢复边框";
            this.ResetBorderButton.UseVisualStyleBackColor = true;
            this.ResetBorderButton.Click += new System.EventHandler(this.SetBorderButton_Click);
            // 
            // FullScreenButton
            // 
            this.FullScreenButton.Location = new System.Drawing.Point(236, 12);
            this.FullScreenButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FullScreenButton.Name = "FullScreenButton";
            this.FullScreenButton.Size = new System.Drawing.Size(75, 23);
            this.FullScreenButton.TabIndex = 5;
            this.FullScreenButton.Text = "无边框全屏";
            this.FullScreenButton.UseVisualStyleBackColor = true;
            this.FullScreenButton.Click += new System.EventHandler(this.FullScreenButton_Click);
            // 
            // EnableFullScreenCheckBox
            // 
            this.EnableFullScreenCheckBox.AutoSize = true;
            this.EnableFullScreenCheckBox.Checked = true;
            this.EnableFullScreenCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EnableFullScreenCheckBox.Location = new System.Drawing.Point(12, 72);
            this.EnableFullScreenCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EnableFullScreenCheckBox.Name = "EnableFullScreenCheckBox";
            this.EnableFullScreenCheckBox.Size = new System.Drawing.Size(84, 16);
            this.EnableFullScreenCheckBox.TabIndex = 6;
            this.EnableFullScreenCheckBox.Text = "无边框全屏";
            this.EnableFullScreenCheckBox.UseVisualStyleBackColor = true;
            this.EnableFullScreenCheckBox.CheckedChanged += new System.EventHandler(this.EnableFullScreenCheckBox_Click);
            // 
            // VersionLabel
            // 
            this.VersionLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.VersionLabel.Location = new System.Drawing.Point(236, 205);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(75, 12);
            this.VersionLabel.TabIndex = 11;
            this.VersionLabel.Text = "vx.x";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EnableTimerFixCheckBox
            // 
            this.EnableTimerFixCheckBox.AutoSize = true;
            this.EnableTimerFixCheckBox.Location = new System.Drawing.Point(12, 52);
            this.EnableTimerFixCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.EnableTimerFixCheckBox.Name = "EnableTimerFixCheckBox";
            this.EnableTimerFixCheckBox.Size = new System.Drawing.Size(72, 16);
            this.EnableTimerFixCheckBox.TabIndex = 15;
            this.EnableTimerFixCheckBox.Text = "定时修复";
            this.EnableTimerFixCheckBox.UseVisualStyleBackColor = true;
            this.EnableTimerFixCheckBox.CheckedChanged += new System.EventHandler(this.EnableTimerFixCheckBox_CheckedChanged);
            // 
            // IntervalInput
            // 
            this.IntervalInput.Location = new System.Drawing.Point(93, 50);
            this.IntervalInput.Margin = new System.Windows.Forms.Padding(0);
            this.IntervalInput.Name = "IntervalInput";
            this.IntervalInput.Size = new System.Drawing.Size(54, 21);
            this.IntervalInput.TabIndex = 16;
            this.IntervalInput.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.IntervalInput.ValueChanged += new System.EventHandler(this.IntervalInput_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "秒";
            // 
            // Mode2CheckBox
            // 
            this.Mode2CheckBox.AutoSize = true;
            this.Mode2CheckBox.Location = new System.Drawing.Point(12, 114);
            this.Mode2CheckBox.Name = "Mode2CheckBox";
            this.Mode2CheckBox.Size = new System.Drawing.Size(72, 16);
            this.Mode2CheckBox.TabIndex = 18;
            this.Mode2CheckBox.Text = "第二模式";
            this.Mode2CheckBox.UseVisualStyleBackColor = true;
            this.Mode2CheckBox.CheckedChanged += new System.EventHandler(this.Mode2CheckBox_CheckedChanged);
            // 
            // ManualButton
            // 
            this.ManualButton.Location = new System.Drawing.Point(236, 174);
            this.ManualButton.Name = "ManualButton";
            this.ManualButton.Size = new System.Drawing.Size(75, 23);
            this.ManualButton.TabIndex = 19;
            this.ManualButton.Text = "使用说明";
            this.ManualButton.UseVisualStyleBackColor = true;
            this.ManualButton.Click += new System.EventHandler(this.ManualButton_Click);
            // 
            // NotifyIcon
            // 
            this.NotifyIcon.ContextMenuStrip = this.PanelMenu;
            this.NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("NotifyIcon.Icon")));
            this.NotifyIcon.Text = "notifyIcon1";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // PanelMenu
            // 
            this.PanelMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowFormMenuItem,
            this.ExitAppMenuItem});
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(181, 70);
            // 
            // ShowFormMenuItem
            // 
            this.ShowFormMenuItem.Name = "ShowFormMenuItem";
            this.ShowFormMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ShowFormMenuItem.Text = "显示";
            this.ShowFormMenuItem.Click += new System.EventHandler(this.ShowWindow);
            // 
            // ExitAppMenuItem
            // 
            this.ExitAppMenuItem.Name = "ExitAppMenuItem";
            this.ExitAppMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ExitAppMenuItem.Text = "退出";
            this.ExitAppMenuItem.Click += new System.EventHandler(this.ExitApplication);
            // 
            // HotKeyInputBox
            // 
            this.HotKeyInputBox.Alt = false;
            this.HotKeyInputBox.Control = false;
            this.HotKeyInputBox.KeyCode = System.Windows.Forms.Keys.None;
            this.HotKeyInputBox.Location = new System.Drawing.Point(102, 90);
            this.HotKeyInputBox.Name = "HotKeyInputBox";
            this.HotKeyInputBox.Shift = false;
            this.HotKeyInputBox.ShortcutsEnabled = false;
            this.HotKeyInputBox.Size = new System.Drawing.Size(153, 21);
            this.HotKeyInputBox.TabIndex = 12;
            this.HotKeyInputBox.WordWrap = false;
            this.HotKeyInputBox.HotKeyChanged += new System.EventHandler(this.HotKeyInputBox_HotKeyChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 226);
            this.Controls.Add(this.ManualButton);
            this.Controls.Add(this.Mode2CheckBox);
            this.Controls.Add(this.EnableTimerFixCheckBox);
            this.Controls.Add(this.IntervalInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HotKeyInputBox);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.EnableFullScreenCheckBox);
            this.Controls.Add(this.FullScreenButton);
            this.Controls.Add(this.ResetBorderButton);
            this.Controls.Add(this.LinkLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EnableHotKeyCheckBox);
            this.Controls.Add(this.FixButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Main";
            this.Text = "魔兽争霸3叠字修复";
            this.SizeChanged += new System.EventHandler(this.Main_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.IntervalInput)).EndInit();
            this.PanelMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private War3FixFont.HotKeyInputBox HotKeyInputBox;
    private System.Windows.Forms.CheckBox EnableHotKeyCheckBox;

    #endregion

    private Button FixButton;
    private Label label1;
    private LinkLabel LinkLabel;
    private Button ResetBorderButton;
    private Button FullScreenButton;
    private CheckBox EnableFullScreenCheckBox;
    private Label VersionLabel;
    private CheckBox EnableTimerFixCheckBox;
    private NumericUpDown IntervalInput;
    private Label label2;
    private CheckBox Mode2CheckBox;
    private Button ManualButton;
    private NotifyIcon NotifyIcon;
    private ContextMenuStrip PanelMenu;
    private ToolStripMenuItem ShowFormMenuItem;
    private ToolStripMenuItem ExitAppMenuItem;
}