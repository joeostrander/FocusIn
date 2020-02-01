using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace FocusIn
{
    public partial class FormFocus : Form
    {
        public FormFocus()
        {
            InitializeComponent();
        }





        #region Constant, Structure and Delegate Definitions
        /// <summary>
        /// defines the callback type for the hook
        /// </summary>
        public delegate int mouseHookProc(int code, int wParam, ref Msllhookstruct lParam);


        public struct Msllhookstruct
        {
            public Point Location;
            public int MouseData;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        const int WH_MOUSE_LL = 14;

        const int WM_MOUSEMOVE = 0x200;
        const int WM_MOUSEWHEEL = 0x20a;
        const int WM_LBUTTONDOWN = 0x201;
        const int WM_LBUTTONUP = 0x202;
        const int WM_RBUTTONDOWN = 0x204;
        const int WM_RBUTTONUP = 0x205;
        const int WM_MBUTTONDOWN = 0x207;
        const int WM_MBUTTONUP = 0x208;

        #endregion

        #region DLL imports

        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, mouseHookProc callback, IntPtr hInstance, uint threadId);

        /// <summary>
        /// Unhooks the windows hook.
        /// </summary>
        /// <param name="hInstance">The hook handle that was returned from SetWindowsHookEx</param>
        /// <returns>True if successful, false otherwise</returns>
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);

        /// <summary>
        /// Calls the next hook.
        /// </summary>
        /// <param name="idHook">The hook id</param>
        /// <param name="nCode">The hook code</param>
        /// <param name="wParam">The wparam.</param>
        /// <param name="lParam">The lparam.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr idHook, int nCode, int wParam, ref Msllhookstruct lParam);

        /// <summary>
        /// Loads the library.
        /// </summary>
        /// <param name="lpFileName">Name of the library</param>
        /// <returns>A handle to the library</returns>
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);
        #endregion

        IntPtr hhook = IntPtr.Zero;
        private mouseHookProc hookProcDelegate;


        Rectangle rcScreen;

                

        public string strMode { get; set; }


        private void drawSpotlight(Point startPoint)
        {

            this.BackColor = SystemColors.ControlDarkDark;
            this.Opacity = 0.8;


            Rectangle rect = new Rectangle();
            int width = 150;
            rect.X = startPoint.X - width / 2;
            rect.Y = startPoint.Y - width / 2;
            rect.Width = width;
            rect.Height = width;

            System.Drawing.Drawing2D.GraphicsPath testPath = new System.Drawing.Drawing2D.GraphicsPath();
            testPath.AddEllipse(rect);

            Region region = new Region(rcScreen);
            region.Exclude(testPath);

            this.Region = region;


        }


        private void startDrawing(Point startPoint)
        {
            switch (strMode)
            {
                case "arrow":
                    drawArrow(startPoint);
                    break;
                case "circle":
                    drawCircle(startPoint);
                    break;
                default:
                    drawSpotlight(startPoint);
                    break;
            }


        }

        private void drawCircle(Point startPoint)
        {


            Rectangle rect = new Rectangle();
            int width = 100;
            rect.X = startPoint.X - width / 2;
            rect.Y = startPoint.Y - width / 2;
            rect.Width = width;
            rect.Height = width;

            this.BackColor = Color.Yellow;
            this.Opacity = 1;

            //Graphics g = CreateGraphics();
            //g.DrawRectangle(Pens.Yellow, rcScreen);
            //g.FillRectangle(Brushes.Yellow, rcScreen);
            //Pen p = new Pen(Color.Yellow, 10);
            //g.DrawEllipse(p, rect);

            System.Drawing.Drawing2D.GraphicsPath testPath = new System.Drawing.Drawing2D.GraphicsPath();
            testPath.AddEllipse(rect);

            System.Drawing.Drawing2D.GraphicsPath testPath2 = new System.Drawing.Drawing2D.GraphicsPath();
            testPath2.AddEllipse(rect.X + 10, rect.Y + 10, rect.Width - 20, rect.Height - 20);


            Region region = new Region(rcScreen);
            region.Intersect(testPath);
            region.Exclude(testPath2);

            this.Region = region;

        }

        private void drawArrow(Point startPoint)
        {


            Rectangle rect = new Rectangle();
            int width = 60;
            rect.X = startPoint.X - width / 2;
            rect.Y = startPoint.Y - width / 2;
            rect.Width = width;
            rect.Height = width;
            Console.WriteLine("{0} [{1},{2}]",startPoint.ToString(), rect.X, rect.Y);
            this.BackColor = Color.Red;
            this.Opacity = 1;


            AdjustableArrowCap triangleCap = new AdjustableArrowCap(3, 2, true);
            Pen myPen = new Pen(Color.Red, 10);
            myPen.CustomEndCap = triangleCap;
            myPen.StartCap = LineCap.NoAnchor;


            System.Drawing.Drawing2D.GraphicsPath testPath = new System.Drawing.Drawing2D.GraphicsPath();
            testPath.AddLine(new Point(startPoint.X + rect.Width, startPoint.Y + rect.Height), startPoint);
            testPath.Widen(myPen);


            Region region = new Region(rcScreen);
            region.Intersect(testPath);

            this.Region = region;

        }


        private void hook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hhook = SetWindowsHookEx(WH_MOUSE_LL, hookProcDelegate, hInstance, 0);
        }


        private int hookProc(int nCode, int wParam, ref Msllhookstruct lParam)
        {
            Boolean boolNext = true;
            try
            {
                if (wParam != WM_MOUSEMOVE)
                {
                    Console.WriteLine(wParam.ToString("X"));
                    String msg = "";
                    if (wParam == WM_MOUSEWHEEL)
                    {
                        short Delta = (short)(lParam.MouseData >> 16);

                        if (Delta > 0)
                        {
                            msg = "Scroll up";
                        }
                        else if (Delta < 0)
                        {
                            msg = "Scroll down";
                        }
                    }
                    else if (wParam == WM_LBUTTONUP)
                    {
                        msg = "Left button up";
                    }
                    else if (wParam == WM_RBUTTONUP)
                    {
                        msg = "Right button up";
                    }
                    else if (wParam == WM_MBUTTONUP)
                    {
                        msg = "Middle button up";
                    }
                    else if (wParam == WM_LBUTTONDOWN)
                    {
                        msg = "Left button down";
                    }
                    else if (wParam == WM_RBUTTONDOWN)
                    {
                        msg = "Right button down";
                        //test...
                        //Cursor.Position = new Point(1700, 700);
                    }
                    else if (wParam == WM_MBUTTONDOWN)
                    {
                        msg = "Middle button down";
                    }
                    //Label1.Text = msg;
                    //lastMessage = DateTime.Now;
                    Console.WriteLine(msg);
                }
                else
                {
                    //Label2.Text = lParam.Location.ToString();
                    Console.WriteLine("Mouse Move: {0}", lParam.Location.ToString());
                    startDrawing(lParam.Location);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (boolNext)
            {
                return CallNextHookEx(hhook, nCode, wParam, ref lParam);
            }
            else
            {
                return 0;
            }


        }

        private void FormFocus_Load(object sender, EventArgs e)
        {
            hookProcDelegate = hookProc;
            hook();
        }

        private void FormFocus_Shown(object sender, EventArgs e)
        {
            
            rcScreen = new Rectangle();
            foreach (Screen scrn in Screen.AllScreens)
            {
                rcScreen = Rectangle.Union(rcScreen, scrn.Bounds);
                //TODO:  there is an issue here with DPI... it may report incorrectly
                Console.WriteLine("scrn:  {0}x{1}", scrn.Bounds.Width, scrn.Bounds.Height);
            }

            this.Height = rcScreen.Height;
            this.Width = rcScreen.Width;
            this.Location = new Point(0, 0);
            
                this.Opacity = 0;
        }

        private void FormFocus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                this.Close();
            }
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    this.TopMost = true;

        //}
    }
}
