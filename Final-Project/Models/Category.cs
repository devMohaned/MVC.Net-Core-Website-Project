using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Final_Project.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required, StringLength(50, MinimumLength = 3)]
        public string CategoryName { get; set; }

        #region Relations
        //public virtual List<SubCategory>? _SubCategories { get; set; }
        public  List<Brand>? _Brands { get; set; }
        public  List<Ad>? _Ads { get; set; }
        #endregion

    }
}
