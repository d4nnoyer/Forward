﻿namespace DvsTesting.SimulationLogic
{
    public interface IEngine
    {
        /// <summary>
        /// Задаёт логику состояния запущенного двигателя.
        /// </summary>
        void Start();

        /// <summary>
        /// Задаёт логику состояния неактивного двигателя.
        /// </summary>
        void Stop();

        /// <summary>
        /// Задаёт логику переподготовки двигателя: остановка вращения коленвала,
        /// охлаждение до температуры окружающей среды и т.д.
        /// </summary>
        /// <param name="environmentTemperature"></param>
        void Reset(double environmentTemperature);
    }
}