using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Institude management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class InstitudeController : ControllerBase
    {
        private readonly GenericRepository<Institude> _repo;

        public InstitudeController(AppDbContext context)
            => _repo = new GenericRepository<Institude>(context);

        /// <summary>Get all active Institude records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Institude>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Institude by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Institude>.Fail($"Institude with Id {id} not found."));
            return Ok(ApiResponse<Institude>.Ok(item));
        }

        /// <summary>Create a new Institude</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] InstitudeRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Institude>.Fail("Validation failed."));

            var entity = new Institude
            {
                Name = r.Name,
                Code = r.Code,
                FacultyId = r.FacultyId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Institude>.Created(created));
        }

        /// <summary>Update an existing Institude</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] InstitudeRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Institude>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
                entity.FacultyId = r.FacultyId;
            });

            if (updated is null)
                return NotFound(ApiResponse<Institude>.Fail($"Institude with Id {id} not found."));

            return Ok(ApiResponse<Institude>.Updated(updated));
        }

        /// <summary>Soft-delete a Institude</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Institude with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Institude deleted successfully."));
        }

        /// <summary>Get total count of active Institude records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
