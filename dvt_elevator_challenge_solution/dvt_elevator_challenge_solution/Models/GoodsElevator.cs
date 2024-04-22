using System;
using Serilog;

namespace dvt_elevator_challenge_solution
{
    public class GoodsElevator : IElevator
    {
        public double WeightCount { get; protected set; }
        public double maxWeightLimitInKgs;

        public int CurrentFloor { get; set; }
        public ElevatorDirection Direction { get; set; }
        public bool IsMoving { get; set; }
        public int ElevatorID { get; }
        public int speed { get; set; }

        public GoodsElevator(double maxWeightLimitInKgs, int elevatorID)
        {
            this.maxWeightLimitInKgs = maxWeightLimitInKgs;
            this.ElevatorID = elevatorID;
            this.speed = 50; // Setting a default speed for GoodsElevator
        }

        public void Load(double weightInKgs)
        {
            // Only load goods onto the elevator if the weight does not exceed the maximum weight limit
            if (weightInKgs <= maxWeightLimitInKgs)
            {
                Log.Information($"Loading {weightInKgs} kilogram units of goods into goods elevator {ElevatorID}");
                WeightCount += weightInKgs;
            }
            else
            {
                Log.Error("Cannot load goods. Maximum weight limit exceeded.");
            }
        }

        public void Unload()
        {
            // Implementation specific to goods elevators
            Log.Information($"Unloading {WeightCount} kilogram units of goods from the goods elevator {ElevatorID}");
            WeightCount = 0;
            Log.Information("Unloaded");
        }

        public void DisplayStatus()
        {
            Log.Information($"Elevator ID: {ElevatorID}, Current Floor: {CurrentFloor}, " +
                              $"Direction: {Direction}, Current Weight (kg): {WeightCount}, Max Weight Limit (kg): {maxWeightLimitInKgs}");
        }
    }
}

