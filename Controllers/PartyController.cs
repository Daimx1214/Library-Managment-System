using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Party management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class PartyController : ControllerBase
    {
        private readonly GenericRepository<Party> _repo;

        public PartyController(AppDbContext context)
            => _repo = new GenericRepository<Party>(context);

        /// <summary>Get all active Party records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Party>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Party by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Party>.Fail($"Party with Id {id} not found."));
            return Ok(ApiResponse<Party>.Ok(item));
        }

        /// <summary>Create a new Party</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] PartyRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Party>.Fail("Validation failed."));

            var entity = new Party
            {
                PartyName = r.PartyName,
                PartyContact = r.PartyContact,
                PartyPositionId = r.PartyPositionId,
                PartyCompanyId = r.PartyCompanyId,
                PartyTypeId = r.PartyTypeId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Party>.Created(created));
        }

        /// <summary>Update an existing Party</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] PartyRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Party>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.PartyName = r.PartyName;
                entity.PartyContact = r.PartyContact;
                entity.PartyPositionId = r.PartyPositionId;
                entity.PartyCompanyId = r.PartyCompanyId;
                entity.PartyTypeId = r.PartyTypeId;
            });

            if (updated is null)
                return NotFound(ApiResponse<Party>.Fail($"Party with Id {id} not found."));

            return Ok(ApiResponse<Party>.Updated(updated));
        }

        /// <summary>Soft-delete a Party</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Party with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Party deleted successfully."));
        }

        /// <summary>Get total count of active Party records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
