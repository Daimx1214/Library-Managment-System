using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Student management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class StudentController : ControllerBase
    {
        private readonly GenericRepository<Student> _repo;

        public StudentController(AppDbContext context)
            => _repo = new GenericRepository<Student>(context);

        /// <summary>Get all active Student records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Student>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Student by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Student>.Fail($"Student with Id {id} not found."));
            return Ok(ApiResponse<Student>.Ok(item));
        }

        /// <summary>Create a new Student</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] StudentRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Student>.Fail("Validation failed."));

            var entity = new Student
            {
                RegistrationNumber = r.RegistrationNumber,
                CandidateId = r.CandidateId,
                DegreeProgramId = r.DegreeProgramId,
                DegreeLevelId = r.DegreeLevelId,
                PartyId = r.PartyId,
                Quota = r.Quota,
                TimingId = r.TimingId,
                ProgramSessionId = r.ProgramSessionId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Student>.Created(created));
        }

        /// <summary>Update an existing Student</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] StudentRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Student>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.RegistrationNumber = r.RegistrationNumber;
                entity.CandidateId = r.CandidateId;
                entity.DegreeProgramId = r.DegreeProgramId;
                entity.DegreeLevelId = r.DegreeLevelId;
                entity.PartyId = r.PartyId;
                entity.Quota = r.Quota;
                entity.TimingId = r.TimingId;
                entity.ProgramSessionId = r.ProgramSessionId;
            });

            if (updated is null)
                return NotFound(ApiResponse<Student>.Fail($"Student with Id {id} not found."));

            return Ok(ApiResponse<Student>.Updated(updated));
        }

        /// <summary>Soft-delete a Student</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Student with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Student deleted successfully."));
        }

        /// <summary>Get total count of active Student records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
