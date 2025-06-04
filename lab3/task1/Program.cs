using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace task1
{
    public class Logger
    {
        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Err(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }

    public class FileWriter
    {
        private readonly string _filePath;
        public FileWriter(string filePath)
        {
            _filePath = filePath;
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
        }

        public void Write(string text)
        {
            using(var writer = File.AppendText(_filePath))
            {
                writer.Write(text);
            }
        }

        public void WriteLine(string text)
        {
            using(var writer = File.AppendText(_filePath))
            {
                writer.WriteLine(text);
                writer.Flush();
            }
        }
    }

    public class FileLoggerAdapter
    {
        private readonly FileWriter _fileWriter;

        public FileLoggerAdapter(string filePath)
        {
            _fileWriter = new FileWriter(filePath);
        }

        public void Log(string message)
        {
            _fileWriter.WriteLine($"[log] {DateTime.Now} - {message}");
        }

        public void Err(string message)
        {
            _fileWriter.WriteLine($"[err] {DateTime.Now} - {message}");
        }

        public void Warn(string message)
        {
            _fileWriter.WriteLine($"[warn] {DateTime.Now} - {message}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var consoleLogger = new Logger();
            consoleLogger.Log("qwe");
            consoleLogger.Err("qwe");
            consoleLogger.Warn("qwe");

            const string logFile = "application.log";
            var fileLogger = new FileLoggerAdapter(logFile);
            consoleLogger.Log("qwe");
            consoleLogger.Err("qwe");
            consoleLogger.Warn("qwe");

            Console.WriteLine("\nлог файл");
            if (File.Exists(logFile))
            {
                foreach(var line in File.ReadAllLines(logFile))
                {
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("err");
            }
        }
    }
}
