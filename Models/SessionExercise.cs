using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeFitApp.Models
{
    public class SessionExercise
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Sesja treningowa")]
        public int TrainingSessionId { get; set; }

        [ForeignKey("TrainingSessionId")]
        public TrainingSession? TrainingSession { get; set; }

        [Required]
        [Display(Name = "Typ ćwiczenia")]
        public int ExerciseTypeId { get; set; }

        [ForeignKey("ExerciseTypeId")]
        public ExerciseType? ExerciseType { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Range(1, 500)]
        [Display(Name = "Obciążenie (kg)")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Range(1, 100)]
        [Display(Name = "Liczba serii")]
        public int Sets { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Range(1, 100)]
        [Display(Name = "Liczba powtórzeń")]
        public int Repetitions { get; set; }
    }
}
