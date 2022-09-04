using System;
using ServiceWire;

namespace Marqi.RGBServer
{
    public class ServiceWireLogger : ILog
    {
        public void Debug(string formattedMessage, params object[] args)
        {
            Console.WriteLine($"[DEBUG] {string.Format(formattedMessage, args)}");
        }

        public void Error(string formattedMessage, params object[] args)
        {
            Console.WriteLine($"[ERROR] {string.Format(formattedMessage, args)}");
        }

        public void Fatal(string formattedMessage, params object[] args)
        {
            Console.WriteLine($"[FATAL] {string.Format(formattedMessage, args)}");
        }

        public void Info(string formattedMessage, params object[] args)
        {
            Console.WriteLine($"[INFO] {string.Format(formattedMessage, args)}");
        }

        public void Warn(string formattedMessage, params object[] args)
        {
            Console.WriteLine($"[WARN] {string.Format(formattedMessage, args)}");
        }
    }
}