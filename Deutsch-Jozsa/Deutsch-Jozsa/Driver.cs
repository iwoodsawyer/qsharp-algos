using Microsoft.Quantum.Simulation.Core;
using Microsoft.Quantum.Simulation.Simulators;
using System;

namespace Quantum.DeutschJozsa
{
    class Driver
    {
        static void Main(string[] args)
        {
            try
            {
                using (var sim = new QuantumSimulator())
                {
                    int[] functions = new int[] { 1, 2, 3, 4, 5 };
                    foreach (int function in functions)
                    {
                        var res = DeutschJozsaTest.Run(sim, function, 2).Result;
                        if (res == 1)
                        {
                            System.Console.WriteLine($"Funcion: Function{function,-1} is constant");
                        }
                        else
                        {
                            System.Console.WriteLine($"Funcion: Function{function,-1} is balanced");
                        }
                    }
                }
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