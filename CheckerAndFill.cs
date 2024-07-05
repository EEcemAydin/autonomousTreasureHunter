using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtonomHazineAvcisi
{
    class CheckerAndFill
    {
        public Boolean check(int x, int y, int[,,] map, int last_x,int last_y)
        {
            
            int check_temp = 0;
            if (last_x > map.GetLength(2))
            {
                last_x = map.GetLength(2);
            }
            if (last_y > map.GetLength(2))
            {
                last_y = map.GetLength(2);
            }
            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }


            for (int i = y; i <last_y; i++)
            {
                for (int j = x; j < last_x; j++)
                {
                    if (map[0,i, j] == 1|| map[0, i, j] == 2)
                    {
                        check_temp = 1;
                        break;
                    }
                }
            }
            if (check_temp != 0 || (x <= (map.GetLength(2)/2)-1 && last_x>= (map.GetLength(2) / 2) - 1))
            {
                return false;
            }
            return true;
        }

        public void fill(int x, int y, int[,,] map,int last_x, int last_y)
        {
            if (last_x > map.GetLength(2))
            {
                last_x = map.GetLength(2);
            }
            if (last_y > map.GetLength(2))
            {
                last_y = map.GetLength(2);
            }
            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }
            for (int i = y;i < y + last_y; i++)
            {
                for (int j=x; j < x + last_x;j++)
                {
                    map[0,i, j] = 1;
                }
            }
        }
        public void fill2(int x, int y, int[,,] map, int last_x, int last_y)
        {
            for (int i = y; i < y + last_y; i++)
            {
                for (int j = x; j < x + last_x; j++)
                {
                    map[0,i, j] = 2;
                }
            }
        }
    }

}
