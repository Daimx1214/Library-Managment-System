using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>UserFine management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserFineController : ControllerBase
    {
        private readonly GenericRepository<UserFine> _repo;

        public UserFineController(AppDbContext context)
            => _repo = new GenericRepository<UserFine>(context);

        /// <summary>Get all active UserFine records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<UserFine>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get UserFine by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<UserFine>.Fail($"UserFine with Id {id} not found."));
            return Ok(ApiResponse<UserFine>.Ok(item));
        }

        /// <summary>Create a new UserFine</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserFineRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<UserFine>.Fail("Validation failed."));

            var entity = new UserFine
            {
                FineAllocationRef = r.FineAllocationRef,
                LibraryMemberId = r.LibraryMemberId,
                FineDefinitionId = r.FineDefinitionId,
                ItemCopyId = r.ItemCopyId,
                TaxPercentagePerDay = r.TaxPercentagePerDay,
                FineAccrued = r.FineAccrued,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<UserFine>.Created(created));
        }

        /// <summary>Update an existing UserFine</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] UserFineRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<UserFine>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.FineAllocationRef = r.FineAllocationRef;
                entity.LibraryMemberId = r.LibraryMemberId;
                entity.FineDefinitionId = r.FineDefinitionId;
                entity.ItemCopyId = r.ItemCopyId;
                entity.TaxPercentagePerDay = r.TaxPercentagePerDay;
                entity.FineAccrued = r.FineAccrued;
            });

            if (updated is null)
                return NotFound(ApiResponse<UserFine>.Fail($"UserFine with Id {id} not found."));

            return Ok(ApiResponse<UserFine>.Updated(updated));
        }

        /// <summary>Soft-delete a UserFine</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"UserFine with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"UserFine deleted successfully."));
        }

        /// <summary>Get total count of active UserFine records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
