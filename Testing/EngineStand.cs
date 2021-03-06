using System;
using System.Globalization;
using DvsTesting.Simulation;

namespace DvsTesting.Testing
{
    public class EngineStand : IEngineStand
    {
        
        private uint _lastTestDuration;
        private double _previousEngineTemperature;

        private Engine _enclosedEngine;
        

        public bool LastTestReachedOverheat { get; private set; }

        public double MaxReachedTemperature { get; private set; }
        
        private  bool IsOverheat
            => (_enclosedEngine.Temperature >= _enclosedEngine.OverheatTemperature);

        private double dT
            => Math.Abs(_enclosedEngine.Temperature - _previousEngineTemperature);

        private bool IsTemperatureIncreasing
            => (dT >= 0.01);
        
        public  string LastTestDuration 
            => _lastTestDuration.ToString(CultureInfo.CurrentCulture);

        public string EngineConfig 
            => _enclosedEngine.ToString();
        
        
        
        public virtual  void Release()
            => _enclosedEngine = null;

        public virtual void Enclose(Engine engine)
            => _enclosedEngine = engine;

        private void CheckIfEngineIsSet()
        {
            if (_enclosedEngine == null)
            {
                throw new Exception("В тестовый стенд не помещён ни один двигатель для тестирования.");
            }
        }

        public virtual void OverheatTest()
        {
            CheckIfEngineIsSet();
            
            _lastTestDuration = 0;
            
            _enclosedEngine.Reset();
            _enclosedEngine.Start();

            _previousEngineTemperature = _enclosedEngine.Temperature;
            
            while (!IsOverheat)
            {
                _enclosedEngine.Update();
                _lastTestDuration++;
                
                if (!IsTemperatureIncreasing)
                {
                    MaxReachedTemperature = _enclosedEngine.Temperature;
                    LastTestReachedOverheat = false;
                    break;
                }
                
                _previousEngineTemperature = _enclosedEngine.Temperature;
            }

            if (IsOverheat)
                LastTestReachedOverheat = true;
            
            _enclosedEngine.Stop();
        }
        

    }
}