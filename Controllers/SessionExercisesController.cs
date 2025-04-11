using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BeFitApp.Data;
using BeFitApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BeFitApp.Controllers
{
    [Authorize]
    public class SessionExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SessionExercises
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessionExercises = await _context.SessionExercises
                .Include(e => e.TrainingSession)
                .Include(e => e.ExerciseType)
                .Where(e => e.TrainingSession.UserId == userId)
                .ToListAsync();

            return View(sessionExercises);
        }



        // GET: SessionExercises/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessionExercise = await _context.SessionExercises
                .Include(e => e.TrainingSession)
                .Include(e => e.ExerciseType)
                .FirstOrDefaultAsync(e => e.Id == id && e.TrainingSession.UserId == userId);

            if (sessionExercise == null)
                return Forbid();

            return View(sessionExercise);
        }



        // GET: SessionExercises/Create
        [Authorize]
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions
                .Where(s => s.UserId == userId), "Id", "StartTime");

            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name");

            return View();
        }


        // POST: SessionExercises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("TrainingSessionId,ExerciseTypeId,Weight,Sets,Repetitions")] SessionExercise sessionExercise)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var session = await _context.TrainingSessions
                .FirstOrDefaultAsync(s => s.Id == sessionExercise.TrainingSessionId && s.UserId == userId);

            if (session == null)
                return Forbid();

            if (ModelState.IsValid)
            {
                _context.Add(sessionExercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["TrainingSessionId"] = new SelectList(_context.TrainingSessions
                .Where(s => s.UserId == userId), "Id", "StartTime", sessionExercise.TrainingSessionId);

            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", sessionExercise.ExerciseTypeId);

            return View(sessionExercise);
        }


        // GET: SessionExercises/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessionExercise = await _context.SessionExercises
                .Include(se => se.ExerciseType)
                .Include(se => se.TrainingSession)
                .FirstOrDefaultAsync(se => se.Id == id && se.TrainingSession.UserId == userId);

            if (sessionExercise == null)
            {
                return NotFound();
            }

            // Pobierz tylko sesje należące do aktualnego użytkownika
            var sessions = await _context.TrainingSessions
                .Where(s => s.UserId == userId)
                .ToListAsync();

            var selectedSessionId = sessions.Count == 1
                ? sessions.First().Id
                : sessionExercise.TrainingSessionId;

            ViewData["TrainingSessionId"] = new SelectList(
                sessions,
                "Id",
                "StartTime", // albo inna właściwość, np. tytuł
                selectedSessionId
            );

            ViewData["ExerciseTypeId"] = new SelectList(
                _context.ExerciseTypes,
                "Id",
                "Name",
                sessionExercise.ExerciseTypeId
            );

            return View(sessionExercise);
        }






        // POST: SessionExercises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Weight,Sets,Repetitions,ExerciseTypeId")] SessionExercise edited)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessionExercise = await _context.SessionExercises
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(e => e.Id == id && e.TrainingSession.UserId == userId);

            if (sessionExercise == null)
                return Forbid();

            if (ModelState.IsValid)
            {
                sessionExercise.Weight = edited.Weight;
                sessionExercise.Sets = edited.Sets;
                sessionExercise.Repetitions = edited.Repetitions;
                sessionExercise.ExerciseTypeId = edited.ExerciseTypeId;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ExerciseTypeId"] = new SelectList(_context.ExerciseTypes, "Id", "Name", edited.ExerciseTypeId);
            return View(edited);
        }


        // GET: SessionExercises/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessionExercise = await _context.SessionExercises
                .Include(e => e.TrainingSession)
                .Include(e => e.ExerciseType)
                .FirstOrDefaultAsync(e => e.Id == id && e.TrainingSession.UserId == userId);

            if (sessionExercise == null)
                return Forbid();

            return View(sessionExercise);
        }



        // POST: SessionExercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessionExercise = await _context.SessionExercises
                .Include(e => e.TrainingSession)
                .FirstOrDefaultAsync(e => e.Id == id && e.TrainingSession.UserId == userId);

            if (sessionExercise == null)
                return Forbid();

            _context.SessionExercises.Remove(sessionExercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool SessionExerciseExists(int id)
        {
            return _context.SessionExercises.Any(e => e.Id == id);
        }
    }
}
