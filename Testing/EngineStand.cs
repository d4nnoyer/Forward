using System.Globalization;
using System.Runtime.InteropServices;
using DvsTesting.Simulation;

namespace DvsTesting.Testing
{
    public class EngineStand : IEngineStand
    {
        
        private int _lastTestDuration;

        private static Engine _enclosedEngine;

        public  void Release()
            => _enclosedEngine = null;

        public void Enclose(Engine engine)
            => _enclosedEngine = engine;


        
        public  void OverheatTest()
        {
            if (_enclosedEngine == null)
            {
                throw new ExternalException();
            }
            
            _lastTestDuration = 0;
            
            _enclosedEngine.Reset();
            _enclosedEngine.Start();
            
            while (!IsOverheat)
            {
                _enclosedEngine.Update();
                _lastTestDuration++;
            }
            
            _enclosedEngine.Stop();
        }
        
        private  bool IsOverheat
            => (_enclosedEngine.Temperature >= _enclosedEngine.OverheatTemperature);
        
        public  string LastTestDuration 
            => _lastTestDuration.ToString(CultureInfo.CurrentCulture);

    }
}