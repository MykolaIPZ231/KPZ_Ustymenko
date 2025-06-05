using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task5
{
    public abstract class LightNode
    {
        public abstract string OuterHtml { get; }
        public abstract string InnerHtml { get; }
    }

    public class LightTextNode : LightNode
    {
        public string Text { get; }

        public LightTextNode(string text)
        {
            Text = text;
        }

        public override string OuterHtml => Text;
        public override string InnerHtml => Text;
    }

    public class LightElementNode : LightNode
    {
        public string TagName { get; }
        public DisplayType Display { get; }
        public ClosingType Closing { get; }
        public List<string> CssClasses { get; }
        public List<LightNode> Children { get; }

        public LightElementNode(string tagName, DisplayType display, ClosingType closing)
        {
            TagName = tagName;
            Display = display;
            Closing = closing;
            CssClasses = new List<string>();
            Children = new List<LightNode>();
        }

        public void AddChild(LightNode child)
        {
            if (Closing == ClosingType.SelfClosing)
            {
                throw new InvalidOperationException("self-closing tags cannot have children");
            }
            Children.Add(child);
        }

        public void AddClass(string cssClass)
        {
            CssClasses.Add(cssClass);
        }

        public override string OuterHtml
        {
            get
            {
                StringBuilder html = new StringBuilder();
                html.Append($"<{TagName}");

                if (CssClasses.Count > 0)
                {
                    html.Append($" class=\"{string.Join(" ", CssClasses)}\"");
                }

                if (Closing == ClosingType.SelfClosing)
                {
                    html.Append(" />");
                    return html.ToString();
                }

                html.Append(">");

                html.Append(InnerHtml);

                html.Append($"</{TagName}>");

                return html.ToString();
            }
        }

        public override string InnerHtml
        {
            get
            {
                if (Closing == ClosingType.SelfClosing)
                {
                    return string.Empty;
                }

                StringBuilder inner = new StringBuilder();
                foreach (var child in Children)
                {
                    inner.Append(child.OuterHtml);
                }
                return inner.ToString();
            }
        }
    }

    public enum DisplayType
    {
        Block,
        Inline
    }

    public enum ClosingType
    {
        Normal,
        SelfClosing
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("= lightHTML Markup Language =");

            var table = new LightElementNode("table", DisplayType.Block, ClosingType.Normal);
            table.AddClass("users-table");

            var caption = new LightElementNode("caption", DisplayType.Block, ClosingType.Normal);
            caption.AddChild(new LightTextNode("cписок користувачів"));
            table.AddChild(caption);

            var headerRow = new LightElementNode("tr", DisplayType.Block, ClosingType.Normal);

            var headerCell1 = new LightElementNode("th", DisplayType.Inline, ClosingType.Normal);
            headerCell1.AddChild(new LightTextNode("ID"));
            headerRow.AddChild(headerCell1);

            var headerCell2 = new LightElementNode("th", DisplayType.Inline, ClosingType.Normal);
            headerCell2.AddChild(new LightTextNode("iм'я"));
            headerRow.AddChild(headerCell2);

            var headerCell3 = new LightElementNode("th", DisplayType.Inline, ClosingType.Normal);
            headerCell3.AddChild(new LightTextNode("email"));
            headerRow.AddChild(headerCell3);

            table.AddChild(headerRow);

            var users = new[]
            {
                new { Id = 1, Name = "іван петренко", Email = "ivan@example.com" },
                new { Id = 2, Name = "марія сидорова", Email = "maria@example.com" },
                new { Id = 3, Name = "олексій коваленко", Email = "oleksiy@example.com" }
            };

            foreach (var user in users)
            {
                var row = new LightElementNode("tr", DisplayType.Block, ClosingType.Normal);

                var cell1 = new LightElementNode("td", DisplayType.Inline, ClosingType.Normal);
                cell1.AddChild(new LightTextNode(user.Id.ToString()));
                row.AddChild(cell1);

                var cell2 = new LightElementNode("td", DisplayType.Inline, ClosingType.Normal);
                cell2.AddChild(new LightTextNode(user.Name));
                row.AddChild(cell2);

                var cell3 = new LightElementNode("td", DisplayType.Inline, ClosingType.Normal);
                cell3.AddChild(new LightTextNode(user.Email));
                row.AddChild(cell3);

                table.AddChild(row);
            }

            var body = new LightElementNode("body", DisplayType.Block, ClosingType.Normal);
            body.AddChild(table);

            var hr = new LightElementNode("hr", DisplayType.Block, ClosingType.SelfClosing);
            body.AddChild(hr);

            var footer = new LightElementNode("div", DisplayType.Block, ClosingType.Normal);
            footer.AddClass("footer");
            footer.AddChild(new LightTextNode("LightHTML"));
            body.AddChild(footer);

            Console.WriteLine("\n= згенерований HTML =");
            Console.WriteLine(body.OuterHtml);

            Console.WriteLine("\n= innerHtml таблиці =");
            Console.WriteLine(table.InnerHtml);

            Console.WriteLine("\n= outerHtml горизонтальної лінії =");
            Console.WriteLine(hr.OuterHtml);
        }
    }
}
