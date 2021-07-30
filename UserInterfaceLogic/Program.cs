using System;
using DvsTesting.SimulationLogic;
using DvsTesting.TestingLogic;

namespace DvsTesting.UserInterfaceLogic
{
    static class Program
    {
        static void Main()
        {
            InternalCombustionEngine eng = new InternalCombustionEngine(10, 110, 0.1, 0.01, 0.0001);
            
            EngineTestingStand.PerformNewTest(eng, 23);
            Console.WriteLine(EngineTestingStand.LastTestDuration);
            
            EngineTestingStand.PerformNewTest(eng, 36);
            Console.WriteLine(EngineTestingStand.LastTestDuration);

            Console.WriteLine("Press any");
            Console.ReadKey();
        }
    }
    
}
