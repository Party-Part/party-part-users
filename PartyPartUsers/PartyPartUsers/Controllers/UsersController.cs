﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PartyPartUsers.Models;

namespace PartyPartUsers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserContext _dbContext;

        public UsersController(ILogger<UsersController> logger, UserContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _dbContext.Users
                .Select(user => UserToDTO(user))
                .ToListAsync();;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(long id)
        {
            var todoItem = await _dbContext.Users.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return UserToDTO(todoItem);
        }

        
        [HttpPost]
        public async Task<ActionResult<User>> Users(User user)
        {
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();

            //return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
            return CreatedAtAction("users", new { id = user.user_id }, user);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(long id, UserDTO userDTO)
        {
            if (id != userDTO.user_id)
            {
                return BadRequest();
            }

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.name = userDTO.name;
            user.login = userDTO.login;
            user.telegram_id = userDTO.telegram_id;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TodoItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(long id)
        {
            var todoItem = await _dbContext.Users.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _dbContext.Users.Remove(todoItem);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        
        private bool TodoItemExists(long id) =>
            _dbContext.Users.Any(e => e.user_id == id);
        
        private static UserDTO UserToDTO(User user) =>
            new UserDTO
            {
                user_id = user.user_id,
                name = user.name,
                login = user.login,
                email = user.email,
                telegram_id = user.telegram_id,
            };
    }
}