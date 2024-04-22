using System;
namespace dvt_elevator_challenge_solution
{
	public class HighSpeedElevator: PassengerElevator
	{
        public HighSpeedElevator(int maxPassengerLimit, int elevatorID) : base(maxPassengerLimit, elevatorID)
        {
            speed = 250; //Default speed for HighSpeedElevator
        }
    }
}

