using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task3
{
    public interface IRender
    {
        void RenderShape(string shapeName);
    }

    public class VectorRender : IRender
    {
        public void RenderShape(string shapeName)
        {
            Console.WriteLine($"{shapeName} as vector");
        }
    }

    public class RasterRander : IRender
    {
        public void RenderShape(string shapeName)
        {
            Console.WriteLine($"{shapeName} as raster");
        }
    }

    public abstract class Shape
    {
        protected IRender render;

        protected Shape( IRender render)
        {
            this.render = render;
        }

        public abstract void Draw();
    }

    public class Circle : Shape
    {
        public Circle(IRender render) : base(render) { }
        public override void Draw()
        {
            render.RenderShape("Circle");
        }
    }

    public class Square : Shape
    {
        public Square(IRender renderer) : base(renderer) { }
        public override void Draw()
        {
            render.RenderShape("Square");
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRender renderer) : base(renderer) { }
        public override void Draw()
        {
            render.RenderShape("Triangle");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            IRender vector = new VectorRender();
            IRender raster = new RasterRander();

            Shape[] shapes =
            {
                new Circle(vector),
                new Circle(raster),
                new Square(vector),
                new Square(raster),
                new Triangle(vector),
                new Triangle(raster)
            };

            foreach(var shape in shapes)
            {
                shape.Draw();
            }
        }
    }
}
