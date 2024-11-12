using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nugaeva_Alsu_OZKT_42_21.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using Nugaeva_Alsu_OZKT_42_21.Filters.SubjectsFilters;
using Nugaeva_Alsu_OZKT_42_21.Database.Models;

namespace Nugaeva_Alsu_OZKT_42_21.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ILogger<SubjectsController> _logger;
        private readonly ISubjectService _studentService;
        private NugaevaDbContext _context;
        public SubjectsController(ILogger<SubjectsController> logger, ISubjectService studentService, NugaevaDbContext context)
        {
            _logger = logger;
            _studentService = studentService;
            _context = context;
        }

        [HttpPost("GetSubjectsByName")]
        public async Task<IActionResult> GetSubjectsByNameAsync(SubjectNameFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetSubjectsByNameAsync(filter, cancellationToken);
            return Ok(students);
        }
        //[HttpPost("GetSubjectsByIsDeleted")]
        //public async Task<IActionResult> GetSubjectsByIsDeletedAsync(SubjectIsDeletedFilter filter, CancellationToken cancellationToken = default)
        //{
        //    var students = await _studentService.GetSubjectsByIsDeletedAsync(filter, cancellationToken);
        //    return Ok(students);
        //}

        [HttpPost("AddSubject")]
        public IActionResult CreateSubject([FromBody] Subject student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Subjects.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }

        // [HttpPut("EditSubject")]
        // public IActionResult UpdateSubject(string name, [FromBody] Subject updatedSubject)
        //{
        //   var existingSubject = _context.Subject.FirstOrDefault(g => g.SubjectName == name);
        //  if (existingSubject == null)
        // {
        ///   return NotFound();
        //  }
        // existingSubject.SubjectName = updatedSubject.SubjectName;
        //existingSubject.SubjectDescription = updatedSubject.SubjectName;
        //_context.SaveChanges();
        //return Ok();

       // [HttpDelete("DeleteSubject")]
       // public IActionResult DeleteSubject(string name, [FromBody] Subject deletedSubject)
       // {
         //   var existingSubject = _context.Subject.FirstOrDefault(g => g.SubjectName == name);
          //  if (existingSubject == null)
          //  {
           //     return NotFound();
          //  }
            //existingSubject.IsDeleted = true;
          //  _context.Subject.Remove(existingSubject);
           // _context.SaveChanges();
           // return Ok();
        }
    }

        
    
