using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>PublisherInfo management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PublisherInfoController : ControllerBase
    {
        private readonly GenericRepository<PublisherInfo> _repo;

        public PublisherInfoController(AppDbContext context)
            => _repo = new GenericRepository<PublisherInfo>(context);

        /// <summary>Get all active PublisherInfo records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<PublisherInfo>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get PublisherInfo by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<PublisherInfo>.Fail($"PublisherInfo with Id {id} not found."));
            return Ok(ApiResponse<PublisherInfo>.Ok(item));
        }

        /// <summary>Create a new PublisherInfo</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PublisherInfoRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<PublisherInfo>.Fail("Validation failed."));

            var entity = new PublisherInfo
            {
                PublisherName = r.PublisherName,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<PublisherInfo>.Created(created));
        }

        /// <summary>Update an existing PublisherInfo</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] PublisherInfoRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<PublisherInfo>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.PublisherName = r.PublisherName;
            });

            if (updated is null)
                return NotFound(ApiResponse<PublisherInfo>.Fail($"PublisherInfo with Id {id} not found."));

            return Ok(ApiResponse<PublisherInfo>.Updated(updated));
        }

        /// <summary>Soft-delete a PublisherInfo</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"PublisherInfo with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"PublisherInfo deleted successfully."));
        }

        /// <summary>Get total count of active PublisherInfo records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
