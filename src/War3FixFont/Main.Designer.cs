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
            this.VersionLabel = new System.Windows.Forms.Label();
            this.EnableTimerFixCheckBox = new System.Windows.Forms.CheckBox();
            this.IntervalInput = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ManualButton = new System.Windows.Forms.Button();
            this.NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.PanelMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowWindowMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitAppMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AutoApplyCheckBox = new System.Windows.Forms.CheckBox();
            this.LockCursorCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.WidthInput = new System.Windows.Forms.NumericUpDown();
            this.HeightInput = new System.Windows.Forms.NumericUpDown();
            this.CustomPositionY = new System.Windows.Forms.NumericUpDown();
            this.CustomPositionX = new System.Windows.Forms.NumericUpDown();
            this.CustomPositionCheckBox = new System.Windows.Forms.CheckBox();
            this.WindowModeComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CustomSizeLabel = new System.Windows.Forms.Label();
            this.ShowMeHotKeyInputBox = new War3FixFont.HotKeyInputBox();
            this.HotKeyInputBox = new War3FixFont.HotKeyInputBox();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalInput)).BeginInit();
            this.PanelMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WidthInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomPositionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomPositionX)).BeginInit();
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
            this.EnableHotKeyCheckBox.Location = new System.Drawing.Point(14, 185);
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
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "作者：Zonciu";
            // 
            // LinkLabel
            // 
            this.LinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LinkLabel.AutoSize = true;
            this.LinkLabel.Location = new System.Drawing.Point(10, 335);
            this.LinkLabel.Name = "LinkLabel";
            this.LinkLabel.Size = new System.Drawing.Size(227, 12);
            this.LinkLabel.TabIndex = 3;
            this.LinkLabel.TabStop = true;
            this.LinkLabel.Text = "https://github.com/Zonciu/War3FixFont";
            this.LinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_LinkClicked);
            // 
            // VersionLabel
            // 
            this.VersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.VersionLabel.Location = new System.Drawing.Point(281, 335);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(75, 12);
            this.VersionLabel.TabIndex = 11;
            this.VersionLabel.Text = "vx.x";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // EnableTimerFixCheckBox
            // 
            this.EnableTimerFixCheckBox.AutoSize = true;
            this.EnableTimerFixCheckBox.Location = new System.Drawing.Point(14, 49);
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
            this.IntervalInput.Location = new System.Drawing.Point(93, 47);
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
            this.label2.Location = new System.Drawing.Point(154, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 17;
            this.label2.Text = "秒";
            // 
            // ManualButton
            // 
            this.ManualButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ManualButton.Location = new System.Drawing.Point(281, 304);
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
            this.NotifyIcon.Text = "魔兽3叠字修复";
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // PanelMenu
            // 
            this.PanelMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowWindowMenuItem,
            this.ExitAppMenuItem});
            this.PanelMenu.Name = "PanelMenu";
            this.PanelMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // ShowWindowMenuItem
            // 
            this.ShowWindowMenuItem.Name = "ShowWindowMenuItem";
            this.ShowWindowMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ShowWindowMenuItem.Text = "显示";
            this.ShowWindowMenuItem.Click += new System.EventHandler(this.ShowWindowMenuItem_Click);
            // 
            // ExitAppMenuItem
            // 
            this.ExitAppMenuItem.Name = "ExitAppMenuItem";
            this.ExitAppMenuItem.Size = new System.Drawing.Size(100, 22);
            this.ExitAppMenuItem.Text = "退出";
            this.ExitAppMenuItem.Click += new System.EventHandler(this.ExitApplication);
            // 
            // AutoApplyCheckBox
            // 
            this.AutoApplyCheckBox.AutoSize = true;
            this.AutoApplyCheckBox.Location = new System.Drawing.Point(193, 49);
            this.AutoApplyCheckBox.Name = "AutoApplyCheckBox";
            this.AutoApplyCheckBox.Size = new System.Drawing.Size(96, 16);
            this.AutoApplyCheckBox.TabIndex = 22;
            this.AutoApplyCheckBox.Text = "自动设置窗口";
            this.AutoApplyCheckBox.UseVisualStyleBackColor = true;
            this.AutoApplyCheckBox.CheckedChanged += new System.EventHandler(this.AutoWindowCheckBox_CheckedChanged);
            // 
            // LockCursorCheckBox
            // 
            this.LockCursorCheckBox.AutoSize = true;
            this.LockCursorCheckBox.Location = new System.Drawing.Point(14, 220);
            this.LockCursorCheckBox.Name = "LockCursorCheckBox";
            this.LockCursorCheckBox.Size = new System.Drawing.Size(96, 16);
            this.LockCursorCheckBox.TabIndex = 23;
            this.LockCursorCheckBox.Text = "锁定鼠标范围";
            this.LockCursorCheckBox.UseVisualStyleBackColor = true;
            this.LockCursorCheckBox.CheckedChanged += new System.EventHandler(this.LockCursorCheckBox_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 255);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "显示本窗口";
            // 
            // WidthInput
            // 
            this.WidthInput.Location = new System.Drawing.Point(112, 113);
            this.WidthInput.Margin = new System.Windows.Forms.Padding(0);
            this.WidthInput.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.WidthInput.Name = "WidthInput";
            this.WidthInput.Size = new System.Drawing.Size(54, 21);
            this.WidthInput.TabIndex = 27;
            this.WidthInput.Value = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            this.WidthInput.ValueChanged += new System.EventHandler(this.WidthInput_ValueChanged);
            // 
            // HeightInput
            // 
            this.HeightInput.Location = new System.Drawing.Point(175, 113);
            this.HeightInput.Margin = new System.Windows.Forms.Padding(0);
            this.HeightInput.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.HeightInput.Name = "HeightInput";
            this.HeightInput.Size = new System.Drawing.Size(54, 21);
            this.HeightInput.TabIndex = 28;
            this.HeightInput.Value = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.HeightInput.ValueChanged += new System.EventHandler(this.HeightInput_ValueChanged);
            // 
            // CustomPositionY
            // 
            this.CustomPositionY.Location = new System.Drawing.Point(175, 148);
            this.CustomPositionY.Margin = new System.Windows.Forms.Padding(0);
            this.CustomPositionY.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.CustomPositionY.Name = "CustomPositionY";
            this.CustomPositionY.Size = new System.Drawing.Size(54, 21);
            this.CustomPositionY.TabIndex = 32;
            this.CustomPositionY.ValueChanged += new System.EventHandler(this.CustomPositionY_ValueChanged);
            // 
            // CustomPositionX
            // 
            this.CustomPositionX.Location = new System.Drawing.Point(112, 148);
            this.CustomPositionX.Margin = new System.Windows.Forms.Padding(0);
            this.CustomPositionX.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.CustomPositionX.Name = "CustomPositionX";
            this.CustomPositionX.Size = new System.Drawing.Size(54, 21);
            this.CustomPositionX.TabIndex = 31;
            this.CustomPositionX.ValueChanged += new System.EventHandler(this.CustomPositionX_ValueChanged);
            // 
            // CustomPositionCheckBox
            // 
            this.CustomPositionCheckBox.AutoSize = true;
            this.CustomPositionCheckBox.Checked = true;
            this.CustomPositionCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CustomPositionCheckBox.Location = new System.Drawing.Point(14, 150);
            this.CustomPositionCheckBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CustomPositionCheckBox.Name = "CustomPositionCheckBox";
            this.CustomPositionCheckBox.Size = new System.Drawing.Size(84, 16);
            this.CustomPositionCheckBox.TabIndex = 33;
            this.CustomPositionCheckBox.Text = "自定义位置";
            this.CustomPositionCheckBox.UseVisualStyleBackColor = true;
            this.CustomPositionCheckBox.CheckedChanged += new System.EventHandler(this.CustomPositionCheckBox_CheckedChanged);
            // 
            // WindowModeComboBox
            // 
            this.WindowModeComboBox.FormattingEnabled = true;
            this.WindowModeComboBox.Location = new System.Drawing.Point(112, 80);
            this.WindowModeComboBox.Name = "WindowModeComboBox";
            this.WindowModeComboBox.Size = new System.Drawing.Size(121, 20);
            this.WindowModeComboBox.TabIndex = 37;
            this.WindowModeComboBox.SelectedIndexChanged += new System.EventHandler(this.WindowModeComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 38;
            this.label4.Text = "窗口模式";
            // 
            // CustomSizeLabel
            // 
            this.CustomSizeLabel.AutoSize = true;
            this.CustomSizeLabel.Location = new System.Drawing.Point(12, 117);
            this.CustomSizeLabel.Name = "CustomSizeLabel";
            this.CustomSizeLabel.Size = new System.Drawing.Size(65, 12);
            this.CustomSizeLabel.TabIndex = 39;
            this.CustomSizeLabel.Text = "自定义大小";
            // 
            // ShowMeHotKeyInputBox
            // 
            this.ShowMeHotKeyInputBox.Location = new System.Drawing.Point(112, 251);
            this.ShowMeHotKeyInputBox.Name = "ShowMeHotKeyInputBox";
            this.ShowMeHotKeyInputBox.ShortcutsEnabled = false;
            this.ShowMeHotKeyInputBox.Size = new System.Drawing.Size(153, 21);
            this.ShowMeHotKeyInputBox.TabIndex = 24;
            this.ShowMeHotKeyInputBox.WordWrap = false;
            this.ShowMeHotKeyInputBox.HotKeyChanged += new System.EventHandler(this.ShowMeHotKeyInputBox_HotKeyChanged);
            // 
            // HotKeyInputBox
            // 
            this.HotKeyInputBox.BackColor = System.Drawing.SystemColors.Window;
            this.HotKeyInputBox.Location = new System.Drawing.Point(112, 183);
            this.HotKeyInputBox.Name = "HotKeyInputBox";
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
            this.ClientSize = new System.Drawing.Size(368, 356);
            this.Controls.Add(this.CustomSizeLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.WindowModeComboBox);
            this.Controls.Add(this.CustomPositionCheckBox);
            this.Controls.Add(this.CustomPositionY);
            this.Controls.Add(this.CustomPositionX);
            this.Controls.Add(this.HeightInput);
            this.Controls.Add(this.WidthInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ShowMeHotKeyInputBox);
            this.Controls.Add(this.LockCursorCheckBox);
            this.Controls.Add(this.AutoApplyCheckBox);
            this.Controls.Add(this.ManualButton);
            this.Controls.Add(this.EnableTimerFixCheckBox);
            this.Controls.Add(this.IntervalInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HotKeyInputBox);
            this.Controls.Add(this.VersionLabel);
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
            ((System.ComponentModel.ISupportInitialize)(this.WidthInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomPositionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CustomPositionX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    private War3FixFont.HotKeyInputBox HotKeyInputBox;
    private System.Windows.Forms.CheckBox EnableHotKeyCheckBox;

    #endregion

    private Button FixButton;
    private Label label1;
    private LinkLabel LinkLabel;
    private Label VersionLabel;
    private CheckBox EnableTimerFixCheckBox;
    private NumericUpDown IntervalInput;
    private Label label2;
    private Button ManualButton;
    private NotifyIcon NotifyIcon;
    private ContextMenuStrip PanelMenu;
    private ToolStripMenuItem ShowWindowMenuItem;
    private ToolStripMenuItem ExitAppMenuItem;
    private CheckBox AutoApplyCheckBox;
    private CheckBox LockCursorCheckBox;
    private HotKeyInputBox ShowMeHotKeyInputBox;
    private Label label3;
    private NumericUpDown WidthInput;
    private NumericUpDown HeightInput;
    private NumericUpDown CustomPositionY;
    private NumericUpDown CustomPositionX;
    private CheckBox CustomPositionCheckBox;
    private ComboBox WindowModeComboBox;
    private Label label4;
    private Label CustomSizeLabel;
}