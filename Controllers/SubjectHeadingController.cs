using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>SubjectHeading management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SubjectHeadingController : ControllerBase
    {
        private readonly GenericRepository<SubjectHeading> _repo;

        public SubjectHeadingController(AppDbContext context)
            => _repo = new GenericRepository<SubjectHeading>(context);

        /// <summary>Get all active SubjectHeading records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<SubjectHeading>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get SubjectHeading by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<SubjectHeading>.Fail($"SubjectHeading with Id {id} not found."));
            return Ok(ApiResponse<SubjectHeading>.Ok(item));
        }

        /// <summary>Create a new SubjectHeading</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] SubjectHeadingRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<SubjectHeading>.Fail("Validation failed."));

            var entity = new SubjectHeading
            {
                Name = r.Name,
                Code = r.Code,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<SubjectHeading>.Created(created));
        }

        /// <summary>Update an existing SubjectHeading</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] SubjectHeadingRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<SubjectHeading>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
            });

            if (updated is null)
                return NotFound(ApiResponse<SubjectHeading>.Fail($"SubjectHeading with Id {id} not found."));

            return Ok(ApiResponse<SubjectHeading>.Updated(updated));
        }

        /// <summary>Soft-delete a SubjectHeading</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"SubjectHeading with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"SubjectHeading deleted successfully."));
        }

        /// <summary>Get total count of active SubjectHeading records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
