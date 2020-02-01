namespace FocusIn
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
            this.buttonArrow = new System.Windows.Forms.Button();
            this.buttonCircle = new System.Windows.Forms.Button();
            this.buttonSpotlight = new System.Windows.Forms.Button();
            this.buttonOnOff = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // buttonArrow
            // 
            this.buttonArrow.Location = new System.Drawing.Point(12, 12);
            this.buttonArrow.Name = "buttonArrow";
            this.buttonArrow.Size = new System.Drawing.Size(75, 23);
            this.buttonArrow.TabIndex = 0;
            this.buttonArrow.Text = "&Arrow";
            this.buttonArrow.UseVisualStyleBackColor = true;
            this.buttonArrow.Click += new System.EventHandler(this.buttonArrow_Click);
            // 
            // buttonCircle
            // 
            this.buttonCircle.Location = new System.Drawing.Point(12, 41);
            this.buttonCircle.Name = "buttonCircle";
            this.buttonCircle.Size = new System.Drawing.Size(75, 23);
            this.buttonCircle.TabIndex = 1;
            this.buttonCircle.Text = "&Circle";
            this.buttonCircle.UseVisualStyleBackColor = true;
            this.buttonCircle.Click += new System.EventHandler(this.buttonCircle_Click);
            // 
            // buttonSpotlight
            // 
            this.buttonSpotlight.Enabled = false;
            this.buttonSpotlight.Location = new System.Drawing.Point(12, 70);
            this.buttonSpotlight.Name = "buttonSpotlight";
            this.buttonSpotlight.Size = new System.Drawing.Size(75, 23);
            this.buttonSpotlight.TabIndex = 2;
            this.buttonSpotlight.Text = "&Spotlight";
            this.buttonSpotlight.UseVisualStyleBackColor = true;
            this.buttonSpotlight.Click += new System.EventHandler(this.buttonSpotlight_Click);
            // 
            // buttonOnOff
            // 
            this.buttonOnOff.Location = new System.Drawing.Point(136, 41);
            this.buttonOnOff.Name = "buttonOnOff";
            this.buttonOnOff.Size = new System.Drawing.Size(75, 23);
            this.buttonOnOff.TabIndex = 3;
            this.buttonOnOff.Text = "&ON";
            this.buttonOnOff.UseVisualStyleBackColor = true;
            this.buttonOnOff.Click += new System.EventHandler(this.buttonOnOff_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.buttonOnOff);
            this.Controls.Add(this.buttonSpotlight);
            this.Controls.Add(this.buttonCircle);
            this.Controls.Add(this.buttonArrow);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonArrow;
        private System.Windows.Forms.Button buttonCircle;
        private System.Windows.Forms.Button buttonSpotlight;
        private System.Windows.Forms.Button buttonOnOff;
        private System.Windows.Forms.Timer timer1;
    }
}

