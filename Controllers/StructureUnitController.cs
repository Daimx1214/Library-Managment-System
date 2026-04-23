using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>StructureUnit management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StructureUnitController : ControllerBase
    {
        private readonly GenericRepository<StructureUnit> _repo;

        public StructureUnitController(AppDbContext context)
            => _repo = new GenericRepository<StructureUnit>(context);

        /// <summary>Get all active StructureUnit records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<StructureUnit>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get StructureUnit by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<StructureUnit>.Fail($"StructureUnit with Id {id} not found."));
            return Ok(ApiResponse<StructureUnit>.Ok(item));
        }

        /// <summary>Create a new StructureUnit</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] StructureUnitRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<StructureUnit>.Fail("Validation failed."));

            var entity = new StructureUnit
            {
                Name = r.Name,
                Code = r.Code,
                Level = r.Level,
                StructureTypeId = r.StructureTypeId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<StructureUnit>.Created(created));
        }

        /// <summary>Update an existing StructureUnit</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] StructureUnitRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<StructureUnit>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
                entity.Level = r.Level;
                entity.StructureTypeId = r.StructureTypeId;
            });

            if (updated is null)
                return NotFound(ApiResponse<StructureUnit>.Fail($"StructureUnit with Id {id} not found."));

            return Ok(ApiResponse<StructureUnit>.Updated(updated));
        }

        /// <summary>Soft-delete a StructureUnit</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"StructureUnit with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"StructureUnit deleted successfully."));
        }

        /// <summary>Get total count of active StructureUnit records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
