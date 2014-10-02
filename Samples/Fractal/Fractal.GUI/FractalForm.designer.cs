namespace Fractal.GUI
{
    partial class FractalForm
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
            this.pcbFractal = new System.Windows.Forms.PictureBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnChangeColors = new System.Windows.Forms.Button();
            this.btnResetColors = new System.Windows.Forms.Button();
            this.btnResetPosition = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFractal)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbFractal
            // 
            this.pcbFractal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pcbFractal.Location = new System.Drawing.Point(12, 46);
            this.pcbFractal.Name = "pcbFractal";
            this.pcbFractal.Size = new System.Drawing.Size(506, 430);
            this.pcbFractal.TabIndex = 0;
            this.pcbFractal.TabStop = false;
            this.pcbFractal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pcbFractal_MouseMove);
            this.pcbFractal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pcbFractal_MouseDown);
            this.pcbFractal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pcbFractal_MouseUp);
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(12, 12);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 1;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Location = new System.Drawing.Point(93, 12);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(75, 23);
            this.btnZoomIn.TabIndex = 2;
            this.btnZoomIn.Text = "Zoom In";
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Location = new System.Drawing.Point(174, 12);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(75, 23);
            this.btnZoomOut.TabIndex = 3;
            this.btnZoomOut.Text = "Zoom Out";
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnChangeColors
            // 
            this.btnChangeColors.Location = new System.Drawing.Point(255, 12);
            this.btnChangeColors.Name = "btnChangeColors";
            this.btnChangeColors.Size = new System.Drawing.Size(75, 23);
            this.btnChangeColors.TabIndex = 4;
            this.btnChangeColors.Text = "New Colors";
            this.btnChangeColors.UseVisualStyleBackColor = true;
            this.btnChangeColors.Click += new System.EventHandler(this.btnChangeColors_Click);
            // 
            // btnResetColors
            // 
            this.btnResetColors.Location = new System.Drawing.Point(336, 12);
            this.btnResetColors.Name = "btnResetColors";
            this.btnResetColors.Size = new System.Drawing.Size(75, 23);
            this.btnResetColors.TabIndex = 5;
            this.btnResetColors.Text = "Reset Colors";
            this.btnResetColors.UseVisualStyleBackColor = true;
            this.btnResetColors.Click += new System.EventHandler(this.btnResetColors_Click);
            // 
            // btnResetPosition
            // 
            this.btnResetPosition.Location = new System.Drawing.Point(417, 12);
            this.btnResetPosition.Name = "btnResetPosition";
            this.btnResetPosition.Size = new System.Drawing.Size(75, 23);
            this.btnResetPosition.TabIndex = 6;
            this.btnResetPosition.Text = "Reset";
            this.btnResetPosition.UseVisualStyleBackColor = true;
            this.btnResetPosition.Click += new System.EventHandler(this.btnResetPosition_Click);
            // 
            // FractalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 488);
            this.Controls.Add(this.btnResetPosition);
            this.Controls.Add(this.btnResetColors);
            this.Controls.Add(this.btnChangeColors);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.pcbFractal);
            this.Name = "FractalForm";
            this.Text = "Fractal";
            ((System.ComponentModel.ISupportInitialize)(this.pcbFractal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbFractal;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnChangeColors;
        private System.Windows.Forms.Button btnResetColors;
        private System.Windows.Forms.Button btnResetPosition;
    }
}