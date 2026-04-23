using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>EmployeeRelationHistory management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmployeeRelationHistoryController : ControllerBase
    {
        private readonly GenericRepository<EmployeeRelationHistory> _repo;

        public EmployeeRelationHistoryController(AppDbContext context)
            => _repo = new GenericRepository<EmployeeRelationHistory>(context);

        /// <summary>Get all active EmployeeRelationHistory records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<EmployeeRelationHistory>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get EmployeeRelationHistory by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<EmployeeRelationHistory>.Fail($"EmployeeRelationHistory with Id {id} not found."));
            return Ok(ApiResponse<EmployeeRelationHistory>.Ok(item));
        }

        /// <summary>Create a new EmployeeRelationHistory</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] EmployeeRelationHistoryRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<EmployeeRelationHistory>.Fail("Validation failed."));

            var entity = new EmployeeRelationHistory
            {
                EmployeeId = r.EmployeeId,
                EffectiveTo = r.EffectiveTo,
                IsCurrent = r.IsCurrent,
                EmployeeType = r.EmployeeType,
                EmployeeTypeLookupId = r.EmployeeTypeLookupId,
                EmployeeCategoryLookupId = r.EmployeeCategoryLookupId,
                EmployeeStatusId = r.EmployeeStatusId,
                EmployeeGrade = r.EmployeeGrade,
                DepartmentId = r.DepartmentId,
                DesignationId = r.DesignationId,
                CampusId = r.CampusId,
                InstituteId = r.InstituteId,
                FacultyId = r.FacultyId,
                ProjectId = r.ProjectId,
                SalaryBankAccountId = r.SalaryBankAccountId,
                ExpenseAccountId = r.ExpenseAccountId,
                SalaryPayable = r.SalaryPayable,
                PayStructureVersionId = r.PayStructureVersionId,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<EmployeeRelationHistory>.Created(created));
        }

        /// <summary>Update an existing EmployeeRelationHistory</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeRelationHistoryRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<EmployeeRelationHistory>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.EmployeeId = r.EmployeeId;
                entity.EffectiveTo = r.EffectiveTo;
                entity.IsCurrent = r.IsCurrent;
                entity.EmployeeType = r.EmployeeType;
                entity.EmployeeTypeLookupId = r.EmployeeTypeLookupId;
                entity.EmployeeCategoryLookupId = r.EmployeeCategoryLookupId;
                entity.EmployeeStatusId = r.EmployeeStatusId;
                entity.EmployeeGrade = r.EmployeeGrade;
                entity.DepartmentId = r.DepartmentId;
                entity.DesignationId = r.DesignationId;
                entity.CampusId = r.CampusId;
                entity.InstituteId = r.InstituteId;
                entity.FacultyId = r.FacultyId;
                entity.ProjectId = r.ProjectId;
                entity.SalaryBankAccountId = r.SalaryBankAccountId;
                entity.ExpenseAccountId = r.ExpenseAccountId;
                entity.SalaryPayable = r.SalaryPayable;
                entity.PayStructureVersionId = r.PayStructureVersionId;
            });

            if (updated is null)
                return NotFound(ApiResponse<EmployeeRelationHistory>.Fail($"EmployeeRelationHistory with Id {id} not found."));

            return Ok(ApiResponse<EmployeeRelationHistory>.Updated(updated));
        }

        /// <summary>Soft-delete a EmployeeRelationHistory</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"EmployeeRelationHistory with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"EmployeeRelationHistory deleted successfully."));
        }

        /// <summary>Get total count of active EmployeeRelationHistory records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
