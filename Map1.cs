using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtonomHazineAvcisi
{
    public partial class Map1 : Form
    {
        Solve solve = new Solve();

        Character character1;
        List<Obstacle> constant_object;
        List<Treasue> treasues;
        int[,,] map;
        public Map1(List<Obstacle> constant_object, List<Treasue> treasues, Character character, int size, int[,,] map)
        {

            character1 = character;
            this.map = map;
            this.constant_object = constant_object;
            this.treasues = treasues;
            recive_size(size);
            InitializeComponent();
            add_obstacle(constant_object);
            add_treasure(treasues);
            add_character(character);
            //printall(map);
            PrintAll(map, "C:\\Users\\melih\\Desktop\\deneme.txt");
            //temizle(map);

            //printall(map);
            panel2.Visible = false;

        }
        public void start_game()
        {

            add_obstacle(constant_object);
            add_treasure(treasues);
            //getfog(map);

            panel2.Visible = true;

            add_character(character1);



            solve.solveMap(map, character1, pictureBox_char1, pictureBox_fog, panel1, panel2,treasues);

        }



        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
        private void test_walk()
        {

        }


        private void Map1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                PrintAll(map, "C:\\Users\\melih\\Desktop\\deneme.txt");
                solve.Son_Exit();
                this.Close();
            }
        }

        private void Map1_Load(object sender, EventArgs e)
        {

        }
    }
}

