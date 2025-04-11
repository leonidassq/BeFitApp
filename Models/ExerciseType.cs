using System.ComponentModel.DataAnnotations;

namespace BeFitApp.Models
{
    public class ExerciseType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [StringLength(100)]
        [Display(Name = "Nazwa ćwiczenia")]
        public string Name { get; set; }
    }

}
