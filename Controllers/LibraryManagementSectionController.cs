using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>LibraryManagementSection management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LibraryManagementSectionController : ControllerBase
    {
        private readonly GenericRepository<LibraryManagementSection> _repo;

        public LibraryManagementSectionController(AppDbContext context)
            => _repo = new GenericRepository<LibraryManagementSection>(context);

        /// <summary>Get all active LibraryManagementSection records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LibraryManagementSection>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get LibraryManagementSection by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<LibraryManagementSection>.Fail($"LibraryManagementSection with Id {id} not found."));
            return Ok(ApiResponse<LibraryManagementSection>.Ok(item));
        }

        /// <summary>Create a new LibraryManagementSection</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LibraryManagementSectionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryManagementSection>.Fail("Validation failed."));

            var entity = new LibraryManagementSection
            {
                Name = r.Name,
                Code = r.Code,
                Description = r.Description,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<LibraryManagementSection>.Created(created));
        }

        /// <summary>Update an existing LibraryManagementSection</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LibraryManagementSectionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryManagementSection>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
                entity.Description = r.Description;
            });

            if (updated is null)
                return NotFound(ApiResponse<LibraryManagementSection>.Fail($"LibraryManagementSection with Id {id} not found."));

            return Ok(ApiResponse<LibraryManagementSection>.Updated(updated));
        }

        /// <summary>Soft-delete a LibraryManagementSection</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"LibraryManagementSection with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"LibraryManagementSection deleted successfully."));
        }

        /// <summary>Get total count of active LibraryManagementSection records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
