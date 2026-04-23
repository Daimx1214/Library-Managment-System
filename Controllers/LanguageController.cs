using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Language management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LanguageController : ControllerBase
    {
        private readonly GenericRepository<Language> _repo;

        public LanguageController(AppDbContext context)
            => _repo = new GenericRepository<Language>(context);

        /// <summary>Get all active Language records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Language>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Language by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Language>.Fail($"Language with Id {id} not found."));
            return Ok(ApiResponse<Language>.Ok(item));
        }

        /// <summary>Create a new Language</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LanguageRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Language>.Fail("Validation failed."));

            var entity = new Language
            {
                LanguageCategory = r.LanguageCategory,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Language>.Created(created));
        }

        /// <summary>Update an existing Language</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LanguageRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Language>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.LanguageCategory = r.LanguageCategory;
            });

            if (updated is null)
                return NotFound(ApiResponse<Language>.Fail($"Language with Id {id} not found."));

            return Ok(ApiResponse<Language>.Updated(updated));
        }

        /// <summary>Soft-delete a Language</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Language with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Language deleted successfully."));
        }

        /// <summary>Get total count of active Language records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
