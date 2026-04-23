using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Department management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DepartmentController : ControllerBase
    {
        private readonly GenericRepository<Department> _repo;

        public DepartmentController(AppDbContext context)
            => _repo = new GenericRepository<Department>(context);

        /// <summary>Get all active Department records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Department>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Department by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Department>.Fail($"Department with Id {id} not found."));
            return Ok(ApiResponse<Department>.Ok(item));
        }

        /// <summary>Create a new Department</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] DepartmentRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Department>.Fail("Validation failed."));

            var entity = new Department
            {
                Name = r.Name,
                Code = r.Code,
                InstituteId = r.InstituteId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Department>.Created(created));
        }

        /// <summary>Update an existing Department</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] DepartmentRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Department>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Code = r.Code;
                entity.InstituteId = r.InstituteId;
            });

            if (updated is null)
                return NotFound(ApiResponse<Department>.Fail($"Department with Id {id} not found."));

            return Ok(ApiResponse<Department>.Updated(updated));
        }

        /// <summary>Soft-delete a Department</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Department with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Department deleted successfully."));
        }

        /// <summary>Get total count of active Department records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
