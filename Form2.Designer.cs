
namespace Snappin
{
    partial class Form2
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
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.btnSaveJPEG = new System.Windows.Forms.ToolStripMenuItem();
        this.btnSavePNG = new System.Windows.Forms.ToolStripMenuItem();
        this.btnClose = new System.Windows.Forms.ToolStripMenuItem();
        this.pictureBox1 = new System.Windows.Forms.PictureBox();
        this.contextMenuStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        this.SuspendLayout();
        // 
        // contextMenuStrip1
        // 
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSaveJPEG,
            this.btnSavePNG,
            this.btnClose});
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(172, 70);
        // 
        // btnSaveJPEG
        // 
        this.btnSaveJPEG.Name = "btnSaveJPEG";
        this.btnSaveJPEG.Size = new System.Drawing.Size(171, 22);
        this.btnSaveJPEG.Text = "Save Snap As JPEG";
        this.btnSaveJPEG.Click += new System.EventHandler(this.btnSaveJPEG_Click);
        // 
        // btnSavePNG
        // 
        this.btnSavePNG.Name = "btnSavePNG";
        this.btnSavePNG.Size = new System.Drawing.Size(171, 22);
        this.btnSavePNG.Text = "Save Snap As PNG";
        this.btnSavePNG.Click += new System.EventHandler(this.btnSavePNG_Click);
        // 
        // btnClose
        // 
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new System.Drawing.Size(171, 22);
        this.btnClose.Text = "Close Snap";
        this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        // 
        // pictureBox1
        // 
        this.pictureBox1.Cursor = System.Windows.Forms.Cursors.SizeAll;
        this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
        this.pictureBox1.Location = new System.Drawing.Point(0, 0);
        this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
        this.pictureBox1.Name = "pictureBox1";
        this.pictureBox1.Size = new System.Drawing.Size(800, 450);
        this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
        this.pictureBox1.TabIndex = 0;
        this.pictureBox1.TabStop = false;
        this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseDown);
        this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseMove);
        this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form2_MouseUp);
        // 
        // Form2
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.BackColor = System.Drawing.SystemColors.GrayText;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.ContextMenuStrip = this.contextMenuStrip1;
        this.Controls.Add(this.pictureBox1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        this.Name = "Form2";
        this.Text = "Form2";
        this.TopMost = true;
        this.SizeChanged += new System.EventHandler(this.Form2_SizeChanged);
        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form2_KeyDown);
        this.contextMenuStrip1.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        this.ResumeLayout(false);

    }

    #endregion

    public System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem btnClose;
    private System.Windows.Forms.ToolStripMenuItem btnSaveJPEG;
    private System.Windows.Forms.ToolStripMenuItem btnSavePNG;
}
}