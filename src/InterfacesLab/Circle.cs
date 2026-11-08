namespace InterfacesLab;

public class Circle : IDrawable, IShape
{
    public double Radius { get; }
    public Circle(double radius) => Radius = radius;

    public void Draw() => Console.WriteLine($"Рисую круг радиусом {Radius}");
    public double GetArea() => Math.PI * Radius * Radius;
    public double GetPerimeter() => 2 * Math.PI * Radius;
}