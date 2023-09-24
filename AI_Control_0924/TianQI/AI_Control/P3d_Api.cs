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

        private P3d_api_data p3d_data = new P3d_api_data();



        public P3d_api_data.Struct_Pos s1 = new P3d_api_data.Struct_Pos();

        // p3d message

        string output = "\n\n\n\n\n\n\n\n\n";
        // Response number
        int response = 1;

        /*
         Initialize plane's position as default
        */
        public P3d_Api()
        {

            s1.latitude = 30 + 28.82 / 60;
            s1.longitude = -86 - 30.55 / 60;
            s1.altitude = 620;
            s1.pitch = 0;
            s1.head = 0;
            s1.bank = 0;
        }

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
            catch (Exception) {

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
            catch
            {

            }
        }


        //

        public void InitDataRequest()
        {
            try
            {
                // listen to connect and quit msgs
                this.simconnect.OnRecvOpen += new SimConnect.RecvOpenEventHandler(Simconnect_OnRecvOpen);
                this.simconnect.OnRecvQuit += new SimConnect.RecvQuitEventHandler(Simconnect_OnRecvQuit);

                // listen to exceptions
                simconnect.OnRecvException += new SimConnect.RecvExceptionEventHandler(Simconnect_OnRecvException);

                //defien Data
                simconnect.AddToDataDefinition(P3d_api_data.DEFINITIONS.DEFINITION_SET_POSITION, "Plane Latitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(P3d_api_data.DEFINITIONS.DEFINITION_SET_POSITION, "Plane Longitude", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(P3d_api_data.DEFINITIONS.DEFINITION_SET_POSITION, "Plane Altitude", "feet", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(P3d_api_data.DEFINITIONS.DEFINITION_SET_POSITION, "PLANE PITCH DEGREES", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(P3d_api_data.DEFINITIONS.DEFINITION_SET_POSITION, "PLANE BANK DEGREES", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(P3d_api_data.DEFINITIONS.DEFINITION_SET_POSITION, "PLANE HEADING DEGREES TRUE", "degrees", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                // register Data Struct
                simconnect.RegisterDataDefineStruct< P3d_api_data.Struct_Pos >(P3d_api_data.DEFINITIONS.DEFINITION_SET_POSITION);
                



                //FREEZE POSITION ALTITUDE AND ATITUDE
                simconnect.AddToDataDefinition(P3d_api_data.DEFINITIONS.DEFINITION_FREEZE, "IS Latitude Longitude freeze on", "BOOL", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(P3d_api_data.DEFINITIONS.DEFINITION_FREEZE, "IS Altitude freeze on", "BOOL", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                simconnect.AddToDataDefinition(P3d_api_data.DEFINITIONS.DEFINITION_FREEZE, "IS Attitude freeze on", "BOOL", SIMCONNECT_DATATYPE.FLOAT64, 0.0f, SimConnect.SIMCONNECT_UNUSED);
                 simconnect.RegisterDataDefineStruct<P3d_api_data.Struct_Freeze>(P3d_api_data.DEFINITIONS.DEFINITION_FREEZE);



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

        void Simconnect_OnRecvException(SimConnect sender, SIMCONNECT_RECV_EXCEPTION data)
        {
            DisplayText("Exception received: " + data.dwException);
        }

        //*********************************************
        // 9/24/2023 new update
        // load_sca, Lock_MyCraft, and Update_Craft_Pos

        /*
         Using FlightLoad method, search and load a scenario to P3D
        */
        public void Load_sca(string sca_name)
        {
            simconnect.FlightLoad(sca_name);

        }
        /*
         * wake up or freeze the craft, decided by lock_flag
         * FlightID points to the craft.
         */
        public void Lock_MyCraft(int lock_flag, uint FlightID)
        {
            // freeze aircraft
            // lock_flag 0 not freeze
            // 1 freeze

            P3d_api_data.Struct_Freeze freeze_flag = new P3d_api_data.Struct_Freeze();

            try
            {
                switch (lock_flag)
                {
                    //state: 
                    case 0:

                        freeze_flag.lat_long_flag = 0.0f;
                        freeze_flag.altitdue_flag = 0.0f;
                        freeze_flag.attitude_flag = 0.0f;
                        //wake up
                        //update all flags
                        simconnect.SetDataOnSimObject(P3d_api_data.DEFINITIONS.DEFINITION_FREEZE, FlightID, 0, freeze_flag);


                        break;
                    case 1:

                        freeze_flag.lat_long_flag = 1.0f;
                        freeze_flag.altitdue_flag = 1.0f;
                        freeze_flag.attitude_flag = 1.0f;

                        //update all flags
                        simconnect.SetDataOnSimObject(P3d_api_data.DEFINITIONS.DEFINITION_FREEZE, FlightID, 0, freeze_flag);



                        break;

                }

            }
            catch
            { }




        }


        /*
         * by using position and atitude in the saved scenario, update the aircraft.
         */
        public void Update_craft_Pos(uint FlightID, P3d_api_data.Struct_Pos s1)
        {
            
            //update aircart position and attitude

            simconnect.SetDataOnSimObject(P3d_api_data.DEFINITIONS.DEFINITION_SET_POSITION, FlightID, 0, s1);



        }

    }
}
