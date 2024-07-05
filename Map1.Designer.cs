using System.Diagnostics;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;
using OtonomHazineAvcisi;

namespace OtonomHazineAvcisi
{
    partial class Map1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        int size_t;
        int square_size = 10;
        PictureBox pictureBox_fog = new PictureBox();
        PictureBox pictureBox_char1 = new PictureBox();
        Graphics g_panel2;


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
        private void recive_size(int size)
        {
            size_t = size * square_size;
        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Kalem oluşturma
            Pen pen = new Pen(Color.Black, 1);
            e.Graphics.FillRectangle(Brushes.White, 0, 0, size_t / 2, size_t);

            e.Graphics.FillRectangle(Brushes.Green, size_t / 2, 0, size_t, size_t);

            // 10x10 kareler çizme
            for (int i = 0; i < size_t / square_size; i++)
            {
                for (int j = 0; j < size_t / square_size; j++)
                {
                    g.DrawRectangle(pen, new Rectangle(i * square_size, j * square_size, square_size, square_size));
                }
            }

        }
        private void add_obstacle(List<Obstacle> constant_object)
        {
            foreach (Obstacle obstacle in constant_object)
            {
                if (obstacle is Motionless)
                {
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Tag = "obstacle";
                    pictureBox.Image = Image.FromFile(obstacle.getImage());
                    pictureBox.Location = new Point(obstacle.getLoc_x() * square_size, obstacle.getLoc_y() * square_size);
                    pictureBox.Size = new Size(obstacle.getSize_x() * square_size, obstacle.getSize_y() * square_size);
                    pictureBox.BackColor = Color.Transparent;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    typeof(PictureBox).InvokeMember("DoubleBuffered",
                                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                                null, panel1, new object[] { true });
                    if (obstacle.getLoc_x() >= size_t / square_size / 2)
                    {
                        pictureBox.BackColor = Color.Green;
                        Debug.WriteLine("x:{0} y:{1},türü{2}", obstacle.getLoc_x(), obstacle.getLoc_y(), obstacle.getObstacle_type());
                    }
                    else
                    {
                        pictureBox.BackColor = Color.White;
                        Debug.WriteLine("x:{0} y:{1},türü{2}", obstacle.getLoc_x(), obstacle.getLoc_y(), obstacle.getObstacle_type());

                    }



                    panel1.Controls.Add(pictureBox);
                    pictureBox.BringToFront();


                }
                else if (obstacle is Movement)
                {
                    Movement movement = obstacle as Movement;
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Image = Image.FromFile(obstacle.getImage());
                    pictureBox.Tag = "obstacle";

                    pictureBox.Location = new Point(obstacle.getLoc_x() * square_size, obstacle.getLoc_y() * square_size);
                    pictureBox.Size = new Size(obstacle.getSize_x() * square_size, obstacle.getSize_y() * square_size);
                    pictureBox.BackColor = Color.Transparent;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;



                    typeof(PictureBox).InvokeMember("DoubleBuffered",
                                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                                null, panel1, new object[] { true });
                    panel1.Controls.Add(pictureBox);
                    pictureBox.BringToFront();



                }
            }
        }
        private void add_treasure(List<Treasue> constant_treasure)
        {
            foreach (Treasue treasure in constant_treasure)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Tag = "treasure";
                pictureBox.Name = treasure.get_chest_ID().ToString();
                pictureBox.Image = Image.FromFile(treasure.get_image());
                pictureBox.Location = new Point(treasure.get_chest_x() * square_size, treasure.get_chest_y() * square_size);
                pictureBox.Size = new Size(square_size * 2, square_size * 2);
                pictureBox.BackColor = Color.Transparent;
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                typeof(PictureBox).InvokeMember("DoubleBuffered",
                                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                                null, panel1, new object[] { true });
                panel1.Controls.Add(pictureBox);
                pictureBox.BringToFront();

                Debug.WriteLine("x:{0} y:{1},türü{2},chestID{3}", treasure.get_chest_x(), treasure.get_chest_y(), treasure.get_tur(), treasure.get_chest_ID());
            }
        }
        private void add_character(Character character)
        {
            PictureBox pictureBox_char = new PictureBox();
            pictureBox_char1 = pictureBox_char;
            pictureBox_char.Tag = "character";
            pictureBox_char.Image = Image.FromFile(character.getImage());
            pictureBox_char.Location = new Point(character.location.getX() * square_size, character.location.getY() * square_size);
            pictureBox_char.Size = new Size(square_size, square_size);
            pictureBox_char.BackColor = Color.Red;
            pictureBox_char.SizeMode = PictureBoxSizeMode.StretchImage;
            typeof(PictureBox).InvokeMember("DoubleBuffered",
                                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                                null, pictureBox_char, new object[] { true });
            panel1.Controls.Add(pictureBox_char);
            //
            pictureBox_char.BringToFront();




        }
        public void PrintAll(int[,,] map, string filePath)
        {
            using (StreamWriter file = new StreamWriter(filePath))
            {
                for (int i = 0; i < size_t / square_size; i++)
                {
                    for (int j = 0; j < size_t / square_size; j++)
                    {
                        file.Write(map[1, i, j]);
                    }
                    file.Write("\n");
                }
            }
        }



        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>List<Obstacle> contanconstant_object
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel2 = new Panel();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(10000, 10000);
            panel1.TabIndex = 0;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.AutoScroll = true;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(10000, 10000);
            panel2.TabIndex = 1;
            // 
            // Map1
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoScroll = true;
            ClientSize = new Size(984, 961);
            Controls.Add(panel2);
            Controls.Add(panel1);
            DoubleBuffered = true;
            KeyPreview = true;
            Name = "Map1";
            Text = "Map1";
            WindowState = FormWindowState.Maximized;
            Load += Map1_Load;
            Scroll += hScrollBar1_Scroll;
            KeyDown += Map1_KeyDown;
            ResumeLayout(false);
        }

        #endregion
        private Panel panel1;
        private Panel panel2;
    }

}


