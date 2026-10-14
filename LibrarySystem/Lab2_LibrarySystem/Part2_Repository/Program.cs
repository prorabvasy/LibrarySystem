using LibraryRepository;
using Microsoft.EntityFrameworkCore;

var options = new DbContextOptionsBuilder<LibraryDbContext>().UseSqlite("Data Source=library.db").Options;
await using var db = new LibraryDbContext(options);
await db.Database.EnsureCreatedAsync();
await using var uow = new UnitOfWork(db);
if (!(await uow.Books.GetAllAsync()).Any()) {
    await uow.Books.AddAsync(new Book { Title = "Преступление и наказание", Author = "Ф. М. Достоевский", Price = 450m });
    await uow.Books.AddAsync(new Book { Title = "Мастер и Маргарита", Author = "М. А. Булгаков", Price = 520m });
    await uow.SaveChangesAsync();
}
Console.WriteLine("Каталог книг:");
foreach (var book in await uow.Books.GetAllAsync()) Console.WriteLine($"{book.Id}: {book.Title} — {book.Author}, {book.Price} руб.");
Console.WriteLine("Поиск по автору Достоевский:");
foreach (var book in await uow.Books.FindByAuthorAsync("Достоевский")) Console.WriteLine(book.Title);
