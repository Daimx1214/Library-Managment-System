using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>University management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UniversityController : ControllerBase
    {
        private readonly GenericRepository<University> _repo;

        public UniversityController(AppDbContext context)
            => _repo = new GenericRepository<University>(context);

        /// <summary>Get all active University records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<University>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get University by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<University>.Fail($"University with Id {id} not found."));
            return Ok(ApiResponse<University>.Ok(item));
        }

        /// <summary>Create a new University</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UniversityRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<University>.Fail("Validation failed."));

            var entity = new University
            {
                Name = r.Name,
                Code = r.Code,
                Description = r.Description,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<University>.Created(created));
        }

        /// <summary>Update an existing University</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UniversityRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<University>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
                entity.Description = r.Description;
            });

            if (updated is null)
                return NotFound(ApiResponse<University>.Fail($"University with Id {id} not found."));

            return Ok(ApiResponse<University>.Updated(updated));
        }

        /// <summary>Soft-delete a University</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"University with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"University deleted successfully."));
        }

        /// <summary>Get total count of active University records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
