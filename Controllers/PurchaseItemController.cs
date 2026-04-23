using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>PurchaseItem management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PurchaseItemController : ControllerBase
    {
        private readonly GenericRepository<PurchaseItem> _repo;

        public PurchaseItemController(AppDbContext context)
            => _repo = new GenericRepository<PurchaseItem>(context);

        /// <summary>Get all active PurchaseItem records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<PurchaseItem>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get PurchaseItem by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<PurchaseItem>.Fail($"PurchaseItem with Id {id} not found."));
            return Ok(ApiResponse<PurchaseItem>.Ok(item));
        }

        /// <summary>Create a new PurchaseItem</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PurchaseItemRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<PurchaseItem>.Fail("Validation failed."));

            var entity = new PurchaseItem
            {
                ItemQuotationId = r.ItemQuotationId,
                PurchaseItemDate = r.PurchaseItemDate,
                LibraryItemId = r.LibraryItemId,
                InvoiceNumber = r.InvoiceNumber,
                PartyId = r.PartyId,
                TotalAmount = r.TotalAmount,
                Discount = r.Discount,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<PurchaseItem>.Created(created));
        }

        /// <summary>Update an existing PurchaseItem</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] PurchaseItemRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<PurchaseItem>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.ItemQuotationId = r.ItemQuotationId;
                entity.PurchaseItemDate = r.PurchaseItemDate;
                entity.LibraryItemId = r.LibraryItemId;
                entity.InvoiceNumber = r.InvoiceNumber;
                entity.PartyId = r.PartyId;
                entity.TotalAmount = r.TotalAmount;
                entity.Discount = r.Discount;
            });

            if (updated is null)
                return NotFound(ApiResponse<PurchaseItem>.Fail($"PurchaseItem with Id {id} not found."));

            return Ok(ApiResponse<PurchaseItem>.Updated(updated));
        }

        /// <summary>Soft-delete a PurchaseItem</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"PurchaseItem with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"PurchaseItem deleted successfully."));
        }

        /// <summary>Get total count of active PurchaseItem records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
