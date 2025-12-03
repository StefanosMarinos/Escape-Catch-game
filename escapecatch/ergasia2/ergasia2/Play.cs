using System;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace ergasia2
{
    public partial class Play : Form
    {
        private int playerX = 350, playerY = 350, playerSize = 40, stepSpeed = 8;
        private int robotX = 700, robotY = 200, robotSize = 40, robotSpeed = 4;

        private bool moveUp, moveDown, moveLeft, moveRight;
        private bool gameStarted, gameEnded;

        private int teleportCounter = 0;
        private int elapsedTime = 0;
        private readonly Random rnd = new Random();

        private readonly System.Windows.Forms.Timer animationTimer;
        private readonly System.Windows.Forms.Timer gameTimer;

        private readonly string connectionString = "Data Source=MyData.db;Version=3;";
        private readonly SQLiteConnection connection;

        public Play()
        {

            this.MaximumSize = new Size(816, 539);
            this.MinimumSize = new Size(816, 539);
            connection = new SQLiteConnection(connectionString);

            this.MaximumSize = new Size(816, 539);
            this.MinimumSize = new Size(816, 539);
            this.Text = "Play";
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            InitializeComponent();

            pictureBox3.Visible = true;
            pictureBox3.Parent = pictureBox1;
            pictureBox1.SendToBack();

            // Ενεργοποίηση double buffering στο PictureBox για ομαλό rendering
            typeof(PictureBox).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(pictureBox1, true, null);

            pictureBox1.Paint += DrawGame;

            this.KeyDown += OnKeyDown;
            this.KeyUp += OnKeyUp;

            // Χρησιμοποιούμε 60fps περίπου
            animationTimer = new System.Windows.Forms.Timer
            {
                Interval = 16 // ~60 FPS
            };
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();

            gameTimer = new System.Windows.Forms.Timer
            {
                Interval = 100 // 0.1 sec
            };
            gameTimer.Tick += GameTimer_Tick;
        }

        private void DrawGame(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

            using (SolidBrush greenBrush = new SolidBrush(Color.Green))
            using (SolidBrush redBrush = new SolidBrush(Color.Red))
            using (SolidBrush robotBrush = new SolidBrush(Color.Blue))
            {
                g.FillRectangle(greenBrush, 70, 180, 80, 80);
                g.FillRectangle(robotBrush, robotX, robotY, robotSize, robotSize);
                g.FillRectangle(redBrush, playerX, playerY, playerSize, playerSize);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) moveUp = true;
            if (e.KeyCode == Keys.Down) moveDown = true;
            if (e.KeyCode == Keys.Left) moveLeft = true;
            if (e.KeyCode == Keys.Right) moveRight = true;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) moveUp = false;
            if (e.KeyCode == Keys.Down) moveDown = false;
            if (e.KeyCode == Keys.Left) moveLeft = false;
            if (e.KeyCode == Keys.Right) moveRight = false;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (gameEnded) return;

            int newX = playerX, newY = playerY;
            if (moveUp) newY -= stepSpeed;
            if (moveDown) newY += stepSpeed;
            if (moveLeft) newX -= stepSpeed;
            if (moveRight) newX += stepSpeed;

            if (newX >= 0 && newX + playerSize <= pictureBox1.Width) playerX = newX;
            if (newY >= 0 && newY + playerSize <= pictureBox1.Height) playerY = newY;

            Rectangle playerRect = new Rectangle(playerX, playerY, playerSize, playerSize);
            Rectangle greenRect = new Rectangle(70, 180, 80, 80);

            if (!gameStarted && greenRect.Contains(playerRect))
            {
                gameStarted = true;
                elapsedTime = 0;
                teleportCounter = 0;
                gameTimer.Start();
                pictureBox3.Visible = false;
            }

            if (gameStarted)
            {
                MoveRobot();
                CheckGameEnd();
            }

            // Καλύτερη απόδοση: δεν ανασχεδιάζει όλο το PictureBox
            pictureBox1.Invalidate(false);
        }

        private void MoveRobot()
        {
            float vx = playerX - robotX;
            float vy = playerY - robotY;
            float distance = Math.Max(1f, (float)Math.Sqrt(vx * vx + vy * vy));

            int direction = Music.Escape ? 1 : -1; // Escape: φεύγει, Catch: κυνηγάει
            int currentSpeed;

            if (Music.Escape)
            {
                // Αυξανόμενη ταχύτητα ανά 5s
                currentSpeed = robotSpeed + (elapsedTime / 50);
            }
            else
            {
                currentSpeed = Math.Max(1, stepSpeed - 2);
                if (distance < 150 && teleportCounter >= 30)
                {
                    robotX = rnd.Next(0, pictureBox1.Width - robotSize);
                    robotY = rnd.Next(0, pictureBox1.Height - robotSize);
                    teleportCounter = 0;
                }
            }

            robotX += (int)(direction * currentSpeed * (vx / distance));
            robotY += (int)(direction * currentSpeed * (vy / distance));

            // Περιορισμός στα όρια
            robotX = Math.Max(0, Math.Min(robotX, pictureBox1.Width - robotSize));
            robotY = Math.Max(0, Math.Min(robotY, pictureBox1.Height - robotSize));
        }

        private void CheckGameEnd()
        {
            if (new Rectangle(playerX, playerY, playerSize, playerSize)
                .IntersectsWith(new Rectangle(robotX, robotY, robotSize, robotSize)))
            {
                EndGame();
            }
        }

        private void EndGame()
        {
            gameEnded = true;
            gameTimer.Stop();
            animationTimer.Stop();

            MessageBox.Show($"Game Over! Χρόνος: {elapsedTime / 10.0:F1} δευτερόλεπτα");

            connection.Open();

            string createTable = @"
                CREATE TABLE IF NOT EXISTS Gamemode (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Gamemode TEXT NOT NULL,
                    Time TEXT NOT NULL
                );";
            using (var cmd = new SQLiteCommand(createTable, connection))
                cmd.ExecuteNonQuery();

            string gamemode = Music.Escape ? "Escape" : "Catch";
            string time = (elapsedTime / 10.0).ToString("F1");

            string insert = "INSERT INTO Gamemode (Gamemode, Time) VALUES (@Gamemode, @Time);";
            using (var cmd = new SQLiteCommand(insert, connection))
            {
                cmd.Parameters.AddWithValue("@Gamemode", gamemode);
                cmd.Parameters.AddWithValue("@Time", time);
                cmd.ExecuteNonQuery();
            }

            connection.Close();
            pictureBox3.Visible = true;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
            teleportCounter++;
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

    // Helper για να ενεργοποιήσουμε double buffering στο PictureBox
    public static class ControlExtensions
    {
        public static void DoubleBuffered(this Control control, bool enable)
        {
            typeof(Control).GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(control, enable, null);
        }
    }
}