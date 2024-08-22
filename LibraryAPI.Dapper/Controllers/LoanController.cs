using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

[ApiController]
[Route("[controller]")]
public class LoanController : ControllerBase
{
	private readonly IDbConnection _dbConnection;
	private readonly IMapper _mapper;

	public LoanController(IDbConnection dbConnection, IMapper mapper)
	{
		_dbConnection = dbConnection;
		_mapper = mapper;
	}

	[HttpPost]
	public async Task<IActionResult> PostLoan(CreateLoanDTO createLoanDTO)
	{
		var loan = _mapper.Map<Loan>(createLoanDTO);

		var bookQuery = "SELECT * FROM Books WHERE BookId = @BookId";
		var book = await _dbConnection.QuerySingleOrDefaultAsync<Book>(bookQuery, new { createLoanDTO.BookId });
		if (book == null)
			return NotFound($"No book found with ID = {createLoanDTO.BookId}.");

		if (!book.IsAvailable)
			return BadRequest("This book is currently on loan and unavailable for borrowing.");

		var borrowerQuery = "SELECT * FROM Borrowers WHERE BorrowerId = @BorrowerId";
		var borrower = await _dbConnection.QuerySingleOrDefaultAsync<Borrower>(borrowerQuery, new { createLoanDTO.BorrowerId });
		if (borrower == null)
			return NotFound($"No borrower found with ID = {createLoanDTO.BorrowerId}.");

		var loanQuery = "INSERT INTO Loans (LoanDate, ReturnDate, BookId, BorrowerId) VALUES (@LoanDate, @ReturnDate, @BookId, @BorrowerId);" +
						"SELECT CAST(SCOPE_IDENTITY() as int)";
		var loanId = await _dbConnection.QuerySingleAsync<int>(loanQuery, loan);
		loan.LoanId = loanId;

		if (loan.ReturnDate == null || loan.ReturnDate > DateOnly.FromDateTime(DateTime.Now))
		{
			var updateBookQuery = "UPDATE Books SET IsAvailable = 0 WHERE BookId = @BookId";
			await _dbConnection.ExecuteAsync(updateBookQuery, new { loan.BookId });
		}

		return CreatedAtRoute(null, new { id = loan.LoanId }, _mapper.Map<ReadLoanDTO>(loan));
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ReadLoanDTO>>> GetLoans()
	{
		var query = "SELECT * FROM Loans";
		var loans = await _dbConnection.QueryAsync<Loan>(query);

		return Ok(loans.Select(b => _mapper.Map<ReadLoanDTO>(b)).ToList());
	}

	[HttpPut("{id}/return")]
	public async Task<IActionResult> ReturnLoan(int id)
	{
		var loanQuery = "SELECT * FROM Loans WHERE LoanId = @Id";
		var loan = await _dbConnection.QuerySingleOrDefaultAsync<Loan>(loanQuery, new { Id = id });
		if (loan == null)
			return NotFound($"No loan found with ID = {id}.");

		var bookQuery = "SELECT * FROM Books WHERE BookId = @BookId";
		var book = await _dbConnection.QuerySingleOrDefaultAsync<Book>(bookQuery, new { BookId = loan.BookId });
		if (book == null)
			return NotFound($"No book found with ID = {loan.BookId}.");

		if (loan.ReturnDate != null && loan.ReturnDate <= DateOnly.FromDateTime(DateTime.Now))
			return BadRequest("This loan has already been returned.");

		var updateLoanQuery = "UPDATE Loans SET ReturnDate = @ReturnDate WHERE LoanId = @LoanId";
		loan.ReturnDate = DateOnly.FromDateTime(DateTime.Now);
		await _dbConnection.ExecuteAsync(updateLoanQuery, new { ReturnDate = loan.ReturnDate, LoanId = loan.LoanId });

		// Mark the book as available again
		var updateBookQuery = "UPDATE Books SET IsAvailable = 1 WHERE BookId = @BookId";
		await _dbConnection.ExecuteAsync(updateBookQuery, new { loan.BookId });

		return NoContent();
	}
}
