using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Control
{
    public static class Geo
    {

        public static double Rad(double degree_val)
        {
            return degree_val / 180 * Math.PI;
        }

        public static double Degree(double rad_val)
        {
            return rad_val * 180 / Math.PI;
        }


        public static double Getfoot(double high_in_meter)
        {
            return high_in_meter*3.2808;
        }

    }
}
