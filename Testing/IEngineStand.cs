using DvsTesting.Simulation;

namespace DvsTesting.Testing
{
    public interface IEngineStand
    {
        void Enclose(Engine engine);

        void Release();

        void OverheatTest();
    }
}