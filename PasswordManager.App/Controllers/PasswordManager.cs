using Microsoft.AspNetCore.Mvc;
using PasswordManager.Data.Models;
using PasswordManager.Data.Repository;

namespace PasswordManager.App.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class PasswordManager : ControllerBase
    {
        private readonly PasswordManagerRepository passwordManagerRepository;

        public PasswordManager(PasswordManagerRepository passwordManagerRepository)
        {
            this.passwordManagerRepository = passwordManagerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var passwords = await passwordManagerRepository.GetAllAsync();
            return Ok(passwords);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromQuery] bool decrypt = false)
        {
            var password = await passwordManagerRepository.GetByIdAsync(id, decrypt);
            return password == null ? NotFound() : Ok(password);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Password password)
        {
            await passwordManagerRepository.AddAsync(password);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Password password)
        {
            var existing = await passwordManagerRepository.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            password.Id = id;
            await passwordManagerRepository.UpdateAsync(password);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await passwordManagerRepository.DeleteAsync(id);
            return Ok();
        }
    }
