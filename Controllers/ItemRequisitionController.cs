using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>ItemRequisition management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemRequisitionController : ControllerBase
    {
        private readonly GenericRepository<ItemRequisition> _repo;

        public ItemRequisitionController(AppDbContext context)
            => _repo = new GenericRepository<ItemRequisition>(context);

        /// <summary>Get all active ItemRequisition records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ItemRequisition>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get ItemRequisition by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<ItemRequisition>.Fail($"ItemRequisition with Id {id} not found."));
            return Ok(ApiResponse<ItemRequisition>.Ok(item));
        }

        /// <summary>Create a new ItemRequisition</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ItemRequisitionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemRequisition>.Fail("Validation failed."));

            var entity = new ItemRequisition
            {
                LibraryBranchId = r.LibraryBranchId,
                ItemInfoId = r.ItemInfoId,
                Quantity = r.Quantity,
                LibraryRackId = r.LibraryRackId,
                EstimateCost = r.EstimateCost,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ItemRequisition>.Created(created));
        }

        /// <summary>Update an existing ItemRequisition</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemRequisitionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemRequisition>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.LibraryBranchId = r.LibraryBranchId;
                entity.ItemInfoId = r.ItemInfoId;
                entity.Quantity = r.Quantity;
                entity.LibraryRackId = r.LibraryRackId;
                entity.EstimateCost = r.EstimateCost;
            });

            if (updated is null)
                return NotFound(ApiResponse<ItemRequisition>.Fail($"ItemRequisition with Id {id} not found."));

            return Ok(ApiResponse<ItemRequisition>.Updated(updated));
        }

        /// <summary>Soft-delete a ItemRequisition</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"ItemRequisition with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"ItemRequisition deleted successfully."));
        }

        /// <summary>Get total count of active ItemRequisition records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
