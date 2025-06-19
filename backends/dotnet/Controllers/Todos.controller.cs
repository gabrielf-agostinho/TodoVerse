using Dotnet.Contexts;
using Dotnet.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class TodosController(DatabaseContext databaseContext) : ControllerBase
  {
    private readonly DatabaseContext _databaseContext = databaseContext;

    private Todo? Find(string id) => _databaseContext.Todos.Find(id);

    [HttpGet]
    public IActionResult GetAll() => Ok(_databaseContext.Todos.ToList());

    [HttpPost]
    public IActionResult Create([FromBody] Todo todo)
    {
      try
      {
        if (string.IsNullOrEmpty(todo.Title))
          throw new Exception("Title cannot be null or empty");

        _databaseContext.Add(todo);
        _databaseContext.SaveChanges();

        return CreatedAtAction(nameof(GetAll), new { todo.Id }, todo);
      }
      catch (Exception e)
      {
        return BadRequest(new { error = e.Message });
      }
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] string id, [FromBody] Todo todo)
    {
      Todo? entity = Find(id);

      if (entity is null) return NotFound();

      entity.Title = todo.Title;
      entity.Completed = todo.Completed;

      _databaseContext.SaveChanges();

      return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] string id)
    {
      Todo? entity = Find(id);

      if (entity is null) return NotFound();

      _databaseContext.Remove(entity);
      _databaseContext.SaveChanges();

      return NoContent();
    }
  }
}