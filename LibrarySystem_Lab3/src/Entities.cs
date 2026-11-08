namespace LibrarySystem;

public class Reader
{
    public int ReaderId { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
}

public class Librarian
{
    public int LibrarianId { get; set; }
    public string FullName { get; set; } = "";
    public string Login { get; set; } = "";
}

public class Membership
{
    public int MembershipId { get; set; }
    public int ReaderId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Status { get; set; } = "";
}

public class Book
{
    public int BookId { get; set; }
    public string Title { get; set; } = "";
    public string ISBN { get; set; } = "";
    public int PublisherId { get; set; }
    public int PublishYear { get; set; }
}

public class Author
{
    public int AuthorId { get; set; }
    public string FullName { get; set; } = "";
}

public class BookAuthor
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
}

public class Publisher
{
    public int PublisherId { get; set; }
    public string Name { get; set; } = "";
}

public class Category
{
    public int CategoryId { get; set; }
    public string Name { get; set; } = "";
}

public class BookCategory
{
    public int BookId { get; set; }
    public int CategoryId { get; set; }
}

public class BookCopy
{
    public int CopyId { get; set; }
    public int BookId { get; set; }
    public int BranchId { get; set; }
    public int ShelfId { get; set; }
    public string Status { get; set; } = "";
}

public class Branch
{
    public int BranchId { get; set; }
    public string Name { get; set; } = "";
    public string Address { get; set; } = "";
}

public class Shelf
{
    public int ShelfId { get; set; }
    public int BranchId { get; set; }
    public string Code { get; set; } = "";
}

public class Loan
{
    public int LoanId { get; set; }
    public int ReaderId { get; set; }
    public int CopyId { get; set; }
    public int LibrarianId { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly DueDate { get; set; }
    public DateOnly? ReturnDate { get; set; }
}

public class Reservation
{
    public int ReservationId { get; set; }
    public int ReaderId { get; set; }
    public int BookId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; } = "";
}

public class Fine
{
    public int FineId { get; set; }
    public int LoanId { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; } = "";
}

public class Payment
{
    public int PaymentId { get; set; }
    public int FineId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaidAt { get; set; }
}

public class Review
{
    public int ReviewId { get; set; }
    public int ReaderId { get; set; }
    public int BookId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = "";
}

public class Notification
{
    public int NotificationId { get; set; }
    public int ReaderId { get; set; }
    public string Message { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
}
