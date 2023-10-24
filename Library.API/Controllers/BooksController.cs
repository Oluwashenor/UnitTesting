using Microsoft.AspNetCore.Mvc;
using Library.API.Models;
using Library.API.Repository;

namespace Library.API.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly IBooksRepository _booksRepository;
    public BooksController(IBooksRepository booksRepository)
    {
        _booksRepository = booksRepository;
    }

    // GET: Books
    [HttpGet("all")]
    [ProducesResponseType(200, Type = typeof(List<Book>))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetAll()
    {
        var books = await _booksRepository.GetAll();
          return books != null ? 
                      Ok(books) :
                      Problem("Entity set 'LibraryAPIContext.Book'  is null.");
    }

    // GET: Books/Details/5
    [HttpGet("get")]
    [ProducesResponseType(200, Type = typeof(Book))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();
        var book = await _booksRepository.Get((int)id);
        if (book == null)
            return NotFound();
        return Ok(book);
    }

    // POST: Books/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("Create")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Create([Bind("Id,Name,Author")] Book book)
    {
        if (ModelState.IsValid)
        {
            var save = await _booksRepository.Create(book); 
            return Ok(save);
        }
        return Problem("Something went wrong");
    }

    // POST: Books/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost("Edit")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Author")] Book book)
    {
        if (id != book.Id)
            return NotFound();
        if (ModelState.IsValid)
        {
            await _booksRepository.Update(book);
            return RedirectToAction(nameof(Index));
        }
        return Ok(book);
    }

    // POST: Books/Delete/5
    [HttpPost, ActionName("Delete")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var deleteBook = await _booksRepository.Delete(id);
        return Ok(nameof(Index));
    }
}