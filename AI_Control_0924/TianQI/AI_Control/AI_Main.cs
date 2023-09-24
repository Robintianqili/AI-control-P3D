//****************************************************
//  Main  entry

// project need x64


// three funtion

// read config file

// connect p3d
// reference p3d sdk example


// connect flightgear


//****************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AI_Control
{
    public partial class AI_Main : Form
    {

        public P3d_Api p3d_api = new P3d_Api();

        public static AI_Main mymainform;

        Config_Def config_def = new Config_Def();

       
        


        public AI_Main()
        {
            InitializeComponent();
            mymainform = this;
        }

        private void AI_Main_Load(object sender, EventArgs e)
        {
            // read config

            read_config();

            //connect p3d
            this.p3d_api.Connect_fsx();


        }




        


        //*****p3d run *****
        //*****************************************************
        protected override void DefWndProc(ref Message m)
        {

            try
            {
                if (m.Msg == P3d_Api.WM_USER_SIMCONNECT)
                {
                    if (p3d_api.simconnect != null)
                    {
                        p3d_api.simconnect.ReceiveMessage();
                    }
                }
                else
                {
                    base.DefWndProc(ref m);
                }
            }
            catch { }
        }


        // read config file ,define by xml format

        private void read_config()
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("config.xml");

            XmlNode xn = xmlDoc.SelectSingleNode("SimConfig");
            XmlNodeList xnl = xn.ChildNodes;


            string jbsim_ip = "";
            string jbsim_port = "";

            foreach (XmlNode xnf in xnl)
            {
                try
                {
                    XmlElement xe = (XmlElement)xnf;
                    switch (xe.Name)
                    {

                        case "scenario_file":

                            config_def.scenario_file = xe.InnerText;

                            break;




                    }
                }
                catch { }
            }
        }



                    //
        private void load_sca_btn_Click(object sender, EventArgs e)
        {
            //load scanerio   by  config file


            p3d_api.Load_sca(config_def.scenario_file);

            // freeze aircraft   this is import , or the aircarfat will 

            p3d_api.Lock_MyCraft(1, 0);


        }

        private void send_btn_Click(object sender, EventArgs e)
        {
            if (send_btn.Text.Equals("send"))
            {
                // send test data to P3d
                this.timer1.Enabled = true;
                send_btn.Text = "stop";

            }
            else
            {
                // stop sending 
                this.timer1.Enabled = false;
                send_btn.Text = "send";

            }
        }

        


        private void timer1_Tick(object sender, EventArgs e)
        {
            p3d_api.s1.latitude += 0.0001;

            p3d_api.Update_craft_Pos(0, p3d_api.s1);
        }
    }
}
