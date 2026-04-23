using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>ReturnItem management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ReturnItemController : ControllerBase
    {
        private readonly GenericRepository<ReturnItem> _repo;

        public ReturnItemController(AppDbContext context)
            => _repo = new GenericRepository<ReturnItem>(context);

        /// <summary>Get all active ReturnItem records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ReturnItem>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get ReturnItem by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<ReturnItem>.Fail($"ReturnItem with Id {id} not found."));
            return Ok(ApiResponse<ReturnItem>.Ok(item));
        }

        /// <summary>Create a new ReturnItem</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ReturnItemRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ReturnItem>.Fail("Validation failed."));

            var entity = new ReturnItem
            {
                LibraryItemId = r.LibraryItemId,
                PurchaseItemId = r.PurchaseItemId,
                IssuanceId = r.IssuanceId,
                ItemInfoId = r.ItemInfoId,
                ReturnDate = r.ReturnDate,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ReturnItem>.Created(created));
        }

        /// <summary>Update an existing ReturnItem</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ReturnItemRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ReturnItem>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.LibraryItemId = r.LibraryItemId;
                entity.PurchaseItemId = r.PurchaseItemId;
                entity.IssuanceId = r.IssuanceId;
                entity.ItemInfoId = r.ItemInfoId;
                entity.ReturnDate = r.ReturnDate;
            });

            if (updated is null)
                return NotFound(ApiResponse<ReturnItem>.Fail($"ReturnItem with Id {id} not found."));

            return Ok(ApiResponse<ReturnItem>.Updated(updated));
        }

        /// <summary>Soft-delete a ReturnItem</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"ReturnItem with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"ReturnItem deleted successfully."));
        }

        /// <summary>Get total count of active ReturnItem records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
