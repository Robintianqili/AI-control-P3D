## Auto horizonal stabiliser trim

According to Reference Manual P27-P30

### Objective

  The job of an autopilot is to attain a state such as wings level, or a heading, or an altitude, and hold it. Designing an autopilot for an aircraft is a science in itself – we will only gloss over the design aspect, paying the most attention on how to implement one and use it in JSBSim. As an example, we can set out to build a wing-leveler autopilot. Wings-level is by definition a roll angle of zero (phi=0). Many forces will tend to disrupt a state of wings-level, such as engine torque, atmospheric turbulence, fuel slosh, etc. To get to a phi of zero (assuming phi is initially non-zero) we will need to attain a non-zero phi rate, which drives us towards wings-level. To get the phi rate, we will need to attain a phi acceleration, which is controlled by aileron deflection


### Implementation of a Simple Proportional Control Method for Aircraft Roll Angle
  One possibility is to simply command the ailerons based on the roll angle. This is called proportional control, because the output is simply the input multiplied by a value – the output is proportional to the input. The autopilot aileron command is sent to the main flight control system and summed in to the aileron command channel. There are a couple of nuances to this autopilot arrangement, however. For instance, it is always on. A better arrangement is shown on the next page.

  The objective is to acquire the aircraft's roll angle through a controlled process. Commence by employing a limiter mechanism to confine the angular variation within a prescribed range, specifically, between -0.255 and 0.255 radians.

As a measure of deviation from the reference, introduce a proportional control strategy to dynamically generate the aileron control input.

For validation purposes, integrate a manual roll feature into the program. This can be achieved by incorporating two distinct buttons: one for manual roll ("manu_roll") and the other for initiating the automatic balancing process ("auto_roll").

Subsequently, delve into the realms of control augmentation by exploring the incorporation of proportional-integral (PI) control and proportional-integral-derivative (PID) control. Conclusively, conduct a comparative analysis to discern the nuanced disparities between these control methodologies.




