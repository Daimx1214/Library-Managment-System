using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>AcquisitionRecord management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AcquisitionRecordController : ControllerBase
    {
        private readonly GenericRepository<AcquisitionRecord> _repo;

        public AcquisitionRecordController(AppDbContext context)
            => _repo = new GenericRepository<AcquisitionRecord>(context);

        /// <summary>Get all active AcquisitionRecord records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<AcquisitionRecord>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get AcquisitionRecord by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<AcquisitionRecord>.Fail($"AcquisitionRecord with Id {id} not found."));
            return Ok(ApiResponse<AcquisitionRecord>.Ok(item));
        }

        /// <summary>Create a new AcquisitionRecord</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] AcquisitionRecordRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<AcquisitionRecord>.Fail("Validation failed."));

            var entity = new AcquisitionRecord
            {
                Type = r.Type,
                PartyId = r.PartyId,
                Invoice = r.Invoice,
                Date = r.Date,
                Amount = r.Amount,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<AcquisitionRecord>.Created(created));
        }

        /// <summary>Update an existing AcquisitionRecord</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] AcquisitionRecordRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<AcquisitionRecord>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Type = r.Type;
                entity.PartyId = r.PartyId;
                entity.Invoice = r.Invoice;
                entity.Date = r.Date;
                entity.Amount = r.Amount;
            });

            if (updated is null)
                return NotFound(ApiResponse<AcquisitionRecord>.Fail($"AcquisitionRecord with Id {id} not found."));

            return Ok(ApiResponse<AcquisitionRecord>.Updated(updated));
        }

        /// <summary>Soft-delete a AcquisitionRecord</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"AcquisitionRecord with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"AcquisitionRecord deleted successfully."));
        }

        /// <summary>Get total count of active AcquisitionRecord records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
