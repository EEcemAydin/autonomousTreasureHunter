using OtonomHazineAvcisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtonomHazineAvcisi
{
     public class Obstacle//engel sınfı
    {
        private int size_x;
        private int size_y;
        private String obstacle_type;
        private int loc_x;
        private int loc_y;
        private String image;
       
       
        public Obstacle(int size_x,int size_y, String obstacle_type,int loc_x,int loc_y,String image,int[,,]map)//CONSTRACTOR
        {
            this.size_x = size_x;
            this.size_y = size_y;
            this.obstacle_type = obstacle_type;
            this.loc_x = loc_x;
            this.loc_y = loc_y;
            this.image = image;

        }
        
        public int getSize_x()
        {
            return size_x;
        }
        public int getSize_y()
        {
            return size_y;
        }
        public String getObstacle_type()
        {
            return obstacle_type;
        }
        public String getImage()
        {
            return image;
        }
        public int getLoc_x()
        {
            return loc_x;
        }
        public int getLoc_y()
        {
            return loc_y;
        }
        
        public void setSize_x(int size_x)
        {
            this.size_x = size_x;
        }
        public void setSize_y(int size_y)
        {
            this.size_y = size_y;
        }
        public void setObstacle_type(String obstacle_type)
        {
            this.obstacle_type = obstacle_type;
        }
        public void setImage(String image)
        {
            this.image = image;
        }
        public void setLoc_x(int loc_x)
        {
            this.loc_x = loc_x;
        }
        public void setLoc_y(int loc_y)
        {
            this.loc_y = loc_y;
        }
        

    }
    public class Movement : Obstacle//hareket eden engeller
    {
        private int move_x;
        private int move_y;
        
        public Movement(int size_x,int size_y, String obstacle_type,int loc_x,int loc_y, String image, int move_x, int move_y,int[,,]map) : base(size_x,size_y, obstacle_type,loc_x,loc_y,image,map)
        {
            this.move_x = move_x;
            this.move_y = move_y;
        }
        public int getMove_x()
        {
            return move_x;
        }
        public int getMove_y()
        {
            return move_y;
        }
        public void setMove_x(int move_x)
        {
            this.move_x = move_x;
        }
        public void setMove_y(int move_y)
        {
            this.move_y = move_y;
        }

    }
    public class Motionless : Obstacle//hareket edemeyen engeller
    {
       
        public Motionless(int size_x,int size_y, String obstacle_type, int loc_x, int loc_y, String image, int[,,]map) : base(size_x,size_y, obstacle_type, loc_x, loc_y, image,map)
        {
            
        }
       
    }
    public class Trees : Motionless
    {
        public Trees(int size_x, int size_y, String obstacle_type, int loc_x, int loc_y, String image, int[,,] map) : base(size_x, size_y, obstacle_type, loc_x, loc_y, image, map)
        { 

        }
    }
    public class Rocks : Motionless
    {
        public Rocks(int size_x, int size_y, String obstacle_type, int loc_x, int loc_y, String image, int[,,] map) : base(size_x, size_y, obstacle_type, loc_x, loc_y, image, map)
        {

        }
    }
    public class Walls : Motionless
    {
        public Walls(int size_x, int size_y, String obstacle_type, int loc_x, int loc_y, String image, int[,,] map) : base(size_x, size_y, obstacle_type, loc_x, loc_y, image, map)
        {

        }
    }
    public class Mountains : Motionless
    {
        public Mountains(int size_x, int size_y, String obstacle_type, int loc_x, int loc_y, String image, int[,,] map) : base(size_x, size_y, obstacle_type, loc_x, loc_y, image, map)
        {

        }
    }
    public class Birds : Movement
    {
        public Birds(int size_x,int size_y,String obstacle_type,int loc_x, int loc_y,   String image,int move_x,int move_y, int[,,] map) :base(size_x,size_y,obstacle_type, loc_x, loc_y, image,move_x,move_y, map)
        {
            move_y = 5;
        }
    }
    public class Bees : Movement
    {
        public Bees(int size_x, int size_y, String obstacle_type, int loc_x, int loc_y, String image , int move_x, int move_y, int[,,] map) : base(size_x, size_y, obstacle_type, loc_x, loc_y, image, move_x, move_y, map)
        {
            move_x = 3;
        }
    }

   





}


