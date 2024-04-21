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

        private Elevator FindNearestElevator(int requestedFloor, Type elevatorType, double goodsWeight, int passengerCount)
        {
            // Filter elevators based on the specified elevator type
            List<Elevator> filteredElevators = elevators.FindAll(e => e.GetType() == elevatorType);

            Elevator nearestElevator = null;
            int shortestDistance = int.MaxValue;

            foreach (var elevator in filteredElevators)
            {
                int distance = Math.Abs(elevator.CurrentFloor - requestedFloor);
                if (!elevator.IsMoving || elevator.Direction == ElevatorDirection.Stationary)
                {
                    if (elevator is PassengerElevator)
                    {
                        PassengerElevator passengerElevator = (PassengerElevator)elevator;
                        if (passengerElevator.PassengerCount + passengerCount <= passengerElevator.MaxPassengerLimit)
                        {
                            // Update nearest elevator if it's closer to the requested floor
                            if (distance < shortestDistance)
                            {
                                shortestDistance = distance;
                                nearestElevator = elevator;
                            }
                        }
                    }
                    else if (elevator is GoodsElevator)
                    {
                        GoodsElevator goodsElevator = (GoodsElevator)elevator;
                        if (goodsElevator.WeightCount + goodsWeight <= goodsElevator.maxWeightLimitInKgs)
                        {
                            // Update nearest elevator if it's closer to the requested floor
                            if (distance < shortestDistance)
                            {
                                shortestDistance = distance;
                                nearestElevator = elevator;
                            }
                        }
                    }
                }
            }

            return nearestElevator;
        }

        public void CallElevator(int requestedFloor, Type elevatorType)
        {
            // Prepare variables to capture user input
            int passengerCount = 0;
            double goodsWeight = 0;

            if (elevatorType == typeof(PassengerElevator))
            {
                Console.Write("Enter the number of passengers: ");
                int.TryParse(Console.ReadLine(), out passengerCount);
            }
            else if (elevatorType == typeof(GoodsElevator))
            {
                Console.Write("Enter the goods weight (kg): ");
                double.TryParse(Console.ReadLine(), out goodsWeight);
            }

            // Find the nearest elevator of the specified type
            Elevator nearestElevator = FindNearestElevator(requestedFloor, elevatorType, goodsWeight, passengerCount);

            // Move the nearest elevator to the requested floor and handle loading
            if (nearestElevator != null)
            {
                nearestElevator.MoveToFloor(requestedFloor, nearestElevator.speed);
                if (elevatorType == typeof(PassengerElevator))
                {
                    Console.WriteLine($"Moving passenger elevator {nearestElevator.ElevatorID} to floor {requestedFloor}");
                }
                else if (elevatorType == typeof(GoodsElevator))
                {
                    Console.WriteLine($"Moving goods elevator {nearestElevator.ElevatorID} to floor {requestedFloor}");
                }

                Load(nearestElevator, passengerCount, goodsWeight);
            }
            else
            {
                Console.WriteLine("No available elevator found.");
            }
        }
    }
}

