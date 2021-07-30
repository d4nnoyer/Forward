using System.Globalization;
using DvsTesting.SimulationLogic;

namespace DvsTesting.TestingLogic
{
    public static class EngineTestingStand
    {
        
        private static int _lastTestDuration;
        
        public static void PerformNewTest(Engine engine, double envTemperature)
        {
            _lastTestDuration = 0;
            
            engine.Reset();

            engine.Start();
            
            while (engine.Temperature < engine.OverheatTemperature)
            {
                engine.Work(envTemperature);
                _lastTestDuration++;
            }
            
            engine.Stop();
        }
        

        public static string LastTestDuration 
            => _lastTestDuration.ToString(CultureInfo.CurrentCulture);
    }
}