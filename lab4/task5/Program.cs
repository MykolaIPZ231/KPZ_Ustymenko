using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    class TextDocument
    {
        private string _content;
        public TextDocument(string content = "")
        {
            _content = content;
        }

        public void AddText(string text)
        {
            _content += text;
        }

        public void Clear()
        {
            _content = string.Empty;
        }

        public void SetContent(string content)
        {
            _content = content;
        }

        public string GetContent()
        {
            return _content;
        }

        public TextDocumentMemento CreateMemento()
        {
            return new TextDocumentMemento(_content);
        }

        public void RestoreMemento(TextDocumentMemento memento)
        {
            _content = memento.GetSavedContent();
        }
    }

    class TextDocumentMemento
    {
        private readonly string _savedContent;

        public TextDocumentMemento(string content)
        {
            _savedContent = content;
        }

        public string GetSavedContent()
        {
            return _savedContent;
        }
    }

    class TextEditor
    {
        private readonly TextDocument _document;
        private readonly Stack<TextDocumentMemento> _history;

        public TextEditor()
        {
            _document = new TextDocument();
            _history  = new Stack<TextDocumentMemento>();
        }

        public void SaveState()
        {
            _history.Push(_document.CreateMemento());
            Console.WriteLine("стан збережено");
        }

        public void TypeText(string text)
        {
            SaveState();
            _document.AddText(text);
            Console.WriteLine($"додано -  {text}");
        }

        public void ClearDocument()
        {
            SaveState();
            _document.Clear();
            Console.WriteLine("документ очищено");
        }

        public void Undo()
        {
            if (_history.Count > 0)
            {
                _document.RestoreMemento(_history.Pop());
                Console.WriteLine("скасовано останню дію");
            }
            else
            {
                Console.WriteLine("err")
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
