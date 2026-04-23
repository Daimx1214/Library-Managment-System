using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Block management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BlockController : ControllerBase
    {
        private readonly GenericRepository<Block> _repo;

        public BlockController(AppDbContext context)
            => _repo = new GenericRepository<Block>(context);

        /// <summary>Get all active Block records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Block>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Block by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Block>.Fail($"Block with Id {id} not found."));
            return Ok(ApiResponse<Block>.Ok(item));
        }

        /// <summary>Create a new Block</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] BlockRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Block>.Fail("Validation failed."));

            var entity = new Block
            {
                Name = r.Name,
                Code = r.Code,
                CampusId = r.CampusId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Block>.Created(created));
        }

        /// <summary>Update an existing Block</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] BlockRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Block>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
                entity.CampusId = r.CampusId;
            });

            if (updated is null)
                return NotFound(ApiResponse<Block>.Fail($"Block with Id {id} not found."));

            return Ok(ApiResponse<Block>.Updated(updated));
        }

        /// <summary>Soft-delete a Block</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Block with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Block deleted successfully."));
        }

        /// <summary>Get total count of active Block records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
