using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using System;
using System.Linq;

namespace Quantum.Simon
{
    class Driver
    {
        static void Main(string[] args)
        {
            try
            {
                using (var sim = new QuantumSimulator())
                {
                    const int n = 4;
                    foreach (var str in Enumerable.Range(0, 1 << n))
                    {
                        var res = SimonTest.Run(sim, str, n).Result;
                        if (res != str)
                        {
                            throw new Exception($"Measured string {res}, but expected {str}.");
                        }
                    }
                }

                System.Console.WriteLine("All parities measured successfully!");
                System.Console.WriteLine("Press any key to continue...");
                System.Console.ReadKey();
            }
            catch (AggregateException e)
            {
                // Unwrap AggregateException to get the message from Q# fail statement.
                // Go through all inner exceptions.
                foreach (Exception inner in e.InnerExceptions)
                {
                    // If the exception of type ExecutionFailException
                    if (inner is ExecutionFailException failException)
                    {
                        // Print the message it contains
                        Console.WriteLine($" {failException.Message}");
                    }
                }
            }
        }
    }
}