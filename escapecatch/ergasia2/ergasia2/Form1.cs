using System;
using System.Media;
using System.Windows.Forms;

namespace ergasia2
{
    public partial class Form1 : Form
    {
        private SoundPlayer hoverSound = new SoundPlayer("hover.wav"); // ήχος που θα παίξει

        public Form1()
        {
            this.Text = "Main Menu";
            this.MaximumSize = new Size(816, 539);
            this.MinimumSize = new Size(816, 539);
            InitializeComponent();

            pictureBox1.SendToBack();
            label1.Parent = pictureBox1;
            pictureBox2.Parent = pictureBox1;
            pictureBox3.Parent = pictureBox1;
            pictureBox4.Parent = pictureBox1;
            pictureBox5.Parent = pictureBox1;

            // σύνδεσε το ίδιο event σε όλα τα pictureboxes
            pictureBox2.MouseClick += PlayHoverSound;
            pictureBox3.MouseClick += PlayHoverSound;
            pictureBox4.MouseClick += PlayHoverSound;
        }

        private void PlayHoverSound(object sender, EventArgs e)
        {
            var audioFile = new NAudio.Wave.MediaFoundationReader("hover.m4a");
            var outputDevice = new NAudio.Wave.WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
        }

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form formset = new settings();
            formset.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form Gamesel = new Game();
            Gamesel.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Form Leaderfor = new leaderboards();
            Leaderfor.Show();
        }
    }
}
