using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>LibraryBranchManagement management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LibraryBranchManagementController : ControllerBase
    {
        private readonly GenericRepository<LibraryBranchManagement> _repo;

        public LibraryBranchManagementController(AppDbContext context)
            => _repo = new GenericRepository<LibraryBranchManagement>(context);

        /// <summary>Get all active LibraryBranchManagement records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LibraryBranchManagement>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get LibraryBranchManagement by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<LibraryBranchManagement>.Fail($"LibraryBranchManagement with Id {id} not found."));
            return Ok(ApiResponse<LibraryBranchManagement>.Ok(item));
        }

        /// <summary>Create a new LibraryBranchManagement</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LibraryBranchManagementRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryBranchManagement>.Fail("Validation failed."));

            var entity = new LibraryBranchManagement
            {
                LibraryBranchId = r.LibraryBranchId,
                LibraryManagementSectionId = r.LibraryManagementSectionId,
                Code = r.Code,
                EstablishedIn = r.EstablishedIn,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<LibraryBranchManagement>.Created(created));
        }

        /// <summary>Update an existing LibraryBranchManagement</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LibraryBranchManagementRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryBranchManagement>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.LibraryBranchId = r.LibraryBranchId;
                entity.LibraryManagementSectionId = r.LibraryManagementSectionId;
                entity.Code = r.Code;
                entity.EstablishedIn = r.EstablishedIn;
            });

            if (updated is null)
                return NotFound(ApiResponse<LibraryBranchManagement>.Fail($"LibraryBranchManagement with Id {id} not found."));

            return Ok(ApiResponse<LibraryBranchManagement>.Updated(updated));
        }

        /// <summary>Soft-delete a LibraryBranchManagement</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"LibraryBranchManagement with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"LibraryBranchManagement deleted successfully."));
        }

        /// <summary>Get total count of active LibraryBranchManagement records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
