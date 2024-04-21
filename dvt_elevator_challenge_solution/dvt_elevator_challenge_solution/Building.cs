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

        private void Load(Elevator elevator, int passengerCount, double goodsWeight)
        {
            if (elevator is PassengerElevator)
            {
                elevator.Load(passengerCount);
            }
            else if (elevator is GoodsElevator)
            {
                elevator.Load(goodsWeight);
            }
        }

        private void Unload(Elevator elevator)
        {
            elevator.Unload();
        }

        public void DisplayAllElevatorStatus(Type elevatorType = null)
        {
            Console.WriteLine("Elevator Status:");
            foreach (var elevator in elevators)
            {
                if (elevatorType == null || elevator.GetType() == elevatorType)
                {
                    elevator.DisplayStatus();
                }
            }
        }

    }
}

