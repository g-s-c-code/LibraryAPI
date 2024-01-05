using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

[ApiController]
[Route("[controller]")]
public class BorrowerController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly IMapper _mapper;
    private DateOnly Today => DateOnly.FromDateTime(DateTime.Now);

    public BorrowerController(LibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> PostBorrower(CreateBorrowerDTO createBorrowerDTO)
    {
        var borrower = _mapper.Map<Borrower>(createBorrowerDTO);

        await _context.AddAsync(borrower);
        await _context.SaveChangesAsync();
        
        return CreatedAtRoute(null, new { id = borrower.BorrowerId }, _mapper.Map<ReadBorrowerDTO>(borrower));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReadBorrowerDTO>>> GetBorrowers()
    {
        var Borrowers = await _context.Borrowers.AsNoTracking().ToListAsync();
        return Ok(Borrowers.Select(b => _mapper.Map<ReadBorrowerDTO>(b)).ToList());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBorrower(int id)
    {
        var borrower = await _context.Borrowers.FindAsync(id);

        if (borrower == null)
            return NotFound($"No borrower found with ID = {id}.");

        var hasActiveLoans = await _context.Loans.AnyAsync(l => l.BorrowerId == id && (l.ReturnDate == null || l.ReturnDate > Today));
        if (hasActiveLoans)
            return BadRequest("Cannot delete the borrower as they currently have active loans.");

        _context.Borrowers.Remove(borrower);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
