using System;
namespace dvt_elevator_challenge_solution
{
    public class PassengerElevator : Elevator
    {
        public int PassengerCount { get; protected set; }
        public int MaxPassengerLimit { get; }

        public PassengerElevator(int maxPassengerLimit, int elevatorID) : base(elevatorID)
        {
            MaxPassengerLimit = maxPassengerLimit;
            PassengerCount = 0;
            speed = 100;
        }

        public override void Load(int passengerCount)
        {
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

        public override void Unload()
        {
            // Implementation specific to passenger elevators
            Console.WriteLine($"Unloading {PassengerCount} passengers from passenger elevator {ElevatorID}");
            PassengerCount = 0;
            Console.WriteLine("Unloaded");
        }

        public override void DisplayStatus()
        {
            Console.WriteLine($"Elevator ID: {ElevatorID}, Current Floor: {CurrentFloor}, " +
                              $"Direction: {Direction}, Passenger Count: {PassengerCount}, Max Passenger Limit: {MaxPassengerLimit}");
        }
    }
}

