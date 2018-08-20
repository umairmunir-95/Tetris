namespace Tetris
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.picField = new System.Windows.Forms.PictureBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.mnuMain = new System.Windows.Forms.ToolStrip();
            this.mnuGame = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuGamePlay = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGameSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGameSettingsEasy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGameSettingsHard = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGameSettingsSound = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblLines = new System.Windows.Forms.Label();
            this.lblScore = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picField)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.mnuMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picField
            // 
            this.picField.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.picField.Location = new System.Drawing.Point(294, 52);
            this.picField.Name = "picField";
            this.picField.Size = new System.Drawing.Size(240, 408);
            this.picField.TabIndex = 0;
            this.picField.TabStop = false;
            this.picField.Paint += new System.Windows.Forms.PaintEventHandler(this.picField_Paint);
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.picPreview.Location = new System.Drawing.Point(42, 100);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(120, 120);
            this.picPreview.TabIndex = 1;
            this.picPreview.TabStop = false;
            this.picPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.picPreview_Paint);
            // 
            // tmrGame
            // 
            this.tmrGame.Interval = 1000;
            this.tmrGame.Tick += new System.EventHandler(this.tmrGame_Tick);
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGame});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(574, 25);
            this.mnuMain.TabIndex = 6;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuGame
            // 
            this.mnuGame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGamePlay,
            this.menuGameSettings,
            this.exitToolStripMenuItem});
            this.mnuGame.Image = ((System.Drawing.Image)(resources.GetObject("mnuGame.Image")));
            this.mnuGame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuGame.Name = "mnuGame";
            this.mnuGame.Size = new System.Drawing.Size(51, 22);
            this.mnuGame.Text = "Game";
            this.mnuGame.Click += new System.EventHandler(this.mnuGame_Click);
            // 
            // mnuGamePlay
            // 
            this.mnuGamePlay.Name = "mnuGamePlay";
            this.mnuGamePlay.Size = new System.Drawing.Size(152, 22);
            this.mnuGamePlay.Text = "&Start";
            this.mnuGamePlay.Click += new System.EventHandler(this.mnuGamePlay_Click);
            // 
            // menuGameSettings
            // 
            this.menuGameSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGameSettingsEasy,
            this.mnuGameSettingsHard,
            this.mnuGameSettingsSound});
            this.menuGameSettings.Name = "menuGameSettings";
            this.menuGameSettings.Size = new System.Drawing.Size(152, 22);
            this.menuGameSettings.Text = "Setting...";
            // 
            // mnuGameSettingsEasy
            // 
            this.mnuGameSettingsEasy.Checked = true;
            this.mnuGameSettingsEasy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuGameSettingsEasy.Name = "mnuGameSettingsEasy";
            this.mnuGameSettingsEasy.Size = new System.Drawing.Size(108, 22);
            this.mnuGameSettingsEasy.Text = "Easy";
            this.mnuGameSettingsEasy.Click += new System.EventHandler(this.mnuGameSettingsEasy_Click);
            // 
            // mnuGameSettingsHard
            // 
            this.mnuGameSettingsHard.Name = "mnuGameSettingsHard";
            this.mnuGameSettingsHard.Size = new System.Drawing.Size(108, 22);
            this.mnuGameSettingsHard.Text = "Hard";
            this.mnuGameSettingsHard.Click += new System.EventHandler(this.mnuGameSettingsHard_Click);
            // 
            // mnuGameSettingsSound
            // 
            this.mnuGameSettingsSound.Checked = true;
            this.mnuGameSettingsSound.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuGameSettingsSound.Name = "mnuGameSettingsSound";
            this.mnuGameSettingsSound.Size = new System.Drawing.Size(108, 22);
            this.mnuGameSettingsSound.Text = "Sound";
            this.mnuGameSettingsSound.Click += new System.EventHandler(this.mnuGameSettingsSound_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.DarkGreen;
            this.label1.Font = new System.Drawing.Font("Modern No. 20", 15.75F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(37, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Next Block!!!";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.BackColor = System.Drawing.Color.DarkGreen;
            this.lblSpeed.Font = new System.Drawing.Font("Modern No. 20", 20.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpeed.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblSpeed.Location = new System.Drawing.Point(6, 156);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(96, 29);
            this.lblSpeed.TabIndex = 5;
            this.lblSpeed.Text = "Speed :";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.BackColor = System.Drawing.Color.DarkGreen;
            this.lblLevel.Font = new System.Drawing.Font("Modern No. 20", 20.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblLevel.Location = new System.Drawing.Point(7, 115);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(95, 29);
            this.lblLevel.TabIndex = 4;
            this.lblLevel.Text = "Level :";
            // 
            // lblLines
            // 
            this.lblLines.AutoSize = true;
            this.lblLines.BackColor = System.Drawing.Color.DarkGreen;
            this.lblLines.Font = new System.Drawing.Font("Modern No. 20", 20.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLines.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblLines.Location = new System.Drawing.Point(7, 77);
            this.lblLines.Name = "lblLines";
            this.lblLines.Size = new System.Drawing.Size(96, 29);
            this.lblLines.TabIndex = 3;
            this.lblLines.Text = "Lines :";
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.BackColor = System.Drawing.Color.DarkGreen;
            this.lblScore.Font = new System.Drawing.Font("Modern No. 20", 20.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic) 
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblScore.Location = new System.Drawing.Point(9, 38);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(93, 29);
            this.lblScore.TabIndex = 2;
            this.lblScore.Text = "Score :";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox1.Controls.Add(this.lblScore);
            this.groupBox1.Controls.Add(this.lblSpeed);
            this.groupBox1.Controls.Add(this.lblLines);
            this.groupBox1.Controls.Add(this.lblLevel);
            this.groupBox1.Font = new System.Drawing.Font("Modern No. 20", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkGreen;
            this.groupBox1.Location = new System.Drawing.Point(24, 245);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(229, 203);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistics";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Tetris.Properties.Resources._6;
            this.ClientSize = new System.Drawing.Size(574, 469);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.picPreview);
            this.Controls.Add(this.picField);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.picField)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picField;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Timer tmrGame;
        private System.Windows.Forms.ToolStrip mnuMain;
        private System.Windows.Forms.ToolStripDropDownButton mnuGame;
        private System.Windows.Forms.ToolStripMenuItem mnuGamePlay;
        private System.Windows.Forms.ToolStripMenuItem menuGameSettings;
        private System.Windows.Forms.ToolStripMenuItem mnuGameSettingsEasy;
        private System.Windows.Forms.ToolStripMenuItem mnuGameSettingsHard;
        private System.Windows.Forms.ToolStripMenuItem mnuGameSettingsSound;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblLines;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

