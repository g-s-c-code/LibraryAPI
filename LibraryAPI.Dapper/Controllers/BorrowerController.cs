using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

[ApiController]
[Route("[controller]")]
public class BorrowerController : ControllerBase
{
	private readonly IDbConnection _dbConnection;
	private readonly IMapper _mapper;

	public BorrowerController(IDbConnection dbConnection, IMapper mapper)
	{
		_dbConnection = dbConnection;
		_mapper = mapper;
	}

	[HttpPost]
	public async Task<IActionResult> PostBorrower(CreateBorrowerDTO createBorrowerDTO)
	{
		var borrower = _mapper.Map<Borrower>(createBorrowerDTO);
		var query = "INSERT INTO Borrowers (FirstName, LastName, SSN) VALUES (@FirstName, @LastName, @SSN);" +
					"SELECT CAST(SCOPE_IDENTITY() as int)";

		var borrowerId = await _dbConnection.QuerySingleAsync<int>(query, borrower);
		borrower.BorrowerId = borrowerId;

		return CreatedAtRoute(null, new { id = borrower.BorrowerId }, _mapper.Map<ReadBorrowerDTO>(borrower));
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ReadBorrowerDTO>>> GetBorrowers()
	{
		var query = "SELECT * FROM Borrowers";
		var borrowers = await _dbConnection.QueryAsync<Borrower>(query);

		return Ok(borrowers.Select(b => _mapper.Map<ReadBorrowerDTO>(b)).ToList());
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteBorrower(int id)
	{
		var query = "DELETE FROM Borrowers WHERE BorrowerId = @Id";
		var affectedRows = await _dbConnection.ExecuteAsync(query, new { Id = id });

		if (affectedRows == 0)
			return NotFound($"No borrower found with ID = {id}.");

		return NoContent();
	}
}
