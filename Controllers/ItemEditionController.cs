using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>ItemEdition management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemEditionController : ControllerBase
    {
        private readonly GenericRepository<ItemEdition> _repo;

        public ItemEditionController(AppDbContext context)
            => _repo = new GenericRepository<ItemEdition>(context);

        /// <summary>Get all active ItemEdition records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ItemEdition>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get ItemEdition by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<ItemEdition>.Fail($"ItemEdition with Id {id} not found."));
            return Ok(ApiResponse<ItemEdition>.Ok(item));
        }

        /// <summary>Create a new ItemEdition</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ItemEditionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemEdition>.Fail("Validation failed."));

            var entity = new ItemEdition
            {
                ItemId = r.ItemId,
                Year = r.Year,
                EditionNumber = r.EditionNumber,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ItemEdition>.Created(created));
        }

        /// <summary>Update an existing ItemEdition</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemEditionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemEdition>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.ItemId = r.ItemId;
                entity.Year = r.Year;
                entity.EditionNumber = r.EditionNumber;
            });

            if (updated is null)
                return NotFound(ApiResponse<ItemEdition>.Fail($"ItemEdition with Id {id} not found."));

            return Ok(ApiResponse<ItemEdition>.Updated(updated));
        }

        /// <summary>Soft-delete a ItemEdition</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"ItemEdition with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"ItemEdition deleted successfully."));
        }

        /// <summary>Get total count of active ItemEdition records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
