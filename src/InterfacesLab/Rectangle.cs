// Rectangle.cs
namespace InterfacesLab;

public class Rectangle : IDrawable
{
    public double Width { get; }
    public double Height { get; }

    public Rectangle(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public void Draw()
    {
        Console.WriteLine($"Рисую прямоугольник {Width}x{Height}");
    }
}