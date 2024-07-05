namespace OtonomHazineAvcisi
{
    partial class otonomHazineAvcisi
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(otonomHazineAvcisi));
            pictureBox1 = new PictureBox();
            charSelectRightBut = new Button();
            charSelectLeftBut = new Button();
            charImageBox = new PictureBox();
            newMapGenerate = new Button();
            startGame = new Button();
            imageList1 = new ImageList(components);
            mapSize = new PictureBox();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)charImageBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)mapSize).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.BackgroundImage = Properties.Resources.OTONOM_HAZİNE_AVCISI;
            pictureBox1.Location = new Point(221, 125);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(600, 60);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // charSelectRightBut
            // 
            charSelectRightBut.BackColor = Color.Transparent;
            charSelectRightBut.BackgroundImage = Properties.Resources.charButton;
            charSelectRightBut.FlatAppearance.BorderSize = 0;
            charSelectRightBut.FlatAppearance.MouseDownBackColor = Color.Transparent;
            charSelectRightBut.FlatAppearance.MouseOverBackColor = Color.Transparent;
            charSelectRightBut.FlatStyle = FlatStyle.Flat;
            charSelectRightBut.Font = new Font("Segoe UI", 50F, FontStyle.Bold, GraphicsUnit.Point);
            charSelectRightBut.Location = new Point(662, 226);
            charSelectRightBut.Name = "charSelectRightBut";
            charSelectRightBut.Size = new Size(50, 100);
            charSelectRightBut.TabIndex = 1;
            charSelectRightBut.UseVisualStyleBackColor = false;
            charSelectRightBut.Click += charSelectRightBut_Click;
            // 
            // charSelectLeftBut
            // 
            charSelectLeftBut.BackColor = Color.Transparent;
            charSelectLeftBut.FlatAppearance.BorderSize = 0;
            charSelectLeftBut.FlatAppearance.MouseDownBackColor = Color.Transparent;
            charSelectLeftBut.FlatAppearance.MouseOverBackColor = Color.Transparent;
            charSelectLeftBut.FlatStyle = FlatStyle.Flat;
            charSelectLeftBut.Font = new Font("Segoe UI", 50F, FontStyle.Bold, GraphicsUnit.Point);
            charSelectLeftBut.Image = Properties.Resources.charButtonL;
            charSelectLeftBut.Location = new Point(312, 226);
            charSelectLeftBut.Name = "charSelectLeftBut";
            charSelectLeftBut.Size = new Size(50, 100);
            charSelectLeftBut.TabIndex = 2;
            charSelectLeftBut.UseVisualStyleBackColor = false;
            charSelectLeftBut.Click += charSelectLeftBut_Click;
            // 
            // charImageBox
            // 
            charImageBox.BackColor = Color.Transparent;
            charImageBox.Image = Properties.Resources.ezgif_com_animated_gif_maker;
            charImageBox.Location = new Point(437, 208);
            charImageBox.Name = "charImageBox";
            charImageBox.Size = new Size(150, 150);
            charImageBox.SizeMode = PictureBoxSizeMode.Zoom;
            charImageBox.TabIndex = 3;
            charImageBox.TabStop = false;
            charImageBox.Click += pictureBox2_Click;
            // 
            // newMapGenerate
            // 
            newMapGenerate.BackColor = Color.Transparent;
            newMapGenerate.BackgroundImage = Properties.Resources.newMap;
            newMapGenerate.FlatAppearance.BorderSize = 0;
            newMapGenerate.FlatAppearance.MouseDownBackColor = Color.Transparent;
            newMapGenerate.FlatAppearance.MouseOverBackColor = Color.Transparent;
            newMapGenerate.FlatStyle = FlatStyle.Flat;
            newMapGenerate.Font = new Font("Arial", 20F, FontStyle.Bold, GraphicsUnit.Point);
            newMapGenerate.Location = new Point(312, 508);
            newMapGenerate.Name = "newMapGenerate";
            newMapGenerate.Size = new Size(400, 60);
            newMapGenerate.TabIndex = 4;
            newMapGenerate.UseVisualStyleBackColor = false;
            newMapGenerate.Click += newMapGenerate_Click;
            // 
            // startGame
            // 
            startGame.BackColor = Color.Transparent;
            startGame.BackgroundImage = Properties.Resources.startGame;
            startGame.FlatAppearance.BorderSize = 0;
            startGame.FlatAppearance.MouseDownBackColor = Color.Transparent;
            startGame.FlatAppearance.MouseOverBackColor = Color.Transparent;
            startGame.FlatStyle = FlatStyle.Flat;
            startGame.Font = new Font("Arial", 20F, FontStyle.Bold, GraphicsUnit.Point);
            startGame.ForeColor = Color.Transparent;
            startGame.Location = new Point(312, 595);
            startGame.Name = "startGame";
            startGame.Size = new Size(400, 60);
            startGame.TabIndex = 5;
            startGame.UseVisualStyleBackColor = false;
            startGame.Click += startGame_Click;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "0.gif");
            imageList1.Images.SetKeyName(1, "1.gif");
            imageList1.Images.SetKeyName(2, "2.gif");
            // 
            // mapSize
            // 
            mapSize.BackColor = Color.Transparent;
            mapSize.BackgroundImageLayout = ImageLayout.None;
            mapSize.Image = Properties.Resources.haritaboyutu;
            mapSize.Location = new Point(312, 384);
            mapSize.Name = "mapSize";
            mapSize.Size = new Size(400, 60);
            mapSize.TabIndex = 6;
            mapSize.TabStop = false;
            // 
            // textBox1
            // 
            textBox1.ImeMode = ImeMode.NoControl;
            textBox1.Location = new Point(428, 479);
            textBox1.MaxLength = 5;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(159, 23);
            textBox1.TabIndex = 7;
            textBox1.Text = "100";
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.KeyPress += textBox1_KeyPress;
            // 
            // otonomHazineAvcisi
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            BackgroundImage = Properties.Resources.girisarkaplan;
            ClientSize = new Size(1008, 985);
            Controls.Add(textBox1);
            Controls.Add(mapSize);
            Controls.Add(startGame);
            Controls.Add(newMapGenerate);
            Controls.Add(charImageBox);
            Controls.Add(charSelectLeftBut);
            Controls.Add(charSelectRightBut);
            Controls.Add(pictureBox1);
            ForeColor = Color.CornflowerBlue;
            Name = "otonomHazineAvcisi";
            Text = "otonomHazineAvcisi";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)charImageBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)mapSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button charSelectRightBut;
        private Button charSelectLeftBut;
        private PictureBox charImageBox;
        private Button newMapGenerate;
        private Button startGame;
        private ImageList imageList1;
        private PictureBox mapSize;
        private TextBox textBox1;
    }
}