using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>AccessionPattern management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccessionPatternController : ControllerBase
    {
        private readonly GenericRepository<AccessionPattern> _repo;

        public AccessionPatternController(AppDbContext context)
            => _repo = new GenericRepository<AccessionPattern>(context);

        /// <summary>Get all active AccessionPattern records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<AccessionPattern>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get AccessionPattern by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<AccessionPattern>.Fail($"AccessionPattern with Id {id} not found."));
            return Ok(ApiResponse<AccessionPattern>.Ok(item));
        }

        /// <summary>Create a new AccessionPattern</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AccessionPatternRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<AccessionPattern>.Fail("Validation failed."));

            var entity = new AccessionPattern
            {
                LibraryBranchId = r.LibraryBranchId,
                Pattern = r.Pattern,
                NextSequence = r.NextSequence,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<AccessionPattern>.Created(created));
        }

        /// <summary>Update an existing AccessionPattern</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] AccessionPatternRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<AccessionPattern>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.LibraryBranchId = r.LibraryBranchId;
                entity.Pattern = r.Pattern;
                entity.NextSequence = r.NextSequence;
            });

            if (updated is null)
                return NotFound(ApiResponse<AccessionPattern>.Fail($"AccessionPattern with Id {id} not found."));

            return Ok(ApiResponse<AccessionPattern>.Updated(updated));
        }

        /// <summary>Soft-delete a AccessionPattern</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"AccessionPattern with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"AccessionPattern deleted successfully."));
        }

        /// <summary>Get total count of active AccessionPattern records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
