using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;
using WebAPINormal.Extension;
using WebAPINormal.Models;

namespace WebAPINormal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext context)
        {
            _context = context;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentM>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentM = student.ToModel();
            studentM.Balance = studentM.Balance; 
            return studentM;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentM>>> GetStudents()
        {
            var ListOfStudents = await _context.Students.ToListAsync();
            List<StudentM> ListOfStudentsM = new List<StudentM>();
            foreach (Student student in ListOfStudents)
            {
                StudentM studentM = new StudentM();
                studentM = student.ToModel();
                
                ListOfStudentsM.Add(studentM);
            }
            return ListOfStudentsM;
        }

        // PUT: api/Students/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.StudentID)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Students
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(StudentM studentM)
        {
            Student student = studentM.ToDAL();

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var studentM2 = student.ToModel();
            return CreatedAtAction(nameof(GetStudent), new { id = student.StudentID }, studentM2);
        }

        // POST: api/Students/5/recharge
        [HttpPost("{id}/recharge")]
        public async Task<IActionResult> RechargeAccount(int id, decimal amount)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentM = student.ToModel();
            studentM.Balance += amount;
            student.Balance = studentM.Balance; 
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(studentM);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentID == id);
        }
        [HttpGet("balance/{accountId}")]
        public async Task<ActionResult<decimal>> GetBalance(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }

            return account.StudentBalance;
        }

    }
}