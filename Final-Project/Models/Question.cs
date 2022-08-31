using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [DataType(DataType.PhoneNumber)]
        [StringLength(11, MinimumLength = 11)]
        public string? Phone { get; set; }



        [Required]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3)]
        public string Subject { get; set; }


        [Required]
        [DataType(DataType.Text)]
        [StringLength(600, MinimumLength = 4)]
        public string Message { get; set; }


    }
}
