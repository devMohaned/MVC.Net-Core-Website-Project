using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }

        [Display(Name = "Brand")]
        [Required, StringLength(50, MinimumLength = 3)]
        public string BrandName { get; set; }
        [Required ,Display(Name ="Category")]
        public int CategoryId { get; set; }

        #region Relation
        [ForeignKey(nameof(CategoryId))]
        public  Category? _Category { get; set; }
        public  List<Ad>? _Ads { get; set; }
        #endregion
    }
}
