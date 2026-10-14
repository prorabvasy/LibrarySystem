using System.Text;

Console.OutputEncoding = Encoding.UTF8;

var point = new Point(1, 2);
point.Move(5, 8);
Console.WriteLine($"Point: ({point.X}, {point.Y})");

List<IDrawable> figures = [new Circle(3), new Rectangle(4, 5)];
foreach (var figure in figures) figure.Draw();
PrintShapeInfo(new Circle(2));
PrintShapeInfo(new Rectangle(3, 6));
I3DShape cube = new Cube(4);
Console.WriteLine($"Куб: площадь поверхности={cube.GetArea():F2}, периметр основания={cube.GetPerimeter():F2}, объём={cube.GetVolume():F2}");

IPrinter printer = new Printer();
IScanner scanner = new Scanner();
MultifunctionDevice mfd = new();
printer.Print("Документ"); scanner.Scan(); mfd.Print("Отчёт"); mfd.Scan(); mfd.Fax("123456");

ProcessPayment(new CreditCard(), 1250m);
ProcessPayment(new Cash(), 300m);
DoWork(new ConsoleLogger());
var fileLogger = new FileLogger("work.log");
DoWork(fileLogger);
Console.WriteLine("Лог записан в work.log");

static void PrintShapeInfo(IShape shape) =>
    Console.WriteLine($"{shape.GetType().Name}: площадь={shape.GetArea():F2}, периметр={shape.GetPerimeter():F2}");
static void ProcessPayment(IPayable method, decimal amount) => method.Pay(amount);
static void DoWork(ILogger logger) { logger.Log("Начало работы"); logger.Log("Работа завершена"); }

public interface IMovable { void Move(int x, int y); }
public sealed class Point(int x, int y) : IMovable {
    public int X { get; private set; } = x; public int Y { get; private set; } = y;
    public void Move(int x, int y) { X = x; Y = y; }
}
public interface IDrawable { void Draw(); }
public interface IShape { double GetArea(); double GetPerimeter(); }
public sealed class Circle(double radius) : IDrawable, IShape {
    public double GetArea() => Math.PI * radius * radius;
    public double GetPerimeter() => 2 * Math.PI * radius;
    public void Draw() => Console.WriteLine($"Рисуем круг радиуса {radius}");
}
public sealed class Rectangle(double width, double height) : IDrawable, IShape {
    public double GetArea() => width * height;
    public double GetPerimeter() => 2 * (width + height);
    public void Draw() => Console.WriteLine($"Рисуем прямоугольник {width}x{height}");
}
public interface I3DShape : IShape { double GetVolume(); }
public sealed class Cube(double side) : I3DShape {
    public double GetArea() => 6 * side * side;
    public double GetPerimeter() => 12 * side;
    public double GetVolume() => side * side * side;
}
public interface IDevice { void Print(string text); void Scan(); void Fax(string number); }
public interface IPrinter { void Print(string text); }
public interface IScanner { void Scan(); }
public interface IFax { void Fax(string number); }
public sealed class Printer : IPrinter { public void Print(string text) => Console.WriteLine($"Печать: {text}"); }
public sealed class Scanner : IScanner { public void Scan() => Console.WriteLine("Сканирование выполнено"); }
public sealed class MultifunctionDevice : IPrinter, IScanner, IFax {
    public void Print(string text) => Console.WriteLine($"МФУ печатает: {text}");
    public void Scan() => Console.WriteLine("МФУ сканирует документ");
    public void Fax(string number) => Console.WriteLine($"Факс отправлен на {number}");
}
public interface IPayable { void Pay(decimal amount); }
public sealed class CreditCard : IPayable { public void Pay(decimal amount) => Console.WriteLine($"Оплата картой: {amount:C}"); }
public sealed class Cash : IPayable { public void Pay(decimal amount) => Console.WriteLine($"Оплата наличными: {amount:C}"); }
public interface ILogger { void Log(string message); }
public sealed class ConsoleLogger : ILogger { public void Log(string message) => Console.WriteLine($"[LOG] {message}"); }
public sealed class FileLogger(string path) : ILogger {
    public void Log(string message) => File.AppendAllText(path, $"[{DateTime.Now:O}] {message}{Environment.NewLine}");
}
