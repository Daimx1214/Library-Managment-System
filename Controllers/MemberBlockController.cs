using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>MemberBlock management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MemberBlockController : ControllerBase
    {
        private readonly GenericRepository<MemberBlock> _repo;

        public MemberBlockController(AppDbContext context)
            => _repo = new GenericRepository<MemberBlock>(context);

        /// <summary>Get all active MemberBlock records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<MemberBlock>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get MemberBlock by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<MemberBlock>.Fail($"MemberBlock with Id {id} not found."));
            return Ok(ApiResponse<MemberBlock>.Ok(item));
        }

        /// <summary>Create a new MemberBlock</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] MemberBlockRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<MemberBlock>.Fail("Validation failed."));

            var entity = new MemberBlock
            {
                LibraryMemberId = r.LibraryMemberId,
                LibraryBranchId = r.LibraryBranchId,
                Reason = r.Reason,
                ActiveFrom = r.ActiveFrom,
                ActiveTo = r.ActiveTo,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<MemberBlock>.Created(created));
        }

        /// <summary>Update an existing MemberBlock</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] MemberBlockRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<MemberBlock>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.LibraryMemberId = r.LibraryMemberId;
                entity.LibraryBranchId = r.LibraryBranchId;
                entity.Reason = r.Reason;
                entity.ActiveFrom = r.ActiveFrom;
                entity.ActiveTo = r.ActiveTo;
            });

            if (updated is null)
                return NotFound(ApiResponse<MemberBlock>.Fail($"MemberBlock with Id {id} not found."));

            return Ok(ApiResponse<MemberBlock>.Updated(updated));
        }

        /// <summary>Soft-delete a MemberBlock</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"MemberBlock with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"MemberBlock deleted successfully."));
        }

        /// <summary>Get total count of active MemberBlock records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
