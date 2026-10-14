using LibrarySystem.DAL;
using LibrarySystem.Domain;

namespace LibrarySystem.BLL;

public sealed class LibraryService(ILibraryRepository repository)
{
    public IReadOnlyList<Book> SearchBooks(string query) =>
        repository.GetBooks().Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase)
            || b.Isbn.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();

    public Loan IssueBook(int bookId, int readerId, int librarianId, int loanDays = 14)
    {
        var book = repository.FindBook(bookId) ?? throw new InvalidOperationException("Книга не найдена.");
        _ = repository.FindReader(readerId) ?? throw new InvalidOperationException("Читатель не найден.");
        var active = repository.GetLoans().Count(l => l.BookId == bookId && l.ReturnDate is null);
        if (active >= book.TotalCopies) throw new InvalidOperationException("Нет доступных экземпляров.");
        var nextId = repository.GetLoans().Select(l => l.Id).DefaultIfEmpty(0).Max() + 1;
        var today = DateTime.Today;
        var loan = new Loan(nextId, bookId, readerId, librarianId, today, today.AddDays(loanDays));
        repository.AddLoan(loan);
        return loan;
    }

    public void ReturnBook(int loanId) => repository.MarkLoanReturned(loanId, DateTime.Today);
}
