namespace LevelGenerator
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
            this.programName = new System.Windows.Forms.Label();
            this.levelWidthLabel = new System.Windows.Forms.Label();
            this.numEnemiesLabel = new System.Windows.Forms.Label();
            this.levelWidth = new System.Windows.Forms.TextBox();
            this.numEnemies = new System.Windows.Forms.TextBox();
            this.createLevel = new System.Windows.Forms.Button();
            this.levelNameLabel = new System.Windows.Forms.Label();
            this.levelName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // programName
            // 
            this.programName.AutoSize = true;
            this.programName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.programName.Location = new System.Drawing.Point(30, 19);
            this.programName.Name = "programName";
            this.programName.Size = new System.Drawing.Size(208, 31);
            this.programName.TabIndex = 0;
            this.programName.Text = "Level Generator";
            // 
            // levelWidthLabel
            // 
            this.levelWidthLabel.AutoSize = true;
            this.levelWidthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.levelWidthLabel.Location = new System.Drawing.Point(32, 120);
            this.levelWidthLabel.Name = "levelWidthLabel";
            this.levelWidthLabel.Size = new System.Drawing.Size(91, 20);
            this.levelWidthLabel.TabIndex = 1;
            this.levelWidthLabel.Text = "Level Width";
            // 
            // numEnemiesLabel
            // 
            this.numEnemiesLabel.AutoSize = true;
            this.numEnemiesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.numEnemiesLabel.Location = new System.Drawing.Point(32, 169);
            this.numEnemiesLabel.Name = "numEnemiesLabel";
            this.numEnemiesLabel.Size = new System.Drawing.Size(149, 20);
            this.numEnemiesLabel.TabIndex = 2;
            this.numEnemiesLabel.Text = "Number of Enemies";
            // 
            // levelWidth
            // 
            this.levelWidth.Location = new System.Drawing.Point(200, 122);
            this.levelWidth.Name = "levelWidth";
            this.levelWidth.Size = new System.Drawing.Size(100, 20);
            this.levelWidth.TabIndex = 3;
            this.levelWidth.TextChanged += new System.EventHandler(this.levelWidth_TextChanged);
            // 
            // numEnemies
            // 
            this.numEnemies.Location = new System.Drawing.Point(200, 169);
            this.numEnemies.Name = "numEnemies";
            this.numEnemies.Size = new System.Drawing.Size(100, 20);
            this.numEnemies.TabIndex = 4;
            this.numEnemies.TextChanged += new System.EventHandler(this.numEnemies_TextChanged);
            // 
            // createLevel
            // 
            this.createLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.createLevel.Location = new System.Drawing.Point(181, 261);
            this.createLevel.Name = "createLevel";
            this.createLevel.Size = new System.Drawing.Size(119, 36);
            this.createLevel.TabIndex = 5;
            this.createLevel.Text = "Create Level";
            this.createLevel.UseVisualStyleBackColor = true;
            this.createLevel.Click += new System.EventHandler(this.createLevel_Click);
            // 
            // levelNameLabel
            // 
            this.levelNameLabel.AutoSize = true;
            this.levelNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.levelNameLabel.Location = new System.Drawing.Point(32, 73);
            this.levelNameLabel.Name = "levelNameLabel";
            this.levelNameLabel.Size = new System.Drawing.Size(92, 20);
            this.levelNameLabel.TabIndex = 6;
            this.levelNameLabel.Text = "Level Name";
            // 
            // levelName
            // 
            this.levelName.Location = new System.Drawing.Point(200, 73);
            this.levelName.Name = "levelName";
            this.levelName.Size = new System.Drawing.Size(100, 20);
            this.levelName.TabIndex = 7;
            this.levelName.TextChanged += new System.EventHandler(this.levelName_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 322);
            this.Controls.Add(this.levelName);
            this.Controls.Add(this.levelNameLabel);
            this.Controls.Add(this.createLevel);
            this.Controls.Add(this.numEnemies);
            this.Controls.Add(this.levelWidth);
            this.Controls.Add(this.numEnemiesLabel);
            this.Controls.Add(this.levelWidthLabel);
            this.Controls.Add(this.programName);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label programName;
        private System.Windows.Forms.Label levelWidthLabel;
        private System.Windows.Forms.Label numEnemiesLabel;
        private System.Windows.Forms.TextBox levelWidth;
        private System.Windows.Forms.TextBox numEnemies;
        private System.Windows.Forms.Button createLevel;
        private System.Windows.Forms.Label levelNameLabel;
        private System.Windows.Forms.TextBox levelName;
    }
}

