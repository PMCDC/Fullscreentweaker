
namespace FT.WinClient
{
    partial class MainForm
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lvWindows = new System.Windows.Forms.ListView();
            this.btnFullscreenize = new System.Windows.Forms.Button();
            this.chkStayOnTop = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.lblCountInfo = new System.Windows.Forms.Label();
            this.lblTopLevelWindows = new System.Windows.Forms.Label();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.chk4x3 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(12, 324);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(83, 23);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lvWindows
            // 
            this.lvWindows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvWindows.FullRowSelect = true;
            this.lvWindows.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvWindows.HideSelection = false;
            this.lvWindows.Location = new System.Drawing.Point(12, 30);
            this.lvWindows.Name = "lvWindows";
            this.lvWindows.Size = new System.Drawing.Size(380, 288);
            this.lvWindows.TabIndex = 1;
            this.lvWindows.UseCompatibleStateImageBehavior = false;
            this.lvWindows.View = System.Windows.Forms.View.Details;
            // 
            // btnFullscreenize
            // 
            this.btnFullscreenize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFullscreenize.Location = new System.Drawing.Point(13, 371);
            this.btnFullscreenize.Name = "btnFullscreenize";
            this.btnFullscreenize.Size = new System.Drawing.Size(83, 23);
            this.btnFullscreenize.TabIndex = 2;
            this.btnFullscreenize.Text = "Fullscreenize";
            this.btnFullscreenize.UseVisualStyleBackColor = true;
            this.btnFullscreenize.Click += new System.EventHandler(this.btnFullscreenize_Click);
            // 
            // chkStayOnTop
            // 
            this.chkStayOnTop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkStayOnTop.AutoSize = true;
            this.chkStayOnTop.Checked = true;
            this.chkStayOnTop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStayOnTop.Location = new System.Drawing.Point(101, 364);
            this.chkStayOnTop.Name = "chkStayOnTop";
            this.chkStayOnTop.Size = new System.Drawing.Size(291, 19);
            this.chkStayOnTop.TabIndex = 3;
            this.chkStayOnTop.Text = "Apply \"stay on top\" flag to avoid taskbar flickering";
            this.chkStayOnTop.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(337, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(55, 15);
            this.linkLabel1.TabIndex = 4;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "↪ github";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblCountInfo
            // 
            this.lblCountInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCountInfo.AutoSize = true;
            this.lblCountInfo.Location = new System.Drawing.Point(101, 328);
            this.lblCountInfo.Name = "lblCountInfo";
            this.lblCountInfo.Size = new System.Drawing.Size(166, 15);
            this.lblCountInfo.TabIndex = 5;
            this.lblCountInfo.Text = "x Toplevel window(s) detected";
            this.lblCountInfo.Visible = false;
            // 
            // lblTopLevelWindows
            // 
            this.lblTopLevelWindows.AutoSize = true;
            this.lblTopLevelWindows.Location = new System.Drawing.Point(13, 8);
            this.lblTopLevelWindows.Name = "lblTopLevelWindows";
            this.lblTopLevelWindows.Size = new System.Drawing.Size(111, 15);
            this.lblTopLevelWindows.TabIndex = 6;
            this.lblTopLevelWindows.Text = "Toplevel window(s):";
            // 
            // lblSeparator
            // 
            this.lblSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeparator.Location = new System.Drawing.Point(13, 351);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(379, 2);
            this.lblSeparator.TabIndex = 7;
            this.lblSeparator.Text = "label1";
            // 
            // chk4x3
            // 
            this.chk4x3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chk4x3.AutoSize = true;
            this.chk4x3.Location = new System.Drawing.Point(101, 383);
            this.chk4x3.Name = "chk4x3";
            this.chk4x3.Size = new System.Drawing.Size(75, 19);
            this.chk4x3.TabIndex = 8;
            this.chk4x3.Text = "4:3 Game";
            this.chk4x3.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 411);
            this.Controls.Add(this.chk4x3);
            this.Controls.Add(this.lblSeparator);
            this.Controls.Add(this.lblTopLevelWindows);
            this.Controls.Add(this.lblCountInfo);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.chkStayOnTop);
            this.Controls.Add(this.btnFullscreenize);
            this.Controls.Add(this.lvWindows);
            this.Controls.Add(this.btnRefresh);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 450);
            this.Name = "MainForm";
            this.Text = "Fullscreen Tweaker";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.MainForm_HelpButtonClicked);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ListView lvWindows;
        private System.Windows.Forms.Button btnFullscreenize;
        private System.Windows.Forms.CheckBox chkStayOnTop;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lblCountInfo;
        private System.Windows.Forms.Label lblTopLevelWindows;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.CheckBox chk4x3;
    }
}

