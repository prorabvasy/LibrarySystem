using Microsoft.EntityFrameworkCore;

namespace LibraryRepository;

public interface IEntity { int Id { get; set; } }
public sealed class Book : IEntity {
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public decimal Price { get; set; }
}
public interface IRepository<T> where T : class, IEntity {
    Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    void Update(T entity);
    Task DeleteAsync(int id, CancellationToken ct = default);
}
public interface IBookRepository : IRepository<Book> {
    Task<IEnumerable<Book>> FindByAuthorAsync(string author, CancellationToken ct = default);
    Task<IEnumerable<Book>> FindByPriceRangeAsync(decimal min, decimal max, CancellationToken ct = default);
}
public sealed class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options) {
    public DbSet<Book> Books => Set<Book>();
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Book>().Property(b => b.Title).IsRequired().HasMaxLength(200);
        modelBuilder.Entity<Book>().Property(b => b.Author).IsRequired().HasMaxLength(150);
        modelBuilder.Entity<Book>().Property(b => b.Price).HasPrecision(10,2);
    }
}
public class GenericRepository<T>(LibraryDbContext db) : IRepository<T> where T : class, IEntity {
    protected readonly DbSet<T> Set = db.Set<T>();
    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default) => await Set.FindAsync([id], ct);
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default) => await Set.AsNoTracking().ToListAsync(ct);
    public async Task AddAsync(T entity, CancellationToken ct = default) { await Set.AddAsync(entity, ct); }
    public void Update(T entity) => Set.Update(entity);
    public async Task DeleteAsync(int id, CancellationToken ct = default) { var item = await Set.FindAsync([id], ct); if (item is not null) Set.Remove(item); }
}
public sealed class BookRepository(LibraryDbContext db) : GenericRepository<Book>(db), IBookRepository {
    public async Task<IEnumerable<Book>> FindByAuthorAsync(string author, CancellationToken ct = default) =>
        await Set.AsNoTracking().Where(b => b.Author.Contains(author)).OrderBy(b => b.Title).ToListAsync(ct);
    public async Task<IEnumerable<Book>> FindByPriceRangeAsync(decimal min, decimal max, CancellationToken ct = default) =>
        await Set.AsNoTracking().Where(b => b.Price >= min && b.Price <= max).OrderBy(b => b.Price).ToListAsync(ct);
}
public interface IUnitOfWork : IAsyncDisposable {
    IBookRepository Books { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
public sealed class UnitOfWork : IUnitOfWork {
    private readonly LibraryDbContext _db;
    public IBookRepository Books { get; }
    public UnitOfWork(LibraryDbContext db) { _db = db; Books = new BookRepository(db); }
    public Task<int> SaveChangesAsync(CancellationToken ct = default) => _db.SaveChangesAsync(ct);
    public ValueTask DisposeAsync() => _db.DisposeAsync();
}
