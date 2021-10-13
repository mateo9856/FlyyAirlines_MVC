using System;

namespace FlyyAirlines.Repository
{
    public class FileLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
