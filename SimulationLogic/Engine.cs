using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DvsTesting.SimulationLogic
{
    public abstract class Engine : IEngine
    {

        
        protected double Volution;

        public double Temperature
        {
            get;
            protected set;
        }
        
        protected bool IsWorking
        {
            get;
            set;
        }
        
        public double Momentum
            => EngineConfig.MomentumByVolution.GetPiecewiceLinearRelation(Volution);
            
        public double AccelerationWithNoLoad 
            => Momentum/EngineConfig.Inertia;

        public double OverheatTemperature
            => EngineConfig.OverheatTemp;
         
        protected  ( double Inertia, 
                    double OverheatTemp,
                    double HeatTransferCoef,
                    double HeatDependencyOnMomentumCoef,
                    double HeatDependencyOnVolutionCoef,
                    Interpolation MomentumByVolution
                    )
                    EngineConfig 
                        = (  0.1, 0.0, 0.0, 0.0, 0.0, new Interpolation(new List<(double momentum, double volution)>()));


        //Нагрев от вращения коленвала
        private double TempIncreaseOnWorkPerSecond ()
            => (Momentum * EngineConfig.HeatDependencyOnMomentumCoef) +
               (Volution * Volution * EngineConfig.HeatDependencyOnVolutionCoef);

        //Стремлению к температурному балансу с окражющей средой
        private double TempCompensationPerSecond (double envT)
            => EngineConfig.HeatTransferCoef * (envT - Temperature);

        //Подаём температуру среды как аргумент так как она тоже может изменяться
        public void TemperatureChangePerSecond (double envT)
            => Temperature += TempIncreaseOnWorkPerSecond() + TempCompensationPerSecond(envT);
        
        //Ускорение вращения коленвала
        public void VolutionIncreasePerSecond ()
            => Volution += AccelerationWithNoLoad;
        
        
        public virtual void Update()
        {
        }

        public virtual void Reset()
        {
        }
        
        public virtual void Start()
        {
        }
        
        public virtual void Stop()
        {
        }

        public override string ToString()
        {
            string valuesV = string.Empty;
            string valuesMv = string.Empty;

            // foreach (var dot in EngineInfo.MomentumByVolutionRelations)
            // {
            //     valuesV += dot.volution.ToString(CultureInfo.CurrentCulture) + ", ";
            //     valuesMv += dot.momentum.ToString(CultureInfo.CurrentCulture) + ", ";
            // }

            return $"I = {EngineConfig.Inertia}\n" +
                   "V    = { " + valuesV + "} \n" +
                   "M(V) = { " + valuesMv + "} \n" +
                   $"Tперегрева = {EngineConfig.OverheatTemp} \n" +
                   $"Hм = {EngineConfig.HeatDependencyOnMomentumCoef} \n" +
                   $"Hv = {EngineConfig.HeatDependencyOnVolutionCoef} \n" +
                   $"C = {EngineConfig.HeatTransferCoef}";
        }
        
        
    }
}