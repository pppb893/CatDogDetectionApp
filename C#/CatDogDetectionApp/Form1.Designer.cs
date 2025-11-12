namespace CatDogDetectionApp
{
    partial class Form1
    {
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCat;
        private System.Windows.Forms.Button btnDog;
        private System.Windows.Forms.Button btnBoth;

        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCat = new System.Windows.Forms.Button();
            this.btnDog = new System.Windows.Forms.Button();
            this.btnBoth = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // pictureBox1
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(760, 480);
            this.pictureBox1.TabStop = false;

            // btnCat
            this.btnCat.Location = new System.Drawing.Point(12, 510);
            this.btnCat.Size = new System.Drawing.Size(120, 35);
            this.btnCat.Text = "Detect Cat";
            this.btnCat.Click += new System.EventHandler(this.btnCat_Click);

            // btnDog
            this.btnDog.Location = new System.Drawing.Point(140, 510);
            this.btnDog.Size = new System.Drawing.Size(120, 35);
            this.btnDog.Text = "Detect Dog";
            this.btnDog.Click += new System.EventHandler(this.btnDog_Click);

            // btnBoth
            this.btnBoth.Location = new System.Drawing.Point(268, 510);
            this.btnBoth.Size = new System.Drawing.Size(120, 35);
            this.btnBoth.Text = "Detect Both";
            this.btnBoth.Click += new System.EventHandler(this.btnBoth_Click);

            // Form1
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnBoth);
            this.Controls.Add(this.btnDog);
            this.Controls.Add(this.btnCat);
            this.Controls.Add(this.pictureBox1);
            this.Text = "Cat & Dog Detector";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

    }
}
