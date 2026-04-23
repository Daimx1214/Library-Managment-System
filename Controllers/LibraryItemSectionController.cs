using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>LibraryItemSection management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LibraryItemSectionController : ControllerBase
    {
        private readonly GenericRepository<LibraryItemSection> _repo;

        public LibraryItemSectionController(AppDbContext context)
            => _repo = new GenericRepository<LibraryItemSection>(context);

        /// <summary>Get all active LibraryItemSection records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LibraryItemSection>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get LibraryItemSection by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<LibraryItemSection>.Fail($"LibraryItemSection with Id {id} not found."));
            return Ok(ApiResponse<LibraryItemSection>.Ok(item));
        }

        /// <summary>Create a new LibraryItemSection</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LibraryItemSectionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryItemSection>.Fail("Validation failed."));

            var entity = new LibraryItemSection
            {
                Name = r.Name,
                Code = r.Code,
                LibraryBranchId = r.LibraryBranchId,
                DepartmentId = r.DepartmentId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<LibraryItemSection>.Created(created));
        }

        /// <summary>Update an existing LibraryItemSection</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LibraryItemSectionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryItemSection>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
                entity.LibraryBranchId = r.LibraryBranchId;
                entity.DepartmentId = r.DepartmentId;
            });

            if (updated is null)
                return NotFound(ApiResponse<LibraryItemSection>.Fail($"LibraryItemSection with Id {id} not found."));

            return Ok(ApiResponse<LibraryItemSection>.Updated(updated));
        }

        /// <summary>Soft-delete a LibraryItemSection</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"LibraryItemSection with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"LibraryItemSection deleted successfully."));
        }

        /// <summary>Get total count of active LibraryItemSection records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
