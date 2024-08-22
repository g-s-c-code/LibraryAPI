using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<Loan> Loans { get; set; }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }
}
