using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WebAPI.MainProject.DataRepositories;
using WebAPI.MainProject.Models;

namespace WebAPI.MainProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly TodoRepository _repository;

        public TodoController(ILogger<TodoController> logger, TodoRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Get All Existing Todo List
        /// </summary>
        /// <returns></returns>
        [Route("read/all")]
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            try
            {
                // Fetching data from Repo
                var todos = _repository.GetAll();

                // Log
                _logger.LogInformation("User interacted with todo data.");

                // Show data to user
                return Ok(todos);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                _logger.LogError(ex, "An error occurred while fetching todos.");
                throw;
            }
        }

        /// <summary>
        /// Get Todo List by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("read/{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            try
            {
                // Fetching data from Repo with ID
                var todo = _repository.GetById(id);

                // Log
                _logger.LogInformation($"Fetched data with ID : {id}.");

                // Check Todo data is avaliable
                if (todo == null)
                {
                    return NotFound();
                }

                // Show data to user
                return Ok(todo);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                _logger.LogError(ex, $"An error occurred while fetching todo with ID {id}.");
                throw;
            }
        }

        /// <summary>
        /// Add to Todo List
        /// </summary>
        /// <param name="todo"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public ActionResult<TodoItem> Create(TodoItem todo)
        {
            try
            {
                // Store data to Repo
                _repository.Add(todo);

                // Log
                _logger.LogInformation($"Added new todo item with ID {todo.Id}.");

                // Show recent user stored data
                return Ok(todo);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                _logger.LogError(ex, "An error occurred while creating a new todo.");
                throw;
            }
        }

        /// <summary>
        /// Update Todo List
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todo"></param>
        /// <returns></returns>
        [HttpPut("update/{id}")]
        public ActionResult<string> Update(int id, TodoItem todo)
        {
            try
            {
                // Check if ID is avaliable in todo list
                if (id != todo.Id)
                {
                    return BadRequest();
                }

                // If ID is avaliable in todo list, update todo related to ID
                _repository.Update(todo);

                // Log
                _logger.LogInformation($"Updated todo item with ID {todo.Id}.");

                // Show to user
                return Ok($"Updated todo item with ID {todo.Id}.");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                _logger.LogError(ex, $"An error occurred while updating todo with ID {id}.");
                throw;
            }
        }

        /// <summary>
        /// Delete Todo List by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                // Delete todo item related to ID
                _repository.Delete(id);

                // Log
                _logger.LogInformation($"Deleted todo item with ID {id}.");
                
                // Show to user
                return Ok($"Deleted todo item with ID {id}.");
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                _logger.LogError(ex, $"An error occurred while deleting todo with ID {id}.");
                throw;
            }
        }
    }

}
