using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Building management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BuildingController : ControllerBase
    {
        private readonly GenericRepository<Building> _repo;

        public BuildingController(AppDbContext context)
            => _repo = new GenericRepository<Building>(context);

        /// <summary>Get all active Building records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Building>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Building by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Building>.Fail($"Building with Id {id} not found."));
            return Ok(ApiResponse<Building>.Ok(item));
        }

        /// <summary>Create a new Building</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] BuildingRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Building>.Fail("Validation failed."));

            var entity = new Building
            {
                Name = r.Name,
                Code = r.Code,
                Description = r.Description,
                BlockId = r.BlockId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Building>.Created(created));
        }

        /// <summary>Update an existing Building</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] BuildingRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Building>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
                entity.Description = r.Description;
                entity.BlockId = r.BlockId;
            });

            if (updated is null)
                return NotFound(ApiResponse<Building>.Fail($"Building with Id {id} not found."));

            return Ok(ApiResponse<Building>.Updated(updated));
        }

        /// <summary>Soft-delete a Building</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Building with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Building deleted successfully."));
        }

        /// <summary>Get total count of active Building records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
