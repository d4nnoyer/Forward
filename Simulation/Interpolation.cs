using System.Collections.Generic;
using System.Linq;

namespace DvsTesting.Simulation
{
    public class Interpolation
    {
        public readonly List<(double Y, double X)> Points;
        
        public Interpolation(List<(double, double)> points)
        {
            Points = points;
        }

        /// <summary>
        /// Возвращает значение функции кусочно-линейной зависимости
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double GetPiecewiceLinearRelation(double value)
        {
            if (value <= Points.First().X)
                return Points.First().Y;
            if (value >= Points.Last().X)
                return Points.Last().Y;

            double derivedValue = 0;

            for (int i = 1; i < Points.Count; ++i) 
            {
                if (value <= Points[i].X)
                {
                    var currentPoint = Points[i];
                    var previousPoint = Points[i-1];

                    derivedValue = previousPoint.Y + 
                                   ( (currentPoint.Y - previousPoint.Y) * 
                                     (value - previousPoint.X) )
                                   / (currentPoint.X - previousPoint.X);
                    break;
                }
            }

            return derivedValue;
        }
    }
}