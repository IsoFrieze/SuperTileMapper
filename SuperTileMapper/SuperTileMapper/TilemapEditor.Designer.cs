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
            this.layerBG1 = new System.Windows.Forms.ToolStripMenuItem();
            this.layerBG2 = new System.Windows.Forms.ToolStripMenuItem();
            this.layerBG3 = new System.Windows.Forms.ToolStripMenuItem();
            this.layerBG4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tilemapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tilemapZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.tilemapZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.tilemapZoom300 = new System.Windows.Forms.ToolStripMenuItem();
            this.tilemapZoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.transparencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transLocal0 = new System.Windows.Forms.ToolStripMenuItem();
            this.transColor00 = new System.Windows.Forms.ToolStripMenuItem();
            this.transBlack = new System.Windows.Forms.ToolStripMenuItem();
            this.transWhite = new System.Windows.Forms.ToolStripMenuItem();
            this.priorityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prioAll = new System.Windows.Forms.ToolStripMenuItem();
            this.prioFocus = new System.Windows.Forms.ToolStripMenuItem();
            this.prioOnly = new System.Windows.Forms.ToolStripMenuItem();
            this.tilePickerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pickerZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.pickerZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.pickerZoom300 = new System.Windows.Forms.ToolStripMenuItem();
            this.pickerZoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.widthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pickerWidth32 = new System.Windows.Forms.ToolStripMenuItem();
            this.pickerWidth16 = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureSelTile = new System.Windows.Forms.PictureBox();
            this.labelTileNo = new System.Windows.Forms.Label();
            this.checkFlipH = new System.Windows.Forms.CheckBox();
            this.checkFlipV = new System.Windows.Forms.CheckBox();
            this.checkPriority = new System.Windows.Forms.CheckBox();
            this.textPalette = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importTilemapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTilemapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSelTile)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
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
            this.layerToolStripMenuItem,
            this.tilemapToolStripMenuItem,
            this.tilePickerToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // layerToolStripMenuItem
            // 
            this.layerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.layerBG1,
            this.layerBG2,
            this.layerBG3,
            this.layerBG4});
            this.layerToolStripMenuItem.Name = "layerToolStripMenuItem";
            this.layerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.layerToolStripMenuItem.Text = "Layer";
            // 
            // layerBG1
            // 
            this.layerBG1.Checked = true;
            this.layerBG1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.layerBG1.Name = "layerBG1";
            this.layerBG1.Size = new System.Drawing.Size(180, 22);
            this.layerBG1.Text = "BG1";
            this.layerBG1.Click += new System.EventHandler(this.layerBG1_Click);
            // 
            // layerBG2
            // 
            this.layerBG2.Name = "layerBG2";
            this.layerBG2.Size = new System.Drawing.Size(180, 22);
            this.layerBG2.Text = "BG2";
            this.layerBG2.Click += new System.EventHandler(this.layerBG2_Click);
            // 
            // layerBG3
            // 
            this.layerBG3.Name = "layerBG3";
            this.layerBG3.Size = new System.Drawing.Size(180, 22);
            this.layerBG3.Text = "BG3";
            this.layerBG3.Click += new System.EventHandler(this.layerBG3_Click);
            // 
            // layerBG4
            // 
            this.layerBG4.Name = "layerBG4";
            this.layerBG4.Size = new System.Drawing.Size(180, 22);
            this.layerBG4.Text = "BG4";
            this.layerBG4.Click += new System.EventHandler(this.layerBG4_Click);
            // 
            // tilemapToolStripMenuItem
            // 
            this.tilemapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem,
            this.transparencyToolStripMenuItem,
            this.priorityToolStripMenuItem});
            this.tilemapToolStripMenuItem.Name = "tilemapToolStripMenuItem";
            this.tilemapToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tilemapToolStripMenuItem.Text = "Tilemap";
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tilemapZoom100,
            this.tilemapZoom200,
            this.tilemapZoom300,
            this.tilemapZoom400});
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.zoomToolStripMenuItem.Text = "Zoom";
            // 
            // tilemapZoom100
            // 
            this.tilemapZoom100.Checked = true;
            this.tilemapZoom100.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tilemapZoom100.Name = "tilemapZoom100";
            this.tilemapZoom100.Size = new System.Drawing.Size(102, 22);
            this.tilemapZoom100.Text = "100%";
            this.tilemapZoom100.Click += new System.EventHandler(this.tilemapZoom100_Click);
            // 
            // tilemapZoom200
            // 
            this.tilemapZoom200.Name = "tilemapZoom200";
            this.tilemapZoom200.Size = new System.Drawing.Size(102, 22);
            this.tilemapZoom200.Text = "200%";
            this.tilemapZoom200.Click += new System.EventHandler(this.tilemapZoom200_Click);
            // 
            // tilemapZoom300
            // 
            this.tilemapZoom300.Name = "tilemapZoom300";
            this.tilemapZoom300.Size = new System.Drawing.Size(102, 22);
            this.tilemapZoom300.Text = "300%";
            this.tilemapZoom300.Click += new System.EventHandler(this.tilemapZoom300_Click);
            // 
            // tilemapZoom400
            // 
            this.tilemapZoom400.Name = "tilemapZoom400";
            this.tilemapZoom400.Size = new System.Drawing.Size(102, 22);
            this.tilemapZoom400.Text = "400%";
            this.tilemapZoom400.Click += new System.EventHandler(this.tilemapZoom400_Click);
            // 
            // transparencyToolStripMenuItem
            // 
            this.transparencyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transLocal0,
            this.transColor00,
            this.transBlack,
            this.transWhite});
            this.transparencyToolStripMenuItem.Name = "transparencyToolStripMenuItem";
            this.transparencyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.transparencyToolStripMenuItem.Text = "Transparency";
            // 
            // transLocal0
            // 
            this.transLocal0.Checked = true;
            this.transLocal0.CheckState = System.Windows.Forms.CheckState.Checked;
            this.transLocal0.Name = "transLocal0";
            this.transLocal0.Size = new System.Drawing.Size(169, 22);
            this.transLocal0.Text = "Local Color #0";
            this.transLocal0.Click += new System.EventHandler(this.transLocal0_Click);
            // 
            // transColor00
            // 
            this.transColor00.Name = "transColor00";
            this.transColor00.Size = new System.Drawing.Size(169, 22);
            this.transColor00.Text = "CGRAM Color $00";
            this.transColor00.Click += new System.EventHandler(this.transColor00_Click);
            // 
            // transBlack
            // 
            this.transBlack.Name = "transBlack";
            this.transBlack.Size = new System.Drawing.Size(169, 22);
            this.transBlack.Text = "Black";
            this.transBlack.Click += new System.EventHandler(this.transBlack_Click);
            // 
            // transWhite
            // 
            this.transWhite.Name = "transWhite";
            this.transWhite.Size = new System.Drawing.Size(169, 22);
            this.transWhite.Text = "White";
            this.transWhite.Click += new System.EventHandler(this.transWhite_Click);
            // 
            // priorityToolStripMenuItem
            // 
            this.priorityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prioAll,
            this.prioFocus,
            this.prioOnly});
            this.priorityToolStripMenuItem.Name = "priorityToolStripMenuItem";
            this.priorityToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.priorityToolStripMenuItem.Text = "Priority";
            // 
            // prioAll
            // 
            this.prioAll.Checked = true;
            this.prioAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prioAll.Name = "prioAll";
            this.prioAll.Size = new System.Drawing.Size(175, 22);
            this.prioAll.Text = "Show All";
            this.prioAll.Click += new System.EventHandler(this.prioAll_Click);
            // 
            // prioFocus
            // 
            this.prioFocus.Name = "prioFocus";
            this.prioFocus.Size = new System.Drawing.Size(175, 22);
            this.prioFocus.Text = "Focus High Priority";
            this.prioFocus.Click += new System.EventHandler(this.prioFocus_Click);
            // 
            // prioOnly
            // 
            this.prioOnly.Name = "prioOnly";
            this.prioOnly.Size = new System.Drawing.Size(175, 22);
            this.prioOnly.Text = "Only High Priority";
            this.prioOnly.Click += new System.EventHandler(this.prioOnly_Click);
            // 
            // tilePickerToolStripMenuItem
            // 
            this.tilePickerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomToolStripMenuItem1,
            this.widthToolStripMenuItem});
            this.tilePickerToolStripMenuItem.Name = "tilePickerToolStripMenuItem";
            this.tilePickerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tilePickerToolStripMenuItem.Text = "Tile Picker";
            // 
            // zoomToolStripMenuItem1
            // 
            this.zoomToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pickerZoom100,
            this.pickerZoom200,
            this.pickerZoom300,
            this.pickerZoom400});
            this.zoomToolStripMenuItem1.Name = "zoomToolStripMenuItem1";
            this.zoomToolStripMenuItem1.Size = new System.Drawing.Size(106, 22);
            this.zoomToolStripMenuItem1.Text = "Zoom";
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
            // widthToolStripMenuItem
            // 
            this.widthToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pickerWidth32,
            this.pickerWidth16});
            this.widthToolStripMenuItem.Name = "widthToolStripMenuItem";
            this.widthToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.widthToolStripMenuItem.Text = "Width";
            // 
            // pickerWidth32
            // 
            this.pickerWidth32.Checked = true;
            this.pickerWidth32.CheckState = System.Windows.Forms.CheckState.Checked;
            this.pickerWidth32.Name = "pickerWidth32";
            this.pickerWidth32.Size = new System.Drawing.Size(142, 22);
            this.pickerWidth32.Text = "32 ($20) Tiles";
            this.pickerWidth32.Click += new System.EventHandler(this.pickerWidth32_Click);
            // 
            // pickerWidth16
            // 
            this.pickerWidth16.Name = "pickerWidth16";
            this.pickerWidth16.Size = new System.Drawing.Size(142, 22);
            this.pickerWidth16.Text = "16 ($10) Tiles";
            this.pickerWidth16.Click += new System.EventHandler(this.pickerWidth16_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(256, 256);
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseDown);
            // 
            // pictureSelTile
            // 
            this.pictureSelTile.Location = new System.Drawing.Point(18, 42);
            this.pictureSelTile.Margin = new System.Windows.Forms.Padding(0);
            this.pictureSelTile.Name = "pictureSelTile";
            this.pictureSelTile.Size = new System.Drawing.Size(64, 64);
            this.pictureSelTile.TabIndex = 3;
            this.pictureSelTile.TabStop = false;
            // 
            // labelTileNo
            // 
            this.labelTileNo.AutoSize = true;
            this.labelTileNo.Location = new System.Drawing.Point(24, 24);
            this.labelTileNo.Name = "labelTileNo";
            this.labelTileNo.Size = new System.Drawing.Size(51, 13);
            this.labelTileNo.TabIndex = 4;
            this.labelTileNo.Text = "Tile $000";
            // 
            // checkFlipH
            // 
            this.checkFlipH.AutoSize = true;
            this.checkFlipH.Location = new System.Drawing.Point(128, 15);
            this.checkFlipH.Name = "checkFlipH";
            this.checkFlipH.Size = new System.Drawing.Size(99, 17);
            this.checkFlipH.TabIndex = 5;
            this.checkFlipH.Text = "Flip Horizontally";
            this.checkFlipH.UseVisualStyleBackColor = true;
            this.checkFlipH.CheckedChanged += new System.EventHandler(this.checkFlipH_CheckedChanged);
            // 
            // checkFlipV
            // 
            this.checkFlipV.AutoSize = true;
            this.checkFlipV.Location = new System.Drawing.Point(128, 41);
            this.checkFlipV.Name = "checkFlipV";
            this.checkFlipV.Size = new System.Drawing.Size(87, 17);
            this.checkFlipV.TabIndex = 6;
            this.checkFlipV.Text = "Flip Vertically";
            this.checkFlipV.UseVisualStyleBackColor = true;
            this.checkFlipV.CheckedChanged += new System.EventHandler(this.checkFlipV_CheckedChanged);
            // 
            // checkPriority
            // 
            this.checkPriority.AutoSize = true;
            this.checkPriority.Location = new System.Drawing.Point(128, 67);
            this.checkPriority.Name = "checkPriority";
            this.checkPriority.Size = new System.Drawing.Size(82, 17);
            this.checkPriority.TabIndex = 7;
            this.checkPriority.Text = "High Priority";
            this.checkPriority.UseVisualStyleBackColor = true;
            this.checkPriority.CheckedChanged += new System.EventHandler(this.checkPriority_CheckedChanged);
            // 
            // textPalette
            // 
            this.textPalette.Location = new System.Drawing.Point(115, 94);
            this.textPalette.Name = "textPalette";
            this.textPalette.Size = new System.Drawing.Size(26, 20);
            this.textPalette.TabIndex = 8;
            this.textPalette.TextChanged += new System.EventHandler(this.textPalette_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Palette";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureSelTile);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.checkPriority);
            this.groupBox1.Controls.Add(this.textPalette);
            this.groupBox1.Controls.Add(this.labelTileNo);
            this.groupBox1.Controls.Add(this.checkFlipH);
            this.groupBox1.Controls.Add(this.checkFlipV);
            this.groupBox1.Location = new System.Drawing.Point(537, 305);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 124);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Tile";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(529, 529);
            this.panel1.TabIndex = 11;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Location = new System.Drawing.Point(529, 24);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(273, 273);
            this.panel2.TabIndex = 12;
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importTilemapToolStripMenuItem,
            this.exportTilemapToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // importTilemapToolStripMenuItem
            // 
            this.importTilemapToolStripMenuItem.Name = "importTilemapToolStripMenuItem";
            this.importTilemapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importTilemapToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.importTilemapToolStripMenuItem.Text = "Import Tilemap...";
            this.importTilemapToolStripMenuItem.Click += new System.EventHandler(this.importTilemapToolStripMenuItem_Click);
            // 
            // exportTilemapToolStripMenuItem
            // 
            this.exportTilemapToolStripMenuItem.Name = "exportTilemapToolStripMenuItem";
            this.exportTilemapToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exportTilemapToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.exportTilemapToolStripMenuItem.Text = "Export Tilemap...";
            this.exportTilemapToolStripMenuItem.Click += new System.EventHandler(this.exportTilemapToolStripMenuItem_Click);
            // 
            // TilemapEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 553);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(818, 592);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(818, 592);
            this.Name = "TilemapEditor";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Tilemap Editor";
            this.Load += new System.EventHandler(this.TilemapEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSelTile)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem layerBG1;
        private System.Windows.Forms.ToolStripMenuItem layerBG2;
        private System.Windows.Forms.ToolStripMenuItem layerBG3;
        private System.Windows.Forms.ToolStripMenuItem layerBG4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureSelTile;
        private System.Windows.Forms.Label labelTileNo;
        private System.Windows.Forms.CheckBox checkFlipH;
        private System.Windows.Forms.CheckBox checkFlipV;
        private System.Windows.Forms.CheckBox checkPriority;
        private System.Windows.Forms.TextBox textPalette;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem tilemapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tilemapZoom100;
        private System.Windows.Forms.ToolStripMenuItem tilemapZoom200;
        private System.Windows.Forms.ToolStripMenuItem tilemapZoom300;
        private System.Windows.Forms.ToolStripMenuItem tilemapZoom400;
        private System.Windows.Forms.ToolStripMenuItem tilePickerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem pickerZoom100;
        private System.Windows.Forms.ToolStripMenuItem pickerZoom200;
        private System.Windows.Forms.ToolStripMenuItem pickerZoom300;
        private System.Windows.Forms.ToolStripMenuItem pickerZoom400;
        private System.Windows.Forms.ToolStripMenuItem widthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pickerWidth32;
        private System.Windows.Forms.ToolStripMenuItem pickerWidth16;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem transparencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transLocal0;
        private System.Windows.Forms.ToolStripMenuItem transColor00;
        private System.Windows.Forms.ToolStripMenuItem transBlack;
        private System.Windows.Forms.ToolStripMenuItem transWhite;
        private System.Windows.Forms.ToolStripMenuItem priorityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prioAll;
        private System.Windows.Forms.ToolStripMenuItem prioFocus;
        private System.Windows.Forms.ToolStripMenuItem prioOnly;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importTilemapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTilemapToolStripMenuItem;
    }
}