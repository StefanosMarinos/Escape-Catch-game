using ergasia2;
using NAudio.Wave;
using System;
using System.Windows.Forms;

namespace _1hergasiac_
{
    internal static class Program
    {
        private static WaveOutEvent outputDevice;
        private static AudioFileReader audioFile;

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Ξεκινάμε μουσική με μέση ένταση
            PlayMusic("ElectromanAdventures.wav", 0.5f);

            Application.Run(new Form1());
        }

        public static void PlayMusic(string filePath, float volume = 0.5f)
        {
            try
            {

                audioFile = new AudioFileReader(filePath)
                {
                    Volume = Math.Clamp(volume, 0f, 1f)
                };

                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
                outputDevice.PlaybackStopped += (s, e) => {
                    audioFile.Position = 0;
                    outputDevice.Play();
                };
                outputDevice.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα κατά την αναπαραγωγή μουσικής:\n" + ex.Message);
            }
        }

        public static void SetVolume(float volume)
        {
            if (audioFile != null)
                audioFile.Volume = Math.Clamp(volume, 0f, 1f);
        }
    }
}