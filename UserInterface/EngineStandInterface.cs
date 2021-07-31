using System;
using DvsTesting.Simulation;
using DvsTesting.Testing;

namespace DvsTesting.UserInterface
{
    public static class EngineStandInterface
    {
        private static EngineStand _controllingStand;

        public static void Connect(EngineStand engineStand)
            => _controllingStand = engineStand;
        
        
        public static void Dispose() 
            => _controllingStand = null;


        private static void CheckInEngineSet()
        {
            if (_controllingStand == null) throw new Exception("Не установлен контролируемый тестовый стенд.");
        }
        
        public static void AskForEnvTemperature()
        {
            double number;
            string question = "Введите температуру окружающей среды в градусах Цельсия";

            Console.Write(question + ": ");
            string response = Console.ReadLine();

            while (!double.TryParse(response, out number))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Введённое значение не является числом!");
                Console.ResetColor();
                Console.Write(question + ": ");
                response = Console.ReadLine();
            }
            
            EnvironmentState.Temperature =  number;
        }

        public static void PrintEngineConfig()
        {
            CheckInEngineSet();

            _controllingStand.PrintEngineConfig();
        }

        public static void RunOverheatTest()
        {
            CheckInEngineSet();

            _controllingStand.OverheatTest();
        }

        public static void PrintOverheatTestResult()
        {
            CheckInEngineSet();

            if (_controllingStand.LastTestReachedOverheat)
            {
                Console.WriteLine($"Температура перегрева достигнута за {_controllingStand.LastTestDuration} секунд.");
            }
            else
            {
                Console.WriteLine($"Двигатель прекратил нагрев на отметке {_controllingStand.MaxReachedTemperature} градусов Цельсия" +
                                  $" спустя {_controllingStand.LastTestDuration} секунд после начала теста." +
                                  $"\nТемпература перегрева не достигнута при текущих условиях среды и конфигурации и состоянии двигателя.. ");
            }
        }
    }
}