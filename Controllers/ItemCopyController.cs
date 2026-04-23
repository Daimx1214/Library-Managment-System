using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>ItemCopy management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemCopyController : ControllerBase
    {
        private readonly GenericRepository<ItemCopy> _repo;

        public ItemCopyController(AppDbContext context)
            => _repo = new GenericRepository<ItemCopy>(context);

        /// <summary>Get all active ItemCopy records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ItemCopy>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get ItemCopy by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<ItemCopy>.Fail($"ItemCopy with Id {id} not found."));
            return Ok(ApiResponse<ItemCopy>.Ok(item));
        }

        /// <summary>Create a new ItemCopy</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ItemCopyRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemCopy>.Fail("Validation failed."));

            var entity = new ItemCopy
            {
                ItemInfoId = r.ItemInfoId,
                LibraryBranchId = r.LibraryBranchId,
                LibrarySectionId = r.LibrarySectionId,
                LibraryRackId = r.LibraryRackId,
                LibraryShelfId = r.LibraryShelfId,
                AccessionPatternId = r.AccessionPatternId,
                AccessionNumber = r.AccessionNumber,
                ConditionRemarks = r.ConditionRemarks,
                Barcode = r.Barcode,
                UnitCost = r.UnitCost,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ItemCopy>.Created(created));
        }

        /// <summary>Update an existing ItemCopy</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemCopyRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemCopy>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.ItemInfoId = r.ItemInfoId;
                entity.LibraryBranchId = r.LibraryBranchId;
                entity.LibrarySectionId = r.LibrarySectionId;
                entity.LibraryRackId = r.LibraryRackId;
                entity.LibraryShelfId = r.LibraryShelfId;
                entity.AccessionPatternId = r.AccessionPatternId;
                entity.AccessionNumber = r.AccessionNumber;
                entity.ConditionRemarks = r.ConditionRemarks;
                entity.Barcode = r.Barcode;
                entity.UnitCost = r.UnitCost;
            });

            if (updated is null)
                return NotFound(ApiResponse<ItemCopy>.Fail($"ItemCopy with Id {id} not found."));

            return Ok(ApiResponse<ItemCopy>.Updated(updated));
        }

        /// <summary>Soft-delete a ItemCopy</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"ItemCopy with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"ItemCopy deleted successfully."));
        }

        /// <summary>Get total count of active ItemCopy records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
