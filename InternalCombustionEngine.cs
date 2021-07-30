using System;
using System.Collections.Generic;

namespace DvsTesting
{
    class InternalCombustionEngine : Engine 
    {

        public InternalCombustionEngine(double inertia, double Tmax, double C, double Hm, double Hv)
        {
            EngineInfo.Inertia = inertia;
            EngineInfo.OverheatTemp = Tmax;
            EngineInfo.HeatTransferCoef = C;
            EngineInfo.HeatDependencyOnMomentumCoef = Hm;
            EngineInfo.HeatDependencyOnVolutionCoef = Hv;
            EngineInfo.MomentumByVolutionRelations = new List<(double momentum, double volution)>()
            {
                (20, 0),
                (75, 75),
                (100, 150),
                (105, 200),
                (75, 250),
                (0, 300)
            };
                
        }
        
    }
}