using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Requests;
using LibraryManagementSystem.DTOs.Responses;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    /// <summary>ItemInfo management endpoints</summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ItemInfoController : ControllerBase
    {
        private readonly GenericRepository<ItemInfo> _repo;

        public ItemInfoController(AppDbContext context)
            => _repo = new GenericRepository<ItemInfo>(context);

        /// <summary>Get all active ItemInfo records</summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var items = await _repo.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<ItemInfo>>.Ok(items, totalCount: items.Count()));
        }

        /// <summary>Get ItemInfo by Id</summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _repo.GetByIdAsync(id);
            if (item is null)
                return NotFound(ApiResponse<ItemInfo>.Fail($"ItemInfo with Id {id} not found."));
            return Ok(ApiResponse<ItemInfo>.Ok(item));
        }

        /// <summary>Create a new ItemInfo</summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] ItemInfoRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemInfo>.Fail("Validation failed."));

            var entity = new ItemInfo
            {
                Title = r.Title,
                Description = r.Description,
                ISBN = r.ISBN,
                PublishDate = r.PublishDate,
                SubjectHeadingId = r.SubjectHeadingId,
                LanguageId = r.LanguageId,
                PublisherInfoId = r.PublisherInfoId,
                TotalPages = r.TotalPages,
                BindingType = r.BindingType,
                AddedToLibraryDate = r.AddedToLibraryDate,
                Keywords = r.Keywords,
            };

            var created = await _repo.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<ItemInfo>.Created(created));
        }

        /// <summary>Update an existing ItemInfo</summary>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] ItemInfoRequest r)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<ItemInfo>.Fail("Validation failed."));

            var updated = await _repo.UpdateAsync(id, entity =>
            {
                entity.Title = r.Title;
                entity.Description = r.Description;
                entity.ISBN = r.ISBN;
                entity.PublishDate = r.PublishDate;
                entity.SubjectHeadingId = r.SubjectHeadingId;
                entity.LanguageId = r.LanguageId;
                entity.PublisherInfoId = r.PublisherInfoId;
                entity.TotalPages = r.TotalPages;
                entity.BindingType = r.BindingType;
                entity.AddedToLibraryDate = r.AddedToLibraryDate;
                entity.Keywords = r.Keywords;
            });

            if (updated is null)
                return NotFound(ApiResponse<ItemInfo>.Fail($"ItemInfo with Id {id} not found."));

            return Ok(ApiResponse<ItemInfo>.Updated(updated));
        }

        /// <summary>Soft-delete a ItemInfo</summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repo.DeleteAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.Fail($"ItemInfo with Id {id} not found."));
            return Ok(ApiResponse<string>.Deleted($"Id {id}", $"ItemInfo deleted successfully."));
        }

        /// <summary>Get total count of active ItemInfo records</summary>
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Count()
        {
            var count = await _repo.CountAsync();
            return Ok(ApiResponse<int>.Ok(count));
        }
    }
}
