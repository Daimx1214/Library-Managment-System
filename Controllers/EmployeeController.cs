using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>Employee management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmployeeController : ControllerBase
    {
        private readonly GenericRepository<Employee> _repo;

        public EmployeeController(AppDbContext context)
            => _repo = new GenericRepository<Employee>(context);

        /// <summary>Get all active Employee records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<Employee>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get Employee by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<Employee>.Fail($"Employee with Id {id} not found."));
            return Ok(ApiResponse<Employee>.Ok(item));
        }

        /// <summary>Create a new Employee</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EmployeeRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Employee>.Fail("Validation failed."));

            var entity = new Employee
            {
                Name = r.Name,
                Contact = r.Contact,
                Code = r.Code,
                JoiningDate = r.JoiningDate,
                LeavingDate = r.LeavingDate,
                CurrentRelationHistoryId = r.CurrentRelationHistoryId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<Employee>.Created(created));
        }

        /// <summary>Update an existing Employee</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<Employee>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Name = r.Name;
                entity.Contact = r.Contact;
                entity.Code = r.Code;
                entity.JoiningDate = r.JoiningDate;
                entity.LeavingDate = r.LeavingDate;
                entity.CurrentRelationHistoryId = r.CurrentRelationHistoryId;
            });

            if (updated is null)
                return NotFound(ApiResponse<Employee>.Fail($"Employee with Id {id} not found."));

            return Ok(ApiResponse<Employee>.Updated(updated));
        }

        /// <summary>Soft-delete a Employee</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"Employee with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"Employee deleted successfully."));
        }

        /// <summary>Get total count of active Employee records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
