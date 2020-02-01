using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FocusIn
{
    public partial class Form1 : Form
    {

        private enum ProcessDPIAwareness
        {
            ProcessDPIUnaware = 0,
            ProcessSystemDPIAware = 1,
            ProcessPerMonitorDPIAware = 2
        }

        [DllImport("shcore.dll")]
        private static extern int SetProcessDpiAwareness(ProcessDPIAwareness value);

        FormFocus focus;



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetDpiAwareness();
        }



        private static void SetDpiAwareness()
        {
            try
            {
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    SetProcessDpiAwareness(ProcessDPIAwareness.ProcessPerMonitorDPIAware);
                }
            }
            catch (EntryPointNotFoundException)//this exception occures if OS does not implement this API, just ignore it.
            {
            }
        }

        private void buttonArrow_Click(object sender, EventArgs e)
        {
            EnableButtons();
            ((Button)sender).Enabled = false;
            if (focus != null)
            {
                focus.strMode = "arrow";
            }
        }

        private void EnableButtons()
        {
            buttonArrow.Enabled = true;
            buttonCircle.Enabled = true;
            buttonSpotlight.Enabled = true;
        }

        private void buttonCircle_Click(object sender, EventArgs e)
        {
            EnableButtons();
            ((Button)sender).Enabled = false;
            if (focus != null)
            {
                focus.strMode = "circle";
            }
        }

        private void buttonSpotlight_Click(object sender, EventArgs e)
        {
            EnableButtons();
            ((Button)sender).Enabled = false;
            if (focus != null)
            {
                focus.strMode = "spotlight";
            }
        }

        private void buttonOnOff_Click(object sender, EventArgs e)
        {
            Button btn = ((Button)sender);
            if (btn.Text=="&ON")
            {
                //Turn it on
                String mode = "spotlight";
                if (buttonArrow.Enabled==false)
                {
                    mode = "arrow";
                }
                else if (buttonCircle.Enabled == false)
                {
                    mode = "circle";
                }

                btn.Text = "&OFF";
                focus = new FormFocus();
                focus.strMode = mode;

                focus.Show();
            }
            else
            {
                btn.Text = "&ON";
                if (focus != null)
                {
                    focus.Close();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (focus == null || !focus.Visible)
            {
                buttonOnOff.Text = "&ON";
            }
        }
    }
}
