using System.Globalization;
using System.Runtime.InteropServices;
using DvsTesting.Simulation;

namespace DvsTesting.Testing
{
    public static class EngineStand
    {
        
        private static int _lastTestDuration;

        public static Engine EnclosedEngine
        {
            get;
            set;
        }

        public static void Release()
            => EnclosedEngine = null;

        private static bool IsOverheat
            => (EnclosedEngine.Temperature >= EnclosedEngine.OverheatTemperature);
        
        public static void PerformNewTest()
        {
            if (EnclosedEngine == null)
            {
                throw new ExternalException();//Хз какой именно здесь эксепшн кидать
            }
            
            _lastTestDuration = 0;
            
            EnclosedEngine.Reset();
            EnclosedEngine.Start();
            
            while (!IsOverheat)
            {
                EnclosedEngine.Update();
                _lastTestDuration++;
            }
            
            EnclosedEngine.Stop();
        }
        

        public static string LastTestDuration 
            => _lastTestDuration.ToString(CultureInfo.CurrentCulture);
    }
}