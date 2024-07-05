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

    public class Solve2
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
        int left_or_right = -1;//0 sol 1 sağ
        int up_or_down = -1;//0 yukarı 1 asağı
        int temp_adim = 0;
        List<Point> points = new List<Point>();





        private void StartTimer()
        {
            myTimer = new System.Windows.Forms.Timer();
            myTimer.Interval = 500; // 1000 milisaniye (1 saniye)
            myTimer.Tick += new EventHandler(Timer_Tick);
            myTimer.Start();
        }
        private void StopTimer()
        {
            myTimer.Stop();
        }

        // Timer'ın Tick olayı

        public void solveMap(int[,,] map, Character character, PictureBox char_picturebox, PictureBox fog_pictureBox, Panel panel1, Panel panel2)
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
            adımsayısı++;
            go();
            temizle2();




            //temizle1(map, panel1);

            if (adımsayısı == 200)
            {
                StopTimer();
            }




        }
        //karakterin bulunduğu yerden 7x7lik alanı kontrol et eğer engel veya hazine varsa pictureboxu görünür yap


        //karakterin gittiği kareleri panel1 de kırmızı ile işaretler



        void go_treasue()
        {

            if (check_treasure().X != -1 && check_treasure().Y != -1)
            {
                if (check_treasure().X < character.location.getX())
                {
                    if (check_go(0))
                    {
                        character.go_left(char_picturebox, map);
                        Debug.WriteLine("Left treasue");
                    }


                }
                else if (check_treasure().X > character.location.getX())
                {
                    if (check_go(1))
                    {

                        character.go_right(char_picturebox, map);
                        Debug.WriteLine("Right treasue");
                    }



                }
                else if (check_treasure().Y < character.location.getY())
                {
                    if (check_go(2))
                    {
                        character.go_up(char_picturebox, map);
                        Debug.WriteLine("Up treasue");
                    }



                }
                else if (check_treasure().Y > character.location.getY())
                {
                    if (check_go(3))
                    {
                        character.go_down(char_picturebox, map);
                        Debug.WriteLine("Down treasue");
                    }



                }
                else
                {
                    pick_treasure(check_treasure().X, check_treasure().Y);

                }

            }
        }
        void go()
        {
            if (check_treasure().X != -1)
            {
                go_treasue();
            }
            else
            {
                temp_adim++;
                int left_or_right_count = 0;
            left_or_right_choice:
                Debug.WriteLine("left_or_right");
                if (left_or_right == -1 && left_or_right_count < 2)
                {
                    left_or_right = random.Next(0, 2);
                    if (!check_go_direction(left_or_right))
                    {
                        left_or_right_count++;
                        goto left_or_right_choice;
                    }
                    up_or_down = -2;
                }
                else
                {

                    if (up_or_down == -1)
                    {
                    up_or_down_choice:
                        Debug.WriteLine("up_or_down");
                        up_or_down = random.Next(2, 4);
                        if (!check_go_direction(up_or_down))
                        {
                            goto up_or_down_choice;
                        }


                    }
                }

                if (temp_adim > 4 && left_or_right != -2)
                {
                    Debug.WriteLine("resetfor lor");
                    temp_adim = 0;
                    up_or_down = -1;
                    left_or_right = -2;
                }
                else if (temp_adim > 4 && up_or_down != -2)
                {
                    Debug.WriteLine("resetfor upordown");
                    temp_adim = 0;
                    up_or_down = -2;
                    left_or_right = -1;

                }
                else if (left_or_right == 0 && temp_adim <= 3)//left
                {
                    if (check_go(0))
                    {
                        character.go_left(char_picturebox, map);
                        Debug.WriteLine("Left");
                    }

                }
                else if (left_or_right == 1 && temp_adim <= 3)
                {
                    if (check_go(1))
                    {
                        character.go_right(char_picturebox, map);
                        Debug.WriteLine("Right");


                    }
                }
                else if (up_or_down == 2 && temp_adim <= 3)
                {
                    if (check_go(2))
                    {
                        character.go_up(char_picturebox, map);
                        Debug.WriteLine("Up");
                    }
                }
                else if (up_or_down == 3 && temp_adim <= 3)
                {
                    if (check_go(3))
                    {
                        character.go_down(char_picturebox, map);
                        Debug.WriteLine("Down");
                    }
                }



            }





        }







        Boolean check_go_direction(int direction)
        {
            switch (direction)
            {
                case 0://left
                    if (map[0, character.location.getY(), Math.Max(0, character.location.getX() - 1)] != 1 && map[0, character.location.getY(), Math.Max(0, character.location.getX() - 2)] != 1 && map[0, character.location.getY(), Math.Max(0, character.location.getX() - 3)] != 1)
                    {


                        if (check_treasure().X != -1 && check_treasure().Y != -1 && check_treasure().X - 1 == character.location.getX())
                        {

                            return false;
                        }


                        return true;
                    }



                    break;
                case 1://right
                    if (map[0, character.location.getY(), Math.Min(character.location.getX() + 1, 99)] != 1 && map[0, character.location.getY(), Math.Min(character.location.getX() + 2, 99)] != 1 && map[0, character.location.getY(), Math.Min(character.location.getX() + 3, 99)] != 1)
                    {

                        if (check_treasure().X != -1 && check_treasure().Y != -1 && check_treasure().X + 1 == character.location.getX())
                        {
                            return false;
                        }


                        return true;
                    }


                    break;//up
                case 2:
                    if (map[0, Math.Max(0, character.location.getY() - 1), character.location.getX()] != 1 && map[0, Math.Max(0, character.location.getY() - 2), character.location.getX()] != 1 && map[0, Math.Max(0, character.location.getY() - 3), character.location.getX()] != 1)
                    {
                        if (check_treasure().X != -1 && check_treasure().Y != -1 && check_treasure().Y - 1 == character.location.getY())
                        {
                            return false;
                        }

                        return true;
                    }


                    break;
                case 3://down
                    if (map[0, Math.Min(character.location.getY() + 1, 99), character.location.getX()] != 1 && map[0, Math.Min(character.location.getY() + 2, 99), character.location.getX()] != 1 && map[0, Math.Min(character.location.getY() + 3, 99), character.location.getX()] != 1)
                    {
                        if (check_treasure().X != -1 && check_treasure().Y != -1 && check_treasure().Y + 1 == character.location.getY())
                        {
                            return false;
                        }
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
        void random_go()
        {
            int random = this.random.Next(0, 4);
            if (character.location.getX() <= square_count && character.location.getY() <= square_count && character.location.getY() >= 0 && character.location.getX() >= 0)
            {
                if (random == 0)
                {
                    if (map[0, Math.Max(0, character.location.getY() - 1), character.location.getX()] == 0)
                    {
                        character.go_up(char_picturebox, map);
                    }

                }
                else if (random == 1)
                {
                    if (map[0, Math.Min(character.location.getY() + 1, 99), character.location.getX()] == 0)
                    {
                        character.go_down(char_picturebox, map);
                    }
                }
                else if (random == 2)
                {
                    if (map[0, character.location.getY(), Math.Max(0, character.location.getX() - 1)] == 0)
                    {
                        character.go_left(char_picturebox, map);
                    }
                }
                else if (random == 3)
                {
                    if (map[0, character.location.getY(), Math.Min(character.location.getX() + 1, 99)] == 0)
                    {
                        character.go_right(char_picturebox, map);
                    }
                }
            }
        }
        void escape()
        {


            int char_x = character.location.getX();
            int char_y = character.location.getY();
            if (character.location.getX() <= square_count && character.location.getY() <= square_count && character.location.getY() >= 0 && character.location.getX() >= 0)
            {
                if (char_x < 20)
                {
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                    character.go_right(char_picturebox, map);
                }
                else if (char_x > 80)
                {
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);
                    character.go_left(char_picturebox, map);

                }
                else if (char_y < 20)
                {
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                    character.go_down(char_picturebox, map);
                }
                else if (char_y > 80)
                {
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                }
                else
                {
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                    character.go_up(char_picturebox, map);
                }



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
        Point search_treasure_yakin()
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
        void pick_treasure(int x, int y)
        {
            //herhangi bir noktası gelen sandığı bul 4 tane 2 olmalı ve matristeki karelerini 3 yap 
            if (map[0, y, x] == 2)
            {

                //sandığın olduğu kareleri 3 yap
                for (int i = y; i < y + 2; i++)
                {
                    for (int j = x; j < x + 2; j++)
                    {
                        map[0, i, j] = 2;
                    }
                }


            }
            points.Clear();


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
            search_treasure_yakin();

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
        Point check_treasure()
        {

            //points listesindeki charactere en yakın olan hazineyi döner
            Point point = search_treasure_yakin();
            return point;


        }




    }

}


/*Bu projemizdeki amaç bize verilen isterler ve kısıtlar doğrultusunda bir otonom hazine avcısı yapmaktır.
 * Projemizi yaparken C# dili ve Windows Form Application kullanarak projemizi gerçekleştirdik.
 * Projede 3 boyutlu bir matris kullanarak karakterin hareketlerini ve etrafındaki engelleri ve hazineyi saklamaktayız.
 * polimorfizm,encapsulation,inheritance gibi OOP prensiplerini kullanarak projemizi gerçekleştirdik.
 * 
 * 
 * 
 * if (panel1.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Location.X == x && pb.Location.Y == y) != null)
                {
                    panel1.Controls.OfType<PictureBox>().FirstOrDefault(pb => pb.Location.X == x && pb.Location.Y == y).BringToFront();
                }
*/




/*void visible_picturebox()
        {
            if(check_obstacle(map, character.location.getX()-3, character.location.getY()-3, character.location.getX() + 4, character.location.getY() + 4))
            {
                foreach(PictureBox pictureBox in panel1.Controls.OfType<PictureBox>())
                {
                    Debug.WriteLine("x:{0} y:{1} EndX{2} EndY{3}", pictureBox.Location.X/square_size, pictureBox.Location.Y/ square_size,(pictureBox.Location.X+pictureBox.Size.Width)/square_size, (pictureBox.Location.Y + pictureBox.Size.Height) / square_size);
                    if ((pictureBox.Location.X / square_size >= character.location.getX() - 3 || (pictureBox.Location.X + pictureBox.Size.Width) / square_size <= character.location.getX() + 4) || (pictureBox.Location.Y / square_size >= character.location.getY() - 3 &&(pictureBox.Location.Y + pictureBox.Size.Height) / square_size <= character.location.getY() + 4))
                    {
                        if (pictureBox.Tag != null)
                        {
                            if (pictureBox.Tag.ToString() == "obstacle" || pictureBox.Tag.ToString() == "treasure")
                            {
                                Debug.WriteLine("aaaaaaaaaax:{0} y:{1} EndX{2} EndY{3}", pictureBox.Location.X / square_size, pictureBox.Location.Y / square_size, (pictureBox.Location.X + pictureBox.Size.Width) / square_size, (pictureBox.Location.Y + pictureBox.Size.Height) / square_size);


                                pictureBox.Visible = true;
                                pictureBox.BringToFront();
                            }
                        }
                    }
                }
            }
        }
*/





/*
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * void visible_picturebox()
        {
            if (check_obstacle(map, character.location.getX() - 3, character.location.getY() - 3, character.location.getX() + 4, character.location.getY() + 4))
            {
                foreach (PictureBox pictureBox in panel1.Controls.OfType<PictureBox>())
                {
                    Debug.WriteLine("x:{0} y:{1} EndX{2} EndY{3}", pictureBox.Location.X / square_size, pictureBox.Location.Y / square_size, (pictureBox.Location.X + pictureBox.Size.Width) / square_size, (pictureBox.Location.Y + pictureBox.Size.Height) / square_size);
                    if (DoRectanglesIntersect(pictureBox.Location.X / square_size, pictureBox.Location.Y / square_size, (pictureBox.Location.X + pictureBox.Size.Width) / square_size, (pictureBox.Location.Y + pictureBox.Size.Height) / square_size, character.location.getX() - 3, character.location.getY() - 3, character.location.getX() + 4, character.location.getY() + 4)
)
                    {
                        if (pictureBox.Tag != null)
                        {
                            if (pictureBox.Tag.ToString() == "obstacle" || pictureBox.Tag.ToString() == "treasure")
                            {
                                Debug.WriteLine("aaaaaaaaaax:{0} y:{1} EndX:{2} EndY:{3}", pictureBox.Location.X / square_size, pictureBox.Location.Y / square_size, (pictureBox.Location.X + pictureBox.Size.Width) / square_size, (pictureBox.Location.Y + pictureBox.Size.Height) / square_size);


                                // Panel2'ye kopyalanan PictureBox'ı ekleyin

                                pictureBox.Visible = true;
                                pictureBox.BringToFront();

                            }
                        }
                    }
                }
            }
        }
 */