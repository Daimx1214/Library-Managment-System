using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>ItemQuotation management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemQuotationController : ControllerBase
    {
        private readonly GenericRepository<ItemQuotation> _repo;

        public ItemQuotationController(AppDbContext context)
            => _repo = new GenericRepository<ItemQuotation>(context);

        /// <summary>Get all active ItemQuotation records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ItemQuotation>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get ItemQuotation by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<ItemQuotation>.Fail($"ItemQuotation with Id {id} not found."));
            return Ok(ApiResponse<ItemQuotation>.Ok(item));
        }

        /// <summary>Create a new ItemQuotation</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ItemQuotationRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemQuotation>.Fail("Validation failed."));

            var entity = new ItemQuotation
            {
                PartyId = r.PartyId,
                LibraryRackId = r.LibraryRackId,
                QuotationDate = r.QuotationDate,
                Amount = r.Amount,
                Discount = r.Discount,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ItemQuotation>.Created(created));
        }

        /// <summary>Update an existing ItemQuotation</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemQuotationRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemQuotation>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.PartyId = r.PartyId;
                entity.LibraryRackId = r.LibraryRackId;
                entity.QuotationDate = r.QuotationDate;
                entity.Amount = r.Amount;
                entity.Discount = r.Discount;
            });

            if (updated is null)
                return NotFound(ApiResponse<ItemQuotation>.Fail($"ItemQuotation with Id {id} not found."));

            return Ok(ApiResponse<ItemQuotation>.Updated(updated));
        }

        /// <summary>Soft-delete a ItemQuotation</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"ItemQuotation with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"ItemQuotation deleted successfully."));
        }

        /// <summary>Get total count of active ItemQuotation records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
