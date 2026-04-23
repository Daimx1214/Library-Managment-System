using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>StructureDesignation management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StructureDesignationController : ControllerBase
    {
        private readonly GenericRepository<StructureDesignation> _repo;

        public StructureDesignationController(AppDbContext context)
            => _repo = new GenericRepository<StructureDesignation>(context);

        /// <summary>Get all active StructureDesignation records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<StructureDesignation>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get StructureDesignation by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<StructureDesignation>.Fail($"StructureDesignation with Id {id} not found."));
            return Ok(ApiResponse<StructureDesignation>.Ok(item));
        }

        /// <summary>Create a new StructureDesignation</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] StructureDesignationRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<StructureDesignation>.Fail("Validation failed."));

            var entity = new StructureDesignation
            {
                DesignationId = r.DesignationId,
                StructureUnitId = r.StructureUnitId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<StructureDesignation>.Created(created));
        }

        /// <summary>Update an existing StructureDesignation</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] StructureDesignationRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<StructureDesignation>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.DesignationId = r.DesignationId;
                entity.StructureUnitId = r.StructureUnitId;
            });

            if (updated is null)
                return NotFound(ApiResponse<StructureDesignation>.Fail($"StructureDesignation with Id {id} not found."));

            return Ok(ApiResponse<StructureDesignation>.Updated(updated));
        }

        /// <summary>Soft-delete a StructureDesignation</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"StructureDesignation with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"StructureDesignation deleted successfully."));
        }

        /// <summary>Get total count of active StructureDesignation records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
