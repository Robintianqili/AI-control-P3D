using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AI_Control
{

    public class JsbSIm_Output_Data
    {
        // aircraft status
        public double Time;
        public double h_sl_meters;
        public double vtrue_fps;
        public double lat_gc_deg;
        public double long_gc_deg;
        public double roll_rad;
        public double pitch_rad;
        public double heading_true_rad;
        public double v_north_fps;
        public double v_east_fps;
        public double v_down_fps;
        public double mach;
        public double phidot_rad_sec;
        public double thetadot_rad_sec;
        public double psidot_rad_sec;
        public double Nz;
        public double total_fuel_lbs;

    }

    public class Jsbsim_Input_Data
    {
        public double throttle_cmd_norm;
        public double elevator_cmd_norm;
        public double aileron_cmd_norm;
        public double rudder_cmd_norm;

        
    }


    public class JSBsim_Interface
    {
        private UdpClient Udp_Jsbsim;   // listen jsbsim output  udp

        Thread Listen_jsb_Udp_TH;         // listen udp thread


        P3d_Api p3d_api;

        private int Mode_type = -1;

        long jump_heart = 0;

        public JSBsim_Interface()
        {

            p3d_api = new P3d_Api();


            //initial udp port

            int jsbsim_port = 1138;  // define in socket.xml
            Udp_Jsbsim = new UdpClient(jsbsim_port); // 

           
            // listen thread
            Listen_jsb_Udp_TH = new Thread(new ThreadStart(Listen_JSBSimSocket))
            {
                IsBackground = true
            };
            Listen_jsb_Udp_TH.Start();


          


        }


        public void Set_Manu_mode(int mode_set)
        {
            this.Mode_type = mode_set;
        }

       


        public  void Start_jsb(P3d_Api p3d)
        {
            
            // jsbsim if exit, Kill
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
            foreach (System.Diagnostics.Process process in processList)
            {
                if (process.ProcessName.IndexOf("JSBSim") >= 0)
                {


                    process.Kill();

                }
            }

            // run args
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "jsbsim.exe";

            


            info.Arguments = "socket.xml c172_test.xml --realtime";

           

            info.UseShellExecute = true;

            info.WindowStyle = ProcessWindowStyle.Minimized;
            Process prc = Process.Start(info);



            p3d_api.simconnect = p3d.simconnect;
            jump_heart = 0;
        }


        private void Listen_JSBSimSocket()
        {
            IPEndPoint ipEndpoint = new IPEndPoint(IPAddress.Any, 0);

            byte[] receiveData;

            byte[] toJsb_sendBytes;

            string rec_str = "";
            string[] rec_element;


            JsbSIm_Output_Data jsbsim_output_Data = new JsbSIm_Output_Data();
            Jsbsim_Input_Data jsbsim_input_Data = new Jsbsim_Input_Data();


            IPEndPoint To_jbsim_udp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5140);

            while (true)
            {


                //Thread.Sleep(1);
                try
                {

                    receiveData = Udp_Jsbsim.Receive(ref ipEndpoint);


                    rec_str = Encoding.ASCII.GetString(receiveData);



                    if (p3d_api.simconnect == null) continue;

                    rec_element = rec_str.Split(',');

                    if ((!rec_element[0].Equals("<LABELS>"))&&(rec_element.Length==17))
                    {

                        // get data 
                        jsbsim_output_Data.Time = Convert.ToDouble(rec_element[0]);
                        jsbsim_output_Data.h_sl_meters = Convert.ToDouble(rec_element[1]);
                        jsbsim_output_Data.vtrue_fps = Convert.ToDouble(rec_element[2]);
                        jsbsim_output_Data.lat_gc_deg = Convert.ToDouble(rec_element[3]);
                        jsbsim_output_Data.long_gc_deg = Convert.ToDouble(rec_element[4]);
                        jsbsim_output_Data.roll_rad = Convert.ToDouble(rec_element[5]);
                        jsbsim_output_Data.pitch_rad = Convert.ToDouble(rec_element[6]);
                        jsbsim_output_Data.heading_true_rad = Convert.ToDouble(rec_element[7]);
                        jsbsim_output_Data.v_north_fps = Convert.ToDouble(rec_element[8]);
                        jsbsim_output_Data.v_east_fps = Convert.ToDouble(rec_element[9]);
                        jsbsim_output_Data.v_down_fps = Convert.ToDouble(rec_element[10]);
                        jsbsim_output_Data.mach = Convert.ToDouble(rec_element[11]);
                        jsbsim_output_Data.phidot_rad_sec = Convert.ToDouble(rec_element[12]);
                        jsbsim_output_Data.thetadot_rad_sec = Convert.ToDouble(rec_element[13]);
                        jsbsim_output_Data.psidot_rad_sec = Convert.ToDouble(rec_element[14]);
                        jsbsim_output_Data.Nz = Convert.ToDouble(rec_element[15]);
                        jsbsim_output_Data.total_fuel_lbs = Convert.ToDouble(rec_element[16]);


                        jump_heart++;

                        //update p3d pos


                        p3d_api.Update_craft_Pos(0, jsbsim_output_Data);


                        //control out
                        string c_str=creat_input(jsbsim_output_Data, jsbsim_input_Data,this.Mode_type,jump_heart);


                        toJsb_sendBytes = Encoding.ASCII.GetBytes(c_str);


                        Udp_Jsbsim.Send(toJsb_sendBytes, toJsb_sendBytes.Length, To_jbsim_udp);


                    }


                   



                }
                catch { }
            }


        }



        //
        private string creat_input(JsbSIm_Output_Data jsb_o_Data,Jsbsim_Input_Data jsb_in_Data,int type,long jump)
        {

            // PID control example
            switch (type)
            {
                case 10:
                    jsb_in_Data.aileron_cmd_norm = -0.2;

                    break;
                
                
                
                case 0:
                    // proportional control 

                    //clip roll 

                    double roll_rad = jsb_o_Data.roll_rad;

                    if (roll_rad > 0.255)
                    {
                        roll_rad = 0.255;
                    }

                    if (roll_rad < -0.255)
                    {
                        roll_rad = -0.255;
                    }

                    //

                    double K_val = -1;
                    jsb_in_Data.aileron_cmd_norm = roll_rad * K_val;


                    break;
            
            
            }

            string control_str =jump.ToString()+"," +jsb_in_Data.throttle_cmd_norm + "," +
                jsb_in_Data.elevator_cmd_norm + "," + jsb_in_Data.aileron_cmd_norm + "," + jsb_in_Data.rudder_cmd_norm+",";



            return control_str;
        }


    }
}
