using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace OtonomHazineAvcisi
{
    public partial class otonomHazineAvcisi : Form
    {
        List<Obstacle> constant_object_t = new List<Obstacle>();
        List<Treasue> treasues_t = new List<Treasue>();
        Character character_t;
        int charIndex = 0;
        int map_size_int;
        int[,,] map;
        RandomGenerator randomGenerator = new RandomGenerator();
        String[] charImages = {
            "C:\\Users\\melih\\Desktop\\prolab2.1\\karakterler\\3\\idle.gif",
            "C:\\Users\\melih\\Desktop\\prolab2.1\\karakterler\\2\\Idle.gif",
            "C:\\Users\\melih\\Desktop\\prolab2.1\\karakterler\\1\\ezgif.com-animated-gif-maker.gif" };


        public otonomHazineAvcisi()
        {
            InitializeComponent();
            charImageBox.Image = Image.FromFile(charImages[charIndex]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void charSelectLeftBut_Click(object sender, EventArgs e)
        {
            if (charIndex != 0 && charIndex < 3)
            {
                charIndex--;
                charImageBox.SizeMode = PictureBoxSizeMode.Zoom;
                charImageBox.Image = Image.FromFile(charImages[charIndex]);

                charImageBox.Update();
            }

        }


        private void charSelectRightBut_Click(object sender, EventArgs e)
        {
            if (charIndex < 2)
            {
                charIndex++;
                charImageBox.SizeMode = PictureBoxSizeMode.Zoom;
                charImageBox.Image = Image.FromFile(charImages[charIndex]);

                charImageBox.Update();
            }

        }
        private void newMapGenerate_Click(object sender, EventArgs e)
        {
            map_size_int = int.Parse(textBox1.Text);
            Debug.Write(map_size_int);
            int[,,] map = new int[3, map_size_int, map_size_int];
            this.map = map;
            constant_object_t = randomGenerator.ObstaclesGenerator(map_size_int, map_size_int, map);
            treasues_t = randomGenerator.TreasueGenerator(map_size_int, map_size_int, map);
            character_t = randomGenerator.CharacterGenerator(map_size_int, map_size_int, map, 3);
            Map1 map1 = new Map1(constant_object_t, treasues_t, character_t, map_size_int, map);
            map1.Show();



        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void startGame_Click(object sender, EventArgs e)
        {
            //açýk olan formlarý kapat
            foreach (Form form in Application.OpenForms)
            {
                // Farklý bir yerde açýlmýþ formu bul
                if (form.Name == "Map1")
                {
                    // Formu kapat
                    form.Hide();
                    break;
                }
            }
            this.Hide();


            Map1 map1 = new Map1(constant_object_t, treasues_t, character_t, map_size_int, map);
            map1.start_game();
            map1.Show();




        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                newMapGenerate_Click(sender, e);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

