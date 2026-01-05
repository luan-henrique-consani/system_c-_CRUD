using backend.Data;
using backend.DTOs;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;


namespace backend.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(CreateUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);

            User user = new User
            {
                Name = userDTO.Name,
                Email = userDTO.Email,
                PasswordHash = passwordHash,
                CreationDate = DateTime.Now,
                Status = "Ativo"
            };


            _context.Users.Add(user);
            _context.SaveChanges();

            var response = new UserResponseDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                CreationDate = user.CreationDate,
                Status = user.Status
            };

            return CreatedAtAction(
                nameof(GetId),
                new { id = user.Id},
                response
            );

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userList =
            _context.Users.Select(u => new UserResponseDTO
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                CreationDate = u.CreationDate,
                Status = u.Status
            }).ToList();

            return Ok(userList);
        }

        [HttpGet("{id}")]
        public IActionResult GetId(int id)
        {
            var userExist = _context.Users.FirstOrDefault(u => u.Id == id);

            if(userExist == null)
            {
                return NotFound();
            }
            var userList = new UserResponseDTO
            {
                Id = userExist.Id,
                Name = userExist.Name,
                Email = userExist.Email,
                CreationDate = userExist.CreationDate,
                Status = userExist.Status
            };

            return Ok(userList);
            
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteId(int id)
        {
            var userExist = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userExist == null)
            {
                return NotFound();
            }
            _context.Users.Remove(userExist);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateId(int id, [FromBody] UpdateUserDTO UpDTO)
        {
            var userExist = _context.Users.FirstOrDefault(u => u.Id == id);
            if (userExist == null)
            {
                return NotFound();
            }

            userExist.Name = UpDTO.Name;
            userExist.Email = UpDTO.Email;
            userExist.Status = UpDTO.Status;

            _context.SaveChanges();

            return NoContent();
            
        }

    }
}