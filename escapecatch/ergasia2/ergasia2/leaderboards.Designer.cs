namespace ergasia2
{
    partial class leaderboards
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            richTextBox1 = new RichTextBox();
            richTextBox2 = new RichTextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.Pixel_Art_GIF;
            pictureBox1.Location = new Point(0, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(800, 452);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Transparent;
            pictureBox3.Image = Properties.Resources.back_4315454;
            pictureBox3.Location = new Point(28, 25);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(133, 110);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 4;
            pictureBox3.TabStop = false;
            pictureBox3.Click += pictureBox3_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Algerian", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(188, 55);
            label1.Name = "label1";
            label1.Size = new Size(581, 80);
            label1.TabIndex = 5;
            label1.Text = "LeaderBoards";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Algerian", 26F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(562, 157);
            label2.Name = "label2";
            label2.Size = new Size(187, 58);
            label2.TabIndex = 6;
            label2.Text = "Catch";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Algerian", 26F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(12, 157);
            label3.Name = "label3";
            label3.Size = new Size(218, 58);
            label3.TabIndex = 7;
            label3.Text = "Escape";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(28, 239);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(187, 199);
            richTextBox1.TabIndex = 8;
            richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            richTextBox2.Location = new Point(562, 239);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.Size = new Size(187, 199);
            richTextBox2.TabIndex = 9;
            richTextBox2.Text = "";
            // 
            // leaderboards
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(richTextBox2);
            Controls.Add(richTextBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox1);
            Name = "leaderboards";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "leaderboards";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private RichTextBox richTextBox1;
        private RichTextBox richTextBox2;
    }
}