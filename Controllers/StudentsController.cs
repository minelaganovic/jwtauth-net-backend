using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using serverstudent.Data;
using serverstudent.Model;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace serverstudent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public StudentsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Registracija korisnika
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Student student)
        {
            if (_context.students.Any(s => s.username == student.username))
                return BadRequest("Username already exists");

            _context.students.Add(student);
            await _context.SaveChangesAsync();

            return Ok("Registration successful");
        }

        // Logovanje korisnika
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var student = await _context.students
                .FirstOrDefaultAsync(s => s.username == loginRequest.username && s.password == loginRequest.password);

            if (student == null)
                return Unauthorized("Invalid username or password");

            // Generisanje JWT tokena
            var token = GenerateJwtToken(student);
            return Ok(new { Token = token });
        }


        // Metoda za generisanje JWT tokena
        private string GenerateJwtToken(Student student)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, student.username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", student.Id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Zaštićeni endpoint primer
        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst("id")?.Value;
            var student = await _context.students.FindAsync(int.Parse(userId));

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // GET: api/Students
        [HttpGet(Name = "GetAllStudents")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.students.ToListAsync();
        }


        // POST: api/Students
        [HttpPost(Name = "CreateStudent")]
        public async Task<ActionResult<Student>> PostStudent([FromBody] Student student)
        {
            _context.students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetStudentById", new { id = student.Id }, student);
        }
        // GET: api/Students/5
        [HttpGet("{id}", Name = "GetStudentById")]
        [Authorize]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.students.FindAsync(id);
            if (student == null)
                return NotFound();

            return student;
        }


        //PUT metoda se koristi za potpuno ažuriranje resursa
        // PUT: api/Students/5
        [HttpPut("{id}", Name = "UpdateStudent")]
        public async Task<IActionResult> PutStudent(int id, [FromBody] Student student)
        {
            if (id != student.Id)
                return BadRequest();

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.students.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        //PATCH metoda se koristi za delimično ažuriranje resursa.
        ///Za razliku od PUT metode,PATCH može ažurirati samo jedan deo resursa
        // PATCH: api/Students/5
        [HttpPatch("{id}/name", Name = "UpdateStudentName")]
        public async Task<IActionResult> PatchStudentName(int id, [FromBody] string newName)
        {
            var student = await _context.students.FindAsync(id);
            if (student == null)
                return NotFound("Student not found");

            if (string.IsNullOrWhiteSpace(newName))
                return BadRequest("Name cannot be empty");

            student.firstname = newName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.students.Any(e => e.Id == id))
                    return NotFound("Student not found");
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}", Name = "DeleteStudent")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.students.FindAsync(id);
            if (student == null)
                return NotFound();

            _context.students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
