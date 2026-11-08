CREATE TABLE Reader (
    ReaderId INT PRIMARY KEY,
    FullName VARCHAR(200) NOT NULL,
    Email VARCHAR(200),
    Phone VARCHAR(30)
);

CREATE TABLE Librarian (
    LibrarianId INT PRIMARY KEY,
    FullName VARCHAR(200) NOT NULL,
    Login VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE Publisher (
    PublisherId INT PRIMARY KEY,
    Name VARCHAR(200) NOT NULL
);

CREATE TABLE Book (
    BookId INT PRIMARY KEY,
    Title VARCHAR(300) NOT NULL,
    ISBN VARCHAR(20),
    PublisherId INT REFERENCES Publisher(PublisherId),
    PublishYear INT
);

CREATE TABLE Author (
    AuthorId INT PRIMARY KEY,
    FullName VARCHAR(200) NOT NULL
);

CREATE TABLE BookAuthor (
    BookId INT REFERENCES Book(BookId),
    AuthorId INT REFERENCES Author(AuthorId),
    PRIMARY KEY (BookId, AuthorId)
);

CREATE TABLE Category (
    CategoryId INT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL
);

CREATE TABLE BookCategory (
    BookId INT REFERENCES Book(BookId),
    CategoryId INT REFERENCES Category(CategoryId),
    PRIMARY KEY (BookId, CategoryId)
);

CREATE TABLE Branch (
    BranchId INT PRIMARY KEY,
    Name VARCHAR(200),
    Address VARCHAR(300)
);

CREATE TABLE Shelf (
    ShelfId INT PRIMARY KEY,
    BranchId INT REFERENCES Branch(BranchId),
    Code VARCHAR(30)
);

CREATE TABLE BookCopy (
    CopyId INT PRIMARY KEY,
    BookId INT REFERENCES Book(BookId),
    BranchId INT REFERENCES Branch(BranchId),
    ShelfId INT REFERENCES Shelf(ShelfId),
    Status VARCHAR(30)
);

CREATE TABLE Membership (
    MembershipId INT PRIMARY KEY,
    ReaderId INT REFERENCES Reader(ReaderId),
    StartDate DATE,
    EndDate DATE,
    Status VARCHAR(30)
);

CREATE TABLE Loan (
    LoanId INT PRIMARY KEY,
    ReaderId INT REFERENCES Reader(ReaderId),
    CopyId INT REFERENCES BookCopy(CopyId),
    LibrarianId INT REFERENCES Librarian(LibrarianId),
    IssueDate DATE,
    DueDate DATE,
    ReturnDate DATE
);

CREATE TABLE Reservation (
    ReservationId INT PRIMARY KEY,
    ReaderId INT REFERENCES Reader(ReaderId),
    BookId INT REFERENCES Book(BookId),
    CreatedAt TIMESTAMP,
    Status VARCHAR(30)
);

CREATE TABLE Fine (
    FineId INT PRIMARY KEY,
    LoanId INT UNIQUE REFERENCES Loan(LoanId),
    Amount DECIMAL(10,2),
    Status VARCHAR(30)
);

CREATE TABLE Payment (
    PaymentId INT PRIMARY KEY,
    FineId INT REFERENCES Fine(FineId),
    Amount DECIMAL(10,2),
    PaidAt TIMESTAMP
);

CREATE TABLE Review (
    ReviewId INT PRIMARY KEY,
    ReaderId INT REFERENCES Reader(ReaderId),
    BookId INT REFERENCES Book(BookId),
    Rating INT,
    Comment TEXT
);

CREATE TABLE Notification (
    NotificationId INT PRIMARY KEY,
    ReaderId INT REFERENCES Reader(ReaderId),
    Message TEXT,
    CreatedAt TIMESTAMP,
    IsRead BOOLEAN
);
