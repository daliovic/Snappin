using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Snappin
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.ControlBox = false;
            this.Text = "";

        }


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x20000; // <--- use 0x20000
                return cp;
            }
        }





        private void Form2_Load(object sender, EventArgs e)
        {

        }
        public float ratio;
        private void Form2_SizeChanged(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.Width = (int)Math.Round(control.Height * ratio);
        }


        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }


        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public int getLastFile(String ext)
        {
            try
            {

                string[] files = Directory.GetFiles(Form1.defPath, "Snapshot*" + ext);


                int periodIndex, fileIndex;

                for (int i = 0; i < files.Length; i++)
                {
                    periodIndex = files[i].IndexOf(".");
                    fileIndex = files[i].IndexOf("Snapshot") + 8;
                    files[i] = files[i].Substring(fileIndex, periodIndex - fileIndex);
                }
                List<int> filesIndexes = files.Select(s => Int32.TryParse(s, out int n) ? n : (int?)null)
                                .Where(n => n.HasValue)
                                .Select(n => n.Value)
                                .ToList();
                filesIndexes.Sort();
                foreach (int n in filesIndexes)
                {
                    Console.WriteLine(n);
                }
                if (filesIndexes.Count > 0)
                    return filesIndexes[filesIndexes.Count - 1];
                else
                    return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return 0;
            }
        }

        private void btnSavePNG_Click(object sender, EventArgs e)
        {
            SavePNG();
        }

        private void btnSaveJPEG_Click(object sender, EventArgs e)
        {
            SaveJPEG();
        }


        private void SaveJPEG()
        {
            System.IO.Directory.CreateDirectory(Form1.defPath);
            String documentsFolder = Form1.defPath + "\\Snapshot" + (getLastFile("jpeg") + 1) + ".jpeg";
            pictureBox1.Image.Save(documentsFolder, ImageFormat.Jpeg);
            Process.Start(Form1.defPath);
        }
        private void SavePNG()
        {
            System.IO.Directory.CreateDirectory(Form1.defPath);
            String documentsFolder = Form1.defPath + "\\Snapshot" + (getLastFile("png") + 1) + ".png";
            pictureBox1.Image.Save(documentsFolder, ImageFormat.Png);
            Process.Start(Form1.defPath);
        }
        private void saveSnap()
        {
            if (Form1.type == "PNG") SavePNG(); else SaveJPEG();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)
            {
                saveSnap();
            }
        }
    }
}
