using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
    public class AdImage
    {
        [Key]
        public int AdImageId { get; set; }

        [Required]
        public int AdId { get; set; }

        [Required]
        public string AdImagePath { get; set; }

        #region Relations
        [ForeignKey(nameof(AdId))]
        public virtual Ad? _Ad { get; set; }
        #endregion
    }
}
