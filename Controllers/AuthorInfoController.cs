using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>AuthorInfo management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthorInfoController : ControllerBase
    {
        private readonly GenericRepository<AuthorInfo> _repo;

        public AuthorInfoController(AppDbContext context)
            => _repo = new GenericRepository<AuthorInfo>(context);

        /// <summary>Get all active AuthorInfo records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<AuthorInfo>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get AuthorInfo by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<AuthorInfo>.Fail($"AuthorInfo with Id {id} not found."));
            return Ok(ApiResponse<AuthorInfo>.Ok(item));
        }

        /// <summary>Create a new AuthorInfo</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AuthorInfoRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<AuthorInfo>.Fail("Validation failed."));

            var entity = new AuthorInfo
            {
                AuthorName = r.AuthorName,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<AuthorInfo>.Created(created));
        }

        /// <summary>Update an existing AuthorInfo</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorInfoRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<AuthorInfo>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.AuthorName = r.AuthorName;
            });

            if (updated is null)
                return NotFound(ApiResponse<AuthorInfo>.Fail($"AuthorInfo with Id {id} not found."));

            return Ok(ApiResponse<AuthorInfo>.Updated(updated));
        }

        /// <summary>Soft-delete a AuthorInfo</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"AuthorInfo with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"AuthorInfo deleted successfully."));
        }

        /// <summary>Get total count of active AuthorInfo records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
