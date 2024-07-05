using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtonomHazineAvcisi
{
    public class ImageDir
    {
        private String[] summer_forest =
        {
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazOrman\\1.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazOrman\\2.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazOrman\\3.png"
        };
        private String[] winter_forest =
        {
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikOrman\\1.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikOrman\\2.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikOrman\\3.png"
        };
        private String[] summer_tree = 
        {
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazAgaclari\\birch_1.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazAgaclari\\birch_3.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazAgaclari\\fir_tree_1.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazAgaclari\\middle_lane_tree2.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazAgaclari\\middle_lane_tree3.png"
        };
        private String[] winter_tree = 
        {
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikAgaclar\\winter_conifer_tree_11.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikAgaclar\\winter_conifer_tree_7.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikAgaclar\\winter_tree_2.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikAgaclar\\winter_tree_4.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikAgaclar\\winter_tree_9.png"
        };
        private String[] summer_rocks = 
        {
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazKayalari\\cave_rock1.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazKayalari\\cave_rock2.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazKayalari\\cave_rock3.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazKayalari\\cave_rock4.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\yazKayalari\\cave_rock5.png"
        };
        private String[] winter_rocks =
        {
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikKayalar\\snowy_rock1.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikKayalar\\snowy_rock2.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikKayalar\\snowy_rock3.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikKayalar\\snowy_rock4.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kislikKayalar\\snowy_rock5.png"
        };
        private String[] summer_mountains =
        {
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\dag\\yaz.png"
        };
        private String[] winter_mountains =
        {
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\dag\\kis.png"
        };
        private String[] walls = 
        {
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\duvar\\1.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\duvar\\2.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\duvar\\3.png"
           ,"C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\duvar\\4.png"

        };
        private String[] bees =
        {
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\ari\\80x20arig.gif",
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\ari\\80x20ariw.gif"
        };
        private String[] birds = 
        {
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kuslar\\7 Bird\\20x120.gif",
            "C:\\Users\\melih\\source\\repos\\OtonomHazineAvcisi\\OtonomHazineAvcisi\\Resources\\photos\\prolab2.1\\kuslar\\7 Bird\\20x120w.gif"
        };
        public String[] get_summer_forest() { return summer_forest;}
        public String[] get_winter_forest() { return winter_forest;}
        public String[] get_summer_tree() { return summer_tree;}
        public String[] get_winter_tree() { return winter_tree;}
        public String[] get_summer_rocks() {  return summer_rocks; }
        public String[] get_winter_rocks() { return winter_rocks;}
        public String[] get_summer_mountains() { return summer_mountains; }
        public String[] get_winter_mountains() { return winter_mountains; }
        public String[] get_walls() {  return walls; }
        public String[] get_bees() {  return bees; }
        public String[] get_birds() {  return birds; }


    }
}
