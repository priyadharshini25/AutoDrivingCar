using System;
using System.Collections.Generic;

public class Ground
{
    public int Width { get; set; }
    public int Height { get; set; }
    private List<Car> Cars { get; set; }
    private HashSet<string> CarPositions { get; set; }

    public Ground(int width, int height)
    {
        Width = width;
        Height = height;
        Cars = new List<Car>();
        CarPositions = new HashSet<string>();
    }

    public void AddCar(Car car)
    {
        Cars.Add(car);
        CarPositions.Add($"{car.X},{car.Y}"); // Initial position of the car
    }

    //public string Simulate()
    //{
    //    int maxSteps = 100; // Max steps to simulate
    //    for (int step = 0; step < maxSteps; step++)
    //    {
    //        foreach (var car in Cars)
    //        {
    //            try
    //            {
    //                car.ExecuteCommands(car.GetCommands(), CarPositions);
    //            }
    //            //catch (InvalidOperationException ex)
    //            //{
    //            //    // In case of collision or boundary violation, return the collision info
    //            //    return $"{car.Name} collided at step {step + 1}: {ex.Message}";
    //            //}
    //               catch (InvalidOperationException)
    //            {
    //                // Return current positions of all cars if collision occurs
    //                string result = string.Join("\n", Cars.ConvertAll(car => $"{car.X} {car.Y} {car.Direction}"));
    //                return $"{result}\nNo Collision";
    //            }
    //        }
    //        }

    //    return "No collision after max steps";
    //}
    //public string Simulate()
    //{
    //    int maxSteps = 100; // Max steps to simulate

    //    for (int step = 0; step < maxSteps; step++)
    //    {
    //        foreach (var car in Cars)
    //        {
    //            try
    //            {
    //                // Attempt to move the car based on commands
    //                car.ExecuteCommands(car.GetCommands(), CarPositions);

    //                // After each car moves, we check for a collision
    //                var currentPosition = $"{car.X},{car.Y}";

    //                // If the position already contains a car, that means a collision occurs
    //                if (CarPositions.Contains(currentPosition))
    //                {
    //                    // Get the name of the car that collided and the current position
    //                    var otherCar = Cars.Find(c => $"{c.X},{c.Y}" == currentPosition);

    //                    if (otherCar != null)
    //                    {
    //                        // Return the names and positions of the two colliding cars
    //                        return $"{car.Name} {otherCar.Name}\n{car.X} {car.Y}\n{step + 1}";
    //                    }
    //                }

    //                // If the move was successful and no collision, update the car's position in the CarPositions
    //                CarPositions.Add(currentPosition);
    //            }
    //            catch (InvalidOperationException)
    //            {
    //                // If a car moves out of bounds or a collision happens
    //                string result = string.Join("\n", Cars.ConvertAll(car => $"{car.X} {car.Y} {car.Direction}"));
    //                return $"{result}\nNo Collision";
    //            }
    //        }
    //    }

    //    // If no collision happens after max steps, return final positions
    //    string finalResult = string.Join("\n", Cars.ConvertAll(car => $"{car.X} {car.Y} {car.Direction}"));
    //    return $"{finalResult}\nNo Collision";
    //}
    public string Simulate()
    {
        int maxSteps = 100; // Max steps to simulate

        // Process up to maxSteps
        for (int step = 0; step < maxSteps; step++)
        {
            // Iterate over each car to simulate its movement
            foreach (var car in Cars)
            {
                // Store the current position of the car before moving
                var initialX = car.X;
                var initialY = car.Y;

                try
                {
                    // Attempt to move the car based on commands
                    car.ExecuteCommands(car.GetCommands(), CarPositions);

                    // After the car has moved, check if there's a collision
                    var currentPosition = $"{car.X},{car.Y}";

                    foreach (var otherCar in Cars)
                    {
                        // Ensure we're not comparing the car with itself
                        if (otherCar != car && $"{otherCar.X},{otherCar.Y}" == currentPosition)
                        {
                            // Return the names of the colliding cars, collision point, and step number
                            return $"{car.Name} {otherCar.Name}\n{car.X} {car.Y}\n{step + 1}";
                        }
                    }

                    // If no collision occurred, update the car's position in CarPositions
                    CarPositions.Add($"{car.X},{car.Y}");

                }
                catch (InvalidOperationException)
                {
                    // If a car moves out of bounds or another issue happens
                    string result = string.Join("\n", Cars.ConvertAll(car => $"{car.X} {car.Y} {car.Direction}"));
                    return $"{result}\nNo Collision";
                }
            }
        }

        // If no collision happens after max steps, return final positions of the cars
        string finalResult = string.Join("\n", Cars.ConvertAll(car => $"{car.Name} {car.X} {car.Y} {car.Direction}"));
        return $"{finalResult}\nNo Collision";
    }

}
