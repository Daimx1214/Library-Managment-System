using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>FineTransaction management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FineTransactionController : ControllerBase
    {
        private readonly GenericRepository<FineTransaction> _repo;

        public FineTransactionController(AppDbContext context)
            => _repo = new GenericRepository<FineTransaction>(context);

        /// <summary>Get all active FineTransaction records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<FineTransaction>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get FineTransaction by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<FineTransaction>.Fail($"FineTransaction with Id {id} not found."));
            return Ok(ApiResponse<FineTransaction>.Ok(item));
        }

        /// <summary>Create a new FineTransaction</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] FineTransactionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<FineTransaction>.Fail("Validation failed."));

            var entity = new FineTransaction
            {
                FineAllocationRef = r.FineAllocationRef,
                FineAllocationDate = r.FineAllocationDate,
                LibraryBranchId = r.LibraryBranchId,
                Reason = r.Reason,
                Amount = r.Amount,
                BankAccount = r.BankAccount,
                PaidOn = r.PaidOn,
                PaymentRef = r.PaymentRef,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<FineTransaction>.Created(created));
        }

        /// <summary>Update an existing FineTransaction</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] FineTransactionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<FineTransaction>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.FineAllocationRef = r.FineAllocationRef;
                entity.FineAllocationDate = r.FineAllocationDate;
                entity.LibraryBranchId = r.LibraryBranchId;
                entity.Reason = r.Reason;
                entity.Amount = r.Amount;
                entity.BankAccount = r.BankAccount;
                entity.PaidOn = r.PaidOn;
                entity.PaymentRef = r.PaymentRef;
            });

            if (updated is null)
                return NotFound(ApiResponse<FineTransaction>.Fail($"FineTransaction with Id {id} not found."));

            return Ok(ApiResponse<FineTransaction>.Updated(updated));
        }

        /// <summary>Soft-delete a FineTransaction</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"FineTransaction with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"FineTransaction deleted successfully."));
        }

        /// <summary>Get total count of active FineTransaction records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
