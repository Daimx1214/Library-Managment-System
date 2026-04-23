using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Issuance management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class IssuanceController : ControllerBase
    {
        private readonly GenericRepository<Issuance> _repo;

        public IssuanceController(AppDbContext context)
            => _repo = new GenericRepository<Issuance>(context);

        /// <summary>Get all active Issuance records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Issuance>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Issuance by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Issuance>.Fail($"Issuance with Id {id} not found."));
            return Ok(ApiResponse<Issuance>.Ok(item));
        }

        /// <summary>Create a new Issuance</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] IssuanceRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Issuance>.Fail("Validation failed."));

            var entity = new Issuance
            {
                LibraryMemberId = r.LibraryMemberId,
                ItemCopyId = r.ItemCopyId,
                DurationId = r.DurationId,
                IssueDate = r.IssueDate,
                DurationNumber = r.DurationNumber,
                FineAccrued = r.FineAccrued,
                RenewalsUsed = r.RenewalsUsed,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Issuance>.Created(created));
        }

        /// <summary>Update an existing Issuance</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] IssuanceRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Issuance>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.LibraryMemberId = r.LibraryMemberId;
                entity.ItemCopyId = r.ItemCopyId;
                entity.DurationId = r.DurationId;
                entity.IssueDate = r.IssueDate;
                entity.DurationNumber = r.DurationNumber;
                entity.FineAccrued = r.FineAccrued;
                entity.RenewalsUsed = r.RenewalsUsed;
            });

            if (updated is null)
                return NotFound(ApiResponse<Issuance>.Fail($"Issuance with Id {id} not found."));

            return Ok(ApiResponse<Issuance>.Updated(updated));
        }

        /// <summary>Soft-delete a Issuance</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Issuance with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Issuance deleted successfully."));
        }

        /// <summary>Get total count of active Issuance records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
