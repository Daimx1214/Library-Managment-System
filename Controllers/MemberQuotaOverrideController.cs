using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>MemberQuotaOverride management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MemberQuotaOverrideController : ControllerBase
    {
        private readonly GenericRepository<MemberQuotaOverride> _repo;

        public MemberQuotaOverrideController(AppDbContext context)
            => _repo = new GenericRepository<MemberQuotaOverride>(context);

        /// <summary>Get all active MemberQuotaOverride records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<MemberQuotaOverride>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get MemberQuotaOverride by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<MemberQuotaOverride>.Fail($"MemberQuotaOverride with Id {id} not found."));
            return Ok(ApiResponse<MemberQuotaOverride>.Ok(item));
        }

        /// <summary>Create a new MemberQuotaOverride</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] MemberQuotaOverrideRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<MemberQuotaOverride>.Fail("Validation failed."));

            var entity = new MemberQuotaOverride
            {
                LibraryMemberId = r.LibraryMemberId,
                LibraryBranchId = r.LibraryBranchId,
                MaxCurrentLoans = r.MaxCurrentLoans,
                MaxCurrentReserves = r.MaxCurrentReserves,
                LoanDays = r.LoanDays,
                GraceDay = r.GraceDay,
                MaxRenewals = r.MaxRenewals,
                OverdueFinePerDay = r.OverdueFinePerDay,
                MaxFinePerItem = r.MaxFinePerItem,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<MemberQuotaOverride>.Created(created));
        }

        /// <summary>Update an existing MemberQuotaOverride</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] MemberQuotaOverrideRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<MemberQuotaOverride>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.LibraryMemberId = r.LibraryMemberId;
                entity.LibraryBranchId = r.LibraryBranchId;
                entity.MaxCurrentLoans = r.MaxCurrentLoans;
                entity.MaxCurrentReserves = r.MaxCurrentReserves;
                entity.LoanDays = r.LoanDays;
                entity.GraceDay = r.GraceDay;
                entity.MaxRenewals = r.MaxRenewals;
                entity.OverdueFinePerDay = r.OverdueFinePerDay;
                entity.MaxFinePerItem = r.MaxFinePerItem;
            });

            if (updated is null)
                return NotFound(ApiResponse<MemberQuotaOverride>.Fail($"MemberQuotaOverride with Id {id} not found."));

            return Ok(ApiResponse<MemberQuotaOverride>.Updated(updated));
        }

        /// <summary>Soft-delete a MemberQuotaOverride</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"MemberQuotaOverride with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"MemberQuotaOverride deleted successfully."));
        }

        /// <summary>Get total count of active MemberQuotaOverride records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
