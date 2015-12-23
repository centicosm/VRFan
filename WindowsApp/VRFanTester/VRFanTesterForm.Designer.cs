namespace VRFanTester
{
    partial class VRFanTesterForm
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
            this.VRFanCheckButton = new System.Windows.Forms.Button();
            this.VRFanStatusLabel = new System.Windows.Forms.Label();
            this.LEDButton = new System.Windows.Forms.Button();
            this.LedLabel = new System.Windows.Forms.Label();
            this.FanButton = new System.Windows.Forms.Button();
            this.FanLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // VRFanCheckButton
            // 
            this.VRFanCheckButton.Location = new System.Drawing.Point(67, 70);
            this.VRFanCheckButton.Name = "VRFanCheckButton";
            this.VRFanCheckButton.Size = new System.Drawing.Size(94, 37);
            this.VRFanCheckButton.TabIndex = 0;
            this.VRFanCheckButton.Text = "Check For VRFan";
            this.VRFanCheckButton.UseVisualStyleBackColor = true;
            this.VRFanCheckButton.Click += new System.EventHandler(this.VRFanCheckButton_Click);
            // 
            // VRFanStatusLabel
            // 
            this.VRFanStatusLabel.AutoSize = true;
            this.VRFanStatusLabel.Font = new System.Drawing.Font("Bookman Old Style", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VRFanStatusLabel.Location = new System.Drawing.Point(195, 79);
            this.VRFanStatusLabel.Name = "VRFanStatusLabel";
            this.VRFanStatusLabel.Size = new System.Drawing.Size(233, 28);
            this.VRFanStatusLabel.TabIndex = 1;
            this.VRFanStatusLabel.Text = "VRFan Not Found";
            // 
            // LEDButton
            // 
            this.LEDButton.Location = new System.Drawing.Point(67, 146);
            this.LEDButton.Name = "LEDButton";
            this.LEDButton.Size = new System.Drawing.Size(94, 37);
            this.LEDButton.TabIndex = 2;
            this.LEDButton.Text = "LED Toggle";
            this.LEDButton.UseVisualStyleBackColor = true;
            this.LEDButton.Click += new System.EventHandler(this.LEDButton_Click);
            // 
            // LedLabel
            // 
            this.LedLabel.AutoSize = true;
            this.LedLabel.Font = new System.Drawing.Font("Bookman Old Style", 18F, System.Drawing.FontStyle.Bold);
            this.LedLabel.Location = new System.Drawing.Point(195, 155);
            this.LedLabel.Name = "LedLabel";
            this.LedLabel.Size = new System.Drawing.Size(49, 28);
            this.LedLabel.TabIndex = 3;
            this.LedLabel.Text = "Off";
            // 
            // FanButton
            // 
            this.FanButton.Location = new System.Drawing.Point(67, 227);
            this.FanButton.Name = "FanButton";
            this.FanButton.Size = new System.Drawing.Size(94, 34);
            this.FanButton.TabIndex = 4;
            this.FanButton.Text = "Fan Toggle";
            this.FanButton.UseVisualStyleBackColor = true;
            this.FanButton.Click += new System.EventHandler(this.FanButton_Click);
            // 
            // FanLabel
            // 
            this.FanLabel.AutoSize = true;
            this.FanLabel.Font = new System.Drawing.Font("Bookman Old Style", 18F, System.Drawing.FontStyle.Bold);
            this.FanLabel.Location = new System.Drawing.Point(195, 233);
            this.FanLabel.Name = "FanLabel";
            this.FanLabel.Size = new System.Drawing.Size(49, 28);
            this.FanLabel.TabIndex = 5;
            this.FanLabel.Text = "Off";
            // 
            // VRFanTesterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 314);
            this.Controls.Add(this.FanLabel);
            this.Controls.Add(this.FanButton);
            this.Controls.Add(this.LedLabel);
            this.Controls.Add(this.LEDButton);
            this.Controls.Add(this.VRFanStatusLabel);
            this.Controls.Add(this.VRFanCheckButton);
            this.Name = "VRFanTesterForm";
            this.Text = "VRFanTester";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button VRFanCheckButton;
        private System.Windows.Forms.Label VRFanStatusLabel;
        private System.Windows.Forms.Button LEDButton;
        private System.Windows.Forms.Label LedLabel;
        private System.Windows.Forms.Button FanButton;
        private System.Windows.Forms.Label FanLabel;
    }
}

