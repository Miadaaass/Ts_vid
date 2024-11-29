using System;
using System.Drawing;
using System.Windows.Forms;
using OpenCvSharp;

namespace WebcamTargetFinder
{
    public partial class MainForm : Form
    {
        private VideoCapture capture;
        private Mat frame;
        private Timer timer;
        private PictureBox pictureBox;
        private Label labelCoordinates;
        private Button btnStartAnalysis;

        public MainForm()
        {
            InitializeComponent();
            InitializeWebcam();
            StartTimer();
        }

        private void InitializeComponent()
        {
            this.pictureBox = new PictureBox();
            this.labelCoordinates = new Label();
            this.btnStartAnalysis = new Button();
            this.SuspendLayout();

            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Size = new System.Drawing.Size(640, 480);
            this.pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            labelCoordinates.Location = new System.Drawing.Point(12, 500);
            this.labelCoordinates.Size = new System.Drawing.Size(200, 30);
            this.labelCoordinates.Text = "Center: ";

            this.btnStartAnalysis.Location = new System.Drawing.Point(220, 500);
            this.btnStartAnalysis.Size = new System.Drawing.Size(100, 30);
            this.btnStartAnalysis.Text = "Analyze";
            this.btnStartAnalysis.Click += new EventHandler(this.btnStartAnalysis_Click);

            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.labelCoordinates);
            this.Controls.Add(this.btnStartAnalysis);
            this.Text = "Webcam Target Finder";
            this.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
        }

        private void InitializeWebcam()
        {
            capture = new VideoCapture(0);
            frame = new Mat();
        }

        private void StartTimer()
        {
            timer = new Timer
            {
                Interval = 30
            };
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (capture.IsOpened())
            {
                capture.Read(frame);
                if (!frame.Empty())
                {
                    Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame);
                    pictureBox.Image = bitmap;
                    ProcessFrame();
                }
            }
        }

        private void ProcessFrame()
        {
        }

        private void btnStartAnalysis_Click(object sender, EventArgs e)
        {
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            capture.Release();
            frame.Dispose();
        }
    }
}
