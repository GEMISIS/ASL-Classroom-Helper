using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Speech.AudioFormat;

namespace ASL_Classroom_Helper
{
    public partial class mainForm : Form
    {
        private KinectSensor kinect;
        private byte[] colorPixels;
        private Bitmap colorBitmap;
        private Rectangle imageRectangle;

        private SpeechRecognitionEngine speechEngine;
        private DictationGrammar dictationGrammar;

        public bool setupKinect()
        {
            if(KinectSensor.KinectSensors.Count > 0)
            {
                this.kinect = KinectSensor.KinectSensors[0];

                this.kinect.ColorFrameReady += kinect_ColorFrameReady;
                this.kinect.ColorStream.Enable();
                this.kinect.Start();

                this.colorPixels = new byte[this.kinect.ColorStream.FramePixelDataLength];
                this.imageRectangle = new Rectangle(0, 0, this.kinect.ColorStream.FrameWidth, this.kinect.ColorStream.FrameHeight);
                this.colorBitmap = new Bitmap(this.kinect.ColorStream.FrameWidth, this.kinect.ColorStream.FrameHeight, PixelFormat.Format32bppRgb);

                this.dictationGrammar = new DictationGrammar();
                this.dictationGrammar.Name = "default dication";
                this.dictationGrammar.Enabled = true;

                this.speechEngine = new SpeechRecognitionEngine();
                this.speechEngine.LoadGrammar(this.dictationGrammar);
                this.speechEngine.SpeechHypothesized += speechEngine_SpeechHypothesized;
                this.speechEngine.SpeechRecognized += speechEngine_SpeechRecognized;
                this.speechEngine.SetInputToAudioStream(
                                    this.kinect.AudioSource.Start(), 
                                    new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
                this.speechEngine.RecognizeAsync(RecognizeMode.Multiple);

                return true;
            }
            return false;
        }

        void speechEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            this.speechTextLabel.Text = e.Result.Text;
        }

        void speechEngine_SpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        {
        }

        IntPtr ptr;
        BitmapData bmpData;

        void kinect_ColorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (colorFrame != null)
                {
                    colorFrame.CopyPixelDataTo(this.colorPixels);
                    bmpData = colorBitmap.LockBits(this.imageRectangle, ImageLockMode.WriteOnly, colorBitmap.PixelFormat);
                    ptr = bmpData.Scan0;
                    Marshal.Copy(this.colorPixels, 0, ptr, colorFrame.PixelDataLength);
                    this.colorBitmap.UnlockBits(bmpData);
                    this.kinectPictureBox.Image = this.colorBitmap;
                }
            }
        }

        public mainForm()
        {
            InitializeComponent();

            if(this.setupKinect())
            {
                this.kinectStatusLabel.Text = "Kinected!";
            }
        }
    }
}
