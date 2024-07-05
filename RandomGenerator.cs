using OtonomHazineAvcisi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

public class RandomGenerator
{


    public List<Obstacle> ObstaclesGenerator(int x, int y, int[,,] map)
    {
        ImageDir imageDir = new ImageDir();
        System.Console.WriteLine("ObstaclesGenerator");
        List<PictureBox> PictureBoxlist = new List<PictureBox>();
        CheckerAndFill checker_and_fill = new CheckerAndFill();
        /*her hareketsiz nesneden en az iki tane olup olmadığınıu kontrol etmek için nesneleri kendi içinde listeledekik
        ;sonrasında hareketli ve hareketsiz olarak listeledik (hareketlilere kuş ve arı koyduk,hareketsiz olanlara diğer
        nesneleri koyduk)en sonunda hareketli ve hareketsizlerş tüm engeller listesine atatık.*/
        List<Obstacle> obstacles = new List<Obstacle>();//bütün engellerin olduğu liste
        String[] treeName = { "Mese", "Çam", "Meşe Ormanı", "Çam Ormanı" };//2*2 lik mese,3*3 lük çam,4*4 lük meşe ormanı 5*5 Çam ormanı olacak mese 0. eleman çam 1. eleman meşe 2. eleman çam omramı 3.eleman meşe ormanı bu yüzden -2 mantığı kullandık
        String[] rockName = { "Cakil", "Kaya" };//2*2 lik çakıl,3*3 lük kaya olacak çakıl 0. eleman kaya 1. eleman bu yüzden -2 mantığı kullandık


        Random random = new Random();
        int motionless_count = 0;
        motionless_count = random.Next(20, 40);//hareketsiz engel sayısı random belirlendi
        
        int motionless_count_temp = motionless_count;
        int randomSize = 0;

        randomSize = random.Next(2, 6);//engellerin boyutları random belirlendi
    back_to_random1:
        int random_x = random.Next(0, x - randomSize);
        int random_y = random.Next(0, y - randomSize);
        Boolean control_size = checker_and_fill.check(random_x, random_y, map, random_x + randomSize, random_y + randomSize);
        if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık.
        {
            goto back_to_random1;
        }
        String image1 = "";
        if (random_x >= x/2)
        {
            image1 = imageDir.get_summer_tree()[random.Next(0, imageDir.get_summer_tree().Length)];
            if(randomSize>3) { image1 = imageDir.get_summer_forest()[random.Next(0, imageDir.get_summer_forest().Length)]; }
            
        }
        else
        {
            image1 = imageDir.get_winter_tree()[random.Next(0, imageDir.get_winter_tree().Length)];
            if (randomSize > 3) { image1 = imageDir.get_winter_forest()[random.Next(0, imageDir.get_winter_forest().Length)]; }
        }

        Trees tree1 = new Trees(randomSize, randomSize, treeName[randomSize - 2], random_x, random_y, image1, map);//en az her engelden 2 tane oluşması için önce bu 2 nesneleri oluşturduktr
        checker_and_fill.fill(random_x, random_y, map, randomSize, randomSize);
        randomSize = random.Next(2, 6);//engellerin boyutları random belirlendi
    back_to_random2:
        random_x = random.Next(0, x - randomSize);
        random_y = random.Next(0, y - randomSize);
        control_size = checker_and_fill.check(random_x, random_y, map, random_x + randomSize, random_y + randomSize);
        if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
        {
            goto back_to_random2;
        }
        if (random_x >= x / 2)
        {
            image1 = imageDir.get_summer_tree()[random.Next(0, imageDir.get_summer_tree().Length)];
            if (randomSize > 3) { image1 = imageDir.get_summer_forest()[random.Next(0, imageDir.get_summer_forest().Length)]; }

        }
        else
        {
            image1 = imageDir.get_winter_tree()[random.Next(0, imageDir.get_winter_tree().Length)];
            if (randomSize > 3) { image1 = imageDir.get_winter_forest()[random.Next(0, imageDir.get_winter_forest().Length)]; }

        }
        Trees tree2 = new Trees(randomSize, randomSize, treeName[randomSize - 2], random_x, random_y, image1, map);
        checker_and_fill.fill(random_x, random_y, map, randomSize, randomSize);

        obstacles.Add(tree1);
        obstacles.Add(tree2);

        randomSize = random.Next(2, 4);//engellerin boyutları random belirlendi
    back_to_random3:
        random_x = random.Next(0, x - randomSize);
        random_y = random.Next(0, y - randomSize);
        control_size = checker_and_fill.check(random_x, random_y, map, random_x + randomSize, random_y + randomSize);
        if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
        {
            goto back_to_random3;
        }
        if (random_x >= x / 2)
        {
            image1 = imageDir.get_summer_rocks()[random.Next(0, imageDir.get_summer_rocks().Length)];
        }
        else
        {
            image1 = imageDir.get_winter_rocks()[random.Next(0, imageDir.get_winter_rocks().Length)];
        }
        Rocks rocks1 = new Rocks(randomSize, randomSize, rockName[randomSize - 2], random_x, random_y, image1, map);//en az her engelden 2 tane oluşması için önce bu 2 nesneleri oluşturduktr
        checker_and_fill.fill(random_x, random_y, map, randomSize, randomSize);

        randomSize = random.Next(2, 4);//engellerin boyutları random belirlendi
    back_to_random4:
        random_x = random.Next(0, x - randomSize);
        random_y = random.Next(0, y - randomSize);
        control_size = checker_and_fill.check(random_x, random_y, map, random_x + randomSize, random_y + randomSize);
        if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
        {
            goto back_to_random4;
        }
        if (random_x >= x / 2)
        {
            image1 = imageDir.get_summer_rocks()[random.Next(0, imageDir.get_summer_rocks().Length)];
        }
        else
        {
            image1 = imageDir.get_winter_rocks()[random.Next(0, imageDir.get_winter_rocks().Length)];
        }

        Rocks rocks2 = new Rocks(randomSize, randomSize, rockName[randomSize - 2], random_x, random_y, image1, map);
        checker_and_fill.fill(random_x, random_y, map, randomSize, randomSize);

        obstacles.Add(rocks1);
        obstacles.Add(rocks2);
    back_to_random5:
        random_x = random.Next(0, x - 10);
        random_y = random.Next(0, y - 1);
        Debug.WriteLine("random_x: {0} random_y: {1}walls1", random_x, random_y);
        control_size = checker_and_fill.check(random_x, random_y, map, random_x + 10, random_y + 1);
        if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
        {
            goto back_to_random5;
        }
        image1 = imageDir.get_walls()[random.Next(0, imageDir.get_walls().Length)];
        Walls walls1 = new Walls(10, 1, "duvar", random_x, random_y, image1, map);//en az her engelden 2 tane oluşması için önce bu 2 nesneleri oluşturduktr
        checker_and_fill.fill(random_x, random_y, map, 10, 1);

    back_to_random6:
        random_x = random.Next(0, x - 11);
        random_y = random.Next(0, y - 1);
        Debug.WriteLine("random_x: {0} random_y: {1}walls2", random_x, random_y);
        control_size = checker_and_fill.check(random_x, random_y, map, random_x + 10, random_y + 1);
        if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
        {
            goto back_to_random6;
        }
        image1 = imageDir.get_walls()[random.Next(0, imageDir.get_walls().Length)];

        Walls walls2 = new Walls(10, 1, "duvar", random_x, random_y, image1, map);
        checker_and_fill.fill(random_x, random_y, map, 10, 1);

        obstacles.Add(walls1);
        obstacles.Add(walls2);
    back_to_random7:
        random_x = random.Next(0, x - 15);
        random_y = random.Next(0, y - 15);
        control_size = checker_and_fill.check(random_x, random_y, map, random_x + 15, random_y + 15);
        if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
        {
            goto back_to_random7;
        }
        if (random_x >= x / 2)
        {
            image1 = imageDir.get_summer_mountains()[random.Next(0, imageDir.get_summer_mountains().Length)];
        }
        else
        {
            image1 = imageDir.get_winter_mountains()[random.Next(0, imageDir.get_winter_mountains().Length)];
        }

        Mountains mountain1 = new Mountains(15, 15, "dağ", random_x, random_y, image1, map);//en az her engelden 2 tane oluşması için önce bu 2 nesneleri oluşturduktr
        checker_and_fill.fill(random_x, random_y, map, 15, 15);

    back_to_random8:
        random_x = random.Next(0, x - 15);
        random_y = random.Next(0, y - 15);
        control_size = checker_and_fill.check(random_x, random_y, map, random_x + 15, random_y + 15);
        if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
        {
            goto back_to_random8;
        }


        if (random_x >= x / 2)
        {
            image1 = imageDir.get_summer_mountains()[random.Next(0, imageDir.get_summer_mountains().Length)];
        }
        else
        {
            image1 = imageDir.get_winter_mountains()[random.Next(0, imageDir.get_winter_mountains().Length)];
        }

        Mountains mountain2 = new Mountains(15, 15, "dağ", random_x, random_y, image1, map);
        checker_and_fill.fill(random_x, random_y, map, 15, 15);

        obstacles.Add(mountain1);
        obstacles.Add(mountain2);
        motionless_count_temp = motionless_count - 7;

        for (int i = 0; i < motionless_count_temp; i++)
        {
            int choice = random.Next(0, 4);
            if (choice == 0)//ağaç 
            {
                randomSize = random.Next(2, 6);//engellerin boyutları random belirlendi
            back_to_random9:
                random_x = random.Next(0, x - randomSize);
                random_y = random.Next(0, y - randomSize);
                control_size = checker_and_fill.check(random_x, random_y, map, random_x + randomSize, random_y + randomSize);
                if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
                {
                    goto back_to_random9;
                }
                if (random_x >= x / 2)
                {
                    image1 = imageDir.get_summer_tree()[random.Next(0, imageDir.get_summer_tree().Length)];
                    if (randomSize > 3) { image1 = imageDir.get_summer_forest()[random.Next(0, imageDir.get_summer_forest().Length)]; }
                }
                else
                {
                    image1 = imageDir.get_winter_tree()[random.Next(0, imageDir.get_winter_tree().Length)];
                    if (randomSize > 3) { image1 = imageDir.get_winter_forest()[random.Next(0, imageDir.get_winter_forest().Length)]; }

                }
                Trees tree = new Trees(randomSize, randomSize, treeName[randomSize - 2], random_x, random_y, image1, map);
                checker_and_fill.fill(random_x, random_y, map, randomSize, randomSize);

                obstacles.Add(tree);
            }
            else if (choice == 1)//kaya
            {
                randomSize = random.Next(2, 4);//engellerin boyutları random belirlendi
            back_to_random10:
                random_x = random.Next(0, x - randomSize);
                random_y = random.Next(0, y - randomSize);
                control_size = checker_and_fill.check(random_x, random_y, map, random_x + randomSize, random_y + randomSize);
                if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
                {
                    goto back_to_random10;
                }
                if (random_x >= x / 2)
                {
                    image1 = imageDir.get_summer_rocks()[random.Next(0, imageDir.get_summer_rocks().Length)];
                }
                else
                {
                    image1 = imageDir.get_winter_rocks()[random.Next(0, imageDir.get_winter_rocks().Length)];
                }
                Rocks rocks = new Rocks(randomSize, randomSize, rockName[randomSize - 2], random_x, random_y, image1, map);
                checker_and_fill.fill(random_x, random_y, map, randomSize, randomSize);

                obstacles.Add(rocks);
            }
            else if (choice == 2)//duvar
            {
            back_to_random11:
                random_x = random.Next(0, x - 11);
                random_y = random.Next(0, y - 2);
                control_size = checker_and_fill.check(random_x, random_y, map, random_x + 11, random_y + 2);
                if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
                {
                    goto back_to_random11;
                }
                image1 = imageDir.get_walls()[random.Next(0, imageDir.get_walls().Length)];

                Walls walls = new Walls(10, 1, "duvar", random_x, random_y, image1, map);
                checker_and_fill.fill(random_x, random_y, map, 10, 1);

                obstacles.Add(walls);
            }
            else if (choice == 3)//dağ
            {
            back_to_random12:
                random_x = random.Next(0, x - 15);
                random_y = random.Next(0, y - 15);
                control_size = checker_and_fill.check(random_x, random_y, map, random_x + 15, random_y + 15);
                if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
                {
                    goto back_to_random12;
                }
                if (random_x >= x / 2)
                {
                    image1 = imageDir.get_summer_mountains()[random.Next(0, imageDir.get_summer_mountains().Length)];
                }
                else
                {
                    image1 = imageDir.get_winter_mountains()[random.Next(0, imageDir.get_winter_mountains().Length)];
                }
                Mountains mountain = new Mountains(15, 15, "dağ", random_x, random_y, image1, map);

                checker_and_fill.fill(random_x, random_y, map, 15, 15);

                obstacles.Add(mountain);
            }

        }

        for (int i = 0; i < 3; i++)
        {
            int choice = random.Next(0, 2);
            if (choice == 0)//kuş// yukarı aşagıya
            {
            back_to_random13:
                random_x = random.Next(0, x - 2);
                random_y = random.Next(0, y - 12);
                control_size = checker_and_fill.check(random_x, random_y, map, random_x + 2, random_y + 12);
                if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
                {
                    goto back_to_random13;
                }
                if(random_x >= x / 2)
                {
                    image1 = imageDir.get_birds()[0];
                }
                else
                {
                    image1 = imageDir.get_birds()[1];
                }

                Birds bird = new Birds(2, 12, "kuş", random_x, random_y, image1, 0, 5, map);
                checker_and_fill.fill(random_x, random_y, map, 2, 12);


                obstacles.Add(bird);
            }
            else if (choice == 1)//arı
            {
            back_to_random14:
                random_x = random.Next(0, x - 8);
                random_y = random.Next(0, y - 2);
                control_size = checker_and_fill.check(random_x, random_y, map, random_x + 8, random_y + 2);
                if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
                {
                    goto back_to_random14;
                }
                if (random_x >= x / 2)
                {
                    image1= imageDir.get_bees()[0];
                }
                else
                {
                    image1 = imageDir.get_bees()[1];
                }

                Bees bee = new Bees(8, 2, "arı", random_x, random_y, image1, 3, 0, map);
                checker_and_fill.fill(random_x, random_y, map, 8, 2);

                obstacles.Add(bee);
            }
        }








        return obstacles;
    }
    public List<Treasue> TreasueGenerator(int x, int y, int[,,] map)
    {
        Random random = new Random();
        CheckerAndFill checker_and_fill = new CheckerAndFill();
        List<Treasue> Treasues = new List<Treasue>();
        int chest_Altin_id = 0;
        int chest_Gumus_id = 0;
        int chest_Bakir_id = 0;
        int chest_Zumrut_id = 0;
        int treasue_count = 0;
        treasue_count = random.Next(30 ,50);//hazine sayısı random belirlendi

        for (int i = 0; i < 5; i++) //0 bakır
        {
        back_to_random15:
            int random_sx = random.Next(0, x - 2);
            int random_sy = random.Next(0, y - 2);
            Boolean control_size = checker_and_fill.check(random_sx, random_sy, map, random_sx + 2, random_sy + 2);
            if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
            {
                goto back_to_random15;
            }
            String image1 = "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\sandikler\\4.png";
            copper_chest copper_Chest = new copper_chest(random_sx, random_sy, 3, chest_Bakir_id, image1);
            checker_and_fill.fill2(random_sx, random_sy, map, 2, 2);
            chest_Bakir_id++;
            Treasues.Add(copper_Chest);


        }
        for (int i = 0; i < 5; i++) //1 gümüş
        {
        back_to_random16:
            int random_sx = random.Next(0, x - 2);
            int random_sy = random.Next(0, y - 2);
            Boolean control_size = checker_and_fill.check(random_sx, random_sy, map, random_sx + 2, random_sy + 2);
            if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
            {
                goto back_to_random16;
            }
            String image1 = "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\sandikler\\2.png";
            silver_chest silver_Chest = new silver_chest(random_sx, random_sy, 2, chest_Gumus_id, image1);
            checker_and_fill.fill2(random_sx, random_sy, map, 2, 2);
            chest_Gumus_id++;
            Treasues.Add(silver_Chest);


        }
        for (int i = 0; i < 5; i++) //2 altın
        {
        back_to_random17:
            int random_sx = random.Next(0, x - 2);
            int random_sy = random.Next(0, y - 2);
            Boolean control_size = checker_and_fill.check(random_sx, random_sy, map, random_sx + 2, random_sy + 2);
            if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
            {
                goto back_to_random17;
            }
            String image1 = "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\sandikler\\5.png";
            gold_chest gold_Chest = new gold_chest(random_sx, random_sy, 1, chest_Altin_id, image1);
            checker_and_fill.fill2(random_sx, random_sy, map, 2, 2);
            chest_Altin_id++;
            Treasues.Add(gold_Chest);


        }
        for (int i = 0; i < 5; i++) //3 zümrüt
        {
        back_to_random18:
            int random_sx = random.Next(0, x - 2);
            int random_sy = random.Next(0, y - 2);
            Boolean control_size = checker_and_fill.check(random_sx, random_sy, map, random_sx + 2, random_sy + 2);
            if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
            {
                goto back_to_random18;
            }
            String image1 = "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\sandikler\\1.png";
            emerald_chest emerald_Chest = new emerald_chest(random_sx, random_sy, 0, chest_Zumrut_id, image1);
            checker_and_fill.fill2(random_sx, random_sy, map, 2, 2);
            chest_Zumrut_id++;
            Treasues.Add(emerald_Chest);


        }
        for (int i = 0; i < treasue_count - 20; i++)
        {
            int choice = random.Next(0, 4);
            int random_sx = 0;
            int random_sy = 0;
            String image1 = "";
            Boolean control_size = false;
            switch (choice)
            {
                case 0:
                back_to_random19:
                    random_sx = random.Next(0, x - 2);
                    random_sy = random.Next(0, y - 2);
                    control_size = checker_and_fill.check(random_sx, random_sy, map, random_sx + 2, random_sy + 2);
                    if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
                    {
                        goto back_to_random19;
                    }
                    image1 = "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\sandikler\\4.png";
                    copper_chest copper_Chest = new copper_chest(random_sx, random_sy, 0, chest_Bakir_id, image1);
                    checker_and_fill.fill2(random_sx, random_sy, map, 2, 2);
                    chest_Bakir_id++;
                    Treasues.Add(copper_Chest);
                    break;
                case 1:
                back_to_random20:
                    random_sx = random.Next(0, x - 2);
                    random_sy = random.Next(0, y - 2);
                    control_size = checker_and_fill.check(random_sx, random_sy, map, random_sx + 2, random_sy + 2);
                    if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
                    {
                        goto back_to_random20;
                    }
                    image1 = "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\sandikler\\2.png";
                    silver_chest silver_Chest = new silver_chest(random_sx, random_sy, 1, chest_Gumus_id, image1);
                    checker_and_fill.fill2(random_sx, random_sy, map, 2, 2);
                    chest_Gumus_id++;
                    Treasues.Add(silver_Chest);
                    break;

                case 2:
                back_to_random21:
                    random_sx = random.Next(0, x - 2);
                    random_sy = random.Next(0, y - 2);
                    control_size = checker_and_fill.check(random_sx, random_sy, map, random_sx + 2, random_sy + 2);
                    if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
                    {
                        goto back_to_random21;
                    }
                    image1 = "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\sandikler\\5.png";
                    gold_chest gold_Chest = new gold_chest(random_sx, random_sy, 2, chest_Altin_id, image1);
                    checker_and_fill.fill2(random_sx, random_sy, map, 2, 2);
                    chest_Altin_id++;
                    Treasues.Add(gold_Chest);
                    break;

                case 3:
                back_to_random22:
                    random_sx = random.Next(0, x - 2);
                    random_sy = random.Next(0, y - 2);
                    control_size = checker_and_fill.check(random_sx, random_sy, map, random_sx + 2, random_sy + 2);
                    if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
                    {
                        goto back_to_random22;
                    }
                    image1 = "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\sandikler\\1.png";
                    emerald_chest emerald_Chest = new emerald_chest(random_sx, random_sy, 3, chest_Zumrut_id, image1);
                    checker_and_fill.fill2(random_sx, random_sy, map, 2, 2);
                    chest_Zumrut_id++;
                    Treasues.Add(emerald_Chest);
                    break;
                default:
                    break;
            }


        }





        return Treasues;
    }
    public Character CharacterGenerator(int x, int y, int[,,] map, int character_choice)
    {
        Random random = new Random();
        CheckerAndFill checker_and_fill = new CheckerAndFill();
        List<Character> Characters = new List<Character>();
        Character Character_choiced = new Character();
        String image1 = "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\karakterler\\1\\ezgif.com-animated-gif-maker.gif";
        Character character1 = new Character(1, "CWENE", -1, -1, image1);
        Characters.Add(character1);
        String image2 = "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\karakterler\\2\\Idle.gif";
        Character character2 = new Character(2, "TARKM", -1, -1, image2);
        Characters.Add(character2);
        String image3 = "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\karakterler\\9\\idle.gif";
        Character character3 = new Character(3, "ARTMS", -1, -1, image3);
        Characters.Add(character3);
        Character character4 = new Character(4, "LMKDC", -1, -1, image1);
        Characters.Add(character4);
        Character character5 = new Character(5, "TUPRS", -1, -1, image1);
        Characters.Add(character5);

        foreach (Character character in Characters)
        {
            if (character.getID()==character_choice)
            {
            back_to_random23:
                int random_x = random.Next(0, x);
                int random_y = random.Next(0, y);
                Boolean control_size = checker_and_fill.check(random_x, random_y, map, random_x +2, random_y + 2);
                if (!control_size)// engellerin aynı karelerde üst üste gelmesini önlemek amacıyla yaptık
                {
                    goto back_to_random23;
                }
                character.location.setX(random_x);
                character.location.setY(random_y);
                Character_choiced = character;
                break;
            }
        }
        return Character_choiced;
    }


}
