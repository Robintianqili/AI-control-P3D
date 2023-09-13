using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Add these two statements to all SimConnect clients
//using Microsoft.FlightSimulator.SimConnect;
using LockheedMartin.Prepar3D.SimConnect;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AI_Control
{
    public class P3d_Api
    {
        public const int WM_USER_SIMCONNECT = 0x0402;

        public  SimConnect simconnect;

        // p3d message
        //generate output, show 10 messages only
        string output = "\n\n\n\n\n\n\n\n\n";
        // Response number
        int response = 1;

        /*
        *Connect C# program to Flight simulator
        *return error sentences if the programs cannot find P3D
        *initiate Data request if we successfully are connected.
        */
        public void Connect_fsx()
        {
            // creat connect p3d by simconnect
            if (this.simconnect != null)
            {
                //displayText("Error - try again");
                CloseConnection();
            }
            try
            {               
                // the constructor is similar to SimConnect_Open in the native API
                this.simconnect = new SimConnect("P3d Data Request", AI_Main.mymainform.Handle, WM_USER_SIMCONNECT, null, 0);
                // init Data interface
                InitDataRequest();
            }
            catch (Exception)
            {
                Debug.WriteLine("connect p3d error");            
            }


        }

        /*
         *if simconnect cannot be found, close conneection and diplay "Connection closed"
         */
        public void CloseConnection()
        {
            // close p3d connect            
            if (this.simconnect != null)
            {
                // Dispose serves the same purpose as SimConnect_Close()
                this.simconnect.Dispose();
                this.simconnect = null;                
                DisplayText("Connection closed");
            }
        }

        /*
         *only put recently 10 message
         */
        public void DisplayText(string s)
        {
            try
            {
                // remove first string from output
                output = output.Substring(output.IndexOf("\n") + 1);

                // add the new string
                output += "\n" + response++ + ": " + s;

                // display it
                AI_Main.mymainform.p3d_message_richbox.Text = output;
            }
            catch (Exception)
            {
                Debug.WriteLine("Display message failed, check display.");
            }
        }


        /*Initiate request
        *create new message and use On RecvSimobjectData to send request
        */

        public void InitDataRequest()
        {
            try
            {
                // listen to connect and quit msgs
                this.simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(Simconnect_OnRecvOpen);
                this.simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(Simconnect_OnRecvQuit);

                // listen to exceptions
               

                this.simconnect.OnRecvSimobjectData += new SimConnect.RecvSimobjectDataEventHandler(Simconnect_OnRecvSimobjectData);

            }
            catch (COMException ex)
            {
                DisplayText(ex.Message);
            }
        }

        public void InitClientEvent()
        {
            try
            {

                // catch the assigned object IDs
                this.simconnect.OnRecvAssignedObjectId += new SimConnect.RecvAssignedObjectIdEventHandler(Simconnect_OnRecvAssignedObjectId);
                // listen to events
                this.simconnect.OnRecvEvent += new SimConnect.RecvEventEventHandler(Simconnect_OnRecvEvent);       
            }
            catch (COMException ex)
            {
                DisplayText(ex.Message);
            }
        }

        private void Simconnect_OnRecvAssignedObjectId(SimConnect sender, SIMCONNECT_RECV_ASSIGNED_OBJECT_ID data)
        {
           
        }




        void Simconnect_OnRecvOpen(SimConnect sender, SIMCONNECT_RECV_OPEN data)
        {
            // connect ok
            DisplayText("Connected to FSX");
            InitClientEvent();           

        }
        void Simconnect_OnRecvQuit(SimConnect sender, SIMCONNECT_RECV data)
        {
            DisplayText("FSX has exited");
            CloseConnection();
        }


        private void Simconnect_OnRecvSimobjectData(SimConnect sender, SIMCONNECT_RECV_SIMOBJECT_DATA data)
        {
           
        }

        void Simconnect_OnRecvEvent(SimConnect sender, SIMCONNECT_RECV_EVENT recEvent)
        {
           
        }

    }
}
