using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UserDb>(o => o.UseSqlite("Data Source=users.db"));
var app = builder.Build();
using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<UserDb>();
    db.Database.EnsureCreated();
}
app.MapPost("/user", async (CreateUser request, UserDb db) => {
    if (string.IsNullOrWhiteSpace(request.Login) || request.Login.Length > 80)
        return Results.BadRequest(new { error = "Login обязателен и должен быть короче 81 символа." });
    if (string.IsNullOrWhiteSpace(request.PassHash) || request.PassHash.Length < 32)
        return Results.BadRequest(new { error = "PassHash должен содержать хеш пароля, а не исходный пароль." });
    if (await db.Users.AnyAsync(u => u.Login == request.Login))
        return Results.Conflict(new { error = "Такой Login уже существует." });
    var user = new User { Login = request.Login.Trim(), PassHash = request.PassHash };
    db.Users.Add(user); await db.SaveChangesAsync();
    return Results.Created($"/user/{user.Id}", new { user.Id, user.Login });
});
app.MapGet("/user/{id:int}", async (int id, UserDb db) => {
    var u = await db.Users.FindAsync(id);
    return u is null ? Results.NotFound(new { error = "Пользователь не найден." }) : Results.Ok(new { u.Id, u.Login });
});
app.MapPut("/user/{id:int}", async (int id, UpdateUser request, UserDb db) => {
    var u = await db.Users.FindAsync(id);
    if (u is null) return Results.NotFound(new { error = "Пользователь не найден." });
    if (string.IsNullOrWhiteSpace(request.Login) || request.Login.Length > 80)
        return Results.BadRequest(new { error = "Некорректный Login." });
    if (await db.Users.AnyAsync(x => x.Login == request.Login && x.Id != id))
        return Results.Conflict(new { error = "Такой Login уже существует." });
    u.Login = request.Login.Trim();
    if (!string.IsNullOrWhiteSpace(request.PassHash)) {
        if (request.PassHash.Length < 32) return Results.BadRequest(new { error = "Передайте хеш, а не исходный пароль." });
        u.PassHash = request.PassHash;
    }
    await db.SaveChangesAsync();
    return Results.Ok(new { u.Id, u.Login });
});
app.MapDelete("/user/{id:int}", async (int id, UserDb db) => {
    var u = await db.Users.FindAsync(id);
    if (u is null) return Results.NotFound(new { error = "Пользователь не найден." });
    db.Users.Remove(u); await db.SaveChangesAsync();
    return Results.Ok(new { message = "Пользователь удалён." });
});
app.MapGet("/", () => Results.Ok(new { service = "User API", endpoints = new[] { "POST /user", "GET /user/{id}", "PUT /user/{id}", "DELETE /user/{id}" } }));
app.Run();

record CreateUser(string Login, string PassHash);
record UpdateUser(string Login, string? PassHash);
sealed class User {
    public int Id { get; set; }
    [Required, MaxLength(80] public string Login { get; set; } = "";
    [Required] public string PassHash { get; set; } = "";
}
sealed class UserDb(DbContextOptions<UserDb> options) : DbContext(options) {
    public DbSet<User> Users => Set<User>();
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<User>().HasIndex(u => u.Login).IsUnique();
        modelBuilder.Entity<User>().Property(u => u.Login).IsRequired().HasMaxLength(80);
        modelBuilder.Entity<User>().Property(u => u.PassHash).IsRequired();
    }
}
