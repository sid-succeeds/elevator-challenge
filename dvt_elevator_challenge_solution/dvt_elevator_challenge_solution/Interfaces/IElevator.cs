using System;

namespace dvt_elevator_challenge_solution
{
    public interface IElevator
    {
        int CurrentFloor { get; set; }
        ElevatorDirection Direction { get; set; }
        bool IsMoving { get; set; }
        int ElevatorID { get; }
        int speed { get; set; }

        //Moving to a floor is the same for all elevators
        void MoveToFloor(int floor, int speed)
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
                //A delay to simulate elevator movement time
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

        void Unload();
        void DisplayStatus();
    }
}
