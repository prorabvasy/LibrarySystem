using LibrarySystem.Domain;

namespace LibrarySystem.DAL;

public interface ILibraryRepository
{
    IReadOnlyList<Book> GetBooks();
    IReadOnlyList<Reader> GetReaders();
    IReadOnlyList<Loan> GetLoans();
    Book? FindBook(int id);
    Reader? FindReader(int id);
    void AddBook(Book book);
    void AddReader(Reader reader);
    void AddLoan(Loan loan);
    void MarkLoanReturned(int loanId, DateTime returnedAt);
}

public sealed class InMemoryLibraryRepository : ILibraryRepository
{
    private readonly List<Book> _books = new();
    private readonly List<Reader> _readers = new();
    private readonly List<Loan> _loans = new();
    public InMemoryLibraryRepository()
    {
        _books.Add(new Book(1, "Преступление и наказание", "978-5-17-090", 1, 1, 1, 1866, 3));
        _books.Add(new Book(2, "Мастер и Маргарита", "978-5-17-091", 2, 1, 1, 1967, 2));
        _readers.Add(new Reader(1, "Иван Иванов", "ivan@example.com", "+7-900-000-00-00"));
    }
    public IReadOnlyList<Book> GetBooks() => _books.AsReadOnly();
    public IReadOnlyList<Reader> GetReaders() => _readers.AsReadOnly();
    public IReadOnlyList<Loan> GetLoans() => _loans.AsReadOnly();
    public Book? FindBook(int id) => _books.FirstOrDefault(x => x.Id == id);
    public Reader? FindReader(int id) => _readers.FirstOrDefault(x => x.Id == id);
    public void AddBook(Book book) => _books.Add(book);
    public void AddReader(Reader reader) => _readers.Add(reader);
    public void AddLoan(Loan loan) => _loans.Add(loan);
    public void MarkLoanReturned(int loanId, DateTime returnedAt)
    {
        var i = _loans.FindIndex(x => x.Id == loanId);
        if (i < 0) throw new InvalidOperationException("Выдача не найдена.");
        _loans[i] = _loans[i] with { ReturnDate = returnedAt };
    }
}
