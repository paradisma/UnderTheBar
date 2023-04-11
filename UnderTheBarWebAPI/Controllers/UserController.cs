using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UnderTheBarWebAPI.DTO;
using UnderTheBarWebAPI.Entities;

namespace UnderTheBarWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserContext _dbContext;

        public UserController(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetUsers")]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var List = await _dbContext.Users.Select(
                s => new UserDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Username = s.Username,
                    //Password = s.Password,
                    EnrollmentDate = s.EnrollmentDate
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                
                return List;
            }
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserDTO>> GetUserById(string Id)
        {
            UserDTO? User = await _dbContext.Users.Select(s => new UserDTO
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Username = s.Username,
                Password = s.Password,
                EnrollmentDate = s.EnrollmentDate
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (User == null)
            {
                return NotFound();
            }

            return User;
        }

        [HttpPost("InsertUser")]
        public async Task<HttpStatusCode> InsertUser(UserDTO User)
        {
            var entity = new User()
            {
                Id = User.Id,
                FirstName = User.FirstName,
                LastName = User.LastName,
                Username = User.Username,
                Password = User.Password,
                EnrollmentDate = User.EnrollmentDate
            };
            _dbContext.Users.Add(entity);
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(UserDTO User)
        {
            var entity = await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == User.Id);
            entity.FirstName = User.FirstName;
            entity.LastName = User.LastName;
            entity.Username = User.Username;
            entity.Password = User.Password;
            entity.EnrollmentDate = User.EnrollmentDate;
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(string Id)
        {
            var entity = new User()
            {
                Id = Id
            };
            _dbContext.Users.Attach(entity);
            _dbContext.Users.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpGet("VerifyUserCredentials")]
        public async Task<ActionResult<UserDTO>> VerifyUserCredentials(string username, string password)
        {
            UserDTO? User = await _dbContext.Users.Select(s => new UserDTO
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Username = s.Username,
                Password = s.Password,
                EnrollmentDate = s.EnrollmentDate
            }).FirstOrDefaultAsync(s => s.Username == username);

            if(User == null)
            {
                return BadRequest("Username not found!");
            }

            if(User.Password != password)
            {
                return BadRequest("Password is incorrect!");
            }

            return User;
        }
    }
}
