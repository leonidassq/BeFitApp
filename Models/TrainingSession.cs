using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BeFitApp.Models
{
    public class TrainingSession
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Display(Name = "Czas rozpoczęcia")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Display(Name = "Czas zakończenia")]
        public DateTime EndTime { get; set; }

        // NIE dodawaj [Required] – ważne!
        public string? UserId { get; set; }

        [Display(Name = "Użytkownik")]
        public IdentityUser? User { get; set; }

        public ICollection<SessionExercise> Exercises { get; set; } = new List<SessionExercise>();
    }


}
