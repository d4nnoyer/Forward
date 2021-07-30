using System.Collections.Generic;
using System.Linq;

namespace DvsTesting
{
    public abstract class Engine 
    {
         public double Volution;
         protected double Temperature;
         public double Momentum 
             => PiecewiseLinearRelation(Volution, EngineInfo.MomentumByVolutionRelations);
         public double NoLoadAcceleration 
             => Momentum/EngineInfo.Inertia;

         
        protected ( double Inertia, 
                    double OverheatTemp,
                    double HeatTransferCoef,
                    double HeatDependencyOnMomentumCoef,
                    double HeatDependencyOnVolutionCoef,
                    List< (double momentum, double volution) > MomentumByVolutionRelations
                    )
                    EngineInfo = (  0.1, 0.0, 0.0, 0.0, 0.0, new List<(double momentum, double volution)>());

        
        public bool IsOverheat
            => (Temperature >= EngineInfo.OverheatTemp);

        //Нагрев от вращения коленвала
        private double TempIncreasePerSecond ()
            => (Momentum * EngineInfo.HeatDependencyOnMomentumCoef) +
               (Volution * Volution * EngineInfo.HeatDependencyOnVolutionCoef);

        //Стремлению к температурному балансу с окражющей средой
        private double TempCompensationPerSecond (double envT)
            => EngineInfo.HeatTransferCoef * (envT - Temperature);

        //Подаём температуру среды как аргумент так как она тоже может изменяться
        public void TemperatureChangePerSecond (double envT)
            => Temperature += TempIncreasePerSecond() + TempCompensationPerSecond(envT);
        
        //Ускорение вращения коленвала
        public void VolutionIncreasePerSecond ()
            => Volution += NoLoadAcceleration;

        //Работа двигателя, при которой ускоряется коленвал и увеличивается температура
        public void Work(double envT)
        {
            VolutionIncreasePerSecond();
            TemperatureChangePerSecond(envT);
        }
        
        protected Engine ()
        {
            Volution = 0.0;
            Temperature = 0.0;
        }

        public void Reset(double t)
        {
            Temperature = t;
            Volution = 0;
        }
        
        //Расчёт кусочно-линейной зависимости момента вращения от скорости вращения
        private static double PiecewiseLinearRelation(double vol, List<(double m, double v)> dots )
        {
            if (vol <= dots[0].v)
                return dots[0].m;
            if (vol >= dots.Last().v)
                return dots.Last().m;

            double mom = 0;

            for (int i = 1; i < dots.Count; ++i) //TODO заменить на бинарный поиск
            {
                if (vol <= dots[i].v)
                {
                    mom = dots[i - 1].m + 
                          ( (dots[i].m - dots[i - 1].m) * 
                            (vol - dots[i - 1].v) )
                          / (dots[i].v - dots[i - 1].v);
                    break;
                }
            }

            return mom;
        }


        
    }
}