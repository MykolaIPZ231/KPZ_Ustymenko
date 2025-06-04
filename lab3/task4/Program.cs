using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace task4
{
    public interface ISmartTextReader
    {
        char[][] ReadFile(string filePath);
    }

    public class SmartTextReader : ISmartTextReader
    {
        public char[][] ReadFile(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            char[][] result = new char[lines.Length][];

            for(int i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i].ToCharArray();
            }
            return result;
        }
    }

    public class SmartTextChecker : ISmartTextReader
    {
        private readonly SmartTextReader _reader = new SmartTextReader();

        public char[][] ReadFile(string filePath)
        {
            Console.WriteLine($"відкриття {filePath}");
            try
            {
                char[][] result = _reader.ReadFile(filePath);
                Console.WriteLine($"успіх");

                int totalLines = result.Length;
                int totalChars = 0;
                foreach (var line in result)
                {
                    totalChars += line.Length;
                }

                Console.WriteLine($"всього ліній - {totalLines}");
                Console.WriteLine($"всього символів - {totalChars}");
                Console.WriteLine($"закриття - {filePath}");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("err");
                throw;
            }
        }
    }

    public class SmartTextReaderLocker : ISmartTextReader
    {
        private readonly SmartTextReader _reader = new SmartTextReader();
        private readonly Regex _restrictionPattern;

        public SmartTextReaderLocker(string pattern)
        {
            _restrictionPattern = new Regex(pattern, RegexOptions.IgnoreCase);
        }

        public char[][] ReadFile(string filePath)
        {
            if (_restrictionPattern.IsMatch(filePath))
            {
                Console.WriteLine("доступ обмежено");
                return Array.Empty<char[]>();
            }
            return _reader.ReadFile(filePath);
        }
    }

    internal class Program
    {
        static void PrintResult(char[][] result)
        {
            Console.WriteLine("контент файлу ");
            foreach(var line in result)
            {
                Console.WriteLine(new string(line));
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var reader = new SmartTextReader();
            var content = reader.ReadFile("lab3.txt");
            PrintResult(content);

            var checker = new SmartTextChecker();
            Console.WriteLine("\nтест проксі");
            checker.ReadFile("lab3.txt");

            var locker = new SmartTextReaderLocker(@"^restricted");
            Console.WriteLine("\nдозволений файл");
            locker.ReadFile("allowed.txt");
            Console.WriteLine("\nзаборонений файл");
            locker.ReadFile("restricted.txt");
        }
    }
}
