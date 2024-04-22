using dvt_elevator_challenge_solution;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateLogger();

// Create a building with 10 floors
Building building = new Building(10);

// Add some elevators to the building
building.AddElevator(new PassengerElevator(12, 1));
building.AddElevator(new HighSpeedElevator(25, 2));
building.AddElevator(new GoodsElevator(1000, 3));

while (true)
{
    Console.WriteLine("Enter the floor you are on (1-10): ");
    int currentFloor;
    if (!int.TryParse(Console.ReadLine(), out currentFloor) || currentFloor < 1 || currentFloor > 10)
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 10.");
        continue;
    }

    Console.WriteLine("Enter the floor you want to go to (1-10): ");
    int requestedFloor;
    if (!int.TryParse(Console.ReadLine(), out requestedFloor) || requestedFloor < 1 || requestedFloor > 10)
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 10.");
        continue;
    }

    if (currentFloor == requestedFloor)
    {
        Console.WriteLine("You are already on the same floor. Please choose a different floor.");
        continue;
    }

    Console.WriteLine("Enter the type of elevator you want (Passenger/Goods): ");
    string elevatorTypeInput = Console.ReadLine().Trim();
    Type elevatorType = null;
    if (elevatorTypeInput.Equals("Passenger", StringComparison.OrdinalIgnoreCase))
    {
        elevatorType = typeof(PassengerElevator);
    }
    else if (elevatorTypeInput.Equals("Goods", StringComparison.OrdinalIgnoreCase))
    {
        elevatorType = typeof(GoodsElevator);
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter 'Passenger' or 'Goods'.");
        continue;
    }

    building.CallElevator(requestedFloor, elevatorType);
    building.DisplayAllElevatorStatus();

    Log.CloseAndFlush();
}
