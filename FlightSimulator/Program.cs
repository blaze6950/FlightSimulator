namespace FlightSimulator
{
    class Program
    {
        static void Main()
        {
            Simulator simulator = new Simulator();
            while (true)
            {
                simulator.MainFunction();
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
