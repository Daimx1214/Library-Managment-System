using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Faculty management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class FacultyController : ControllerBase
    {
        private readonly GenericRepository<Faculty> _repo;

        public FacultyController(AppDbContext context)
            => _repo = new GenericRepository<Faculty>(context);

        /// <summary>Get all active Faculty records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Faculty>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Faculty by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Faculty>.Fail($"Faculty with Id {id} not found."));
            return Ok(ApiResponse<Faculty>.Ok(item));
        }

        /// <summary>Create a new Faculty</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] FacultyRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Faculty>.Fail("Validation failed."));

            var entity = new Faculty
            {
                Name = r.Name,
                Code = r.Code,
                Description = r.Description,
                EstablishedIn = r.EstablishedIn,
                CampusId = r.CampusId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Faculty>.Created(created));
        }

        /// <summary>Update an existing Faculty</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] FacultyRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Faculty>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
                entity.Description = r.Description;
                entity.EstablishedIn = r.EstablishedIn;
                entity.CampusId = r.CampusId;
            });

            if (updated is null)
                return NotFound(ApiResponse<Faculty>.Fail($"Faculty with Id {id} not found."));

            return Ok(ApiResponse<Faculty>.Updated(updated));
        }

        /// <summary>Soft-delete a Faculty</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Faculty with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Faculty deleted successfully."));
        }

        /// <summary>Get total count of active Faculty records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
