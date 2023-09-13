//****************************************************
//  Main  entry
// Project need x64
// Funtion: AI_main, AI_Main_Load, DefWndDoc
// read config file, not accomplished yet
// connect to p3d
// reference p3d sdk example: Managed Data Request
// Reference link: https://www.prepar3d.com/SDKv4/sdk/simconnect_api/samples/vb_managed_data_requests.html
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

namespace AI_Control
{
    public partial class AI_Main : Form
    {

        public P3d_Api p3d_api = new P3d_Api();

        public static AI_Main mymainform;
        /*
         * Constructor,initialized basic graphic buttons.
         * Use mymainform to return contructed object.
         */
        public AI_Main()
        {
            InitializeComponent();
            mymainform = this;
        }
        /*
         * Reading basic configuration and connect this to Flight simulator
         * Would use XML file to configuration file
         */
        private void AI_Main_Load(object sender, EventArgs e)
        {
            // read config


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

    }
}
