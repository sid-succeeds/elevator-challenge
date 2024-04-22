using dvt_elevator_challenge_solution;
using Serilog;

Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

Console.WriteLine("Create a building with 10 floors");
Building building = new Building(10);

Console.WriteLine("Adding Passenger elevator with elevatorID 1 with maxPassengerLimit of 10 to building");
building.AddElevator(new PassengerElevator(10, 1));

Console.WriteLine("Adding Passenger elevator with elevatorID 2 with maxPassengerLimit of 15 to building");
building.AddElevator(new PassengerElevator(15, 2));

Console.WriteLine("Adding Goods elevator with elevatorID 3 with maxWeightLimitInKgs of 1000 to building");
building.AddElevator(new GoodsElevator(1000, 3));

// Display the status of all elevators
Console.WriteLine("\nDisplaying elevator status:");
building.DisplayAllElevatorStatus();

// Test calling a passenger elevator
Console.WriteLine("Testing calling a passenger elevator:");
building.CallElevator(5, typeof(PassengerElevator));

// Test calling a goods elevator
Console.WriteLine("\nTesting calling a goods elevator:");
building.CallElevator(8, typeof(GoodsElevator));

// Display the status of all elevators
Console.WriteLine("\nDisplaying elevator status:");
building.DisplayAllElevatorStatus();

Log.CloseAndFlush();

