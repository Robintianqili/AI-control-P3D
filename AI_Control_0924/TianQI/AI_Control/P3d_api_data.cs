using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Control
{
 //Here to define all datas AI should use to control a plane
    public class P3d_api_data
    {
        //define DEFINITIONS SET
        //Set_position is to set plane to an assigned scenario
        //and Freeze is to freeze the plane (similiar to Pause) 
        public enum DEFINITIONS
        {

            DEFINITION_SET_POSITION,
           
            DEFINITION_FREEZE,
            

        }
        //define three flags for freeze
        //Could use flags to customize the freeze mode
         public struct Struct_Freeze
        {
            public double lat_long_flag;
            public double altitdue_flag;
            public double attitude_flag;
           


        };

        //define the basic datas AI should access,
        //latitude, longitude, and altitude for plane's position
        //pitch, bank, and head for plane's attitude
        public struct Struct_Pos
        {
            
            public double latitude;
            public double longitude;
            public double altitude;
            public double pitch;
            public double bank;
            public double head;
            

        };


    }

    
}
