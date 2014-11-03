namespace ASL_Classroom_Helper
{
    partial class mainForm
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
            this.kinectPictureBox = new System.Windows.Forms.PictureBox();
            this.kinectStatusLabel = new System.Windows.Forms.Label();
            this.speechTextLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.kinectPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // kinectPictureBox
            // 
            this.kinectPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kinectPictureBox.Location = new System.Drawing.Point(12, 12);
            this.kinectPictureBox.Name = "kinectPictureBox";
            this.kinectPictureBox.Size = new System.Drawing.Size(600, 391);
            this.kinectPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.kinectPictureBox.TabIndex = 0;
            this.kinectPictureBox.TabStop = false;
            // 
            // kinectStatusLabel
            // 
            this.kinectStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.kinectStatusLabel.AutoSize = true;
            this.kinectStatusLabel.Location = new System.Drawing.Point(558, 419);
            this.kinectStatusLabel.Name = "kinectStatusLabel";
            this.kinectStatusLabel.Size = new System.Drawing.Size(54, 13);
            this.kinectStatusLabel.TabIndex = 1;
            this.kinectStatusLabel.Text = "No Kinect";
            // 
            // speechTextLabel
            // 
            this.speechTextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.speechTextLabel.AutoSize = true;
            this.speechTextLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speechTextLabel.Location = new System.Drawing.Point(7, 406);
            this.speechTextLabel.Name = "speechTextLabel";
            this.speechTextLabel.Size = new System.Drawing.Size(87, 26);
            this.speechTextLabel.TabIndex = 2;
            this.speechTextLabel.Text = "No Text";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.speechTextLabel);
            this.Controls.Add(this.kinectStatusLabel);
            this.Controls.Add(this.kinectPictureBox);
            this.Name = "mainForm";
            this.Text = "ASL Classroom Helper";
            ((System.ComponentModel.ISupportInitialize)(this.kinectPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox kinectPictureBox;
        private System.Windows.Forms.Label kinectStatusLabel;
        private System.Windows.Forms.Label speechTextLabel;
    }
}

