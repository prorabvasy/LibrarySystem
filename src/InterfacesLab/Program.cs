using InterfacesLab;
using InterfacesLab.Devices;
using InterfacesLab.Payments;
using InterfacesLab.Logging;

Console.OutputEncoding = System.Text.Encoding.UTF8;

Console.WriteLine("========== Задание 1. Базовые интерфейсы ==========");
var point = new Point(1, 2);
Console.WriteLine($"До Move: {point}");
point.Move(5, 3);
Console.WriteLine($"После Move: {point}");

var drawables = new List<IDrawable> { new Circle(5), new Rectangle(3, 4) };
Drawer.DrawAll(drawables);

Console.WriteLine("\n========== Задание 2. Интерфейсы и наследование ==========");
ShapeInfo.PrintShapeInfo(new Circle(5));
ShapeInfo.PrintShapeInfo(new Rectangle(3, 4));
var cube = new Cube(2);
ShapeInfo.PrintShapeInfo(cube);
Console.WriteLine($"  Объём:    {cube.GetVolume():F2}");

Console.WriteLine("\n========== Задание 3. Сегрегация интерфейсов (ISP) ==========");
IPrinter printer = new Printer();
printer.Print("Отчёт.pdf");

IScanner scanner = new Scanner();
scanner.Scan("Договор.pdf");

var mfu = new MultifunctionDevice();
mfu.Print("Документ.docx");
mfu.Scan("Фото.jpg");
mfu.Fax("Счёт.pdf");

Console.WriteLine("\n========== Задание 4. Интерфейсы и полиморфизм ==========");
PaymentProcessor.ProcessPayment(new CreditCard("1234-5678-9012-3456"), 1500m);
PaymentProcessor.ProcessPayment(new Cash(), 250m);

Worker.DoWork(new ConsoleLogger());
Worker.DoWork(new FileLogger("log.txt"));

Console.WriteLine("\nГотово.");