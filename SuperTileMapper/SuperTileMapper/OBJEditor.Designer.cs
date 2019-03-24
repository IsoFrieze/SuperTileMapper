namespace SuperTileMapper
{
    partial class OBJEditor
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oBJViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerZoom300 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerZoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.widthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerWidth32 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerWidth16 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerWidth8 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerWidth4 = new System.Windows.Forms.ToolStripMenuItem();
            this.oBJPickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pickerZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.pickerZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.pickerZoom300 = new System.Windows.Forms.ToolStripMenuItem();
            this.pickerZoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkSize = new System.Windows.Forms.CheckBox();
            this.pictureSelTile = new System.Windows.Forms.PictureBox();
            this.labelTileNo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textPalette = new System.Windows.Forms.TextBox();
            this.textPriority = new System.Windows.Forms.TextBox();
            this.checkFlipV = new System.Windows.Forms.CheckBox();
            this.checkFlipH = new System.Windows.Forms.CheckBox();
            this.transparencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerTransLocal0 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerTransColor00 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerTransBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.viewerTransWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSelTile)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(802, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oBJViewerToolStripMenuItem,
            this.oBJPickerToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // oBJViewerToolStripMenuItem
            // 
            this.oBJViewerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem1,
            this.transparencyToolStripMenuItem,
            this.widthToolStripMenuItem});
            this.oBJViewerToolStripMenuItem.Name = "oBJViewerToolStripMenuItem";
            this.oBJViewerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.oBJViewerToolStripMenuItem.Text = "OBJ Viewer";
            // 
            // zoomToolStripMenuItem1
            // 
            this.zoomToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewerZoom100,
            this.viewerZoom200,
            this.viewerZoom300,
            this.viewerZoom400});
            this.zoomToolStripMenuItem1.Name = "zoomToolStripMenuItem1";
            this.zoomToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.zoomToolStripMenuItem1.Text = "Zoom";
            // 
            // viewerZoom100
            // 
            this.viewerZoom100.Checked = true;
            this.viewerZoom100.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewerZoom100.Name = "viewerZoom100";
            this.viewerZoom100.Size = new System.Drawing.Size(102, 22);
            this.viewerZoom100.Text = "100%";
            this.viewerZoom100.Click += new System.EventHandler(this.viewerZoom100_Click);
            // 
            // viewerZoom200
            // 
            this.viewerZoom200.Name = "viewerZoom200";
            this.viewerZoom200.Size = new System.Drawing.Size(102, 22);
            this.viewerZoom200.Text = "200%";
            this.viewerZoom200.Click += new System.EventHandler(this.viewerZoom200_Click);
            // 
            // viewerZoom300
            // 
            this.viewerZoom300.Name = "viewerZoom300";
            this.viewerZoom300.Size = new System.Drawing.Size(102, 22);
            this.viewerZoom300.Text = "300%";
            this.viewerZoom300.Click += new System.EventHandler(this.viewerZoom300_Click);
            // 
            // viewerZoom400
            // 
            this.viewerZoom400.Name = "viewerZoom400";
            this.viewerZoom400.Size = new System.Drawing.Size(102, 22);
            this.viewerZoom400.Text = "400%";
            this.viewerZoom400.Click += new System.EventHandler(this.viewerZoom400_Click);
            // 
            // widthToolStripMenuItem
            // 
            this.widthToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewerWidth32,
            this.viewerWidth16,
            this.viewerWidth8,
            this.viewerWidth4});
            this.widthToolStripMenuItem.Name = "widthToolStripMenuItem";
            this.widthToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.widthToolStripMenuItem.Text = "Width";
            // 
            // viewerWidth32
            // 
            this.viewerWidth32.Checked = true;
            this.viewerWidth32.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewerWidth32.Name = "viewerWidth32";
            this.viewerWidth32.Size = new System.Drawing.Size(158, 22);
            this.viewerWidth32.Text = "32 ($20) Objects";
            this.viewerWidth32.Click += new System.EventHandler(this.viewerWidth32_Click);
            // 
            // viewerWidth16
            // 
            this.viewerWidth16.Name = "viewerWidth16";
            this.viewerWidth16.Size = new System.Drawing.Size(158, 22);
            this.viewerWidth16.Text = "16 ($10) Objects";
            this.viewerWidth16.Click += new System.EventHandler(this.viewerWidth16_Click);
            // 
            // viewerWidth8
            // 
            this.viewerWidth8.Name = "viewerWidth8";
            this.viewerWidth8.Size = new System.Drawing.Size(158, 22);
            this.viewerWidth8.Text = "8 ($08) Objects";
            this.viewerWidth8.Click += new System.EventHandler(this.viewerWidth8_Click);
            // 
            // viewerWidth4
            // 
            this.viewerWidth4.Name = "viewerWidth4";
            this.viewerWidth4.Size = new System.Drawing.Size(158, 22);
            this.viewerWidth4.Text = "4 ($04) Objects";
            this.viewerWidth4.Click += new System.EventHandler(this.viewerWidth4_Click);
            // 
            // oBJPickerToolStripMenuItem
            // 
            this.oBJPickerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem});
            this.oBJPickerToolStripMenuItem.Name = "oBJPickerToolStripMenuItem";
            this.oBJPickerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.oBJPickerToolStripMenuItem.Text = "OBJ Picker";
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pickerZoom100,
            this.pickerZoom200,
            this.pickerZoom300,
            this.pickerZoom400});
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.zoomToolStripMenuItem.Text = "Zoom";
            // 
            // pickerZoom100
            // 
            this.pickerZoom100.Checked = true;
            this.pickerZoom100.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pickerZoom100.Name = "pickerZoom100";
            this.pickerZoom100.Size = new System.Drawing.Size(102, 22);
            this.pickerZoom100.Text = "100%";
            this.pickerZoom100.Click += new System.EventHandler(this.pickerZoom100_Click);
            // 
            // pickerZoom200
            // 
            this.pickerZoom200.Name = "pickerZoom200";
            this.pickerZoom200.Size = new System.Drawing.Size(102, 22);
            this.pickerZoom200.Text = "200%";
            this.pickerZoom200.Click += new System.EventHandler(this.pickerZoom200_Click);
            // 
            // pickerZoom300
            // 
            this.pickerZoom300.Name = "pickerZoom300";
            this.pickerZoom300.Size = new System.Drawing.Size(102, 22);
            this.pickerZoom300.Text = "300%";
            this.pickerZoom300.Click += new System.EventHandler(this.pickerZoom300_Click);
            // 
            // pickerZoom400
            // 
            this.pickerZoom400.Name = "pickerZoom400";
            this.pickerZoom400.Size = new System.Drawing.Size(102, 22);
            this.pickerZoom400.Text = "400%";
            this.pickerZoom400.Click += new System.EventHandler(this.pickerZoom400_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 529);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(512, 512);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // panel2
            // 
            this.panel2.AllowDrop = true;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(529, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(273, 273);
            this.panel2.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(256, 256);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkSize);
            this.groupBox1.Controls.Add(this.pictureSelTile);
            this.groupBox1.Controls.Add(this.labelTileNo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textPalette);
            this.groupBox1.Controls.Add(this.textPriority);
            this.groupBox1.Controls.Add(this.checkFlipV);
            this.groupBox1.Controls.Add(this.checkFlipH);
            this.groupBox1.Location = new System.Drawing.Point(537, 305);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 152);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Tile";
            // 
            // checkSize
            // 
            this.checkSize.AutoSize = true;
            this.checkSize.Location = new System.Drawing.Point(128, 66);
            this.checkSize.Name = "checkSize";
            this.checkSize.Size = new System.Drawing.Size(76, 17);
            this.checkSize.TabIndex = 8;
            this.checkSize.Text = "Large Size";
            this.checkSize.UseVisualStyleBackColor = true;
            this.checkSize.CheckedChanged += new System.EventHandler(this.checkSize_CheckedChanged);
            // 
            // pictureSelTile
            // 
            this.pictureSelTile.Location = new System.Drawing.Point(18, 77);
            this.pictureSelTile.Name = "pictureSelTile";
            this.pictureSelTile.Size = new System.Drawing.Size(64, 64);
            this.pictureSelTile.TabIndex = 7;
            this.pictureSelTile.TabStop = false;
            // 
            // labelTileNo
            // 
            this.labelTileNo.AutoSize = true;
            this.labelTileNo.Location = new System.Drawing.Point(24, 59);
            this.labelTileNo.Name = "labelTileNo";
            this.labelTileNo.Size = new System.Drawing.Size(51, 13);
            this.labelTileNo.TabIndex = 6;
            this.labelTileNo.Text = "Tile $000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Palette";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Priority";
            // 
            // textPalette
            // 
            this.textPalette.Location = new System.Drawing.Point(115, 122);
            this.textPalette.Name = "textPalette";
            this.textPalette.Size = new System.Drawing.Size(26, 20);
            this.textPalette.TabIndex = 3;
            this.textPalette.TextChanged += new System.EventHandler(this.textPalette_TextChanged);
            // 
            // textPriority
            // 
            this.textPriority.Location = new System.Drawing.Point(115, 94);
            this.textPriority.Name = "textPriority";
            this.textPriority.Size = new System.Drawing.Size(26, 20);
            this.textPriority.TabIndex = 2;
            this.textPriority.TextChanged += new System.EventHandler(this.textPriority_TextChanged);
            // 
            // checkFlipV
            // 
            this.checkFlipV.AutoSize = true;
            this.checkFlipV.Location = new System.Drawing.Point(128, 41);
            this.checkFlipV.Name = "checkFlipV";
            this.checkFlipV.Size = new System.Drawing.Size(87, 17);
            this.checkFlipV.TabIndex = 1;
            this.checkFlipV.Text = "Flip Vertically";
            this.checkFlipV.UseVisualStyleBackColor = true;
            this.checkFlipV.CheckedChanged += new System.EventHandler(this.checkFlipV_CheckedChanged);
            // 
            // checkFlipH
            // 
            this.checkFlipH.AutoSize = true;
            this.checkFlipH.Location = new System.Drawing.Point(128, 15);
            this.checkFlipH.Name = "checkFlipH";
            this.checkFlipH.Size = new System.Drawing.Size(99, 17);
            this.checkFlipH.TabIndex = 0;
            this.checkFlipH.Text = "Flip Horizontally";
            this.checkFlipH.UseVisualStyleBackColor = true;
            this.checkFlipH.CheckedChanged += new System.EventHandler(this.checkFlipH_CheckedChanged);
            // 
            // transparencyToolStripMenuItem
            // 
            this.transparencyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewerTransLocal0,
            this.viewerTransColor00,
            this.viewerTransBlack,
            this.viewerTransWhite});
            this.transparencyToolStripMenuItem.Name = "transparencyToolStripMenuItem";
            this.transparencyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.transparencyToolStripMenuItem.Text = "Transparency";
            // 
            // viewerTransLocal0
            // 
            this.viewerTransLocal0.Checked = true;
            this.viewerTransLocal0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewerTransLocal0.Name = "viewerTransLocal0";
            this.viewerTransLocal0.Size = new System.Drawing.Size(180, 22);
            this.viewerTransLocal0.Text = "Local Color #0";
            this.viewerTransLocal0.Click += new System.EventHandler(this.viewerTransLocal0_Click);
            // 
            // viewerTransColor00
            // 
            this.viewerTransColor00.Name = "viewerTransColor00";
            this.viewerTransColor00.Size = new System.Drawing.Size(180, 22);
            this.viewerTransColor00.Text = "CGRAM Color $00";
            this.viewerTransColor00.Click += new System.EventHandler(this.viewerTransColor00_Click);
            // 
            // viewerTransBlack
            // 
            this.viewerTransBlack.Name = "viewerTransBlack";
            this.viewerTransBlack.Size = new System.Drawing.Size(180, 22);
            this.viewerTransBlack.Text = "Black";
            this.viewerTransBlack.Click += new System.EventHandler(this.viewerTransBlack_Click);
            // 
            // viewerTransWhite
            // 
            this.viewerTransWhite.Name = "viewerTransWhite";
            this.viewerTransWhite.Size = new System.Drawing.Size(180, 22);
            this.viewerTransWhite.Text = "White";
            this.viewerTransWhite.Click += new System.EventHandler(this.viewerTransWhite_Click);
            // 
            // OBJEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 553);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(818, 592);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(818, 592);
            this.Name = "OBJEditor";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "OBJ Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSelTile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oBJPickerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pickerZoom100;
        private System.Windows.Forms.ToolStripMenuItem pickerZoom200;
        private System.Windows.Forms.ToolStripMenuItem pickerZoom300;
        private System.Windows.Forms.ToolStripMenuItem pickerZoom400;
        private System.Windows.Forms.ToolStripMenuItem oBJViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewerZoom100;
        private System.Windows.Forms.ToolStripMenuItem viewerZoom200;
        private System.Windows.Forms.ToolStripMenuItem viewerZoom300;
        private System.Windows.Forms.ToolStripMenuItem viewerZoom400;
        private System.Windows.Forms.ToolStripMenuItem widthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewerWidth16;
        private System.Windows.Forms.ToolStripMenuItem viewerWidth8;
        private System.Windows.Forms.ToolStripMenuItem viewerWidth4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textPalette;
        private System.Windows.Forms.TextBox textPriority;
        private System.Windows.Forms.CheckBox checkFlipV;
        private System.Windows.Forms.CheckBox checkFlipH;
        private System.Windows.Forms.PictureBox pictureSelTile;
        private System.Windows.Forms.Label labelTileNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem viewerWidth32;
        private System.Windows.Forms.CheckBox checkSize;
        private System.Windows.Forms.ToolStripMenuItem transparencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewerTransLocal0;
        private System.Windows.Forms.ToolStripMenuItem viewerTransColor00;
        private System.Windows.Forms.ToolStripMenuItem viewerTransBlack;
        private System.Windows.Forms.ToolStripMenuItem viewerTransWhite;
    }
}