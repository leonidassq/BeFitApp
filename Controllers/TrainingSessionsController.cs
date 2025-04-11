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
    public class TrainingSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrainingSessions
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sessions = await _context.TrainingSessions
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return View(sessions);
        }


        // GET: TrainingSessions/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (trainingSession == null)
                return Forbid();

            return View(trainingSession);
        }



        // GET: TrainingSessions/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: TrainingSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("StartTime,EndTime")] TrainingSession trainingSession)
        {
            if (ModelState.IsValid)
            {
                trainingSession.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(trainingSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingSession);
        }


        // GET: TrainingSessions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (trainingSession == null)
                return Forbid();

            return View(trainingSession);
        }



        // POST: TrainingSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartTime,EndTime")] TrainingSession trainingSession)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sessionToUpdate = await _context.TrainingSessions
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (sessionToUpdate == null)
                return Forbid();

            if (ModelState.IsValid)
            {
                sessionToUpdate.StartTime = trainingSession.StartTime;
                sessionToUpdate.EndTime = trainingSession.EndTime;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(sessionToUpdate);
        }



        // GET: TrainingSessions/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (trainingSession == null)
                return Forbid();

            return View(trainingSession);
        }



        // POST: TrainingSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var trainingSession = await _context.TrainingSessions
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (trainingSession == null)
                return Forbid();

            _context.TrainingSessions.Remove(trainingSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool TrainingSessionExists(int id)
        {
            return _context.TrainingSessions.Any(e => e.Id == id);
        }
    }
}
