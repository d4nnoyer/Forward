using System;

namespace DvsTesting
{
    static class Program
    {
        static void Main(string[] args)
        {
            Engine eng = new InternalCombustionEngine(10, 110, 0.1, 0.01, 0.0001);
            
            EngineTestingStand.StartNewTest(eng, 23);
            Console.WriteLine(EngineTestingStand.LastTestDuration);
            //
            // eng.Volution = 260;
            // var m = eng.Momentum;
            // Console.WriteLine(m);

            Console.WriteLine("Press any");
            Console.ReadKey();
        }
    }
    
}
