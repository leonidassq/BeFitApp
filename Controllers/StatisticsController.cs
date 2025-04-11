using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BeFitApp.Data;
using BeFitApp.Models;

namespace BeFitApp.Controllers
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var fromDate = DateTime.Now.AddDays(-28);

            var stats = await _context.SessionExercises
                .Include(se => se.ExerciseType)
                .Include(se => se.TrainingSession)
                .Where(se => se.TrainingSession.UserId == userId && se.TrainingSession.StartTime >= fromDate)
                .GroupBy(se => se.ExerciseType.Name)
                .Select(g => new ExerciseStatsViewModel
                {
                    ExerciseName = g.Key,
                    Count = g.Count(),
                    TotalReps = g.Sum(e => e.Sets * e.Repetitions),
                    AverageWeight = g.Average(e => e.Weight),
                    MaxWeight = g.Max(e => e.Weight)
                })
                .ToListAsync();

            return View(stats);
        }
    }

    public class ExerciseStatsViewModel
    {
        public string ExerciseName { get; set; }
        public int Count { get; set; }
        public int TotalReps { get; set; }
        public decimal AverageWeight { get; set; }
        public decimal MaxWeight { get; set; }
    }
}
