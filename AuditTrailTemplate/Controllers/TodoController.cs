using AuditTrailTemplate.DTOs;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuditTrailTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly AuditTrailTemplateContext _context;

        public TodoController(AuditTrailTemplateContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
        {
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(o => o.Id == id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(todoItem);
        }
        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateTodoItem(TodoItemDto todoItemDTO)
        {
            var todoItem = new TodoItem
            {
                IsComplete = todoItemDTO.IsComplete,
                Name = todoItemDTO.Name,
                IsDelete = todoItemDTO.IsDelete
            };

            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
            nameof(GetTodoItem),
            new { id = todoItem.Id },
            ItemToDTO(todoItem));
        }
        private static TodoItemDto ItemToDTO(TodoItem todoItem) =>
        new TodoItemDto
        {
            Id = todoItem.Id,
            Name = todoItem.Name,
            IsComplete = todoItem.IsComplete,
            IsDelete = todoItem.IsDelete
        };
    }
}
