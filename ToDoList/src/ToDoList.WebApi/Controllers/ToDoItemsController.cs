namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    private static List<ToDoItem> items = new List<ToDoItem>(){
        new() {
            ToDoItemId = 2,
            Name = "gg",
            Description ="gg",
            IsCompleted = false,
            }
        };

    public static object ToDoItemId { get; private set; }

    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return Created(); //201
    }

    [HttpGet]
    public IActionResult Read()
    {
        try
        {
            if (items == null)
            {
                return NotFound(); //404
            }
            return Ok(items); //200
        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId) => Ok(items.Find(i => i.ToDoItemId == toDoItemId)); //this is not really bulletproof, but it works :-) 

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            var item = items.Find(i => i.ToDoItemId == toDoItemId);
            if (item != null)
            {
                item.Name = request.Name;
                item.Description = request.Description;
                item.IsCompleted = request.IsCompleted;
                return Ok();
            }
            return NotFound();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var item = items.Find(i => i.ToDoItemId == toDoItemId);
            if (item == null)
            {
                return NotFound(); //404
            }
            return Ok(items.Remove(item)); //200
        }
        catch (System.Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }
}
