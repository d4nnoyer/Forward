using System.Globalization;

namespace DvsTesting
{
    public static class EngineTestingStand
    {
        
        private static int _lastTestDuration;
        
        public static void StartNewTest(Engine engine, double envTemperature)
        {
            _lastTestDuration = 0;
            engine.Reset(envTemperature);
            while (!engine.IsOverheat)
            {
                engine.Work(envTemperature);
                _lastTestDuration++;
            }
        }
        

        public static string LastTestDuration 
            => _lastTestDuration.ToString(CultureInfo.CurrentCulture);
    }
}