using System.Collections.Generic;
using System.Linq;

namespace DvsTesting.Simulation
{
    public class Interpolation
    {
        private readonly List<(double Y, double X)> _points;
        
        public Interpolation(List<(double, double)> points)
        {
            _points = points;
        }

        /// <summary>
        /// Возвращает значение функции кусочно-линейной зависимости
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double GetPiecewiceLinearRelation(double value)
        {
            if (value <= _points.First().X)
                return _points.First().Y;
            if (value >= _points.Last().X)
                return _points.Last().Y;

            double derivedValue = 0;

            for (int i = 1; i < _points.Count; ++i) //TODO заменить на бинарный поиск
            {
                if (value <= _points[i].X)
                {
                    var currentPoint = _points[i];
                    var previousPoint = _points[i-1];
                    
                    // derivedValue = _points[i-1].Y + 
                    //          ( (_points[i].Y - _points[i-1].Y) * 
                    //            (value - _points[i-1].X) )
                    //          / (_points[i].X - _points[i-1].X);
                    
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