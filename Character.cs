using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtonomHazineAvcisi
{
    public class Character
    {
        private int ID;
        private string Name;
        public Location location = new Location();

        private String image;
        public Character(int ID, string Name, int Loc_x, int Loc_y,String image)
        {
            this.ID = ID;
            this.Name = Name;
            location.setX(Loc_x);
            location.setX(Loc_y);
            this.image = image;
        }
        public Character()
        {
            this.ID = 0;
            this.Name = "";
            location.setX(-1);
            location.setY(-1);
            this.image = "";
        }

        public int getID()
        {
            return ID;
        }
        public string getName()
        {
            return Name;
        }
       
        
        public String getImage()
        {
            return image;
        }
        public void setID(int ID)
        {
            this.ID = ID;
        }
        public void setName(string Name)
        {
            this.Name = Name;
        }
       
        public void setImage(String image)
        {
            this.image = image;
        }

        public void go_up(PictureBox char_picturebox, int[,,] map)
        {
            //gidebilir mi kontrol et
            if (location.getY() > 0)
            {
                map[1, location.getY(), location.getX()] = 1;
                location.setY(location.getY() - 1);
                int size = char_picturebox.ClientSize.Width;
                char_picturebox.Location = new Point(location.getX() * size, location.getY() * size);
                char_picturebox.Refresh();
                map[1, location.getY(), location.getX()] = 1;
            }

        }
        public void go_down(PictureBox char_picturebox, int[,,] map)
        {//gidebilir mi kontrol et
            if (location.getY() < map.GetLength(2) - 1)
            {
                map[1, location.getY(), location.getX()] = 1;
                location.setY(location.getY() + 1);
                int size = char_picturebox.ClientSize.Width;
                char_picturebox.Location = new Point(location.getX() * size, location.getY() * size);
                char_picturebox.Refresh();
                map[1, location.getY(), location.getX()] = 1;
            }
        }
        public void go_left(PictureBox char_picturebox,int[,,] map)
        {
            //gidebilir mi kontrol et
            if (location.getX() > 0)
            {
                map[1, location.getY(), location.getX()] = 1;
                location.setX(location.getX() - 1);
                int size = char_picturebox.ClientSize.Width;
                char_picturebox.Location = new Point(location.getX() * size, location.getY() * size);
                char_picturebox.Refresh();
                map[1, location.getY(), location.getX()] = 1;
            }
            
        }
        public void go_right(PictureBox char_picturebox, int[,,] map)
        {
            //gidebilir mi kontrol et
            if (location.getX() < map.GetLength(1) - 1)
            {
                map[1, location.getY(), location.getX()] = 1;
                location.setX(location.getX() + 1);
                int size = char_picturebox.ClientSize.Width;
                char_picturebox.Location = new Point(location.getX() * size, location.getY() * size);
                char_picturebox.Refresh();
                map[1, location.getY(), location.getX()] = 1;
            }
            
        }



    }
}