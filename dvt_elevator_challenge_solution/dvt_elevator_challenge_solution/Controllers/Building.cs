using Serilog;

namespace dvt_elevator_challenge_solution
{
    public class Building: IBuilding
    {
        private List<IElevator> elevators;
        private int totalFloors;

        public Building(int numberOfFloors)
        {
            totalFloors = numberOfFloors;
            elevators = new List<IElevator>();
        }

        public Building(int numberOfFloors, IEnumerable<IElevator> initialElevators)
        {
            totalFloors = numberOfFloors;
            elevators = new List<IElevator>(initialElevators);
        }

        public void AddElevator(IElevator elevator)
        {
            elevators.Add(elevator);
        }

        public void CallElevator(int requestedFloor, Type elevatorType)
        {
            // Prepare variables to capture user input
            int passengerCount = 0;
            double goodsWeight = 0;

            if (elevatorType == typeof(PassengerElevator))
            {
                Log.Information("Enter the number of passengers: ");
                int.TryParse(Console.ReadLine(), out passengerCount);
            }
            else if (elevatorType == typeof(GoodsElevator))
            {
                Log.Information("Enter the goods weight (kg): ");
                double.TryParse(Console.ReadLine(), out goodsWeight);
            }

            // Find the nearest elevator of the specified type
            IElevator nearestElevator = FindNearestElevator(requestedFloor, elevatorType, goodsWeight, passengerCount);

            // Move the nearest elevator to the requested floor and handle loading
            if (nearestElevator != null)
            {
                nearestElevator.MoveToFloor(requestedFloor, nearestElevator.speed);
                if (elevatorType == typeof(PassengerElevator))
                {
                    Log.Information($"Moving passenger elevator {nearestElevator.ElevatorID} to floor {requestedFloor}");
                }
                else if (elevatorType == typeof(GoodsElevator))
                {
                    Log.Information($"Moving goods elevator {nearestElevator.ElevatorID} to floor {requestedFloor}");
                }

                Load(nearestElevator, passengerCount, goodsWeight);
            }
            else
            {
                Log.Information("No available elevator found.");
            }
        }

        public void DisplayAllElevatorStatus(Type elevatorType = null)
        {
            Log.Information("Elevator Status:");
            foreach (var elevator in elevators)
            {
                if (elevatorType == null || elevator.GetType() == elevatorType)
                {
                    elevator.DisplayStatus();
                }
            }
        }

        private void Load(IElevator elevator, int passengerCount, double goodsWeight)
        {
            if (elevator is PassengerElevator)
            {
                ((PassengerElevator)elevator).Load(passengerCount);
            }
            else if (elevator is GoodsElevator)
            {
                ((GoodsElevator)elevator).Load(goodsWeight);
            }
        }

        private void Unload(IElevator elevator)
        {
            elevator.Unload();
        }

        private IElevator FindNearestElevator(int requestedFloor, Type elevatorType, double goodsWeight, int passengerCount)
        {
            try
            {
                // Filter elevators based on the specified elevator type
                List<IElevator> filteredElevators = elevators.FindAll(e => e.GetType() == elevatorType);

                IElevator nearestElevator = null;
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
            catch (Exception ex)
            {
                Log.Error($"Error in FindNearestElevator: {ex.Message}");
                return null; // Return null to indicate failure
            }
        }



    }
}