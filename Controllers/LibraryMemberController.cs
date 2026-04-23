using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>LibraryMember management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LibraryMemberController : ControllerBase
    {
        private readonly GenericRepository<LibraryMember> _repo;

        public LibraryMemberController(AppDbContext context)
            => _repo = new GenericRepository<LibraryMember>(context);

        /// <summary>Get all active LibraryMember records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<LibraryMember>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get LibraryMember by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<LibraryMember>.Fail($"LibraryMember with Id {id} not found."));
            return Ok(ApiResponse<LibraryMember>.Ok(item));
        }

        /// <summary>Create a new LibraryMember</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LibraryMemberRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryMember>.Fail("Validation failed."));

            var entity = new LibraryMember
            {
                LibraryBranchId = r.LibraryBranchId,
                PartyId = r.PartyId,
                CardNo = r.CardNo,
                IssuedOn = r.IssuedOn,
                ExpiredOn = r.ExpiredOn,
                MembershipType = r.MembershipType,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<LibraryMember>.Created(created));
        }

        /// <summary>Update an existing LibraryMember</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] LibraryMemberRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<LibraryMember>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.LibraryBranchId = r.LibraryBranchId;
                entity.PartyId = r.PartyId;
                entity.CardNo = r.CardNo;
                entity.IssuedOn = r.IssuedOn;
                entity.ExpiredOn = r.ExpiredOn;
                entity.MembershipType = r.MembershipType;
            });

            if (updated is null)
                return NotFound(ApiResponse<LibraryMember>.Fail($"LibraryMember with Id {id} not found."));

            return Ok(ApiResponse<LibraryMember>.Updated(updated));
        }

        /// <summary>Soft-delete a LibraryMember</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"LibraryMember with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"LibraryMember deleted successfully."));
        }

        /// <summary>Get total count of active LibraryMember records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
