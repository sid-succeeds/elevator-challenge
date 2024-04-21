using System;
namespace dvt_elevator_challenge_solution
{
    public class Building
    {
        private List<Elevator> elevators;
        private int totalFloors;

        public Building(int numberOfFloors)
        {
            totalFloors = numberOfFloors;
            elevators = new List<Elevator>();
        }

        public void AddElevator(Elevator elevator)
        {
            elevators.Add(elevator);
        }

    }
}

