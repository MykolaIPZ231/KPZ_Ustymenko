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

        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
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
        }

        public void Write(string text)
        {
            File.AppendAllText(_filePath, text);
        }

        public void WriteLine(string text)
        {
            File.AppendAllText(_filePath, text + Environment.NewLine);
        }
    }

    public class FileLogger
    {
        private readonly FileWriter _fileWriter;

        public FileLogger(FileWriter fileWriter)
        {
            _fileWriter = fileWriter;
        }

        public void Log(string message)
        {
            _fileWriter.WriteLine($"[LOG] {message}");
        }

        public void Error(string message)
        {
            _fileWriter.WriteLine($"[ERROR] {message}");
        }

        public void Warn(string message)
        {
            _fileWriter.WriteLine($"[WARN] {message}");
        }
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("= Консольний логер =");
            var consoleLogger = new Logger();
            consoleLogger.Log("Інформаційне повідомлення");
            consoleLogger.Error("Критична помилка");
            consoleLogger.Warn("Попередження");

            Console.WriteLine("\n= Файловий логер (через адаптер) =");

            var fileWriter = new FileWriter("log.txt");
            var fileLogger = new FileLogger(fileWriter);

            fileLogger.Log("Запис інформації у файл");
            fileLogger.Error("Запис помилки у файл");
            fileLogger.Warn("Запис попередження у файл");

            Console.WriteLine("\nВміст файлу:");
            Console.WriteLine(File.ReadAllText("log.txt"));
        }
    }
}
