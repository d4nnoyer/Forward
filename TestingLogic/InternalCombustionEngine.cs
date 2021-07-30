using System.Collections.Generic;
using DvsTesting.SimulationLogic;

namespace DvsTesting.TestingLogic
{
    class InternalCombustionEngine : Engine
    {

        public InternalCombustionEngine(double inertia, double tmax, double c, double hm, double hv, List<(double , double )> points)
        {
            EngineConfig.Inertia = inertia;
            EngineConfig.OverheatTemp = tmax;
            EngineConfig.HeatTransferCoef = c;
            EngineConfig.HeatDependencyOnMomentumCoef = hm;
            EngineConfig.HeatDependencyOnVolutionCoef = hv;
            EngineConfig.MomentumByVolution = new Interpolation(points);
        }
        
        public override void Update()
        {
            if (IsWorking)
            {
                VolutionIncreasePerSecond();
                TemperatureChangePerSecond(EnvironmentState.Temperature);
            }
        }

        public override void Reset()
        {
            Temperature = EnvironmentState.Temperature;
            Volution = 0;
        }

        public override void Start()
        {
            IsWorking = true;
        }

        public override void Stop()
        {
            IsWorking = false;
        }
        
    }
}