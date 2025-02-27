using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToDo.Models;

namespace ToDo.Controllers
{
    public class TodoController : Controller
    {
        private readonly AppDbContext _context;

        public TodoController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchQuery)
        {
            var tasks = string.IsNullOrEmpty(searchQuery)
                ? await _context.ToDoItems.ToListAsync()
                :await _context.ToDoItems
                .Where(t=>EF.Functions.Like(t.Title,$"%{searchQuery}%"))
                .ToListAsync();
            ViewData["SearchQuery"] = searchQuery;
            return View(tasks);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title")]ToDoItem toDoItem)
        {
            toDoItem.IsCompleted = false;
            if (ModelState.IsValid)
            {
                _context.ToDoItems.Add(toDoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItem);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var todoItem = await _context.ToDoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Title,IsCompleted")]ToDoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.Update(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var  todoItem = await _context.ToDoItems.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoItem = await _context.ToDoItems.FindAsync(id);
            if (todoItem != null)
            {
                _context.ToDoItems.Remove(todoItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
