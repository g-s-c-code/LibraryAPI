using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly IMapper _mapper;
    private DateOnly Today => DateOnly.FromDateTime(DateTime.Now);

    public BookController(LibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> PostBook(CreateBookDTO createBookDTO)
    {
        var book = _mapper.Map<Book>(createBookDTO);

        await _context.AddAsync(book);
        await _context.SaveChangesAsync();

        return CreatedAtRoute(null, new { id = book.BookId }, _mapper.Map<ReadBookDTO>(book));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadBookDTO>>> GetBooks()
    {
        var books = await _context.Books.AsNoTracking().ToListAsync();

        return Ok(books.Select(b => _mapper.Map<ReadBookDTO>(b)).ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReadBookDTO>> GetBook(int id)
    {
        var book = await _context.Books.AsNoTracking()
                                  .Where(b => b.BookId == id)
                                  .FirstOrDefaultAsync();

        return book == null
            ? NotFound($"No book found with ID = {id}.")
            : _mapper.Map<ReadBookDTO>(book);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);

        if (book == null)
            return NotFound($"No book found with ID = {id}.");

        var isOnLoan = await _context.Loans.AnyAsync(l => l.BookId == id && (l.ReturnDate == null || l.ReturnDate > Today));
        if (isOnLoan)
            return BadRequest("Cannot delete the book as it is currently on loan.");

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
