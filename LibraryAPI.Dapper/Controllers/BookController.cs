using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
	private readonly IDbConnection _dbConnection;
	private readonly IMapper _mapper;

	public BookController(IDbConnection dbConnection, IMapper mapper)
	{
		_dbConnection = dbConnection;
		_mapper = mapper;
	}

	[HttpPost]
	public async Task<IActionResult> PostBook(CreateBookDTO createBookDTO)
	{
		var book = _mapper.Map<Book>(createBookDTO);
		var query = "INSERT INTO Books (Title, ISBN, ReleaseYear, IsAvailable) VALUES (@Title, @ISBN, @ReleaseYear, @IsAvailable);" +
					"SELECT CAST(SCOPE_IDENTITY() as int)";

		var bookId = await _dbConnection.QuerySingleAsync<int>(query, book);
		book.BookId = bookId;

		return CreatedAtRoute(null, new { id = book.BookId }, _mapper.Map<ReadBookDTO>(book));
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ReadBookDTO>>> GetBooks()
	{
		var query = "SELECT * FROM Books";
		var books = await _dbConnection.QueryAsync<Book>(query);

		return Ok(books.Select(b => _mapper.Map<ReadBookDTO>(b)).ToList());
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ReadBookDTO>> GetBook(int id)
	{
		var query = "SELECT * FROM Books WHERE BookId = @Id";
		var book = await _dbConnection.QuerySingleOrDefaultAsync<Book>(query, new { Id = id });

		if (book == null)
			return NotFound($"No book found with ID = {id}.");

		return Ok(_mapper.Map<ReadBookDTO>(book));
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> DeleteBook(int id)
	{
		var query = "DELETE FROM Books WHERE BookId = @Id";
		var affectedRows = await _dbConnection.ExecuteAsync(query, new { Id = id });

		if (affectedRows == 0)
			return NotFound($"No book found with ID = {id}.");

		return NoContent();
	}
}
