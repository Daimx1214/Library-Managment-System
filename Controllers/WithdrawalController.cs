using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Withdrawal management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WithdrawalController : ControllerBase
    {
        private readonly GenericRepository<Withdrawal> _repo;

        public WithdrawalController(AppDbContext context)
            => _repo = new GenericRepository<Withdrawal>(context);

        /// <summary>Get all active Withdrawal records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Withdrawal>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Withdrawal by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Withdrawal>.Fail($"Withdrawal with Id {id} not found."));
            return Ok(ApiResponse<Withdrawal>.Ok(item));
        }

        /// <summary>Create a new Withdrawal</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] WithdrawalRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Withdrawal>.Fail("Validation failed."));

            var entity = new Withdrawal
            {
                ItemCopyId = r.ItemCopyId,
                Date = r.Date,
                LibraryId = r.LibraryId,
                Reason = r.Reason,
                EmployeeId = r.EmployeeId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Withdrawal>.Created(created));
        }

        /// <summary>Update an existing Withdrawal</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] WithdrawalRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Withdrawal>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.ItemCopyId = r.ItemCopyId;
                entity.Date = r.Date;
                entity.LibraryId = r.LibraryId;
                entity.Reason = r.Reason;
                entity.EmployeeId = r.EmployeeId;
            });

            if (updated is null)
                return NotFound(ApiResponse<Withdrawal>.Fail($"Withdrawal with Id {id} not found."));

            return Ok(ApiResponse<Withdrawal>.Updated(updated));
        }

        /// <summary>Soft-delete a Withdrawal</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Withdrawal with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Withdrawal deleted successfully."));
        }

        /// <summary>Get total count of active Withdrawal records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
