using System.Collections.Generic;

namespace DvsTesting.SimulationLogic
{
    class InternalCombustionEngine : Engine
    {

        public InternalCombustionEngine(double inertia, double tmax, double c, double hm, double hv)
        {
            EngineInfo.Inertia = inertia;
            EngineInfo.OverheatTemp = tmax;
            EngineInfo.HeatTransferCoef = c;
            EngineInfo.HeatDependencyOnMomentumCoef = hm;
            EngineInfo.HeatDependencyOnVolutionCoef = hv;
            EngineInfo.MomentumByVolutionRelations = new List<(double , double )>()
            {
                (20, 0),
                (75, 75),
                (100, 150),
                (105, 200),
                (75, 250),
                (0, 300)
            };
                
        }

        public new void Reset(double environmentTemperature)
        {
            // Temperature = environmentTemperature;
            // Volution = 0;
        }

        public new void Start()
        {
            // IsWorking = true;
        }

        public new void Stop()
        {
            
        }
        
    }
}