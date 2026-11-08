// Drawer.cs
namespace InterfacesLab;

public static class Drawer
{
    public static void DrawAll(List<IDrawable> shapes)
    {
        foreach (var shape in shapes)
            shape.Draw();
    }
}