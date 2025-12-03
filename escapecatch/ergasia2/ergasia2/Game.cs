using System;
using System.Drawing;
using System.Windows.Forms;
using NAudio.Wave;

namespace ergasia2
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
            pictureBox1.SendToBack();
            this.MaximumSize = new Size(816, 539);
            this.MinimumSize = new Size(816, 539);
            this.Text = "Game";

            button1.Parent = pictureBox1;
            button2.Parent = pictureBox1;
            pictureBox3.Parent = pictureBox1;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var audioFile = new NAudio.Wave.MediaFoundationReader("hover.m4a");
            var outputDevice = new NAudio.Wave.WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form formp = new Play();
            formp.Show();
            Music.Escape = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form formp = new Play();
            formp.Show();
            Music.Escape = true;
        }
    }
}