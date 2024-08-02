using Microsoft.AspNetCore.Mvc;
using ProjetoBiblioteca.Models;

namespace ProjetoBiblioteca.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    public static List<Book> books = new();

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(books);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        books.FirstOrDefault(x => x.Id == id);
        if (id == null)
            return NotFound();
        return Ok(books);
    }

    [HttpPost]
    public IActionResult Criar(Book book)
    {
        book.Id = books.Any() ? books.Max(b => b.Id) + 1 : 1;
        books.Add(book);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpPut]
    public IActionResult Put(int id, [FromBody] Book book) 
    {
        var livro = books.FirstOrDefault(x => x.Id == id);
        if(id == null)
            return NotFound();
        livro.Title = book.Title;
        livro.Author = book.Author;
        livro.Price = book.Price;
        livro.QuantityInStock = livro.QuantityInStock;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book == null)
            return NotFound();

        books.Remove(book);
        return NoContent();
    }
}
