using System.Collections.Generic;
using System.Linq;
using WebAPI.MainProject.Models;

namespace WebAPI.MainProject.DataRepositories
{
    public class TodoRepository
    {
        private readonly List<TodoItem> _todos = new List<TodoItem>();

        public List<TodoItem> GetAll() => _todos;

        public TodoItem GetById(int id) => _todos.FirstOrDefault(todo => todo.Id == id);

        public void Add(TodoItem todo) => _todos.Add(todo);

        public void Update(TodoItem todo)
        {
            var existingTodo = _todos.FirstOrDefault(t => t.Id == todo.Id);
            if (existingTodo != null)
            {
                existingTodo.Name = todo.Name;
                existingTodo.IsComplete = todo.IsComplete;
            }
        }

        public void Delete(int id) => _todos.RemoveAll(todo => todo.Id == id);
    }

}
