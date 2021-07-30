using System.Globalization;
using DvsTesting.SimulationLogic;

namespace DvsTesting.TestingLogic
{
    public static class EngineTestingStand
    {
        
        private static int _lastTestDuration;

        public static Engine EnclosedEngine
        {
            get;
            set;
        }

        public static void Release()
            => EnclosedEngine = null;
        
        public static void PerformNewTest()
        {
            _lastTestDuration = 0;
            
            EnclosedEngine.Reset();

            EnclosedEngine.Start();
            
            while (EnclosedEngine.Temperature < EnclosedEngine.OverheatTemperature)
            {
                EnclosedEngine.Work(EnvironmentState.Temperature);
                _lastTestDuration++;
            }
            
            EnclosedEngine.Stop();
        }
        

        public static string LastTestDuration 
            => _lastTestDuration.ToString(CultureInfo.CurrentCulture);
    }
}