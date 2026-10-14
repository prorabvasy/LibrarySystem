-- Логическая схема для Microsoft SQL Server (8 сущностей).
CREATE TABLE Authors (
    AuthorId INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(200) NOT NULL,
    Country NVARCHAR(100) NULL
);
CREATE TABLE Publishers (
    PublisherId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL
);
CREATE TABLE Categories (
    CategoryId INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE
);
CREATE TABLE Books (
    BookId INT IDENTITY PRIMARY KEY,
    Title NVARCHAR(250) NOT NULL,
    ISBN NVARCHAR(30) NOT NULL UNIQUE,
    AuthorId INT NOT NULL REFERENCES Authors(AuthorId),
    PublisherId INT NOT NULL REFERENCES Publishers(PublisherId),
    CategoryId INT NOT NULL REFERENCES Categories(CategoryId),
    PublishedYear INT NOT NULL,
    TotalCopies INT NOT NULL CHECK (TotalCopies >= 0)
);
CREATE TABLE Readers (
    ReaderId INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(200) NOT NULL,
    Email NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(40) NULL
);
CREATE TABLE Librarians (
    LibrarianId INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(200) NOT NULL,
    Login NVARCHAR(100) NOT NULL UNIQUE
);
CREATE TABLE Loans (
    LoanId INT IDENTITY PRIMARY KEY,
    BookId INT NOT NULL REFERENCES Books(BookId),
    ReaderId INT NOT NULL REFERENCES Readers(ReaderId),
    LibrarianId INT NOT NULL REFERENCES Librarians(LibrarianId),
    LoanDate DATE NOT NULL,
    DueDate DATE NOT NULL,
    ReturnDate DATE NULL
);
CREATE TABLE Reservations (
    ReservationId INT IDENTITY PRIMARY KEY,
    BookId INT NOT NULL REFERENCES Books(BookId),
    ReaderId INT NOT NULL REFERENCES Readers(ReaderId),
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    Status NVARCHAR(30) NOT NULL DEFAULT N'Active'
);
