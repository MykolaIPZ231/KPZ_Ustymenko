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
        char[][] ReadText(string filePath);
    }

    public class SmartTextReader : ISmartTextReader
    {
        public char[][] ReadText(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
            char[][] result = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i].ToCharArray();
            }

            return result;
        }
    }

    public class SmartTextChecker : ISmartTextReader
    {
        private readonly SmartTextReader _reader;

        public SmartTextChecker()
        {
            _reader = new SmartTextReader();
        }

        public char[][] ReadText(string filePath)
        {
            Console.WriteLine($"відкриваємо файл: {filePath}");

            try
            {
                char[][] result = _reader.ReadText(filePath);

                int totalLines = result.Length;
                int totalChars = 0;
                foreach (char[] line in result)
                {
                    totalChars += line.Length;
                }

                Console.WriteLine($"файл успішно прочитано: {filePath}");
                Console.WriteLine($"рядків прочитано: {totalLines}");
                Console.WriteLine($"символів прочитано: {totalChars}");
                Console.WriteLine($"закриваємо файл: {filePath}");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"помилка при читанні файлу: {ex.Message}");
                throw;
            }
        }
    }

    public class SmartTextReaderLocker : ISmartTextReader
    {
        private readonly SmartTextReader _reader;
        private readonly Regex _accessPattern;

        public SmartTextReaderLocker(string pattern)
        {
            _reader = new SmartTextReader();
            _accessPattern = new Regex(pattern);
        }

        public char[][] ReadText(string filePath)
        {
            if (_accessPattern.IsMatch(filePath))
            {
                Console.WriteLine($"аccess to {filePath} denied");
                return Array.Empty<char[]>();
            }

            return _reader.ReadText(filePath);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string testFilePath = "test.txt";
            string restrictedFile = "restricted.txt";

            CreateTestFile(testFilePath);
            CreateTestFile(restrictedFile);

            Console.WriteLine("= базовий SmartTextReader =");
            ISmartTextReader basicReader = new SmartTextReader();
            DisplayFileContent(basicReader, testFilePath);

            Console.WriteLine("\n= SmartTextChecker (логуючий проксі) =");
            ISmartTextReader loggingReader = new SmartTextChecker();
            DisplayFileContent(loggingReader, testFilePath);

            Console.WriteLine("\n= SmartTextReaderLocker (обмежуючий проксі) =");
            ISmartTextReader restrictedReader = new SmartTextReaderLocker("restricted");

            Console.WriteLine("спроба прочитати дозволений файл:");
            DisplayFileContent(restrictedReader, testFilePath);

            Console.WriteLine("\nспроба прочитати заборонений файл:");
            DisplayFileContent(restrictedReader, restrictedFile);
        }

        static void DisplayFileContent(ISmartTextReader reader, string filePath)
        {
            try
            {
                char[][] content = reader.ReadText(filePath);

                if (content.Length == 0)
                {
                    Console.WriteLine("(файл порожній або доступ заборонено)");
                    return;
                }

                Console.WriteLine($"вміст файлу {filePath}:");
                for (int i = 0; i < content.Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {new string(content[i])}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"помилка: {ex.Message}");
            }
        }

        static void CreateTestFile(string path)
        {
            try
            {
                File.WriteAllText(path, "qwe\nasd\nzxc", Encoding.UTF8);
            }
            catch
            {

            }
        }
    }
}
