using System;
namespace dvt_elevator_challenge_solution
{
    public class PassengerElevator : IElevator
    {
        public int PassengerCount { get; protected set; }
        public int MaxPassengerLimit { get; }

        public int CurrentFloor { get; set; }
        public ElevatorDirection Direction { get; set; }
        public bool IsMoving { get; set; }
        public int ElevatorID { get; }
        public int speed { get; set; }

        public PassengerElevator(int maxPassengerLimit, int elevatorID)
        {
            MaxPassengerLimit = maxPassengerLimit;
            PassengerCount = 0;
            speed = 100; // Setting a default speed for PassengerElevator
        }

        public void Load(int passengerCount)
        {
            // Only load passengers onto the elevator if the count does not exceed the maximum passenger limit
            if (PassengerCount + passengerCount <= MaxPassengerLimit)
            {
                Console.WriteLine($"Loading {passengerCount} passengers into passenger elevator {ElevatorID}");
                PassengerCount += passengerCount;
            }
            else
            {
                throw new InvalidOperationException("Cannot load passengers. Elevator is at full capacity.");
            }
        }

        public void Unload()
        {
            // Implementation specific to passenger elevators
            // Assumed that ALL passengers will be unloaded on Unload
            Console.WriteLine($"Unloading {PassengerCount} passengers from passenger elevator {ElevatorID}");
            PassengerCount = 0;
            Console.WriteLine("Unloaded");
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"Elevator ID: {ElevatorID}, Current Floor: {CurrentFloor}, " +
                              $"Direction: {Direction}, Passenger Count: {PassengerCount}, Max Passenger Limit: {MaxPassengerLimit}");
        }
    }
}

