using System.Collections.Generic;
using System.Linq;

namespace DvsTesting.SimulationLogic
{
    public class Interpolation
    {
        private readonly List<(double Y, double X)> _points;
        
        public Interpolation(List<(double, double)> dots)
        {
            _points = dots;
        }

        /// <summary>
        /// Возвращает значение функции кусочно-линейной зависимости
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double GetPiecewiceLinearRelation(double value)
        {
            if (value <= _points[0].X)
                return _points[0].Y;
            if (value >= _points.Last().X)
                return _points.Last().Y;

            double result = 0;

            for (int i = 1; i < _points.Count; ++i) //TODO заменить на бинарный поиск
            {
                if (value <= _points[i].X)
                {
                    result = _points[i-1].Y + 
                             ( (_points[i].Y - _points[i-1].Y) * 
                               (value - _points[i-1].X) )
                             / (_points[i].X - _points[i-1].X);
                    break;
                }
            }

            return result;
        }
    }
}