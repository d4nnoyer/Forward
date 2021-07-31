using System;
using System.Collections.Generic;
using DvsTesting.Simulation;
using DvsTesting.Testing;

namespace DvsTesting.UserInterface
{
    static class Program
    {
        static void Main()
        {
            Engine testEngine = new InternalCombustionEngine(10, 110, 0.1, 0.01, 0.0001,
                new List<(double, double)>()
                {
                    (20, 0),
                    (75, 75), 
                    (100, 150),
                    (105, 200),
                    (75, 250),
                    (0, 300)
                });
            
            EngineStand forwardStand = new EngineStand();
            
            forwardStand.Enclose(testEngine);
            // EngineStandInterface.AskForEnvTemperature();
            EnvironmentState.Temperature = 20;
            forwardStand.OverheatTest();
            Console.WriteLine(forwardStand.LastTestDuration);
            
            // EnvironmentState.Temperature = 23;
            // EngineStand.PerformNewTest();
            // Console.WriteLine(EngineStand.LastTestDuration);
            
            forwardStand.Release();

            Console.WriteLine("Press any");
            Console.ReadKey();
        }
    }
    
}
