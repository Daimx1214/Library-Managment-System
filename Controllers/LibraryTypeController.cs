using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>LibraryType management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LibraryTypeController : ControllerBase
    {
        private readonly GenericRepository<LibraryType> _repo;

        public LibraryTypeController(AppDbContext context)
            => _repo = new GenericRepository<LibraryType>(context);

        /// <summary>Get all active LibraryType records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LibraryType>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get LibraryType by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<LibraryType>.Fail($"LibraryType with Id {id} not found."));
            return Ok(ApiResponse<LibraryType>.Ok(item));
        }

        /// <summary>Create a new LibraryType</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LibraryTypeRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryType>.Fail("Validation failed."));

            var entity = new LibraryType
            {
                Name = r.Name,
                Code = r.Code,
                Description = r.Description,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<LibraryType>.Created(created));
        }

        /// <summary>Update an existing LibraryType</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LibraryTypeRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryType>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
                entity.Description = r.Description;
            });

            if (updated is null)
                return NotFound(ApiResponse<LibraryType>.Fail($"LibraryType with Id {id} not found."));

            return Ok(ApiResponse<LibraryType>.Updated(updated));
        }

        /// <summary>Soft-delete a LibraryType</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"LibraryType with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"LibraryType deleted successfully."));
        }

        /// <summary>Get total count of active LibraryType records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
