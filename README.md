# AI-control-P3D
The possible way to use AI to control planes in flight simulation.

This is a record of a whim from my high school when I established a flight simulation club. I always wonder whether AI could completely handle the control of a plane. After two years of study at UC San Diego, I am here to explore a possible way to practice AI algorithms and fulfill my wonders.

## Target

The program is made for achieving control of a plane in the flight simulation (the P3D v4 platform would be used). AI would directly decide and change all flight variables without using the Autopilot from the plane, which means that AI would manage all manual flight procedures by using traditional PID auto control, including:

The essential altitude and velocity holding mode.

The auto heading mode, the auto climbing mode, and the auto descent mode.

Correctly follow the flight route, follow SID (standard instrument departure) to take off, and IAP/STAR (Instrument Approach ProcedureStandard Terminal Arrival) to land.

### Basic setup
1. Flight environment
P3D version 4 would be used to set up the Flight environment since it has SDK. P3D would be a review system and show the flight altitude. The tactview would also be used as recording and replay. However, the user of P3D would change from human to AI.
2. Plane choice
Cessna 172 would be used in AI flight simulation. Since AI's target is to control the plane completely, I have to use an open-source plane model, where all aerodynamic data are accessible.
JsbSIM in FlightGear would be a possible choice.
3. Core task
Read flight altitude from JsbSim. The pitch, roll, rudder, and throttle control are generated by the core task to achieve flight control. Send the flying attitude back to P3D for display. The core task is to implement hierarchical control.
