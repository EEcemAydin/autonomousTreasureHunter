using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtonomHazineAvcisi
{

    public class Solve
    {
        private System.Windows.Forms.Timer myTimer;
        int[,,] map;
        Character character;
        PictureBox char_picturebox;
        PictureBox fog_picturebox;
        Panel panel1;
        Panel panel2;
        int square_size;
        int square_count;
        Random random = new Random();
        Graphics g;
        int directions = 0;//0 sol 1 sağ 2yukarı 3aşağı
        List<Treasue> treasues;

        int temp_adim = 0;
        List<Point> points = new List<Point>();
        Queue<Treasue> sirayla = new Queue<Treasue>();
        List<Treasue> zumrut = new List<Treasue>();
        List<Treasue> altin = new List<Treasue>();
        List<Treasue> gümüş = new List<Treasue>();
        List<Treasue> demir = new List<Treasue>();




        public void Son_Exit()
        {
            // Assuming you want to display the information in a MessageBox instead of the debug terminal:
            string message = "";

            message += "\nAccording to the Order of Collection\n";
            while (sirayla.Count > 0)
            {
                Treasue t = sirayla.Dequeue();
                message += $"x:{t.get_chest_x()} y:{t.get_chest_y()} tur:{t.get_tur()} ID:{t.get_chest_ID()}\n";

                if (t.get_tur() == 0)
                {
                    zumrut.Add(t);
                }
                else if (t.get_tur() == 1)
                {
                    altin.Add(t);
                }
                else if (t.get_tur() == 2)
                {
                    gümüş.Add(t);
                }
                else if (t.get_tur() == 3)
                {
                    demir.Add(t);
                }
            }

            message += "\nImportant Order by Type:\n";

            foreach (Treasue t in zumrut)
            {
                message += $"x:{t.get_chest_x()} y:{t.get_chest_y()} tur:{t.get_tur()} ID:{t.get_chest_ID()}\n";
            }

            foreach (Treasue t in altin)
            {
                message += $"x:{t.get_chest_x()} y:{t.get_chest_y()} tur:{t.get_tur()} ID:{t.get_chest_ID()}\n";
            }

            foreach (Treasue t in gümüş)
            {
                message += $"x:{t.get_chest_x()} y:{t.get_chest_y()} tur:{t.get_tur()} ID:{t.get_chest_ID()}\n";
            }

            foreach (Treasue t in demir)
            {
                message += $"x:{t.get_chest_x()} y:{t.get_chest_y()} tur:{t.get_tur()} ID:{t.get_chest_ID()}\n";
            }

            // Display the message in a MessageBox
            MessageBox.Show(message, "Treasure Chest Information", MessageBoxButtons.OK, MessageBoxIcon.Information);



        }


        private void StartTimer()
        {
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Interval = 1; // 1000 milisaniye (1 saniye)
            myTimer.Tick += new EventHandler(Timer_Tick);
            myTimer.Start();
        }
        private void StopTimer()
        {
            myTimer.Stop();
        }

        // Timer'ın Tick olayı

        public void solveMap(int[,,] map, Character character, PictureBox char_picturebox, PictureBox fog_pictureBox, Panel panel1, Panel panel2, List<Treasue> treasues)
        {
            this.map = map;
            this.character = character;
            this.char_picturebox = char_picturebox;
            this.fog_picturebox = fog_pictureBox;
            square_size = char_picturebox.Width;
            square_count = map.GetLength(1);
            this.panel1 = panel1;
            this.panel2 = panel2;
            panel2.Controls.Add(char_picturebox);
            this.treasues = treasues;
            char_picturebox.BackColor = System.Drawing.Color.Red;
            // Graphics nesnesi oluştur
            g = panel2.CreateGraphics();
            typeof(Panel).InvokeMember("DoubleBuffered",
                                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                                null, panel2, new object[] { true });



            //panel1 deki tagları obstacle ve treasure olanları görünmez yap
            foreach (Control c in panel1.Controls)
            {
                if (c.Tag != null)
                {
                    if (c.Tag.ToString() == "obstacle" || c.Tag.ToString() == "treasure")
                    {
                        c.Visible = false;
                    }
                }
            }
            //SİSLERİ 1 YAP
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = 0; j < map.GetLength(2); j++)
                {
                    map[2, i, j] = 1;
                }
            }
            StartTimer();

        }
        int adımsayısı = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            temizle2();
            matris_fog_clear(map, character);
            char_picturebox.Refresh();
            update_visible_picturebox();
            go2();
            temizle2();




            //temizle1(map, panel1);

            if (map.GetLength(2) >= 100)
            {
                if (adımsayısı > 2500)
                {
                    Son_Exit();
                    StopTimer();
                }
            }
            else if (map.GetLength(2) == 1000)
            {
                if (adımsayısı > 25000)
                {
                    Son_Exit();
                    StopTimer();
                }
            }




        }
        //karakterin bulunduğu yerden 7x7lik alanı kontrol et eğer engel veya hazine varsa pictureboxu görünür yap


        //karakterin gittiği kareleri panel1 de kırmızı ile işaretler

        int dogrultu = 0;//0 sol-sağ 1 yukarı-aşağı
        int yukarımı_asagimi = 0;//0 yukarı 1 aşağı
        int solmu_sağmı = 0;//0 sol 1 sağ
        int temp_adim2 = 0;
        int go_treasue_count = 0;
        int kac_count = 0;
        int temp_adim_sayisi = 0;

        void go_treasue()
        {
            Point closest_treasure = bring_close_treasue();
            Debug.WriteLine("closest_treasure x:{0} y:{1}", closest_treasure.X, closest_treasure.Y);
            int char_x = character.location.getX();
            int char_y = character.location.getY();
            if (closest_treasure.X != -1)
            {
                if (character.location.getX() >= closest_treasure.X - 1 && character.location.getX() <= closest_treasure.X + 2 && character.location.getY() >= closest_treasure.Y - 1 && character.location.getY() <= closest_treasure.Y + 2)
                {
                    pick_treasure(closest_treasure.X, closest_treasure.Y);
                    points.Remove(closest_treasure);
                }
                else if (char_x < closest_treasure.X)
                {
                    if (check_go(1))
                    {
                        dogrultu = 0;
                        solmu_sağmı = 0;
                        character.go_right(char_picturebox, map);
                        adımsayısı++;
                    }
                    else
                    {
                        random_go();
                    }


                }
                else if (char_x > closest_treasure.X)
                {
                    if (check_go(0))
                    {
                        dogrultu = 0;
                        solmu_sağmı = 1;
                        character.go_left(char_picturebox, map);
                        adımsayısı++;
                    }
                    else
                    {
                        random_go();
                    }

                }
                else if (char_y < closest_treasure.Y)
                {
                    if (check_go(3))
                    {
                        dogrultu = 1;
                        yukarımı_asagimi = 1;
                        character.go_down(char_picturebox, map);
                        adımsayısı++;
                    }
                    else
                    {
                        random_go();
                    }

                }
                else if (char_y > closest_treasure.Y)
                {
                    if (check_go(2))
                    {
                        dogrultu = 1;
                        yukarımı_asagimi = 0;
                        character.go_up(char_picturebox, map);
                        adımsayısı++;
                    }
                    else
                    {
                        random_go();
                    }

                }


            }



        }
        void pass_the_obstaclce(int direction)
        {
            switch (direction)
            {
                case 0:
                    //sağa ya da sola gitme için engel varsa sağa gidebileceği en yakın yolu bul
                    int char_x = character.location.getX();
                    int char_y = character.location.getY();
                    int min = map.GetLength(2);
                    int min_y = map.GetLength(1);
                    for (int i = char_y - 2; i < char_y + 2; i++)
                    {
                        for (int j = char_x - 2; j < char_x + 2; j++)
                        {
                            if (map[0, i, j] == 0)
                            {
                                int distance = Math.Abs(j - char_x);
                                if (distance < min)
                                {
                                    min = distance;
                                    min_y = i;
                                }
                            }
                        }
                    }
                    if (min_y < char_y)
                    {
                        if (check_go(2))
                        {
                            character.go_up(char_picturebox, map);
                        }
                    }
                    else
                    {
                        if (check_go(3))
                        {
                            character.go_down(char_picturebox, map);
                        }
                    }

                    break;
                case 1:
                    //yukarı ya da aşağı gitme için engel varsa yukarı gidebileceği en yakın yolu bul
                    char_x = character.location.getX();
                    char_y = character.location.getY();
                    min = map.GetLength(2);
                    int min_x = map.GetLength(1);
                    for (int i = char_y - 2; i < char_y + 2; i++)
                    {
                        for (int j = char_x - 2; j < char_x + 2; j++)
                        {
                            if (map[0, i, j] == 0)
                            {
                                int distance = Math.Abs(i - char_y);
                                if (distance < min)
                                {
                                    min = distance;
                                    min_x = j;
                                }
                            }
                        }
                    }
                    if (min_x < char_x)
                    {
                        if (check_go(0))
                        {
                            character.go_left(char_picturebox, map);
                        }
                    }
                    else
                    {
                        if (check_go(1))
                        {
                            character.go_right(char_picturebox, map);
                        }
                    }
                    break;



            }
        }

        void go()
        {
            Debug.WriteLine("adımsayısı:{0}", adımsayısı);
            if (points.Count != 0)
            {
                Debug.WriteLine("points:{0}", points.Count);
                Point closest_treasure = bring_close_treasue();

                go_treasue();

                directions = -1;
            }
            else
            {
                Debug.WriteLine("go normal");

                /* int left_or_right_count = 0;
             left_or_right_choice:
                 Debug.WriteLine("left_or_right");
                 if (left_or_right == -1 && left_or_right_count < 2)
                 {
                     left_or_right = random.Next(0, 2);
                     if (!can_go_direction(left_or_right))
                     {
                         left_or_right_count++;
                         goto left_or_right_choice;
                     }
                     up_or_down = -2;
                 }
                 else if (up_or_down == -1) {

                         int up_or_down_count = 0;
                     up_or_down_choice:
                         Debug.WriteLine("up_or_down");
                         up_or_down = random.Next(2, 4);
                         if (!can_go_direction(up_or_down) && up_or_down_count < 2)
                         {
                             up_or_down_count++;
                             goto up_or_down_choice;
                         }
                         left_or_right = -2;



                 }
                */

                // ilk önce sağ sol rastgele seç doluysa diğerini seç o da doluysa yukarı aşağı seç 
                int sayac = 0;
                while (directions == -1)
                {

                    directions = random.Next(0, 2);//0 sol 1 sağ 2 yukarı 3 aşağı
                    if (sayac > 5)
                    {
                        escape();
                        break;
                    }
                    if (can_go_direction(directions))
                    {
                        break;
                    }
                    else
                    {
                        directions = -1;
                        sayac++;
                    }
                    Debug.WriteLine("directions:{0}", directions);
                }


                int char_x = character.location.getX();
                int char_y = character.location.getY();

                if (!can_go_direction(directions) || temp_adim > 5)
                {
                    directions = -1;
                    temp_adim = 0;
                }
                else if (directions == 0 && temp_adim < 6 && char_x > 4)//left
                {
                    if (check_go(0))
                    {
                        character.go_left(char_picturebox, map);
                        temp_adim++;
                        adımsayısı++;

                        Debug.WriteLine("Left");
                    }

                }
                else if (directions == 1 && temp_adim < 6 && char_x < 96)
                {
                    if (check_go(1))
                    {
                        character.go_right(char_picturebox, map);
                        adımsayısı++;

                        temp_adim++;
                        Debug.WriteLine("Right");


                    }
                }
                else if (directions == 2 && temp_adim < 6 && char_y > 4)
                {
                    if (check_go(2))
                    {
                        adımsayısı++;

                        temp_adim++;

                        character.go_up(char_picturebox, map);
                        Debug.WriteLine("Up");
                    }
                }
                else if (directions == 3 && temp_adim < 6 && char_y < 96)
                {
                    if (check_go(3))
                    {
                        character.go_down(char_picturebox, map);
                        adımsayısı++;
                        temp_adim++;
                        Debug.WriteLine("Down");
                    }
                }
                else
                {
                    directions = -1;
                    if (temp_adim > 6)
                    {
                        temp_adim = 0;
                        if (dogrultu == 0)
                        {
                            dogrultu = 1;
                        }
                        else
                        {
                            dogrultu = 0;
                        }
                    }

                }

            }

        }


        void go2()
        {


            Debug.WriteLine("adımsayısı:{0}", adımsayısı);
            Debug.WriteLine("kac_count:{0}", kac_count);
            if (points.Count != 0)
            {
                Debug.WriteLine("points:{0}", points.Count);
                Point closest_treasure = bring_close_treasue();

                go_treasue();
                go_treasue_count++;
            }
            else
            {
                if (kac_count >= 2)
                {
                    kac_count = 0;
                    escape();
                }

                Debug.WriteLine("go normal");
                if (go_treasue_count != 0)
                {
                    if (dogrultu == 0)
                    {
                        dogrultu = 1;
                        temp_adim2 = 0;

                    }
                    else
                    {
                        dogrultu = 0;
                        temp_adim2 = 0;
                    }
                    go_treasue_count = 0;

                }
                if (dogrultu == 0)
                {

                    if (solmu_sağmı == 0 && temp_adim2 < 6 && can_go_direction(0))
                    {

                        if (check_go(0))
                        {
                            temp_adim2++;
                            adımsayısı++;
                            character.go_left(char_picturebox, map);
                            Debug.WriteLine("Left");
                        }
                        else
                        {
                            kac_count++;
                            Debug.WriteLine("NOLeft");
                            if (check_go(2))
                            {
                                character.go_up(char_picturebox, map);
                                Debug.WriteLine("Up");
                            }
                            else if (check_go(1))
                            {
                                character.go_right(char_picturebox, map);
                                Debug.WriteLine("Right");
                            }

                            else if (check_go(3))
                            {
                                character.go_down(char_picturebox, map);
                                Debug.WriteLine("Down");
                            }
                        }

                    }
                    else if (solmu_sağmı == 1 && temp_adim2 < 6 && can_go_direction(1))
                    {
                        if (check_go(1))
                        {
                            temp_adim2++;
                            adımsayısı++;
                            character.go_right(char_picturebox, map);
                            Debug.WriteLine("Right");


                        }
                        else
                        {
                            kac_count++;
                            Debug.WriteLine("NORight");
                            if (check_go(2))
                            {
                                character.go_up(char_picturebox, map);
                                Debug.WriteLine("Up");
                            }
                            else if (check_go(0))
                            {
                                character.go_left(char_picturebox, map);
                                Debug.WriteLine("Left");
                            }

                            else if (check_go(3))
                            {
                                character.go_down(char_picturebox, map);
                                Debug.WriteLine("Down");
                            }

                        }

                    }
                    else if (temp_adim2 >= 5)
                    {
                        temp_adim2 = 0;
                        dogrultu = 1;


                    }

                    else if (!can_go_direction(0) && !can_go_direction(1))
                    {
                        Debug.WriteLine("sol-sağ dolu");
                        dogrultu = 1;
                        temp_adim2 = 0;
                        kac_count++;





                    }
                    else if (!can_go_direction(0))
                    {
                        Debug.WriteLine("sol dolu");
                        solmu_sağmı = 1;
                        temp_adim2 = 0;





                    }
                    else if (!can_go_direction(1))
                    {
                        Debug.WriteLine("sağ dolu");
                        solmu_sağmı = 0;
                        temp_adim2 = 0;




                    }
                    else
                    {
                        kac_count++;

                        escape();
                    }






                }

                else if (dogrultu == 1)
                {
                    if (yukarımı_asagimi == 0 && temp_adim2 < 6 && can_go_direction(2))
                    {
                        if (check_go(2))
                        {
                            temp_adim2++;
                            adımsayısı++;
                            character.go_up(char_picturebox, map);
                            Debug.WriteLine("Up");

                        }
                        else
                        {
                            kac_count++;
                            Debug.WriteLine("NOUp");
                            if (check_go(1))
                            {
                                character.go_right(char_picturebox, map);
                                Debug.WriteLine("Right");
                            }
                            else if (check_go(3))
                            {
                                character.go_down(char_picturebox, map);
                                Debug.WriteLine("Down");
                            }
                            else if (check_go(0))
                            {
                                character.go_left(char_picturebox, map);
                                Debug.WriteLine("Left");
                            }
                        }
                    }
                    else if (yukarımı_asagimi == 1 && temp_adim2 < 6 && can_go_direction(3))
                    {
                        if (check_go(3))
                        {
                            temp_adim2++;
                            adımsayısı++;
                            character.go_down(char_picturebox, map);
                            Debug.WriteLine("Down");

                        }
                        else
                        {
                            kac_count++;
                            Debug.WriteLine("NODown");
                            if (check_go(2))
                            {
                                character.go_up(char_picturebox, map);
                                Debug.WriteLine("Up");
                            }
                            else if (check_go(1))
                            {
                                character.go_right(char_picturebox, map);
                                Debug.WriteLine("Right");
                            }

                            else if (check_go(0))
                            {
                                character.go_left(char_picturebox, map);
                                Debug.WriteLine("Left");
                            }

                        }
                    }
                    else if (temp_adim2 >= 5)
                    {
                        temp_adim2 = 0;
                        dogrultu = 0;


                    }
                    else if (!can_go_direction(2) && !can_go_direction(3))
                    {
                        Debug.WriteLine("yukarı-asağı dolu");
                        dogrultu = 0;
                        temp_adim2 = 0;

                        kac_count++;




                    }
                    else if (!can_go_direction(2))
                    {
                        Debug.WriteLine("yukarı dolu");
                        yukarımı_asagimi = 1; temp_adim2 = 0;



                    }
                    else if (!can_go_direction(3))
                    {
                        Debug.WriteLine("aşağı dolu");
                        yukarımı_asagimi = 0; temp_adim2 = 0;


                    }
                    else
                    {
                        escape();


                    }



                }







            }
        }






        Boolean can_go_direction(int direction)
        {
            int char_x = character.location.getX();
            int char_y = character.location.getY();

            switch (direction)
            {
                case 0://left
                    if ((map[0, char_y, Math.Max(0, char_x - 1)] == 1
                       || map[0, char_y, Math.Max(0, char_x - 2)] == 1
                       || map[0, char_y, Math.Max(0, char_x - 3)] == 1)
                       || (map[1, char_y, Math.Max(0, char_x - 1)] != 0
                       || map[1, char_y, Math.Max(0, char_x - 2)] != 0
                       || map[1, char_y, Math.Max(0, char_x - 3)] != 0)
                       || char_x < 3)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                case 1://right
                    if ((map[0, char_y, Math.Min(char_x + 1, square_count - 1)] == 1
                       || map[0, char_y, Math.Min(char_x + 2, square_count - 1)] == 1
                       || map[0, char_y, Math.Min(char_x + 3, square_count - 1)] == 1)
                       || map[1, char_y, Math.Min(char_x + 3, square_count - 1)] != 0
                       || map[1, char_y, Math.Min(char_x + 3, square_count - 1)] != 0
                       || map[1, char_y, Math.Min(char_x + 3, square_count - 1)] != 0
                       || char_x > map.GetLength(2) - 4)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                case 2://up

                    if ((map[0, Math.Max(0, char_y - 1), char_x] == 1
                        || map[0, Math.Max(0, char_y - 2), char_x] == 1
                        || map[0, Math.Max(0, char_y - 3), char_x] == 1)
                        || map[1, Math.Max(0, char_y - 1), char_x] != 0
                        || map[1, Math.Max(0, char_y - 2), char_x] != 0
                        || map[1, Math.Max(0, char_y - 3), char_x] != 0
                        || char_y < 3)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                case 3://down
                    if ((map[0, Math.Min(char_y + 1, square_count - 1), char_x] == 1
                        || map[0, Math.Min(char_y + 2, square_count - 1), char_x] == 1
                        || map[0, Math.Min(char_y + 3, square_count - 1), char_x] == 1)
                        || map[1, Math.Min(char_y + 1, square_count - 1), char_x] != 0
                        || map[1, Math.Min(char_y + 2, square_count - 1), char_x] != 0
                        || map[1, Math.Min(char_y + 3, square_count - 1), char_x] != 0
                        || char_y > map.GetLength(2) - 4)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                default:
                    return false;

            }
            return false;

        }










        Boolean check_go(int direction)
        {
            switch (direction)
            {
                case 0://left
                    if (map[0, character.location.getY(), Math.Max(0, character.location.getX() - 1)] == 0)
                    {
                        return true;
                    }
                    break;
                case 1://right
                    if (map[0, character.location.getY(), Math.Min(character.location.getX() + 1, 99)] == 0)
                    {
                        return true;
                    }
                    break;//up
                case 2:
                    if (map[0, Math.Max(0, character.location.getY() - 1), character.location.getX()] == 0)
                    {
                        return true;
                    }
                    break;
                case 3://down
                    if (map[0, Math.Min(character.location.getY() + 1, 99), character.location.getX()] == 0)
                    {
                        return true;
                    }
                    break;
                default:
                    return false;
            }
            return false;
        }
        int random_count = 0;
        void random_go()
        {
            random_count++;
            int random = this.random.Next(0, 4);
            if (character.location.getX() <= square_count && character.location.getY() <= square_count && character.location.getY() >= 0 && character.location.getX() >= 0)
            {
                if (random == 2)
                {
                    if (check_go(2))
                    {
                        character.go_up(char_picturebox, map);
                    }

                }
                else if (random == 3)
                {
                    if (check_go(3))
                    {
                        character.go_down(char_picturebox, map);
                    }
                }
                else if (random == 0)
                {
                    if (check_go(0))
                    {
                        character.go_left(char_picturebox, map);
                    }
                }
                else if (random == 1)
                {
                    if (check_go(1))
                    {
                        character.go_right(char_picturebox, map);
                    }
                }

            }

        }
        void escape()
        {
            //gidebilceği yollardan 10 adım ilerle
            Debug.WriteLine("escape");
            int char_x = character.location.getX();
            int char_y = character.location.getY();
            if (char_x > 0 && char_x < map.GetLength(2) && char_y > 0 && char_y < map.GetLength(2))
            {
                int random = this.random.Next(0, 4);
                for (int i = 0; i < 4; i++)
                {
                    if (random == 0)
                    {
                        if (check_go(0))
                        {
                            character.go_left(char_picturebox, map);
                        }
                    }
                    else if (random == 1)
                    {
                        if (check_go(1))
                        {
                            character.go_right(char_picturebox, map);
                        }
                    }
                    else if (random == 2)
                    {
                        if (check_go(2))
                        {
                            character.go_up(char_picturebox, map);
                        }
                    }
                    else if (random == 3)
                    {
                        if (check_go(3))
                        {
                            character.go_down(char_picturebox, map);
                        }
                    }
                }
            }
            else
            {
                random_go();
            }








        }
        Boolean is_escape()
        {
            //karakterin gezdiği yolu kontrol et eğer karakterin 7*7 alanda 20 den 1 fazla varsa true döner
            int count = 0;
            int x = Math.Max(0, character.location.getX() - 3);
            int end_x = Math.Min(character.location.getX() + 4, 99);
            int y = Math.Max(0, character.location.getY() - 3);
            int end_y = Math.Min(character.location.getY() + 4, 99);
            for (int i = y; i < end_y; i++)
            {
                for (int j = x; j < end_x; j++)
                {
                    if (map[1, i, j] == 1)
                    {
                        count++;
                    }
                }
            }

            if (count > 15)
            {
                return true;
            }
            //karakterin etrafındaki 10*10 luk karede sisin %30 ı varsa true döner
            int count_fog = 0;
            int x_fog = Math.Max(0, character.location.getX() - 5);
            int end_x_fog = Math.Min(character.location.getX() + 6, 99);
            int y_fog = Math.Max(0, character.location.getY() - 5);
            int end_y_fog = Math.Min(character.location.getY() + 6, 99);
            for (int i = y_fog; i < end_y_fog; i++)
            {
                for (int j = x_fog; j < end_x_fog; j++)
                {
                    if (map[2, i, j] == 1)
                    {
                        count_fog++;
                    }
                }
            }
            if (count_fog < 30)
            {
                return true;
            }


            return false;

        }

        void pick_treasure(int x, int y)
        {
            int sandik_id = -1;
            //herhangi bir noktası gelen sandığı bul 4 tane 2 olmalı ve matristeki karelerini 3 yap 
            if (map[0, y, x] == 2)
            {
                for (int i = y; i < y + 2; i++)
                {
                    for (int j = x; j < x + 2; j++)
                    {
                        map[0, i, j] = 3;
                    }
                }
                foreach (PictureBox pictureBox in panel2.Controls.OfType<PictureBox>())
                {
                    if (pictureBox.Location.X / square_size == x && pictureBox.Location.Y / square_size == y)
                    {
                        pictureBox.Tag = "picked";
                        pictureBox.BackColor = System.Drawing.Color.Red;
                        Debug.WriteLine("hazine Alındı x:{0} y:{1},SandıkID:{2}", x, y, pictureBox.Name);
                        sandik_id = Convert.ToInt32(pictureBox.Name);
                        break;

                    }
                }

                //listeden sandığı bul ve sıralı kuruğuna ekle ve önemsıralı kuyruğua ekle

                foreach (Treasue t in treasues)
                {
                    if (t.get_chest_x() == x && t.get_chest_y() == y)
                    {
                        t.set_tur(adımsayısı);
                        sirayla.Enqueue(t);
                        break;
                    }
                }



            }
            points.Remove(new Point(x, y));


        }

        void update_visible_picturebox()
        {
            points.Clear();
            if (check_obstacle(map, character.location.getX() - 3, character.location.getY() - 3, character.location.getX() + 4, character.location.getY() + 4))
            {
                foreach (PictureBox pictureBox in panel1.Controls.OfType<PictureBox>())
                {
                    int picX = (pictureBox.Location.X) / square_size;
                    int picY = (pictureBox.Location.Y) / square_size;
                    int picEndX = (pictureBox.Location.X + pictureBox.Size.Width - 1) / square_size;
                    int picEndY = (pictureBox.Location.Y + pictureBox.Size.Height - 1) / square_size;

                    if (DoRectanglesIntersect(picX, picY, picEndX, picEndY, character.location.getX() - 3, character.location.getY() - 3, character.location.getX() + 4, character.location.getY() + 4) &&
                        (pictureBox.Tag != null && (pictureBox.Tag.ToString() == "obstacle" || pictureBox.Tag.ToString() == "treasure")))
                    {
                        PictureBox pictureBoxCopy = new PictureBox();
                        pictureBoxCopy.Image = pictureBox.Image;
                        pictureBoxCopy.Location = pictureBox.Location;
                        pictureBoxCopy.Size = pictureBox.Size;
                        pictureBoxCopy.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBoxCopy.Tag = pictureBox.Tag;
                        pictureBoxCopy.BackColor = pictureBox.BackColor;
                        pictureBoxCopy.Visible = true;
                        pictureBoxCopy.Name = pictureBox.Name;
                        pictureBoxCopy.BringToFront();
                        pictureBoxCopy.Show();

                        kareli_kagit(picX, picY, picEndX, picEndY);
                        panel2.Controls.Add(pictureBoxCopy);
                        if (pictureBox.Tag.ToString() == "treasure" && !points.Contains(new Point(picX, picY)) && map[0, picY, picX] == 2)
                        {
                            points.Add(new Point(picX, picY));
                        }




                        panel2.Update();
                        //Debug.WriteLine("aaaaaaaaaax:{0} y:{1} EndX{2} EndY{3}", picX, picY, picEndX, picEndY);

                        pictureBox.Visible = true;
                        pictureBox.BringToFront();
                    }
                }
            }
            foreach (Point point in points)
            {
                Debug.WriteLine("sadnıkk x:{0} y:{1}", point.X, point.Y);
            }
            Debug.WriteLine("");

        }


        bool DoRectanglesIntersect(int xStart, int yStart, int xEnd, int yEnd, int xStart1, int yStart1, int xEnd1, int yEnd1)
        {
            // Birinci dikdörtgenin sağ üst ve sol alt koordinatları
            int rect1Left = xStart;
            int rect1Right = xEnd;
            int rect1Top = yStart;
            int rect1Bottom = yEnd;

            // İkinci dikdörtgenin sağ üst ve sol alt koordinatları
            int rect2Left = xStart1;
            int rect2Right = xEnd1;
            int rect2Top = yStart1;
            int rect2Bottom = yEnd1;

            // Dikdörtgenlerin birbirine göre konumlarını kontrol et
            if (rect1Left > rect2Right || rect2Left > rect1Right)
                return false; // Yatay eksende kesişmiyorlar

            if (rect1Top > rect2Bottom || rect2Top > rect1Bottom)
                return false; // Dikey eksende kesişmiyorlar

            return true; // Dikdörtgenler kesişiyor
        }

        void kareli_kagit(int x, int y, int end_x, int end_y)
        {
            //kareli kağıt çiz
            for (int i = y; i < end_y; i++)
            {
                for (int j = x; j < end_x; j++)
                {
                    if (map[1, i, j] == 1)
                    {
                        g.FillRectangle(new SolidBrush(Color.Red), j * square_size + 1, i * square_size + 1, square_size - 1, square_size - 1);

                    }
                    else if (square_count / 2 > j)
                    {
                        g.FillRectangle(new SolidBrush(Color.White), j * square_size + 1, i * square_size + 1, square_size - 1, square_size - 1);
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(Color.Green), j * square_size + 1, i * square_size + 1, square_size - 1, square_size - 1);
                    }
                    g.DrawRectangle(new Pen(Color.Black, 1), j * square_size, i * square_size, square_size, square_size);



                    //içini doldur eğer square_count /2 den büyükse yeşil küüçkse beyazla doldur

                }
            }

        }
        void temizle2()
        {
            //karaketerin kordintalarını al
            int startX = (Math.Max(0, character.location.getX() - 3));
            int startY = (Math.Max(0, character.location.getY() - 3));
            int endX = (Math.Min(square_count, character.location.getX() + 4));
            int endY = (Math.Min(square_count, character.location.getY() + 4));
            kareli_kagit(startX, startY, endX, endY);
        }


        public void matris_fog_clear(int[,,] map, Character character)
        {

            Debug.WriteLine("charrrrrrrrrx:{0} y:{1}", character.location.getX(), character.location.getY());
            int x = character.location.getX() - 3;
            int end_x = character.location.getX() + 4;
            int y = character.location.getY() - 3;
            int end_y = character.location.getY() + 4;
            //karakterin görüş alanı 0 olarak işaretlenir ama karakter köşedeyse 7x7lik kareyi dışarı taşmaması için kontrol yapılır
            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }
            if (end_x > map.GetLength(2))
            {
                end_x = map.GetLength(2);
            }
            if (end_y > map.GetLength(2))
            {
                end_y = map.GetLength(2);
            }


            for (int i = y; i < end_y; i++)
            {
                for (int j = x; j < end_x; j++)
                {
                    map[2, i, j] = 0;//0:obstacle and treasure 1:characterMove 2:fog
                }
            }
        }
        Boolean check_obstacle(int[,,] map, int start_x, int start_y, int end_x, int end_Y)
        {
            if (start_x < 0)
            {
                start_x = 0;
            }
            if (start_y < 0)
            {
                start_y = 0;
            }
            if (start_x > map.GetLength(2))
            {
                start_x = map.GetLength(2);
            }
            if (start_y > map.GetLength(2))
            {
                start_y = map.GetLength(2);
            }
            if (end_x < 0)
            {
                end_x = 0;
            }
            if (end_Y < 0)
            {
                end_Y = 0;
            }
            if (end_x > map.GetLength(2))
            {
                end_x = map.GetLength(2);
            }
            if (end_Y > map.GetLength(2))
            {
                end_Y = map.GetLength(2);
            }


            //eğer 7x7lik karede engel varsa true döner
            for (int i = start_y; i < end_Y; i++)
            {
                for (int j = start_x; j < end_x; j++)
                {
                    if (map[0, i, j] == 1 || map[0, i, j] == 2)
                    {
                        return true;
                    }
                }
            }
            return false;


        }
        Point bring_close_treasue()
        {
            Point point = new Point(-1, -1);
            int min = map.GetLength(2);
            foreach (Point p in points)
            {
                if (Math.Abs(p.X - character.location.getX()) + Math.Abs(p.Y - character.location.getY()) < min)
                {
                    min = Math.Abs(p.X - character.location.getX()) + Math.Abs(p.Y - character.location.getY());
                    point = p;
                }
            }
            if (point.X != -1)
            {
                Debug.WriteLine("hazine bulundu x:{0} y:{1}", point.X, point.Y);
                //pick_treasure(point.X, point.Y);

            }
            return point;



        }




    }


}

