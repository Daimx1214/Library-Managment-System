using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>LibrarianInfo management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LibrarianInfoController : ControllerBase
    {
        private readonly GenericRepository<LibrarianInfo> _repo;

        public LibrarianInfoController(AppDbContext context)
            => _repo = new GenericRepository<LibrarianInfo>(context);

        /// <summary>Get all active LibrarianInfo records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LibrarianInfo>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get LibrarianInfo by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<LibrarianInfo>.Fail($"LibrarianInfo with Id {id} not found."));
            return Ok(ApiResponse<LibrarianInfo>.Ok(item));
        }

        /// <summary>Create a new LibrarianInfo</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LibrarianInfoRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibrarianInfo>.Fail("Validation failed."));

            var entity = new LibrarianInfo
            {
                Name = r.Name,
                Address = r.Address,
                Email = r.Email,
                Contact = r.Contact,
                LibrarySectionId = r.LibrarySectionId,
                LibraryBranchManagementId = r.LibraryBranchManagementId,
                LibraryTransactionId = r.LibraryTransactionId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<LibrarianInfo>.Created(created));
        }

        /// <summary>Update an existing LibrarianInfo</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LibrarianInfoRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibrarianInfo>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Address = r.Address;
                entity.Email = r.Email;
                entity.Contact = r.Contact;
                entity.LibrarySectionId = r.LibrarySectionId;
                entity.LibraryBranchManagementId = r.LibraryBranchManagementId;
                entity.LibraryTransactionId = r.LibraryTransactionId;
            });

            if (updated is null)
                return NotFound(ApiResponse<LibrarianInfo>.Fail($"LibrarianInfo with Id {id} not found."));

            return Ok(ApiResponse<LibrarianInfo>.Updated(updated));
        }

        /// <summary>Soft-delete a LibrarianInfo</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"LibrarianInfo with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"LibrarianInfo deleted successfully."));
        }

        /// <summary>Get total count of active LibrarianInfo records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
