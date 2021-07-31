using System.Collections.Generic;
using System.Globalization;

namespace DvsTesting.Simulation
{
    public abstract class Engine : IEngine
    {
        protected double Volution;

        public double Temperature { get; protected set; }
        
        protected bool IsWorking { get; set; }

        protected double Momentum
            => EngineConfig.MomentumByVolution.GetPiecewiceLinearRelation(Volution);

        protected double AccelerationWithNoLoad 
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
        protected double TempIncreaseOnWorkPerSecond ()
            => (Momentum * EngineConfig.HeatDependencyOnMomentumCoef) +
               (Volution * Volution * EngineConfig.HeatDependencyOnVolutionCoef);

        //Стремлению к температурному балансу с окражющей средой
        protected double TempCompensationPerSecond (double envT)
            => EngineConfig.HeatTransferCoef * (envT - Temperature);

        //Подаём температуру среды как аргумент так как она тоже может изменяться
        protected void TemperatureChangePerSecond (double envT)
            => Temperature += TempIncreaseOnWorkPerSecond() + TempCompensationPerSecond(envT);
        
        //Ускорение вращения коленвала
        protected void VolutionIncreasePerSecond ()
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
            string pairs = string.Empty;

            foreach (var dot in EngineConfig.MomentumByVolution.Points)
            {
                pairs += "( " + dot.Y.ToString(CultureInfo.CurrentCulture) + ", " +
                                dot.X.ToString(CultureInfo.CurrentCulture) + " ), ";
            }

            return $"I = {EngineConfig.Inertia}\n" +
                   "M(V), V  = { " + pairs + "} \n" +
                   $"Tmax = {EngineConfig.OverheatTemp} \n" +
                   $"Hм = {EngineConfig.HeatDependencyOnMomentumCoef} \n" +
                   $"Hv = {EngineConfig.HeatDependencyOnVolutionCoef} \n" +
                   $"C = {EngineConfig.HeatTransferCoef}";
        }
        
        
    }
}