using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>LibraryRequisition management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LibraryRequisitionController : ControllerBase
    {
        private readonly GenericRepository<LibraryRequisition> _repo;

        public LibraryRequisitionController(AppDbContext context)
            => _repo = new GenericRepository<LibraryRequisition>(context);

        /// <summary>Get all active LibraryRequisition records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LibraryRequisition>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get LibraryRequisition by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<LibraryRequisition>.Fail($"LibraryRequisition with Id {id} not found."));
            return Ok(ApiResponse<LibraryRequisition>.Ok(item));
        }

        /// <summary>Create a new LibraryRequisition</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LibraryRequisitionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryRequisition>.Fail("Validation failed."));

            var entity = new LibraryRequisition
            {
                ItemInfoId = r.ItemInfoId,
                LibrarianInfoId = r.LibrarianInfoId,
                LibraryBranchId = r.LibraryBranchId,
                EmployeeId = r.EmployeeId,
                Date = r.Date,
                Purpose = r.Purpose,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<LibraryRequisition>.Created(created));
        }

        /// <summary>Update an existing LibraryRequisition</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LibraryRequisitionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryRequisition>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.ItemInfoId = r.ItemInfoId;
                entity.LibrarianInfoId = r.LibrarianInfoId;
                entity.LibraryBranchId = r.LibraryBranchId;
                entity.EmployeeId = r.EmployeeId;
                entity.Date = r.Date;
                entity.Purpose = r.Purpose;
            });

            if (updated is null)
                return NotFound(ApiResponse<LibraryRequisition>.Fail($"LibraryRequisition with Id {id} not found."));

            return Ok(ApiResponse<LibraryRequisition>.Updated(updated));
        }

        /// <summary>Soft-delete a LibraryRequisition</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"LibraryRequisition with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"LibraryRequisition deleted successfully."));
        }

        /// <summary>Get total count of active LibraryRequisition records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
