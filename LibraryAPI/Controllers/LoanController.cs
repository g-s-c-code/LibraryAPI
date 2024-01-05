using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

[ApiController]
[Route("[controller]")]
public class LoanController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly IMapper _mapper;
    private DateOnly Today => DateOnly.FromDateTime(DateTime.Now);

    public LoanController(LibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> PostLoan(CreateLoanDTO createLoanDTO)
    {
        if (createLoanDTO.ReturnDate.HasValue && createLoanDTO.LoanDate > createLoanDTO.ReturnDate.Value)
            return BadRequest("Loan date cannot be a later date than the return date.");

        var book = await _context.Books.FindAsync(createLoanDTO.BookId);
        if (book == null)
            return NotFound($"No book found with ID = {createLoanDTO.BookId}.");

        if (!book.IsAvailable)
            return BadRequest("This book is currently on loan and unavailable for borrowing.");

        var borrower = await _context.Borrowers.FindAsync(createLoanDTO.BorrowerId);
        if (borrower == null)
            return NotFound($"No borrower found with ID = {createLoanDTO.BorrowerId}.");

        var loan = _mapper.Map<Loan>(createLoanDTO);

        if (loan.ReturnDate == null || loan.ReturnDate > Today)
        {
            book.IsAvailable = false;
            _context.Books.Update(book);
        }

        await _context.AddAsync(loan);
        await _context.SaveChangesAsync();

        return CreatedAtRoute(null, new { id = loan.LoanId }, _mapper.Map<ReadLoanDTO>(loan));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadLoanDTO>>> GetLoans()
    {
        var loans = await _context.Loans.AsNoTracking().ToListAsync();

        return Ok(loans.Select(b => _mapper.Map<ReadLoanDTO>(b)).ToList());
    }

    [HttpPut("{id}/return")]
    public async Task<IActionResult> ReturnLoan(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null)
            return NotFound($"No loan found with ID = {id}.");

        var book = await _context.Books.FindAsync(loan.BookId);
        if (book == null)
            return NotFound($"No book found with ID = {id}.");

        if (loan.ReturnDate != null && loan.ReturnDate <= Today)
            return BadRequest("This loan has already been returned.");

        loan.ReturnDate = Today;
        book.IsAvailable = true;

        _context.Books.Update(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
