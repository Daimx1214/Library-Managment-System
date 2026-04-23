using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>AuthorItemInfo management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthorItemInfoController : ControllerBase
    {
        private readonly GenericRepository<AuthorItemInfo> _repo;

        public AuthorItemInfoController(AppDbContext context)
            => _repo = new GenericRepository<AuthorItemInfo>(context);

        /// <summary>Get all active AuthorItemInfo records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<AuthorItemInfo>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get AuthorItemInfo by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<AuthorItemInfo>.Fail($"AuthorItemInfo with Id {id} not found."));
            return Ok(ApiResponse<AuthorItemInfo>.Ok(item));
        }

        /// <summary>Create a new AuthorItemInfo</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AuthorItemInfoRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<AuthorItemInfo>.Fail("Validation failed."));

            var entity = new AuthorItemInfo
            {
                AuthorId = r.AuthorId,
                ItemInfoId = r.ItemInfoId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<AuthorItemInfo>.Created(created));
        }

        /// <summary>Update an existing AuthorItemInfo</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorItemInfoRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<AuthorItemInfo>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.AuthorId = r.AuthorId;
                entity.ItemInfoId = r.ItemInfoId;
            });

            if (updated is null)
                return NotFound(ApiResponse<AuthorItemInfo>.Fail($"AuthorItemInfo with Id {id} not found."));

            return Ok(ApiResponse<AuthorItemInfo>.Updated(updated));
        }

        /// <summary>Soft-delete a AuthorItemInfo</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"AuthorItemInfo with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"AuthorItemInfo deleted successfully."));
        }

        /// <summary>Get total count of active AuthorItemInfo records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
