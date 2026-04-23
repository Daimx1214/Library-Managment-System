using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>FineDefinition management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FineDefinitionController : ControllerBase
    {
        private readonly GenericRepository<FineDefinition> _repo;

        public FineDefinitionController(AppDbContext context)
            => _repo = new GenericRepository<FineDefinition>(context);

        /// <summary>Get all active FineDefinition records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<FineDefinition>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get FineDefinition by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<FineDefinition>.Fail($"FineDefinition with Id {id} not found."));
            return Ok(ApiResponse<FineDefinition>.Ok(item));
        }

        /// <summary>Create a new FineDefinition</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] FineDefinitionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<FineDefinition>.Fail("Validation failed."));

            var entity = new FineDefinition
            {
                FineCategoryId = r.FineCategoryId,
                ItemCopyId = r.ItemCopyId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<FineDefinition>.Created(created));
        }

        /// <summary>Update an existing FineDefinition</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] FineDefinitionRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<FineDefinition>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.FineCategoryId = r.FineCategoryId;
                entity.ItemCopyId = r.ItemCopyId;
            });

            if (updated is null)
                return NotFound(ApiResponse<FineDefinition>.Fail($"FineDefinition with Id {id} not found."));

            return Ok(ApiResponse<FineDefinition>.Updated(updated));
        }

        /// <summary>Soft-delete a FineDefinition</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"FineDefinition with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"FineDefinition deleted successfully."));
        }

        /// <summary>Get total count of active FineDefinition records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
