using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtonomHazineAvcisi
{
    public class Treasue
    {
        private int chest_x;
        private int chest_y;
        private int tur;//önem sırası için bu sandıklara numara vermek istedik
        private int chest_ID;
        private String image;
        public Treasue(int chest_x, int chest_y, int tur, int chest_ID, String image)
        {
            this.chest_x = chest_x;
            this.chest_y = chest_y;
            this.tur = tur;
            this.chest_ID = chest_ID;
            this.image = image;
        }
        public int get_chest_x()
        {
            return chest_x;
        }
        public int get_chest_y()
        {
            return chest_y;
        }
        public int get_tur()
        {
            return tur;
        }
        public int get_chest_ID()
        {
            return chest_ID;
        }
        public String get_image()
        {
            return image;
        }
        public void set_chest_x(int chest_x)
        {
            this.chest_x = chest_x;
        }
        public void set_chest_y(int chest_y)
        {
            this.chest_y = chest_y;
        }
        public void set_tur(int no)
        {
            this.tur = tur;
        }
        public void set_chest_ID(int chest_ID)
        {
            this.chest_ID = chest_ID;
        }
        public void set_image(String image)
        {
            this.image = image;
        }

    }
    public class gold_chest:Treasue//altın sandık
    {
        public gold_chest(int chest_x, int chest_y, int no, int chest_ID, String image) : base(chest_x, chest_y, no, chest_ID,image)
        {
        }

    }
    public class silver_chest:Treasue//gümüs sandık
    {
        public silver_chest(int chest_x, int chest_y, int no, int chest_ID, String image): base(chest_x, chest_y, no, chest_ID,image)
        {
            
        }

    }
    public class emerald_chest:Treasue//zümrüt sandık
    {
        public emerald_chest(int chest_x, int chest_y, int no, int chest_ID, String image): base(chest_x, chest_y, no, chest_ID,image)
        {
           
        }

    }
    public class copper_chest:Treasue //bakır sandık
    { 
        public copper_chest(int chest_x, int chest_y, int no, int chest_ID, String image) : base (chest_x, chest_y, no, chest_ID,image)
        {
            
        }

    }
    




}
