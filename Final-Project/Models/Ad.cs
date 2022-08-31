using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{

    public class Ad
    {
        [Required]
        [Key]
        public int AdId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [Display(Name ="Title Of Ad")]
        public string AdTitle { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Brief")]
        public string AdBrief { get; set; }
        [StringLength(600, MinimumLength = 3)]
        [Display(Name = "Discription")]
        [Required]
        public string? AdDescription { get; set; }
        [Required]
        [Range(minimum:1,maximum:100000000)]
        [Display(Name = "Price")]
        public float AdPrice { get; set; }

        public string? AdMainImage { get; set; }

        //[Required]
        //public int SubCategoryId { get; set; }
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        [Required]
        public string SellerId { get; set; }
        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        [Required]
        public bool ProductReviewed { get; set; }

        public int? PhoneRequested { get; set; }
        public int? AddedToFav { get; set; }
        public int? ViewCount { get; set; }

        #region Relations

        //[ForeignKey(nameof(SubCategoryId))]
        //public virtual SubCategory? _SubCategory { get; set; }
        [ForeignKey(nameof(SellerId))]
        public  User? _Seller { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public   Category? _Category { get; set; }
        [ForeignKey(nameof(BrandId))]
        public   Brand? _Brand { get; set; }
        public  List<AdImage>? _ProductImages { get; set; }
        public  List<Favorite>? _Favorite { get; set; }
        #endregion
    }
}
