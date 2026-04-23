using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>CirculationRule management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CirculationRuleController : ControllerBase
    {
        private readonly GenericRepository<CirculationRule> _repo;

        public CirculationRuleController(AppDbContext context)
            => _repo = new GenericRepository<CirculationRule>(context);

        /// <summary>Get all active CirculationRule records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<CirculationRule>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get CirculationRule by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<CirculationRule>.Fail($"CirculationRule with Id {id} not found."));
            return Ok(ApiResponse<CirculationRule>.Ok(item));
        }

        /// <summary>Create a new CirculationRule</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CirculationRuleRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<CirculationRule>.Fail("Validation failed."));

            var entity = new CirculationRule
            {
                PartyId = r.PartyId,
                MaxCurrentLoans = r.MaxCurrentLoans,
                MaxCurrentReserves = r.MaxCurrentReserves,
                LoanDays = r.LoanDays,
                GraceDay = r.GraceDay,
                MaxRenewals = r.MaxRenewals,
                OverdueFinePerDay = r.OverdueFinePerDay,
                MaxFinePerItem = r.MaxFinePerItem,
                LibraryBranchId = r.LibraryBranchId,
                LostChargesItem = r.LostChargesItem,
                DamageValue = r.DamageValue,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<CirculationRule>.Created(created));
        }

        /// <summary>Update an existing CirculationRule</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] CirculationRuleRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<CirculationRule>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.PartyId = r.PartyId;
                entity.MaxCurrentLoans = r.MaxCurrentLoans;
                entity.MaxCurrentReserves = r.MaxCurrentReserves;
                entity.LoanDays = r.LoanDays;
                entity.GraceDay = r.GraceDay;
                entity.MaxRenewals = r.MaxRenewals;
                entity.OverdueFinePerDay = r.OverdueFinePerDay;
                entity.MaxFinePerItem = r.MaxFinePerItem;
                entity.LibraryBranchId = r.LibraryBranchId;
                entity.LostChargesItem = r.LostChargesItem;
                entity.DamageValue = r.DamageValue;
            });

            if (updated is null)
                return NotFound(ApiResponse<CirculationRule>.Fail($"CirculationRule with Id {id} not found."));

            return Ok(ApiResponse<CirculationRule>.Updated(updated));
        }

        /// <summary>Soft-delete a CirculationRule</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"CirculationRule with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"CirculationRule deleted successfully."));
        }

        /// <summary>Get total count of active CirculationRule records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
