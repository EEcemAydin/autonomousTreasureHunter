using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtonomHazineAvcisi
{
    public class Location
    {
        private int _x;
        private int _y;
        public Location()//önceden doldurulmayan yerler için tanımladık
        {

        }

        public Location(int x, int y)
        {
            _x = x;
            _y = y;
        }


        


        public int getX()
        {
            return _x;
        }
        public int getY()
        {
            return _y;
        }
        public void setX(int x)
        {
            _x = x;
        }
        public void setY(int y)
        {
            _y = y;
        }

    }
}
