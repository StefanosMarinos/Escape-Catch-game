using System;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;

namespace ergasia2
{
    public partial class leaderboards : Form
    {
        string connectionString = "Data Source=MyData.db;Version=3;";

        public leaderboards()
        {
            InitializeComponent();
            this.Load += Leaderboards_Load; // Όταν ανοίγει η φόρμα, φορτώνει τα δεδομένα
        }

        private void Leaderboards_Load(object sender, EventArgs e)
        {
            LoadLeaderboards();
        }

        private void LoadLeaderboards()
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // ===== ESCAPE: 5 ΚΑΛΥΤΕΡΕΣ =====
                    string queryEscape = @"
                        SELECT Time
                        FROM Gamemode
                        WHERE Gamemode = 'Escape'
                        ORDER BY CAST(Time AS REAL) DESC
                        LIMIT 5;";

                    using (var cmd = new SQLiteCommand(queryEscape, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        StringBuilder sbEscape = new StringBuilder();
                        int rank = 1;

                        while (reader.Read())
                        {
                            sbEscape.AppendLine($"{rank}. {reader["Time"]} δευτερόλεπτα");
                            rank++;
                        }

                        richTextBox1.Text = sbEscape.Length > 0
                            ? sbEscape.ToString()
                            : "Δεν υπάρχουν ακόμα επιδόσεις για Escape.";
                    }

                    // ===== CHASE: 5 ΚΑΛΥΤΕΡΕΣ =====
                    string queryChase = @"
                        SELECT Time
                        FROM Gamemode
                        WHERE Gamemode = 'Catch'
                        ORDER BY CAST(Time AS REAL) ASC
                        LIMIT 5;";

                    using (var cmd = new SQLiteCommand(queryChase, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        StringBuilder sbChase = new StringBuilder();
                        int rank = 1;

                        while (reader.Read())
                        {
                            sbChase.AppendLine($"{rank}. {reader["Time"]} δευτερόλεπτα");
                            rank++;
                        }

                        richTextBox2.Text = sbChase.Length > 0
                            ? sbChase.ToString()
                            : "Δεν υπάρχουν ακόμα επιδόσεις για Catch.";
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Σφάλμα κατά τη φόρτωση των leaderboards:\n" + ex.Message);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            var audioFile = new NAudio.Wave.MediaFoundationReader("hover.m4a");
            var outputDevice = new NAudio.Wave.WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
        }
    }
}