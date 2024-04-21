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

        private Elevator FindNearestElevator(List<Elevator> elevators, int requestedFloor, Type elevatorType, double goodsWeight, int passengerCount, Elevator nearestElevator, int shortestDistance)
        {
            // Base case: If there are no more elevators to consider, return the nearest elevator found so far
            if (elevators.Count == 0)
            {
                return nearestElevator;
            }

            var elevator = elevators[0];

            int distance = Math.Abs(elevator.CurrentFloor - requestedFloor);

            if (!elevator.IsMoving || elevator.Direction == ElevatorDirection.Stationary)
            {
                // Check if the elevator can accommodate passengers or goods based on the type
                bool canAccommodate = (elevator is PassengerElevator && ((PassengerElevator)elevator).PassengerCount + passengerCount <= ((PassengerElevator)elevator).MaxPassengerLimit) ||
                                      (elevator is GoodsElevator && ((GoodsElevator)elevator).WeightCount + goodsWeight <= ((GoodsElevator)elevator).maxWeightLimitInKgs);

                if (canAccommodate)
                {
                    // Update nearest elevator if it's closer to the requested floor
                    if (distance < shortestDistance)
                    {
                        nearestElevator = elevator;
                        shortestDistance = distance;
                    }
                }
            }

            // Move to the next elevator in the list and recursively call FindNearestElevator
            return FindNearestElevator(elevators.GetRange(1, elevators.Count - 1), requestedFloor, elevatorType, goodsWeight, passengerCount, nearestElevator, shortestDistance);
        }



    }
}

