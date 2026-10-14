using LibrarySystem.BLL;
using LibrarySystem.DAL;
using LibrarySystem.Domain;

var repository = new InMemoryLibraryRepository();
var service = new LibraryService(repository);

while (true)
{
    Console.WriteLine("\n=== LibrarySystem ===");
    Console.WriteLine("1. Список книг");
    Console.WriteLine("2. Поиск книги");
    Console.WriteLine("3. Выдать книгу");
    Console.WriteLine("4. Список выдач");
    Console.WriteLine("5. Вернуть книгу");
    Console.WriteLine("0. Выход");
    Console.Write("Выберите действие: ");
    var choice = Console.ReadLine()?.Trim();
    try
    {
        switch (choice)
        {
            case "1":
                foreach (var b in repository.GetBooks())
                    Console.WriteLine($"{b.Id}. {b.Title} | ISBN: {b.Isbn} | экземпляров: {b.TotalCopies}");
                break;
            case "2":
                Console.Write("Название или ISBN: ");
                foreach (var b in service.SearchBooks(Console.ReadLine() ?? ""))
                    Console.WriteLine($"{b.Id}. {b.Title} ({b.PublishedYear})");
                break;
            case "3":
                Console.Write("ID книги: "); var bookId = int.Parse(Console.ReadLine() ?? "");
                Console.Write("ID читателя: "); var readerId = int.Parse(Console.ReadLine() ?? "");
                var loan = service.IssueBook(bookId, readerId, librarianId: 1);
                Console.WriteLine($"Выдано. Номер выдачи: {loan.Id}; вернуть до {loan.DueDate:dd.MM.yyyy}");
                break;
            case "4":
                foreach (var l in repository.GetLoans())
                    Console.WriteLine($"Выдача #{l.Id}: книга {l.BookId}, читатель {l.ReaderId}, до {l.DueDate:dd.MM.yyyy}, возврат: {(l.ReturnDate is null ? "не возвращена" : l.ReturnDate.Value.ToString("dd.MM.yyyy"))}");
                break;
            case "5":
                Console.Write("ID выдачи: ");
                service.ReturnBook(int.Parse(Console.ReadLine() ?? ""));
                Console.WriteLine("Возврат зарегистрирован.");
                break;
            case "0": return;
            default: Console.WriteLine("Неизвестная команда."); break;
        }
    }
    catch (Exception ex) { Console.WriteLine($"Ошибка: {ex.Message}"); }
}
