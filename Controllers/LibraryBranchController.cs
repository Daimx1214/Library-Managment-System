using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>LibraryBranch management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LibraryBranchController : ControllerBase
    {
        private readonly GenericRepository<LibraryBranch> _repo;

        public LibraryBranchController(AppDbContext context)
            => _repo = new GenericRepository<LibraryBranch>(context);

        /// <summary>Get all active LibraryBranch records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LibraryBranch>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get LibraryBranch by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<LibraryBranch>.Fail($"LibraryBranch with Id {id} not found."));
            return Ok(ApiResponse<LibraryBranch>.Ok(item));
        }

        /// <summary>Create a new LibraryBranch</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LibraryBranchRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryBranch>.Fail("Validation failed."));

            var entity = new LibraryBranch
            {
                Description = r.Description,
                LibraryTypeId = r.LibraryTypeId,
                ParentId = r.ParentId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<LibraryBranch>.Created(created));
        }

        /// <summary>Update an existing LibraryBranch</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LibraryBranchRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryBranch>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Description = r.Description;
                entity.LibraryTypeId = r.LibraryTypeId;
                entity.ParentId = r.ParentId;
            });

            if (updated is null)
                return NotFound(ApiResponse<LibraryBranch>.Fail($"LibraryBranch with Id {id} not found."));

            return Ok(ApiResponse<LibraryBranch>.Updated(updated));
        }

        /// <summary>Soft-delete a LibraryBranch</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"LibraryBranch with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"LibraryBranch deleted successfully."));
        }

        /// <summary>Get total count of active LibraryBranch records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
