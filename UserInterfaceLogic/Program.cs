using System;
using DvsTesting.SimulationLogic;
using DvsTesting.TestingLogic;

namespace DvsTesting.UserInterfaceLogic
{
    static class Program
    {
        static void Main()
        {
            EngineTestingStand.EnclosedEngine = new InternalCombustionEngine(10, 110, 0.1, 0.01, 0.0001);
            
            EngineStandInterface.AskForEnvTemperature();
            EngineTestingStand.PerformNewTest();
            Console.WriteLine(EngineTestingStand.LastTestDuration);
            
            
            EngineTestingStand.PerformNewTest();
            Console.WriteLine(EngineTestingStand.LastTestDuration);

            Console.WriteLine("Press any");
            Console.ReadKey();
        }
    }
    
}
