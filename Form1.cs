using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Snappin
{
    public partial class Form1 : Form
    {

        public static Boolean b = false;
        int x = 0;
        int y = 0;
        int w = 0;
        int h = 0;
        public static String type = "PNG";
        public static String docs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static String defPath = docs + "\\SnapPin\\";
        public Graphics captureGraphics;


        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.Sizable;

            notifyIcon1.Visible = true;
            this.WindowState = FormWindowState.Minimized;

            this.ShowInTaskbar = false;
            if (this.WindowState == FormWindowState.Minimized)
            {
                try
                {
                    var kbh = new LowLevelKeyboardHook();
                    kbh.OnKeyPressed += kbh_OnKeyPressed;
                    kbh.OnKeyUnpressed += kbh_OnKeyUnPressed;
                    kbh.HookKeyboard();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    //MessageBox.Show(ex.Message);
                }
            }
            this.KeyPreview = true;
            //toggleMaximize();

        }

        bool lctrlKeyPressed;
        bool f1KeyPressed;

        void kbh_OnKeyPressed(object sender, Keys e)
        {
            if (e == Keys.LControlKey || e == Keys.RControlKey)
            {
                lctrlKeyPressed = true;
            }
            else if (e == Keys.PrintScreen)
            {
                f1KeyPressed = true;
            }
            CheckKeyCombo();
        }

        void kbh_OnKeyUnPressed(object sender, Keys e)
        {
            if (e == Keys.LControlKey || e == Keys.RControlKey)
            {
                lctrlKeyPressed = false;
            }
            else if (e == Keys.PrintScreen)
            {
                f1KeyPressed = false;
            }
        }

        void CheckKeyCombo()
        {
            if (lctrlKeyPressed && f1KeyPressed)
            {
                toggleMaximize();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        string file;
        private void captureScreen()
        {
            try
            {
                if (file == "" || file == null)
                {
                    if (rectangles.Count > 0)
                    {
                        x = rectangles[rectangles.Count - 1].X;
                        y = rectangles[rectangles.Count - 1].Y;
                        w = rectangles[rectangles.Count - 1].Width;
                        h = rectangles[rectangles.Count - 1].Height;
                        Bitmap captureBitmap = new Bitmap(w, h, PixelFormat.Format32bppPArgb);
                        captureBitmap.SetResolution(1600.0f, 1600.0f);
                        Rectangle captureRectangle = new Rectangle(x, y, w, h);

                        captureGraphics = Graphics.FromImage(captureBitmap);


                        captureGraphics.CopyFromScreen(captureRectangle.Left, captureRectangle.Top, 0, 0, captureRectangle.Size);
                        captureBitmap.Save(@"D:\Capture.Png", ImageFormat.Png);
                        var PreviewForm = new Form2();
                        PreviewForm.ratio = w / (h * 1.0f);
                        PreviewForm.Show();
                        PreviewForm.Width = w;
                        PreviewForm.Height = h;
                        PreviewForm.TopMost = true;

                        PreviewForm.pictureBox1.Image = captureBitmap;

                        //MessageBox.Show("Screen Captured");
                    }
                }
                else
                {
                    var img = Image.FromFile(file);
                    var PreviewForm = new Form2
                    {
                        ratio = img.Width / (img.Height * 1.0f)
                    };
                    PreviewForm.Show();
                    PreviewForm.Width = img.Width;
                    PreviewForm.Height = img.Height;
                    PreviewForm.TopMost = true;
                    PreviewForm.pictureBox1.Image = img;
                    file = "";


                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                //MessageBox.Show(ex.Message);
            }
        }

        public void toggleMinimize()
        {
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.WindowState = FormWindowState.Minimized;
            this.Opacity = 1;
            this.TopMost = false;
        }
        public void toggleMaximize()
        {

            this.BackColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0.3;
            this.TopMost = true;
            this.Visible = true;
            btnCapture.Visible = false;
            this.WindowState = FormWindowState.Maximized;

        }


        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                currentPos = e.Location;
                if (drawing) this.Invalidate();

            }
        }

        Point startPos;      // mouse-down position
        Point currentPos;    // current mouse position
        bool drawing;        // busy drawing
        List<Rectangle> rectangles = new List<Rectangle>();  // previous rectangles

        private Rectangle getRectangle()
        {
            return new Rectangle(
            Math.Min(startPos.X, currentPos.X),
            Math.Min(startPos.Y, currentPos.Y),
            Math.Abs(startPos.X - currentPos.X),
            Math.Abs(startPos.Y - currentPos.Y));
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            currentPos = startPos = e.Location;
            drawing = true;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /*            Graphics g = e.Graphics;
                        Pen pen = new Pen(Color.White, 2);
                        g.DrawRectangle(pen, x,y,w,h);*/

            if (rectangles.Count > 0) e.Graphics.DrawRectangles(Pens.Black, rectangles.ToArray());
            Pen pen = new Pen(Color.White, 2);
            if (drawing) e.Graphics.DrawRectangle(pen, getRectangle());

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                if (drawing)
                {
                    drawing = false;
                    var rc = getRectangle();
                    if (rc.Width > 0 && rc.Height > 0) rectangles.Add(rc);
                    this.Invalidate();
                }
                this.Visible = false;
                captureScreen();
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            toggleMaximize();
        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //Show();
            //this.WindowState = FormWindowState.Normal;
        }

        private void btnCaptureTray_Click(object sender, EventArgs e)
        {
            toggleMaximize();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                toggleMaximize();
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void o1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toggleMinimize();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                toggleMinimize();
            }
        }

        private void openImageAsSnapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.ShowDialog();
            file = openFileDialog1.FileName;
            captureScreen();


        }


        private void pNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = "PNG";
        }

        private void jPEGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = "JPEG";
        }

        /// <summary>
        /// /////////
        /// </summary>

    }




















    public class LowLevelKeyboardHook
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_KEYUP = 0x101;
        private const int WM_SYSKEYUP = 0x105;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        public event EventHandler<Keys> OnKeyPressed;
        public event EventHandler<Keys> OnKeyUnpressed;

        private LowLevelKeyboardProc _proc;
        private IntPtr _hookID = IntPtr.Zero;


        public LowLevelKeyboardHook()
        {
            _proc = HookCallback;
        }

        public void HookKeyboard()
        {
            _hookID = SetHook(_proc);
        }

        public void UnHookKeyboard()
        {
            UnhookWindowsHookEx(_hookID);
        }

        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                OnKeyPressed.Invoke(this, ((Keys)vkCode));
            }
            else if (nCode >= 0 && wParam == (IntPtr)WM_KEYUP || wParam == (IntPtr)WM_SYSKEYUP)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                OnKeyUnpressed.Invoke(this, ((Keys)vkCode));
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }
    }

}




