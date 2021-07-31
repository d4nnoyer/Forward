﻿using System;
using System.Collections.Generic;
using DvsTesting.Simulation;
using DvsTesting.Testing;

namespace DvsTesting.UserInterface
{
    static class Program
    {
        static void Main()
        {
            EngineStand.EnclosedEngine = new InternalCombustionEngine(10, 110, 0.1, 0.01, 0.0001,
                new List<(double, double)>()
                {
                    (20, 0),
                    (75, 75),
                    (100, 150),
                    (105, 200),
                    (75, 250),
                    (0, 300)
                });
            
            // EngineStandInterface.AskForEnvTemperature();
            EnvironmentState.Temperature = 23;
            EngineStand.PerformNewTest();
            Console.WriteLine(EngineStand.LastTestDuration);
            
            EnvironmentState.Temperature = 36;
            EngineStand.PerformNewTest();
            Console.WriteLine(EngineStand.LastTestDuration);
            
            EngineStand.Release();

            Console.WriteLine("Press any");
            Console.ReadKey();
        }
    }
    
}