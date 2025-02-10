using System;
using System.Collections.Generic;
using AutoDrivingCarSimulation;

public class Car
{
    public string Name { get; }
    public int X { get; private set; }
    public int Y { get; private set; }
    public DrivingDirection Direction { get; private set; }

    private string Commands { get; set; }

    public Car(string name, int x, int y, DrivingDirection direction, string commands)
    {
        Name = name;
        X = x;
        Y = y;
        Direction = direction;
        Commands = commands;
    }

    public void ExecuteCommands(List<string> commands, HashSet<string> carPositions)
    {
        char[] directions = { 'N', 'E', 'S', 'W' };

        var moveDelta = new Dictionary<DrivingDirection, (int dx, int dy)>
        {
            { DrivingDirection.N, (0, 1) },
            { DrivingDirection.E, (1, 0) },
            { DrivingDirection.S, (0, -1) },
            { DrivingDirection.W, (-1, 0) }
        };

        foreach (var command in commands)
        {
            if (command == "L") // Turn Left
            {
                int currentDirectionIndex = Array.IndexOf(directions, (char)Direction);
                Direction = (DrivingDirection)directions[(currentDirectionIndex - 1 + 4) % 4];
            }
            else if (command == "R") // Turn Right
            {
                int currentDirectionIndex = Array.IndexOf(directions, (char)Direction);
                Direction = (DrivingDirection)directions[(currentDirectionIndex + 1) % 4];
            }
            else if (command == "F") // Move Forward
            {
                var delta = moveDelta[Direction];
                int dx = delta.Item1;
                int dy = delta.Item2;

                int newX = X + dx, newY = Y + dy;

                // Check if the new position is within bounds (assuming 10x10 grid here)
                if (newX < 0 || newX >= 10 || newY < 0 || newY >= 10)
                {
                    throw new InvalidOperationException($"Car {Name} moved out of bounds.");
                }

                // Check for collisions with other cars **before** updating the car's position
                string newPosition = $"{newX},{newY}";
                if (carPositions.Contains(newPosition))
                {
                    throw new InvalidOperationException($"Car {Name} collided with another car.");
                }

                // Remove the old position before updating
                carPositions.Remove($"{X},{Y}");

                // Update the car's position
                X = newX;
                Y = newY;

                // Add the new position to the list of car positions
                carPositions.Add(newPosition);
            }
        }
    }

    public List<string> GetCommands()
    {
        return new List<string>(Commands.ToCharArray().Select(c => c.ToString()));
    }

    // Get the current position as a string
    public string GetCurrentPosition() => $"{X} {Y} {Direction}";
}
