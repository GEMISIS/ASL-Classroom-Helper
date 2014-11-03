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

        private double soundDirection = 0.0f;

        private Skeleton[] skeletons;

        public bool setupKinect()
        {
            if(KinectSensor.KinectSensors.Count > 0)
            {
                skeletons = new Skeleton[6];
                this.kinect = KinectSensor.KinectSensors[0];

                this.kinect.AudioSource.BeamAngleChanged += AudioSource_BeamAngleChanged;
                this.kinect.AudioSource.SoundSourceAngleChanged += AudioSource_SoundSourceAngleChanged;
                this.kinect.ColorFrameReady += kinect_ColorFrameReady;
                this.kinect.ColorStream.Enable();
                this.kinect.SkeletonFrameReady += kinect_SkeletonFrameReady;
                this.kinect.SkeletonStream.Enable();
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

        void kinect_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
            }
        }

        void AudioSource_BeamAngleChanged(object sender, BeamAngleChangedEventArgs e)
        {
        }

        void AudioSource_SoundSourceAngleChanged(object sender, SoundSourceAngleChangedEventArgs e)
        {
            this.soundDirection = e.Angle;
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
        private byte[] white = { 255, 255, 255 };

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

                    using (Graphics g = Graphics.FromImage(this.colorBitmap))
                    {
                        foreach(Skeleton s in skeletons)
                        {
                            if(s != null && s.TrackingState == SkeletonTrackingState.Tracked
                                && this.speechTextLabel.Text != "")
                            {
                                g.FillRectangle(new SolidBrush(Color.White), 
                                    ((s.Joints[JointType.Head].Position.X + 1) / 2) * 
                                    colorBitmap.Width - 64,
                                    colorBitmap.Height - 
                                    ((s.Joints[JointType.Head].Position.Y + 1) / 2) * 
                                    colorBitmap.Height + 64, 128, 32);

                                g.DrawString(this.speechTextLabel.Text.Substring(0, 
                                    this.speechTextLabel.Text.Length > 11 ?
                                    12 : this.speechTextLabel.Text.Length), 
                                    new System.Drawing.Font("Arial", 16), new SolidBrush(Color.Black),
                                    ((s.Joints[JointType.Head].Position.X + 1) / 2) *
                                    colorBitmap.Width - 64,
                                    colorBitmap.Height -
                                    ((s.Joints[JointType.Head].Position.Y + 1) / 2) *
                                    colorBitmap.Height + 64);
                                break;
                            }
                        }
                    }

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
