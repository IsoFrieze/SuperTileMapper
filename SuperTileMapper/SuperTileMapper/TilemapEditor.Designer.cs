namespace SuperTileMapper
{
    partial class TilemapEditor
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.layerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bG1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bG2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bG3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bG4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(512, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layerToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bG1ToolStripMenuItem,
            this.bG2ToolStripMenuItem,
            this.bG3ToolStripMenuItem,
            this.bG4ToolStripMenuItem});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // bG1ToolStripMenuItem
            // 
            this.bG1ToolStripMenuItem.Name = "bG1ToolStripMenuItem";
            this.bG1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bG1ToolStripMenuItem.Text = "BG1";
            // 
            // bG2ToolStripMenuItem
            // 
            this.bG2ToolStripMenuItem.Name = "bG2ToolStripMenuItem";
            this.bG2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bG2ToolStripMenuItem.Text = "BG2";
            // 
            // bG3ToolStripMenuItem
            // 
            this.bG3ToolStripMenuItem.Name = "bG3ToolStripMenuItem";
            this.bG3ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bG3ToolStripMenuItem.Text = "BG3";
            // 
            // bG4ToolStripMenuItem
            // 
            this.bG4ToolStripMenuItem.Name = "bG4ToolStripMenuItem";
            this.bG4ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bG4ToolStripMenuItem.Text = "BG4";
            // 
            // TilemapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 536);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(528, 575);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(528, 575);
            this.Name = "TilemapEditor";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Tilemap Editor";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bG1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bG2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bG3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bG4ToolStripMenuItem;
    }
}