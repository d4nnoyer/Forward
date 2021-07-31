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

            Engine testEngine = new InternalCombustionEngine(inertia: 10, tmax: 110, c: 0.1, hm: 0.01, hv: 0.0001,
                points: new List<(double, double)>()
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
            EngineStandInterface.Connect(forwardStand);

            
            EngineStandInterface.PrintEngineConfig();
            EngineStandInterface.AskForEnvTemperature();
            EngineStandInterface.RunOverheatTest();
            EngineStandInterface.PrintOverheatTestResult();
            
            EngineStandInterface.Dispose();
            forwardStand.Release();
            
            Console.WriteLine("\nРабота со стендом окончена. \nНажмите любую клавишу для продолжения.");
            Console.ReadKey();
        }
    }
    
}
