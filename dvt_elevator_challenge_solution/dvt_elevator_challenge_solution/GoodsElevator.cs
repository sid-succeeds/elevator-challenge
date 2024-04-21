using System;
namespace dvt_elevator_challenge_solution
{
    public class GoodsElevator : Elevator
    {
        public double WeightCount { get; protected set; }
        public double maxWeightLimitInKgs;

        public GoodsElevator(double maxWeightLimitInKgs, int elevatorID) : base(elevatorID)
        {
            this.maxWeightLimitInKgs = maxWeightLimitInKgs;
            this.WeightCount = 0;
            speed = 50;

        }

        public override void Load(double weightInKgs)
        {
            // Implementation specific to goods elevators
            // Only load goods onto the elevator if the weight does not exceed the maximum weight limit
            if (weightInKgs <= maxWeightLimitInKgs)
            {
                Console.WriteLine($"Loading {weightInKgs} kilogram units of goods into goods elevator {ElevatorID}");
                WeightCount += weightInKgs;
            }
            else
            {
                Console.WriteLine("Cannot load goods. Maximum weight limit exceeded.");
            }
        }

        public override void Unload()
        {
            // Implementation specific to goods elevators
            Console.WriteLine($"Unloading {WeightCount} kilogram units of goods from the goods elevator {ElevatorID}");
            WeightCount = 0;
            Console.WriteLine("Unloaded");
        }

        public override void DisplayStatus()
        {
            Console.WriteLine($"Elevator ID: {ElevatorID}, Current Floor: {CurrentFloor}, " +
                              $"Direction: {Direction}, Current Weight (kg): {WeightCount}, Max Weight Limit (kg): {maxWeightLimitInKgs}");
        }
    }
}

