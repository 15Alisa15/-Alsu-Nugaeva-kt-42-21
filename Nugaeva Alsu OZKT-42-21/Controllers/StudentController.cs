using Nugaeva_Alsu_OZKT_42_21.Filters.StudentFilters;
using Nugaeva_Alsu_OZKT_42_21.Interfaces.StudentsInterfaces;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace Nugaeva_Alsu_OZKT_42_21.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;
        private NugaevaDbContext _context;
        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService, NugaevaDbContext context)
        {
            _logger = logger;
            _studentService = studentService;
            _context = context;
        }



        [HttpPost("GetStudentsByGroup")]
        public async Task<IActionResult> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpPost("GetStudentsByGroupId")]
        public async Task<IActionResult> GetStudentsByGroupIdAsync(StudentGroupIdFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupIdAsync(filter, cancellationToken);



            return Ok(students);
        }

        [HttpPost("GetStudentsByFIO")]
            public async Task<IActionResult> GetStudentsByFIOAsync(StudentFIOFilter filter, CancellationToken cancellationToken = default)
            {
                var students = await _studentService.GetStudentsByFIOAsync(filter, cancellationToken);
                return Ok(students);
            }

            [HttpPost("GetStudentsByIsDeleted")]
            public async Task<IActionResult> GetStudentsByIsDeletedAsync(StudentIsDeletedFilter filter, CancellationToken cancellationToken = default)
            {
                var students = await _studentService.GetStudentsByIsDeletedAsync(filter, cancellationToken);

                return Ok(students);
            }


        [HttpPost("AddStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.Database.BeginTransactionAsync();

                try
                {
                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    await _context.Database.RollbackTransactionAsync();
                    _logger.LogError($"Error occurred while saving entity changes: {ex.Message}");
                    throw;
                }
            }
            catch (DbUpdateException ex)
            {
                await _context.Database.RollbackTransactionAsync();
                _logger.LogError($"Database update exception: {ex.Message}");
                throw;
            }

            return Ok(student);
        }
        [HttpPut("EditStudent")]
            public IActionResult UpdateStudent(string LastName, [FromBody] Student updatedStudent)
            {
                var existingStudent = _context.Students.FirstOrDefault(g => g.LastName == LastName);
                if (existingStudent == null)
                {
                    return NotFound();
                }
                existingStudent.LastName = updatedStudent.LastName;
                existingStudent.FirstName = updatedStudent.FirstName;
                existingStudent.MiddleName = updatedStudent.MiddleName;
                existingStudent.GroupId = updatedStudent.GroupId;
                _context.SaveChanges();
                return Ok();
            }

            [HttpDelete("DeleteStudent")]
            public IActionResult DeleteStudent(string LastName, [FromBody] Student deletedStudent)
            {
                var existingStudent = _context.Students.FirstOrDefault(g => g.LastName == LastName);
                if (existingStudent == null)
                {
                    return NotFound();
                }

                // existingStudent.IsDeleted = true;
                _context.Students.Remove(existingStudent);
                _context.SaveChanges();
                return Ok();
            }

        }
    }


    

   



