using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DvsTesting.SimulationLogic
{
    public abstract class Engine : IEngine
    {
        protected bool IsWorking
        {
            get;
            set;
        }
        
        protected double Volution;

        public double Temperature
        {
            get;
            protected set;
        }
         
        public double Momentum 
            => PiecewiseLinearRelation(Volution, EngineInfo.MomentumByVolutionRelations);
         
        public double AccelerationWithNoLoad 
            => Momentum/EngineInfo.Inertia;

        public double OverheatTemperature
            => EngineInfo.OverheatTemp;
         
        protected ( double Inertia, 
                    double OverheatTemp,
                    double HeatTransferCoef,
                    double HeatDependencyOnMomentumCoef,
                    double HeatDependencyOnVolutionCoef,
                    List< (double momentum, double volution) > MomentumByVolutionRelations
                    )
                    EngineInfo = (  0.1, 0.0, 0.0, 0.0, 0.0, new List<(double momentum, double volution)>());


        //Нагрев от вращения коленвала
        private double TempIncreaseOnWorkPerSecond ()
            => (Momentum * EngineInfo.HeatDependencyOnMomentumCoef) +
               (Volution * Volution * EngineInfo.HeatDependencyOnVolutionCoef);

        //Стремлению к температурному балансу с окражющей средой
        private double TempCompensationPerSecond (double envT)
            => EngineInfo.HeatTransferCoef * (envT - Temperature);

        //Подаём температуру среды как аргумент так как она тоже может изменяться
        public void TemperatureChangePerSecond (double envT)
            => Temperature += TempIncreaseOnWorkPerSecond() + TempCompensationPerSecond(envT);
        
        //Ускорение вращения коленвала
        public void VolutionIncreasePerSecond ()
            => Volution += AccelerationWithNoLoad;

        //Работа двигателя, при которой ускоряется коленвал и увеличивается температура
        public void Work(double envT)
        {
            if (IsWorking)
            {
                VolutionIncreasePerSecond();
                TemperatureChangePerSecond(envT);
            }
        }

        public void Reset()
        {
            Temperature = EnvironmentState.Temperature;
            Volution = 0;
        }
        
        public void Start()
        {
            IsWorking = true;
        }
        
        public void Stop()
        {
            IsWorking = false;
        }

        public override string ToString()
        {
            string valuesV = string.Empty;
            string valuesMv = string.Empty;

            foreach (var dot in EngineInfo.MomentumByVolutionRelations)
            {
                valuesV += dot.volution.ToString(CultureInfo.CurrentCulture) + ", ";
                valuesMv += dot.momentum.ToString(CultureInfo.CurrentCulture) + ", ";
            }

            return $"I = {EngineInfo.Inertia}\n" +
                   "V    = { " + valuesV + "} \n" +
                   "M(V) = { " + valuesMv + "} \n" +
                   $"Tперегрева = {EngineInfo.OverheatTemp} \n" +
                   $"Hм = {EngineInfo.HeatDependencyOnMomentumCoef} \n" +
                   $"Hv = {EngineInfo.HeatDependencyOnVolutionCoef} \n" +
                   $"C = {EngineInfo.HeatTransferCoef}";
        }
        
        //Расчёт кусочно-линейной зависимости момента вращения от скорости вращения
        private static double PiecewiseLinearRelation(double volution, List<(double momentum, double volution)> dots )
        {
            if (volution <= dots[0].volution)
                return dots[0].momentum;
            if (volution >= dots.Last().volution)
                return dots.Last().momentum;

            double mom = 0;

            for (int i = 1; i < dots.Count; ++i) //TODO заменить на бинарный поиск
            {
                if (volution <= dots[i].volution)
                {
                    mom = dots[i-1].momentum + 
                          ( (dots[i].momentum - dots[i-1].momentum) * 
                            (volution - dots[i-1].volution) )
                          / (dots[i].volution - dots[i-1].volution);
                    break;
                }
            }

            return mom;
        }
        
    }
}