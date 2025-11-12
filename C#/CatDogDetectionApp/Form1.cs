using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;


namespace CatDogDetectionApp
{
    public partial class Form1 : Form
    {
        private Mat originalImg;
        private List<Detection> lastDetections = new List<Detection>();
        private string currentMode = "both";

        // 🔹 Replace this with your Colab ngrok URL
        private string apiUrl = "http://127.0.0.1:8000/detect";
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.Load += async (s, e) => await AskToUpload();
            pictureBox1.Click += async (s, e) => await AskToUpload();
        }

        private async Task AskToUpload()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select an image";
                ofd.Filter = "Image Files|*.jpg;*.png;*.jpeg";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    originalImg = CvInvoke.Imread(ofd.FileName, Emgu.CV.CvEnum.ImreadModes.Color);
                    pictureBox1.Image = originalImg.ToBitmap();

                    await DetectCatDog(ofd.FileName);
                }
            }
        }

        private async Task DetectCatDog(string filePath)
        {
            using (var client = new HttpClient())
            using (var form = new MultipartFormDataContent())
            {
                var fileStream = File.OpenRead(filePath);
                var streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                form.Add(streamContent, "file", Path.GetFileName(filePath));

                try
                {
                    var response = await client.PostAsync(apiUrl, form);
                    response.EnsureSuccessStatusCode();

                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<DetectionResponse>(json);
                    lastDetections = data.detections;
                    DrawDetections();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "API Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DrawDetections()
        {
            if (originalImg == null || lastDetections == null) return;

            Mat display = originalImg.Clone();
            int catCount = 0, dogCount = 0;

            foreach (var d in lastDetections)
            {
                if (d.label == "cat") catCount++;
                if (d.label == "dog") dogCount++;

                if (currentMode == "cat" && d.label != "cat") continue;
                if (currentMode == "dog" && d.label != "dog") continue;

                MCvScalar boxColor = d.label == "cat" ? new MCvScalar(0, 255, 0) : new MCvScalar(255, 0, 0);

                CvInvoke.Rectangle(display, new Rectangle(d.x, d.y, d.w, d.h),
                    boxColor, 2);

                CvInvoke.PutText(display, $"{d.label} {d.confidence:P1}",
                    new Point(d.x, d.y - 5),
                    Emgu.CV.CvEnum.FontFace.HersheySimplex,
                    0.6, boxColor, 2);
            }

            pictureBox1.Image = display.ToBitmap();

            // ✅ Display summary message
            if (currentMode == "cat" && catCount == 0)
            {
                MessageBox.Show("No cats detected!", "Detection Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (currentMode == "dog" && dogCount == 0)
            {
                MessageBox.Show("No dogs detected!", "Detection Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (currentMode == "both" && catCount == 0 && dogCount == 0)
            {
                MessageBox.Show("No cats or dogs detected!", "Detection Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // ✅ Optionally show counts in the window title
            this.Text = $"Cat & Dog Detector  |  Cats: {catCount}  Dogs: {dogCount}";
        }


        private void btnCat_Click(object sender, EventArgs e)
        {
            currentMode = "cat";
            DrawDetections();
        }

        private void btnDog_Click(object sender, EventArgs e)
        {
            currentMode = "dog";
            DrawDetections();
        }

        private void btnBoth_Click(object sender, EventArgs e)
        {
            currentMode = "both";
            DrawDetections();
        }

    }

    public class Detection
    {
        public string label { get; set; }
        public double confidence { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }

    public class DetectionResponse
    {
        public List<Detection> detections { get; set; }
    }
}
