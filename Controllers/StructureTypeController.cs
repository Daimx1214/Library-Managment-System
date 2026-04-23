using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>StructureType management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StructureTypeController : ControllerBase
    {
        private readonly GenericRepository<StructureType> _repo;

        public StructureTypeController(AppDbContext context)
            => _repo = new GenericRepository<StructureType>(context);

        /// <summary>Get all active StructureType records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<StructureType>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get StructureType by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<StructureType>.Fail($"StructureType with Id {id} not found."));
            return Ok(ApiResponse<StructureType>.Ok(item));
        }

        /// <summary>Create a new StructureType</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] StructureTypeRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<StructureType>.Fail("Validation failed."));

            var entity = new StructureType
            {
                Name = r.Name,
                Description = r.Description,
                Code = r.Code,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<StructureType>.Created(created));
        }

        /// <summary>Update an existing StructureType</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] StructureTypeRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<StructureType>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Description = r.Description;
                entity.Code = r.Code;
            });

            if (updated is null)
                return NotFound(ApiResponse<StructureType>.Fail($"StructureType with Id {id} not found."));

            return Ok(ApiResponse<StructureType>.Updated(updated));
        }

        /// <summary>Soft-delete a StructureType</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"StructureType with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"StructureType deleted successfully."));
        }

        /// <summary>Get total count of active StructureType records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
