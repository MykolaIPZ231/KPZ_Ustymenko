using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public interface IRenderer
    {
        void RenderShape(string shapeName);
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderShape(string shapeName)
        {
            Console.WriteLine($"{shapeName} як растр");
        }
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderShape(string shapeName)
        {
            Console.WriteLine($"{shapeName} як вектор");
        }
    }

    public abstract class Shape
    {
        protected IRenderer renderer;
        public string Name { get; protected set; }

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public abstract void Draw();
    }

    public class Circle : Shape
    {
        public Circle(IRenderer renderer) : base(renderer)
        {
            Name = "коло";
        }

        public override void Draw()
        {
            renderer.RenderShape(Name);
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer)
        {
            Name = "квадрат";
        }

        public override void Draw()
        {
            renderer.RenderShape(Name);
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer)
        {
            Name = "трикутник";
        }

        public override void Draw()
        {
            renderer.RenderShape(Name);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
