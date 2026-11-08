namespace InterfacesLab;

public static class ShapeInfo
{
    public static void PrintShapeInfo(IShape shape)
    {
        Console.WriteLine($"Фигура: {shape.GetType().Name}");
        Console.WriteLine($"  Площадь:  {shape.GetArea():F2}");
        Console.WriteLine($"  Периметр: {shape.GetPerimeter():F2}");
    }
}