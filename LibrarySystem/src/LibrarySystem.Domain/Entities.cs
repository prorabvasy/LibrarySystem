namespace LibrarySystem.Domain;

public sealed record Author(int Id, string FullName, string? Country = null);
public sealed record Publisher(int Id, string Name);
public sealed record Category(int Id, string Name);
public sealed record Book(int Id, string Title, string Isbn, int AuthorId, int PublisherId, int CategoryId, int PublishedYear, int TotalCopies);
public sealed record Reader(int Id, string FullName, string Email, string Phone);
public sealed record Librarian(int Id, string FullName, string Login);
public sealed record Loan(int Id, int BookId, int ReaderId, int LibrarianId, DateTime LoanDate, DateTime DueDate, DateTime? ReturnDate = null);
public sealed record Reservation(int Id, int BookId, int ReaderId, DateTime CreatedAt, string Status = "Active");
