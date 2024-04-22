using System;
namespace dvt_elevator_challenge_solution
{
	public interface IBuilding
	{
        void AddElevator(IElevator elevator);
        void CallElevator(int requestedFloor, Type elevatorType);
        void DisplayAllElevatorStatus(Type elevatorType = null);
    }
}

