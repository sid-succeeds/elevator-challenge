using System;
namespace dvt_elevator_challenge_solution
{
	public abstract class Elevator
	{
        public int CurrentFloor { get; protected set; }
        public ElevatorDirection Direction { get; protected set; }
        public bool IsMoving { get; protected set; }
        public int ElevatorID { get; }
        public int speed { get; set; }

        public Elevator(int elevatorID)
        {
            CurrentFloor = 1; // Assuming the elevator starts at the ground floor
            Direction = ElevatorDirection.Stationary; // Assuming the elevator starts in a stationary state
            IsMoving = false; // Assuming the elevator starts in a stationary state
            ElevatorID = elevatorID;
        }

        public virtual void MoveToFloor(int floor, int speed)
        {
            if (floor == CurrentFloor)
            {
                Direction = ElevatorDirection.Stationary;
                IsMoving = false;
                return; // Already at the requested floor
            }

            Direction = floor > CurrentFloor ? ElevatorDirection.Up : ElevatorDirection.Down;
            IsMoving = true;

            // Determine the delay based on the distance to the requested floor
            int distance = Math.Abs(floor - CurrentFloor);
            int delay = distance * (1000 / speed);

            // Simulate elevator movement to the specified floor (e.g., update CurrentFloor)
            while (CurrentFloor != floor)
            {
                // Introduce a delay to simulate elevator movement time
                Thread.Sleep(delay);

                if (Direction == ElevatorDirection.Up)
                {
                    CurrentFloor++;
                }
                else
                {
                    CurrentFloor--;
                }
            }

            IsMoving = false; // Elevator has reached the requested floor
        }

        public virtual void Load(int passengerCount) { }

        public virtual void Load(double weightInKgs) { }

        public abstract void Unload();
        public abstract void DisplayStatus();
    }
}

