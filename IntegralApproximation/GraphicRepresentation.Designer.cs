namespace IntegralApproximation
{
    partial class GraphicRepresentation
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
            this.pictureBoxToDraw = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxToDraw)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBoxToDraw.BackColor = System.Drawing.Color.White;
            this.pictureBoxToDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxToDraw.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxToDraw.Name = "pictureBox1";
            this.pictureBoxToDraw.Size = new System.Drawing.Size(484, 461);
            this.pictureBoxToDraw.TabIndex = 0;
            this.pictureBoxToDraw.TabStop = false;
            // 
            // GraphicRepresentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.pictureBoxToDraw);
            this.Name = "GraphicRepresentation";
            this.Text = "GraphicRepresentation";
            this.Load += new System.EventHandler(this.GraphicRepresentation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxToDraw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox pictureBoxToDraw;
    }
}