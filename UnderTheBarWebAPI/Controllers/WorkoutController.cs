using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UnderTheBarWebAPI.DTO;
using UnderTheBarWebAPI.Entities;

namespace UnderTheBarWebAPI.Controllers
{
    public class WorkoutController : ControllerBase
    {
        private readonly WorkoutsContext _workoutContext;

        public WorkoutController(WorkoutsContext workoutContext)
        {
            _workoutContext = workoutContext;
        }

        [HttpGet("GetWorkouts")]
        public async Task<ActionResult<List<WorkoutDTO>>> GetAllWorkouts()
        {
            var list = await _workoutContext.Workouts.Select(
                s => new WorkoutDTO
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    Name = s.Name,
                    Notes = s.Notes,
                    Time = s.Time
                }
            ).ToListAsync();

            if (list.Count < 0)
            {
                return NotFound();
            }

            return list;
        }

        [HttpPost("CreateWorkout")]
        public async Task<HttpStatusCode> CreateWorkout(WorkoutDTO workoutDTO)
        {
            var entity = new Workout()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = Guid.NewGuid().ToString(),
                Name = workoutDTO.Name,
                Notes = workoutDTO.Notes,
                Time = workoutDTO.Time,
            };

            _workoutContext.Workouts.Add(entity);
            await _workoutContext.SaveChangesAsync();
            return HttpStatusCode.Created;
        }
    }
}
