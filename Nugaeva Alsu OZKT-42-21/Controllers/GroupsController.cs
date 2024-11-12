using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Nugaeva_Alsu_OZKT_42_21.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Nugaeva_Alsu_OZKT_42_21.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupService _studentService;
        private NugaevaDbContext _context;
        public GroupsController(ILogger<GroupsController> logger, IGroupService studentService, NugaevaDbContext context)
        {
            _logger = logger;
            _studentService = studentService;
            _context = context;
        }
        //[HttpPost("GetGroupsByIsDeleted")]
        //public async Task<IActionResult> GetGroupsByIsDeletedAsync(GroupIsDeletedFilter filter, CancellationToken cancellationToken = default)
        //{
        //    var students = await _studentService.GetGroupsByIsDeletedAsync(filter, cancellationToken);
        //    return Ok(students);
        //}
        [HttpPost("AddGroup")]
        public async Task<IActionResult> CreateGroup([FromBody] Group group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Groups ON");

                    // Добавление объекта группы
                    _context.Groups.Add(group);

                    // Сохранение изменений в базе данных
                    await _context.SaveChangesAsync();

                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Groups OFF");

                    transaction.Commit();
                }
                return Ok(group);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error occurred while saving entity changes: {ex.Message}");
                throw;
            }
        }

        [HttpPut("EditGroup")]
        public IActionResult UpdateGroup(string name, [FromBody] Group updatedGroup)
        {
            var existingGroup = _context.Groups.FirstOrDefault(g => g.GroupName == name);

            if (existingGroup == null)
            {
                return NotFound();
            }

            try
            {
                existingGroup.GroupName = updatedGroup.GroupName;
                _context.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error occurred while saving entity changes: {ex.Message}");
                throw;
            }
        }

        [HttpDelete("DeleteGroup")]
        public IActionResult DeleteGroup(string name, [FromBody] Group deletedGroup)
        {
            var existingGroup = _context.Groups.FirstOrDefault(g => g.GroupName == name);

            if (existingGroup == null)
            {
                return NotFound("Группа не найдена.");
            }

            try
            {
                _context.Groups.Remove(existingGroup);
                _context.SaveChanges();
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError($"Error occurred while saving entity changes: {ex.Message}");
                throw;
            }
        }
    }
}
