using _1hergasiac_;
using NAudio.Wave;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ergasia2
{
    public partial class settings : Form
    {
        private TrackBar volumeBar;
        private Label volumeLabel;
        private Label valueLabel;

        public settings()
        {
            InitializeComponent();
            InitializeMusicControls();
            pictureBox1.SendToBack();

            pictureBox3.Parent = pictureBox1;
            this.Text = "Settings";
            this.MaximumSize = new Size(816, 539);
            this.MinimumSize = new Size(816, 539);
        }

        private void InitializeMusicControls()
        {
            this.Text = "Settings";
            this.BackColor = Color.FromArgb(30, 30, 30); // σκούρο φόντο

            // Δημιουργία Label
            System.Windows.Forms.Label volumeLabel = new System.Windows.Forms.Label();
            volumeLabel.Text = "Music Volume:";
            volumeLabel.ForeColor = Color.White;
            volumeLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            volumeLabel.AutoSize = true;
            volumeLabel.Top = 50;
            volumeLabel.Left = 480;
            this.Controls.Add(volumeLabel);

            // Δημιουργία TrackBar
            volumeBar = new TrackBar();
            volumeBar.Minimum = 0;
            volumeBar.Maximum = 100;
            volumeBar.Value = (int)(Music.SharedVolume * 100);
            volumeBar.TickFrequency = 10;
            volumeBar.Width = 250;
            volumeBar.Top = volumeLabel.Bottom + 2; // λίγο πιο κάτω από το label
            volumeBar.Left = 480;
            volumeBar.BackColor = Color.FromArgb(50, 50, 50);
            this.Controls.Add(volumeBar);

            // Προαιρετικά: εμφανίζει την τιμή του volume δίπλα στο TrackBar
            System.Windows.Forms.Label valueLabel = new System.Windows.Forms.Label();
            valueLabel.Text = $"{volumeBar.Value}%";
            valueLabel.ForeColor = Color.White;
            valueLabel.Font = new Font("Segoe UI", 9);
            valueLabel.AutoSize = true;
            valueLabel.Top = volumeBar.Top + (volumeBar.Height / 2) - 10;
            valueLabel.Left = volumeBar.Right + 1;
            this.Controls.Add(valueLabel);

            // Σύνδεση για ενημέρωση της τιμής
            volumeBar.Scroll += (s, e) =>
            {
                float volume = volumeBar.Value / 100f;
                Music.SharedVolume = volume;
                Program.SetVolume(volume);
                valueLabel.Text = $"{volumeBar.Value}%";
            };
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var audioFile = new NAudio.Wave.MediaFoundationReader("hover.m4a");
            var outputDevice = new NAudio.Wave.WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
            this.Close();
        }
    }
}
